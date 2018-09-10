#r "System.Drawing"
#load "HSLColor.csx"

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public void Colorize(string originalImagePath, string newImagePath, Color newColor)
{
    using (var lower = new Bitmap(originalImagePath))
    using (var output = new Bitmap(lower.Width, lower.Height))
    {
        int width = lower.Width;
        int height = lower.Height;
        var rect = new Rectangle(0, 0, width, height);

        BitmapData lowerData = lower.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
        BitmapData outputData = output.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

        unsafe
        {
            byte* lowerPointer = (byte*)lowerData.Scan0;
            byte* outputPointer = (byte*)outputData.Scan0;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    HSLColor lowerColor = new HSLColor(lowerPointer[2], lowerPointer[1], lowerPointer[0]);

                    var upperColor = new HSLColor(newColor) { Luminosity = lowerColor.Luminosity };
                    Color outputColor = (Color)upperColor;
                    outputColor = Color.FromArgb(lowerPointer[3], outputColor);

                    outputPointer[0] = outputColor.B;
                    outputPointer[1] = outputColor.G;
                    outputPointer[2] = outputColor.R;
                    outputPointer[3] = outputColor.A;

                    // Moving the pointers by 4 bytes per pixel
                    lowerPointer += 4;
                    outputPointer += 4;
                }

                // Moving the pointers to the next pixel row
                lowerPointer += lowerData.Stride - (width * 4);
                outputPointer += outputData.Stride - (width * 4);
            }
        }

        lower.UnlockBits(lowerData);
        output.UnlockBits(outputData);

        output.Save(newImagePath, ImageFormat.Png);
    }
}