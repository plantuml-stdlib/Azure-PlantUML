using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Playwright;
using Svg;
using System.Globalization;
using System.Drawing;
using System.Diagnostics;

public class SvgManager : IImageManager
{
    private readonly ILogger _logger;
    private IPage? page;
    private IPlaywright? playwrightContext;
    private IBrowser? browser;

    public SvgManager(ILogger<SvgManager> logger)
    {
        _logger = logger;
    }

    private async Task Init()
    {
        // Ensure browsers are installed: https://playwright.dev/dotnet/docs/browsers#prerequisites-for-net
        Microsoft.Playwright.Program.Main(new string[] { "install" });
        
        try
        {
            playwrightContext = await Playwright.CreateAsync();
            browser = await playwrightContext.Chromium.LaunchAsync(new() { Headless = true });
            page = await browser.NewPageAsync();
        }
        catch(Exception ex)
        {
            _logger.LogCritical(ex.Message);
            if (OperatingSystem.IsLinux())
            {
                _logger.LogWarning("Ensure Linux dependencies are installed using the Microsoft.Playwright.CLI. See: https://playwright.dev/dotnet/docs/browsers#prerequisites-for-net");
            }
        }

    }

    public async Task<bool> ExportToPng(string inputPath, string outputPath, int targetImageHeight, bool omitBackground = true)
    {
        var currentDirectory = Environment.CurrentDirectory;
        var rootDirectory = System.IO.Directory.GetParent(currentDirectory);

        var inputPathNormalized = inputPath.Replace(@"..\", "").Replace(@"../", "").Replace(@"\", @"/");
        var inputTokens = inputPathNormalized.Split(@"/");
        var inputFile = inputTokens.Last();

        var outputPathNormalized = outputPath.Replace(@"..\", "").Replace(@"../", "").Replace(@"\", @"/");
        var outputTokens = outputPathNormalized.Split(@"/");
        var outputFile = outputTokens.Last();

        var newInputPath = Path.Combine(rootDirectory!.FullName, inputTokens[0], inputTokens[1], inputFile);

        if (!OperatingSystem.IsWindows())
            newInputPath = "file://" + newInputPath;

        var newOutputPath = Path.Combine(rootDirectory.FullName, outputTokens[0],  outputTokens[1], outputFile);

        if (page == null)
            await Init();

        try
        {
            await page!.SetViewportSizeAsync(targetImageHeight, targetImageHeight);
            await page!.GotoAsync(newInputPath);
            var item = await page.QuerySelectorAsync("svg");
            await item!.ScreenshotAsync(new() { Path = newOutputPath, OmitBackground = omitBackground });
            return await Task.FromResult(true);
        }
        catch(Exception ex)
        {
            _logger.LogError($"Failed to export png {inputPath}  Message: {ex.Message}");
            return await Task.FromResult(false);
        }
    }

    public async Task<bool> ResizeAndCopy(string inputPath, string outputPath, int targetImageHeight)
    {
        try
        {
            var doc = SvgDocument.Open(inputPath);
            var viewBox = doc.ViewBox;

            if (viewBox.Height != 0 && viewBox.Width != 0)
            {
                doc.Height = targetImageHeight;
                doc.Width = targetImageHeight;
                doc.AspectRatio.Align = SvgPreserveAspectRatio.xMidYMid;
            }
            else    // Items that don't have a viewbox
            {
                doc.Height = targetImageHeight;
                doc.Width = targetImageHeight;
                doc.ViewBox = new SvgViewBox(0, 0, targetImageHeight, targetImageHeight);
                doc.AspectRatio.Align = SvgPreserveAspectRatio.xMidYMid;
            }

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(doc.GetXML());
            XmlDocumentType docType = xmldoc.DocumentType!;
            xmldoc.RemoveChild(docType);

            XmlWriter writer = XmlWriter.Create(outputPath);
            xmldoc.Save(writer);
            writer.Close();
            return await Task.FromResult(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine("error in ResizeAndCopySvg"  + ex.Message);
            return await Task.FromResult(false);
        }
    }

    public async Task<bool> ExportToMonochrome(string inputPath, string outputPath, Color azureColor)
    {
        // Making it first grayscale and after that monochrom gives best result
        await ManipulateSvgFills(inputPath, outputPath, (color) =>
        {
            var grayScale = (int)((color.R * 0.3f) + (color.G * 0.59f) + (color.B * 0.11f));
            return Color.FromArgb(grayScale, grayScale, grayScale);
        });

        await ManipulateSvgFills(outputPath, outputPath, (color) =>
        {
            HSLColor lowerColor = new HSLColor(color);
            var upperColor = new HSLColor(azureColor) { Luminosity = lowerColor.Luminosity };
            return upperColor;
        });

        return await Task.FromResult(true);
    }

    private async Task<bool> ManipulateSvgFills(string inputPath, string outputPath, Func<Color, Color> manipulation)
    {
        var content = File.ReadAllText(inputPath);

        // get all hexidecimal colors in the SVG
        var hexColors = Regex.Matches(content, "#([a-fA-F0-9]{6}|[a-fA-F0-9]{3})");

        foreach(var hexColor in hexColors)
        {
            var currentColor = System.Drawing.ColorTranslator.FromHtml(hexColor.ToString()!);
            var newColor = manipulation(currentColor);
            content = content.Replace(hexColor.ToString()!, newColor.ToHexString());
        }

        // Handle RGB colors
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

        await File.WriteAllTextAsync(outputPath, content);
        return await Task.FromResult(true);
    }

}
