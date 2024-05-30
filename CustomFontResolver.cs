using PdfSharp.Fonts;
using System;
using System.IO;
using System.Reflection;

public class CustomFontResolver : IFontResolver
{
    public static readonly CustomFontResolver Instance = new CustomFontResolver();

    private CustomFontResolver()
    {
    }

    public byte[] GetFont(string faceName)
    {
        string resourceName = GetResourceName(faceName);
        using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
        {
            if (stream == null)
            {
                throw new FileNotFoundException("Font not found.", resourceName);
            }

            byte[] fontData = new byte[stream.Length];
            stream.Read(fontData, 0, fontData.Length);
            return fontData;
        }
    }

    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        string fontName = familyName;
        if (isBold)
            fontName += "-Bold";
        else if (isItalic)
            fontName += "-Italic";

        if (GetResourceName(fontName) != null)
            return new FontResolverInfo(fontName);

        return null;
    }

    private string GetResourceName(string fontName)
    {
        string fontPath = "PDFGeneratorApp.Fonts." + fontName + ".ttf";
        return Assembly.GetExecutingAssembly().GetManifestResourceNames().FirstOrDefault(r => r.EndsWith(fontPath));
    }
}
