#load "lib/Config.csx"
#load "lib/Grayscale.csx"
#load "lib/Colorize.csx"
#load "lib/MarkdownGeneration.csx"
#load "lib/Resize.csx"
#load "lib/VSCodeSnippets.csx"

using System.Diagnostics;

var sourceFolder = @"../source";

var originalSourceFolder = Path.Combine(sourceFolder, "official");

var manualSourceFolder = Path.Combine(sourceFolder, "manual");

var targetFolder = @"../dist";

var targetMaxSize = 65;

var plantUmlPath = @"C:\ProgramData\chocolatey\lib\plantuml\tools\plantuml.jar";

var inkScapePath = @"C:\Program Files\Inkscape\inkscape.exe";

Main();

public void Main()
{
    var lookupTable = ReadConfig("Config.yaml");

    // Cleanup
    //System.IO.DirectoryInfo di = new DirectoryInfo(targetFolder);
    // foreach (DirectoryInfo dir in di.GetDirectories())
    // {
    //     dir.Delete(true); 
    // }
    //di.Delete(true);
    if (Directory.Exists(targetFolder))
    {
        Directory.Delete(targetFolder, true);
    }
    Directory.CreateDirectory(targetFolder);

    File.Copy(Path.Combine(sourceFolder, "AzureCommon.puml"), Path.Combine(targetFolder, "AzureCommon.puml"));

    foreach (var service in lookupTable)
    {
        var coloredSourceFileName = $"{service.ServiceSource}_COLOR.svg";
        var monochromSourceFileName = $"{service.ServiceSource}.svg";

        var coloredSourceFilePath = GetSourceFilePath(coloredSourceFileName);
        var monochromSourceFilePath = GetSourceFilePath(monochromSourceFileName);

        var coloredExists = coloredSourceFilePath != null;
        var monochromExists = monochromSourceFilePath != null;

        if (!coloredExists && !monochromExists)
        {
            WriteErrorLine($"Error: Missing {coloredSourceFileName} and {monochromSourceFileName}");
            continue;
        }

        var categoryDirectoryPath = Path.Combine(targetFolder, service.Category);
        Directory.CreateDirectory(categoryDirectoryPath);

        Console.WriteLine($"Processing {service.ServiceSource}");

        var coloredPngFilePath = Path.Combine(categoryDirectoryPath, service.ServiceTarget + ".png");
        if (coloredExists)
        {
            ConvertToPng(coloredSourceFilePath, coloredPngFilePath, withBackgroundForPuml: false);
            Resize(coloredPngFilePath, coloredPngFilePath, targetMaxSize, targetMaxSize);
        }
        else
        {
            WriteWarningLine($"Warning: Missing COLOR {coloredSourceFileName}");
        }

        var monochromPngFilePath = Path.Combine(categoryDirectoryPath, service.ServiceTarget + "(m).png");

        if (!monochromExists)
        {
            monochromSourceFilePath = coloredSourceFilePath;
        }

        // First generation with background needed for PUML sprite generation
        ConvertToPng(monochromSourceFilePath, monochromPngFilePath, withBackgroundForPuml: true);
        if (!monochromExists)
        {
            WriteWarningLine($"Warning: Missing MONOCHROM {monochromSourceFileName}, generating...");
            CreateMonochrom(monochromPngFilePath, monochromPngFilePath, "#0072C6");
        }
        Resize(monochromPngFilePath, monochromPngFilePath, targetMaxSize, targetMaxSize);
        ConvertToPuml(monochromPngFilePath, service.ServiceTarget + ".puml");

        // Second generation without background needed other usages
        ConvertToPng(monochromSourceFilePath, monochromPngFilePath, withBackgroundForPuml: false);
        if (!monochromExists)
        {
            CreateMonochrom(monochromPngFilePath, monochromPngFilePath, "#0072C6");
        }
        Resize(monochromPngFilePath, monochromPngFilePath, targetMaxSize, targetMaxSize);
    }

    foreach (var category in lookupTable.Select(_ => _.Category).Distinct())
    {
        var categoryDirectoryPath = Path.Combine(targetFolder, category);
        var catAllFilePath = Path.Combine(categoryDirectoryPath, "all.puml");

        CombineMultipleFilesIntoSingleFile(categoryDirectoryPath, "*.puml", catAllFilePath);
    }

    GenerateMarkdownTable(targetFolder);
    GenerateVSCodeSnippets(targetFolder);

    Console.WriteLine("Finished");
}

