#! "netcoreapp3.1"

#r "nuget: System.Drawing.Common, 4.7.0"

#load "lib/Config.csx"
#load "lib/HSLColor.csx"
#load "lib/MarkdownGeneration.csx"
#load "lib/VSCodeSnippets.csx"

using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;

var sourceFolder = @"../source";

var originalSourceFolder = Path.Combine(sourceFolder, "official");

var manualSourceFolder = Path.Combine(sourceFolder, "manual");

var targetFolder = @"../dist";

var targetImageHeight = 70;

var azureColor = System.Drawing.ColorTranslator.FromHtml("#0072C6");

var plantUmlPath = @"C:\ProgramData\chocolatey\lib\plantuml\tools\plantuml.jar";

var inkScapePath = @"C:\Program Files\Inkscape\bin\inkscape.exe";

static string rsvgConvertPath = @"C:\ProgramData\chocolatey\lib\rsvg-convert\tools\rsvg-convert.exe";


Main();

public void Main()
{
    var lookupTable = ReadConfig("Config.yaml");

    // Cleanup
    if (Directory.Exists(targetFolder))
    {
        Directory.Delete(targetFolder, true);
    }
    Directory.CreateDirectory(targetFolder);

    File.Copy(Path.Combine(sourceFolder, "AzureRaw.puml"), Path.Combine(targetFolder, "AzureRaw.puml"));
    File.Copy(Path.Combine(sourceFolder, "AzureCommon.puml"), Path.Combine(targetFolder, "AzureCommon.puml"));
    File.Copy(Path.Combine(sourceFolder, "AzureC4Integration.puml"), Path.Combine(targetFolder, "AzureC4Integration.puml"));
    File.Copy(Path.Combine(sourceFolder, "AzureSimplified.puml"), Path.Combine(targetFolder, "AzureSimplified.puml"));

    foreach (var service in lookupTable)
    {       
        ProcessService(service); 
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

public void ProcessService(ConfigLookupEntry service) 
{
    Console.WriteLine($"Processing {service.ServiceSource}");

    //var coloredSourceFileName = $"{service.ServiceSource}_COLOR.svg";
    //#var monochromSourceFileName = $"{service.ServiceSource}.svg";

    // NOTE: All icons as colored as supplied
    var coloredSourceFilePath = GetSourceFilePath(service.ServiceSource, true);
    //var monochromSourceFilePath = GetSourceFilePath(service.ServiceSource, false);
    var monochromSourceFilePath = (string) null;        

    var coloredExists = coloredSourceFilePath != null;
    var monochromExists = false;

    if (!coloredExists && !monochromExists)
    {
        WriteErrorLine($"Error: Missing {service.ServiceSource} color or mono");
        return;
    }

    var categoryDirectoryPath = Path.Combine(targetFolder, service.Category);
    Directory.CreateDirectory(categoryDirectoryPath);

    if (coloredExists)
    {
        // Only if needed - takes too long
        if (service.FitToCanvas)
        {
            FitCanvasToDrawing(coloredSourceFilePath);
        }

        // Resize and copy SVG
        RsvgConvert(coloredSourceFilePath, Path.Combine(categoryDirectoryPath, service.ServiceTarget + ".svg"), targetImageHeight);
        // Resize and export PNG
        RsvgConvert(coloredSourceFilePath, Path.Combine(categoryDirectoryPath, service.ServiceTarget + ".png"), targetImageHeight, exportAsPng: true);
    }
    else
    {
        WriteWarningLine($"Warning: Missing COLOR {service.ServiceSource}");
    }

    var monochromSvgFilePath = Path.Combine(categoryDirectoryPath, service.ServiceTarget + "(m).svg");
    if (monochromExists)
    {
        // Only if needed - takes too long
        //FitCanvasToDrawing(monochromSourceFilePath);

        // Resize and copy SVG
        RsvgConvert(monochromSourceFilePath, monochromSvgFilePath, targetImageHeight);    
    }
    else
    {
        // We generated every mono file so don't bother warning
        //WriteWarningLine($"Warning: Missing MONOCHROM {service.ServiceSource}, generating...");

        // Resize and copy colored SVG, making monochrom afterwards
        RsvgConvert(coloredSourceFilePath, monochromSvgFilePath, targetImageHeight);
        CreateMonochromNew(monochromSvgFilePath, monochromSvgFilePath);
    }

    var monochromPngFilePath = Path.Combine(categoryDirectoryPath, service.ServiceTarget + "(m).png");
    // First generation with background needed for PUML sprite generation
    RsvgConvert(monochromSvgFilePath, monochromPngFilePath, targetImageHeight, exportAsPng: true, withWhiteBackground: true);
    ConvertToPuml(monochromPngFilePath, service.ServiceTarget + ".puml");

    // Second generation without background needed other usages
    RsvgConvert(monochromSvgFilePath, monochromPngFilePath, targetImageHeight, exportAsPng: true, withWhiteBackground: false);
}

public bool FitCanvasToDrawing(string inputPath)
{
    Console.WriteLine("Fit canvas to drawing");

    var processInfo = new ProcessStartInfo
    {
        FileName = inkScapePath,
        Arguments = $"--verb=FitCanvasToDrawing --verb=FileSave --verb=FileClose --verb=FileQuit \"{inputPath}\"",
        RedirectStandardOutput = true,
        UseShellExecute = false,
        CreateNoWindow = true
    };

    using (var process = Process.Start(processInfo))
    {
        if (!process.WaitForExit(10000))
        {
            Console.WriteLine($"Killing InkScape for FitCanvasToDrawing {inputPath}");
            process.Kill();
            return false;
        }
    }

    return true;
}

public static bool RsvgConvert(string inputPath, string outputPath, int targetImageHeight, bool exportAsPng = false, bool withWhiteBackground = false)
{
    var processInfo = new ProcessStartInfo
    {
        FileName = rsvgConvertPath,
        Arguments = $"--height {targetImageHeight} --width {targetImageHeight} --keep-aspect-ratio --output \"{outputPath}\" --format {(exportAsPng ? "png" : "svg")} --background-color {(withWhiteBackground ? "white" : "none")} \"{inputPath}\"",
        RedirectStandardOutput = true,
        UseShellExecute = false,
        CreateNoWindow = true
    };

    using (var process = Process.Start(processInfo))
    {
        if (!process.WaitForExit(10000))
        {
            Console.WriteLine($"Killing rsvg-convert {inputPath}");
            process.Kill();
            return false;
        }
    }

    return true;
}

public bool CreateMonochromNew(string inputPath, string outputPath)
{
    // Making it first grayscale and after that monochrom gives best result
    ManipulateSvgFills(inputPath, outputPath, (color) =>
    {
        var grayScale = (int)((color.R * 0.3f) + (color.G * 0.59f) + (color.B * 0.11f));
        return Color.FromArgb(grayScale, grayScale, grayScale);
    });

    ManipulateSvgFills(outputPath, outputPath, (color) =>
    {
        HSLColor lowerColor = new HSLColor(color);
        var upperColor = new HSLColor(azureColor) { Luminosity = lowerColor.Luminosity };
        return upperColor;
    });

    return true;
}

private static void ManipulateSvgFills(string inputPath, string outputPath, Func<Color, Color> manipulation)
{
    var content = File.ReadAllText(inputPath);

    var start = "rgb(";
    var end = ")";
    var delimiter = ',';

    var fillStartPos = content.IndexOf(start, 0);
    while (fillStartPos > 0)
    {
        var fillContentStartPos = fillStartPos + start.Length;
        var fillContentEndPos = content.IndexOf(end, fillContentStartPos);

        var rgbPerc = content
            .Substring(fillContentStartPos, fillContentEndPos - fillContentStartPos)
            .Split(delimiter);

        int rValue, gValue, bValue;
        if (rgbPerc[0].Contains('%'))
        {
            var rPercValue = float.Parse(rgbPerc[0].TrimEnd('%'), CultureInfo.InvariantCulture);
            var gPercValue = float.Parse(rgbPerc[1].TrimEnd('%'), CultureInfo.InvariantCulture);
            var bPercValue = float.Parse(rgbPerc[2].TrimEnd('%'), CultureInfo.InvariantCulture);

            rValue = (int)Math.Round(rPercValue / 100 * 255);
            gValue = (int)Math.Round(gPercValue / 100 * 255);
            bValue = (int)Math.Round(bPercValue / 100 * 255);    
        }
        else
        {
            rValue = int.Parse(rgbPerc[0]);
            gValue = int.Parse(rgbPerc[1]);
            bValue = int.Parse(rgbPerc[2]);
        }

        var currentColor = Color.FromArgb(rValue, gValue, bValue);

        var newColor = manipulation(currentColor);
        content = content.ReplaceAt(fillStartPos, fillContentEndPos + 1 - fillStartPos, $"rgb({newColor.R.ToString(CultureInfo.InvariantCulture)},{newColor.G.ToString(CultureInfo.InvariantCulture)},{newColor.B.ToString(CultureInfo.InvariantCulture)})");

        fillStartPos = content.IndexOf(start, fillContentEndPos);
    }

    File.WriteAllText(outputPath, content);
}

//// str - the source string
//// index- the start location to replace at (0-based)
//// length - the number of characters to be removed before inserting
//// replace - the string that is replacing characters
public static string ReplaceAt(this string str, int index, int length, string replace)
{
    return str.Remove(index, Math.Min(length, str.Length - index))
            .Insert(index, replace);
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

public string GetSourceFilePath(string sourceFileName, bool color = true)
{
    var patterns = new List<string> { 
        "*-icon-service-{0}.svg",
        "{0}_COLOR.svg",
        "{0}.svg"        
    };

    foreach (var p in patterns)
    {
        var x = string.Format(p, sourceFileName);
        if (x.Contains("-") || x.Contains(" "))
        {
            // Original files have hyphen not space so assume that based on pattern
            x = x.Replace(" ", "-");
        }
        var files = Directory.GetFiles(originalSourceFolder, x, SearchOption.AllDirectories);
        if (files.Length == 0)
        {
            files = Directory.GetFiles(manualSourceFolder, x, SearchOption.AllDirectories);
        }

        if (files.Length >= 1)
        {
            if (files.Length > 1)
            {
                var matches = String.Join(",", files);
                WriteWarningLine($"Warning: File found in multiple locations {sourceFileName}: {matches}");
            }
            return files[0];
        }
    }

    return null;
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