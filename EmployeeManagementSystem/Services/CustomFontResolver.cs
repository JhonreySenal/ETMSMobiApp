using PdfSharpCore.Fonts;
using System;
using System.IO;
using System.Reflection;

public class CustomFontResolver : IFontResolver
{
    public static readonly CustomFontResolver Instance = new();

    public string DefaultFontName => "Verdana";

    public byte[] GetFont(string faceName)
    {
        var assembly = typeof(CustomFontResolver).GetTypeInfo().Assembly;
        var resource = "EmployeeManagementSystem.Fonts.Verdana.ttf"; // Replace with actual resource name

        using var stream = assembly.GetManifestResourceStream(resource);
        if (stream == null)
            throw new Exception($"Could not find font resource '{resource}'");

        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }

    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        return new FontResolverInfo("Verdana");
    }
}
