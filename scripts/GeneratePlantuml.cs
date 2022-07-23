using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Drawing;
using System.Text;
using System.Diagnostics;

public class GeneratePlantuml : IHostedService
{
    readonly ILogger logger;
    readonly IHostApplicationLifetime lifetime;
    readonly string sourceFolder;
    readonly string originalSourceFolder;
    readonly string manualSourceFolder;
    readonly string targetFolder;
    readonly int targetImageHeight = 70;
    readonly Color azureColor;
    readonly string plantUmlPath;
    IImageManager svgManager;

    public GeneratePlantuml(ILogger<GeneratePlantuml> logprovider, IConfiguration config, IImageManager exporter, IHostApplicationLifetime appLifetime)
    {
        logger = logprovider;
        lifetime = appLifetime;
        sourceFolder = config.GetSection("sourceFolderPath").Value;
        originalSourceFolder = Path.Combine(sourceFolder, "official");
        manualSourceFolder = Path.Combine(sourceFolder, "manual");
        targetFolder = config.GetSection("targetFolderPath").Value;
        azureColor = System.Drawing.ColorTranslator.FromHtml(config.GetSection("monochromeColorHex").Value);
        plantUmlPath = config.GetSection("plantUmlPath").Value;
        svgManager = exporter;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("GeneratePlantUml is starting.");
        _ = ProcessFiles();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("GeneratePlantUml is stopping.");
        lifetime.StopApplication();
        return Task.CompletedTask;
    }

    private async Task ProcessFiles()
    {
        var lookupTable = Config.ReadConfig("Config.yaml");

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
            await ProcessService(service); 
        }

        foreach (var category in lookupTable.Select(_ => _.Category).Distinct())
        {
            var categoryDirectoryPath = Path.Combine(targetFolder, category!);
            var catAllFilePath = Path.Combine(categoryDirectoryPath, "all.puml");
            CombineMultipleFilesIntoSingleFile(categoryDirectoryPath, "*.puml", catAllFilePath);
        }

        await VSCodeSnippets.GenerateSnippets(targetFolder);
        await MarkdownTable.GenerateTable(targetFolder);
        await this.StopAsync(new System.Threading.CancellationToken());
    }

    async Task ProcessService(ConfigLookupEntry service) 
    {
        logger.LogInformation($"Processing {service.ServiceSource}");

        // NOTE: All icons as colored as supplied
        var monochromExists = false;
        var coloredExists = false;
        var coloredSourceFilePath = GetSourceFilePath(service.ServiceSource!, true);
        coloredExists = !string.IsNullOrEmpty(coloredSourceFilePath);

        if (!coloredExists && !monochromExists)
        {
            logger.LogWarning($"Error: Missing {service.ServiceSource} color or mono");
            return;
        }

        var categoryDirectoryPath = Path.Combine(targetFolder, service.Category!);
        Directory.CreateDirectory(categoryDirectoryPath);

        try
        {
            if (coloredExists)
                {
        
                    await svgManager.ResizeAndCopy(coloredSourceFilePath, Path.Combine(categoryDirectoryPath, service.ServiceTarget + ".svg"), targetImageHeight);
                    await svgManager.ExportToPng(Path.Combine(categoryDirectoryPath, service.ServiceTarget + ".svg"), Path.Combine(categoryDirectoryPath, service.ServiceTarget + ".png"), targetImageHeight);    
                }
                else
                {
                    logger.LogWarning($"Missing SVG file for: {service.ServiceSource}. Please add the file or remove the entry from Config.yaml ");
                }

                var monochromSvgFilePath = Path.Combine(categoryDirectoryPath, service.ServiceTarget + "(m).svg");

                // This should never be called if we assume only colored SVGs are provided on input.
                if (monochromExists)
                { 
                    await svgManager.ResizeAndCopy(Path.Combine(categoryDirectoryPath, service.ServiceTarget + ".svg"), monochromSvgFilePath, targetImageHeight);    
                }
                else
                {
                    await svgManager.ResizeAndCopy(Path.Combine(categoryDirectoryPath, service.ServiceTarget + ".svg"), monochromSvgFilePath, targetImageHeight);
                    await svgManager.ExportToMonochrome(monochromSvgFilePath, monochromSvgFilePath, azureColor);
                }

                var monochromPngFilePath = Path.Combine(categoryDirectoryPath, service.ServiceTarget + "(m).png");
                // First generation with background needed for PUML sprite generation
                await  svgManager.ExportToPng(monochromSvgFilePath, monochromPngFilePath, targetImageHeight, omitBackground: false);
                ConvertToPuml(monochromPngFilePath, service.ServiceTarget + ".puml");

                // Second generation without background needed other usages
                await svgManager.ExportToPng(monochromSvgFilePath, monochromPngFilePath, targetImageHeight, omitBackground: true);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return;
        }
        
    }

    void CombineMultipleFilesIntoSingleFile(string inputDirectoryPath, string inputFileNamePattern, string outputFilePath)
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

    string GetSourceFilePath(string sourceFileName, bool color = true)
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
                    logger.LogWarning($"Warning: File found in multiple locations {sourceFileName}: {matches}");
                }
                return files[0];
            }
        }

        return string.Empty;    // no result
    }

    string ConvertToPuml(string pngPath, string pumlFileName)
    {
        var format = "16z";
        var entityName = Path.GetFileNameWithoutExtension(pumlFileName);
        var pumlPath = Path.Combine(Directory.GetParent(pngPath)!.FullName, pumlFileName);
        var processInfo = new ProcessStartInfo();

        if (OperatingSystem.IsMacOS())
        {
            processInfo.FileName = "/bin/zsh";
            // processInfo.FileName = "java";
            string commandArgs = $"-c \"java -Djava.awt.headless=true -jar {plantUmlPath} -encodesprite {format} '{pngPath}'\"";
            processInfo.Arguments = commandArgs;
            // processInfo.Arguments = $"-Djava.awt.headless=true -jar {plantUmlPath} -encodesprite {format} {pngPath}";
        }
        if (OperatingSystem.IsWindows())
        {
            processInfo.FileName = "java";
            processInfo.Arguments = $"-jar {plantUmlPath} -encodesprite {format} \"{pngPath}\"";       
        }
        if (OperatingSystem.IsLinux())
        {
            processInfo.FileName = "java";
            processInfo.Arguments = $"-jar {plantUmlPath} -encodesprite {format} {pngPath}";  
        }

        processInfo.RedirectStandardOutput = true;
        processInfo.UseShellExecute = false;
        processInfo.CreateNoWindow = true;

        var pumlContent = new StringBuilder();
        using (var process = Process.Start(processInfo))
        {
            process!.WaitForExit();
            pumlContent.Append(process.StandardOutput.ReadToEnd());
        }

        pumlContent.AppendLine($"AzureEntityColoring({entityName})");
        pumlContent.AppendLine($"!define {entityName}(e_alias, e_label, e_techn) AzureEntity(e_alias, e_label, e_techn, AZURE_SYMBOL_COLOR, {entityName}, {entityName})");
        pumlContent.AppendLine($"!define {entityName}(e_alias, e_label, e_techn, e_descr) AzureEntity(e_alias, e_label, e_techn, e_descr, AZURE_SYMBOL_COLOR, {entityName}, {entityName})");

        File.WriteAllText(pumlPath, pumlContent.ToString());
        return pumlPath;
    }

}
