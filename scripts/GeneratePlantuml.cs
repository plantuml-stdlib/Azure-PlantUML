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
    readonly List<string> missingServices = new();
    readonly List<string> failedServices = new();

    public GeneratePlantuml(ILogger<GeneratePlantuml> logprovider, IConfiguration config, IImageManager exporter, IHostApplicationLifetime appLifetime)
    {
        logger = logprovider;
        lifetime = appLifetime;
        sourceFolder = GetRequiredSetting(config, "sourceFolderPath");
        originalSourceFolder = Path.Combine(sourceFolder, "official");
        manualSourceFolder = Path.Combine(sourceFolder, "manual");
        targetFolder = GetRequiredSetting(config, "targetFolderPath");
        azureColor = System.Drawing.ColorTranslator.FromHtml(GetRequiredSetting(config, "monochromeColorHex"));
        plantUmlPath = GetRequiredSetting(config, "plantUmlPath");
        svgManager = exporter;
    }

    static string GetRequiredSetting(IConfiguration config, string key)
    {
        var value = config.GetSection(key).Value;
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidOperationException($"Missing setting '{key}'. Set it in appsettings.json or pass --{key}=<value> on the command line. See scripts/README.md.");
        }
        return value;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("GeneratePlantUml is starting.");
        _ = Run();
        return Task.CompletedTask;
    }

    async Task Run()
    {
        try
        {
            await ProcessFiles();

            if (missingServices.Count > 0)
            {
                logger.LogError($"{missingServices.Count} service(s) in Config.yaml have no source SVG: {string.Join(", ", missingServices)}");
            }
            if (failedServices.Count > 0)
            {
                logger.LogError($"{failedServices.Count} service(s) failed to generate: {string.Join(", ", failedServices)}");
            }
            if (missingServices.Count > 0 || failedServices.Count > 0)
            {
                Environment.ExitCode = 1;
            }
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Generation failed");
            Environment.ExitCode = 1;
        }
        finally
        {
            lifetime.StopApplication();
        }
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

        // Fail fast on browser problems before the existing output is deleted
        await svgManager.Init();

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
            logger.LogInformation($"Processing {category} Category");
            var categoryDirectoryPath = Path.Combine(targetFolder, category!);
            var catAllFilePath = Path.Combine(categoryDirectoryPath, "all.puml");
            CombineMultipleFilesIntoSingleFile(categoryDirectoryPath, "*.puml", catAllFilePath);
        }

        await VSCodeSnippets.GenerateSnippets(targetFolder);
        await MarkdownTable.GenerateTable(targetFolder);
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
            missingServices.Add(service.ServiceSource!);
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
            logger.LogError($"Failed to process {service.ServiceSource}: {ex.Message}");
            failedServices.Add(service.ServiceSource!);
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
            var files = Directory.GetFiles(originalSourceFolder, x, new EnumerationOptions { MatchCasing = MatchCasing.CaseInsensitive, RecurseSubdirectories = true });

            if (files.Length == 0)
            {
                files = Directory.GetFiles(manualSourceFolder, x, new EnumerationOptions { MatchCasing = MatchCasing.CaseInsensitive, RecurseSubdirectories = true });
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
        // ArgumentList quotes each argument per platform, so paths with spaces or parentheses work everywhere
        var processInfo = new ProcessStartInfo("java");
        processInfo.ArgumentList.Add("-Djava.awt.headless=true");
        processInfo.ArgumentList.Add("-jar");
        processInfo.ArgumentList.Add(plantUmlPath);
        processInfo.ArgumentList.Add("-encodesprite");
        processInfo.ArgumentList.Add(format);
        processInfo.ArgumentList.Add(pngPath);

        processInfo.RedirectStandardOutput = true;
        processInfo.RedirectStandardError = true;
        processInfo.UseShellExecute = false;
        processInfo.CreateNoWindow = true;

        var pumlContent = new StringBuilder();
        using (var process = Process.Start(processInfo))
        {
            // Read output before waiting to avoid blocking on a full pipe buffer
            var stdout = process!.StandardOutput.ReadToEndAsync();
            var stderr = process.StandardError.ReadToEndAsync();
            process.WaitForExit();
            if (process.ExitCode != 0 || !stdout.Result.StartsWith("sprite"))
            {
                throw new Exception($"PlantUML sprite encoding failed for {pngPath} (exit code {process.ExitCode}): {stderr.Result}");
            }
            pumlContent.Append(stdout.Result);
        }

        pumlContent.AppendLine($"AzureEntityColoring({entityName})");
        pumlContent.AppendLine($"!define {entityName}(e_alias, e_label, e_techn) AzureEntity(e_alias, e_label, e_techn, AZURE_SYMBOL_COLOR, {entityName}, {entityName})");
        pumlContent.AppendLine($"!define {entityName}(e_alias, e_label, e_techn, e_descr) AzureEntity(e_alias, e_label, e_techn, e_descr, AZURE_SYMBOL_COLOR, {entityName}, {entityName})");
        pumlContent.AppendLine($"!procedure {entityName}($alias, $label, $techn, $descr,  $color, $tags, $link)\n AzureEntity($alias, $label, $techn, $descr,  $color, $sprite={entityName}, $stereo={entityName}, $tags, $link)\n !endprocedure");
        pumlContent.AppendLine($"!procedure {entityName}($alias, $label, $techn, $descr=\"\",  $tags=\"\", $link=\"\", $color=\"\")");
        pumlContent.AppendLine($"   AzureEntity($alias, $label, $color, $techn, $descr, {entityName}, $tags, $link, {entityName})");
        pumlContent.AppendLine("!endprocedure");

        File.WriteAllText(pumlPath, pumlContent.ToString());
        return pumlPath;
    }

}