private static void CombineMultipleFilesIntoSingleFile(string inputDirectoryPath, string inputFileNamePattern, string outputFilePath)
{
    string[] inputFilePaths = Directory.GetFiles(inputDirectoryPath, inputFileNamePattern, SearchOption.AllDirectories);
    using (var outputStream = File.Create(outputFilePath))
    {
        foreach (var inputFilePath in inputFilePaths)
        {
            using (var inputStream = File.OpenRead(inputFilePath))
            {
                inputStream.CopyTo(outputStream);

                byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);
                outputStream.Write(newline, 0, newline.Length);
            }
        }
    }
}

public string GetSourceFilePath(string sourceFileName)
{
    var sourceFilePath = Path.Combine(originalSourceFolder, sourceFileName);
    if (!File.Exists(sourceFilePath))
    {
        sourceFilePath = Path.Combine(manualSourceFolder, sourceFileName);
        if (!File.Exists(sourceFilePath))
        {
            return null;
        }
    }

    return sourceFilePath;
}

public void CreateMonochrom(string sourceFilePath, string targetFilePath, string color)
{
    // Making it first grayscale and after that monochrom gives best result
    var grayPath = MakeGrayscale(sourceFilePath);
    Colorize(grayPath, targetFilePath, System.Drawing.ColorTranslator.FromHtml(color));
    File.Delete(grayPath);
}

public void WriteWarningLine(string message)
{
    var tmp = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(message);
    Console.ForegroundColor = tmp;
}

public void WriteErrorLine(string message)
{
    var tmp = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(message);
    Console.ForegroundColor = tmp;
}

public bool ConvertToPng(string svgPath, string pngPath, bool withBackgroundForPuml)
{
    var backgroundOpacity = withBackgroundForPuml ? "1.0" : "0.0";

    var processInfo = new ProcessStartInfo
    {
        FileName = inkScapePath,
        Arguments = $"-z --file=\"{svgPath}\" --export-height={targetMaxSize} --export-png=\"{pngPath}\" --export-background=#FFFFFF --export-background-opacity={backgroundOpacity} --export-area-drawing",
        RedirectStandardOutput = true,
        UseShellExecute = false,
        CreateNoWindow = true
    };

    using (var process = Process.Start(processInfo))
    {
        if (!process.WaitForExit(10000))
        {
            Console.WriteLine($"Killing InkScape for ConvertToPng {svgPath}");
            process.Kill();
            return false;
        }

        if (!File.Exists(pngPath))
        {
            throw new Exception($"File could not be generated for ConvertToPng {svgPath}");
        }
    }

    return true;
}

public string ConvertToPuml(string pngPath, string pumlFileName)
{
    var format = "16z";

    var entityName = Path.GetFileNameWithoutExtension(pumlFileName);
    var pumlPath = Path.Combine(Directory.GetParent(pngPath).FullName, pumlFileName);

    var processInfo = new ProcessStartInfo
    {
        FileName = "java",
        Arguments = $"-jar {plantUmlPath} -encodesprite {format} \"{pngPath}\"",
        RedirectStandardOutput = true,
        UseShellExecute = false,
        CreateNoWindow = true
    };

    var pumlContent = new StringBuilder();
    using (var process = Process.Start(processInfo))
    {
        process.WaitForExit();
        pumlContent.Append(process.StandardOutput.ReadToEnd());
    }

    pumlContent.AppendLine($"AzureEntityColoring({entityName})");
    pumlContent.AppendLine($"!define {entityName}(e_alias, e_label, e_techn) AzureEntity(e_alias, e_label, e_techn, AZURE_SYMBOL_COLOR, {entityName}, {entityName})");
    pumlContent.AppendLine($"!define {entityName}(e_alias, e_label, e_techn, e_descr) AzureEntity(e_alias, e_label, e_techn, e_descr, AZURE_SYMBOL_COLOR, {entityName}, {entityName})");

    File.WriteAllText(pumlPath, pumlContent.ToString());
    return pumlPath;
}