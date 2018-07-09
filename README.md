# PDF2PNG

Class Library Core 2.0
You can use this both with ASP.NET Framework 4.7.1 and ASP.NET Core 2.0
A project i work with i needed to convert a PDF to PNG's.
I find Ghostscript, but i did not find a solution for core then.
So i did a Class Library Core with Ghostscript and that you can call it from both
ASP.NET Framework 4.7.1 and ASP.NET Core 2.0.

---------------------------------------------------------------------------------------------------------------
To use it in ASP.NET Framework 4.7.1:
Converting.PDF2PNG(string Path, int Dpi, int HeightResolution, int WidthResolution, HttpPostedFileBase PdfFile)

HttpPostedFileBase is what you use in 4.7.1

---------------------------------------------------------------------------------------------------------------

To use it in ASP.NET Core 2.0:
Converting.PDF2PNG(string Path, int Dpi, int HeightResolution, int WidthResolution, Stream PdfFile)

In Core you use IFormFile.
And you have to convert it to a stream:

```
var filePath = Path.GetTempFileName();

using (var stream = new FileStream(filePath, FileMode.Create))
{
    await file.CopyToAsync(stream);
    Converting.Pdf2Png(folder, fileNameWithoutExt , 300, 1280, 720, stream);
}
```

----------------------------------------------------------------------------------------------------------------

It will convert Pdf-file to png-files and thumbnails in that folder you choice.
I hope you now how to get path from server.
