using Ghostscript.NET.Rasterizer;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace Pdf2Png
{
    public static class Converting
    {
        /// <summary>
        /// Convert a PDF to PNG-files, and a thumbnail for each PNG.
        /// Using GhostScript.NET.
        /// </summary>
        /// <param name="imagePath">Desired path where files will be saved</param>
        /// <param name="dpi">Desired dpi. 100-600 is normal. The higher dpi, the bigger file, and slower converting.</param>
        /// <param name="heightResolution">Set desired height Resolution</param>
        /// <param name="widthResolution">Set desired width Resolution</param>
        /// <param name="file">pdf file send as HttpPostedFileBase</param>
        public static void Pdf2Png(string imagePath , int dpi, int heightResolution, int widthResolution, HttpPostedFileBase pdfFile)
        {
            if (pdfFile != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(pdfFile.FileName);
                string outputFolder = imagePath;                                
                var xDpi = dpi;
                var yDpi = dpi;
                
                using (var rasterizer = new GhostscriptRasterizer()) //create an instance for GhostscriptRasterizer
                {
                    rasterizer.Open(pdfFile.InputStream);
                    var numberOfPages = rasterizer.PageCount;
                    for (int i = 1; i < numberOfPages + 1; i++)
                    {                        
                        var outputPNGPath = Path.Combine(outputFolder, string.Format("{0}.png", fileName + i.ToString("D2")));
                        var outputPNGThumbnailPath = Path.Combine(outputFolder, string.Format("Thumbnail_{0}.png", fileName + i.ToString("D2")));
                        
                        var pdf2PNG = rasterizer.GetPage(xDpi, yDpi, i);

                        Bitmap bitmap = new Bitmap(pdf2PNG, new Size(widthResolution, heightResolution));
                        Bitmap thumbnailBitMap = new Bitmap(pdf2PNG, new Size(320, 180));
                        
                        bitmap.Save(outputPNGPath, ImageFormat.Png);
                        thumbnailBitMap.Save(outputPNGThumbnailPath, ImageFormat.Png);
                    }
                }                
            }
        }

        /// <summary>
        /// Convert a PDF to PNG-files, and a thumbnail for each PNG.
        /// Using GhostScript.NET.
        /// </summary>
        /// <param name="imagePath">Desired path where files will be saved</param>
        /// /// <param name="imageName">Desired name on the image files</param>
        /// <param name="dpi">Desired dpi. 100-600 is normal. The higher dpi, the bigger file, and slower converting.</param>
        /// <param name="heightResolution">Set desired height Resolution</param>
        /// <param name="widthResolution">Set desired width Resolution</param>
        /// <param name="file">pdf file send as HttpPostedFileBase</param>
        public static void Pdf2Png(string imagePath, string imageName, int dpi, int heightResolution, int widthResolution, Stream pdfFile)
        {
            if (pdfFile != null)
            {
                string outputFolder = imagePath;
                var xDpi = dpi;
                var yDpi = dpi;

                using (var rasterizer = new GhostscriptRasterizer()) //create an instance for GhostscriptRasterizer
                {
                    rasterizer.Open(pdfFile);
                    var numberOfPages = rasterizer.PageCount;
                    for (int i = 1; i < numberOfPages + 1; i++)
                    {
                        var outputPNGPath = Path.Combine(outputFolder, string.Format("{0}.png", imageName + i.ToString("D2")));
                        var outputPNGThumbnailPath = Path.Combine(outputFolder, string.Format("Thumbnail_{0}.png", imageName + i.ToString("D2")));

                        var pdf2PNG = rasterizer.GetPage(xDpi, yDpi, i);

                        Bitmap bitmap = new Bitmap(pdf2PNG, new Size(widthResolution, heightResolution));
                        Bitmap thumbnailBitMap = new Bitmap(pdf2PNG, new Size(320, 180));

                        bitmap.Save(outputPNGPath, ImageFormat.Png);
                        thumbnailBitMap.Save(outputPNGThumbnailPath, ImageFormat.Png);
                    }
                }
            }
        }
    }
}
