using System.Drawing;

public interface IImageManager
{
    Task Init();
    Task<bool> ExportToPng(string inputPath, string outputPath, int targetImageHeight, bool omitBackground = true);
    Task<bool> ResizeAndCopy(string inputPath, string outputPath, int targetImageHeight);
    Task<bool> ExportToMonochrome(string inputPath, string outputPath, Color color);
}
