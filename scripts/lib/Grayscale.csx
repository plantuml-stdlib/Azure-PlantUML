#r "nuget: System.Drawing.Common, 4.5.0"

using System.Drawing;
using System.Drawing.Imaging;

public string MakeGrayscale(string imagePath)
{
    using (var original = Image.FromFile(imagePath))
    using (Image grayImage = new Bitmap(original.Width, original.Height))
    {
        //create the grayscale ColorMatrix
        ColorMatrix colorMatrix = new ColorMatrix(
            new float[][]
            {
                    new float[] {.3f, .3f, .3f, 0, 0},
                    new float[] {.59f, .59f, .59f, 0, 0},
                    new float[] {.11f, .11f, .11f, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1}
            });

        //create some image attributes
        ImageAttributes attributes = new ImageAttributes();

        //set the color matrix attribute
        attributes.SetColorMatrix(colorMatrix);

        using (var graphic = Graphics.FromImage(grayImage))
        {
            //draw the original image on the new image
            //using the grayscale color matrix
            graphic.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
        }

        var grayImagePath = Path.Combine(Path.GetDirectoryName(imagePath), String.Concat(Path.GetFileNameWithoutExtension(imagePath), "(g)", Path.GetExtension(imagePath)));
        grayImage.Save(grayImagePath, ImageFormat.Png);

        return grayImagePath;
    }
}