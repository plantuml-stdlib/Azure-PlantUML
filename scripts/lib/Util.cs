using System.Drawing;

public static class Util
{
    public static string ToHexString(this Color c) => $"#{c.R:X2}{c.G:X2}{c.B:X2}";

    public static string ToRgbString(this Color c) => $"RGB({c.R}, {c.G}, {c.B})";

    public static string ReplaceAt(this string str, int index, int length, string replace)
    {
        return str.Remove(index, Math.Min(length, str.Length - index)).Insert(index, replace);
    }
}
