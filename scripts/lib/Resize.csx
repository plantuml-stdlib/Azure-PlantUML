#r "System.Drawing"

using System.Drawing;
using System.Drawing.Drawing2D;

public static void Resize(string imageFile, string outputFile, int maxWidth, int maxHeight)
{
    var resizeDone = false;
    var tmpOutputFile = outputFile + ".tmp";

    using (var srcImage = Image.FromFile(imageFile))
    {
        if (srcImage.Width > maxWidth || srcImage.Height > maxHeight)
        {
            var ratioX = (double)maxWidth / srcImage.Width;
            var ratioY = (double)maxHeight / srcImage.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(srcImage.Width * ratio);
            var newHeight = (int)(srcImage.Height * ratio);

            using (var newImage = new Bitmap(newWidth, newHeight))
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawImage(srcImage, new Rectangle(0, 0, newWidth, newHeight));
                newImage.Save(tmpOutputFile);
            }
            resizeDone = true;
        }

        srcImage.Dispose();
    }

    if (resizeDone)
    {
        File.Copy(tmpOutputFile, outputFile, true);
        File.Delete(tmpOutputFile);
    }
    else if (imageFile != outputFile)
    {
        File.Copy(imageFile, outputFile, true);
    }
}