using Microsoft.Win32;
using System;
using VouwwandImages.UI;
using iTextSharp.text.pdf.parser;
using System.IO;
using System.Windows.Media.Effects;
using iTextSharp.text.pdf;
using Path = System.IO.Path;

namespace VouwwandImages.ViewModels
{
    public class PdfViewModel : ViewModel
    {
        public TargetCommand ExportImagesCommand
        {
            get { return new TargetCommand(ExportImages); }
        }

        private void ExportImages()
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                string folder = Path.GetDirectoryName(ofd.FileName);
                foreach (var fileName in Directory.EnumerateFiles(folder, "*.pdf"))
                {
                    string filenameExt = Path.GetFileNameWithoutExtension(fileName);
                    ImageExtractor.ExtractImagesFromFile(fileName, "", Path.Combine("c:\\temp\\pdf", filenameExt), true);
                }

            }
        }

       
        /// <summary>
        /// Helper lass to dump all images from a PDF into separate files
        /// </summary>
        internal class ImageExtractor : IRenderListener
        {
            int _currentPage = 1;
            int _imageCount = 0;
            readonly string _outputFilePrefix;
            readonly string _outputFolder;
            readonly bool _overwriteExistingFiles;

            private ImageExtractor(string outputFilePrefix, string outputFolder, bool overwriteExistingFiles)
            {
                _outputFilePrefix = outputFilePrefix;
                _outputFolder = outputFolder;
                _overwriteExistingFiles = overwriteExistingFiles;
            }

            /// <summary>
            /// Extract all images from a PDF file
            /// </summary>
            /// <param name="pdfPath">Full path and file name of PDF file</param>
            /// <param name="outputFilePrefix">Basic name of exported files. If null then uses same name as PDF file.</param>
            /// <param name="outputFolder">Where to save images. If null or empty then uses same folder as PDF file.</param>
            /// <param name="overwriteExistingFiles">True to overwrite existing image files, false to skip past them</param>
            /// <returns>Count of number of images extracted.</returns>
            public static int ExtractImagesFromFile(string pdfPath, string outputFilePrefix, string outputFolder, bool overwriteExistingFiles)
            {
                Directory.CreateDirectory(outputFolder);

                // Handle setting of any default values
                outputFilePrefix = outputFilePrefix ?? Path.GetFileNameWithoutExtension(pdfPath);
                outputFolder = String.IsNullOrEmpty(outputFolder) ? Path.GetDirectoryName(pdfPath) : outputFolder;

                var instance = new ImageExtractor(outputFilePrefix, outputFolder, overwriteExistingFiles);

                using (var pdfReader = new PdfReader(pdfPath))
                {
                    if (pdfReader.IsEncrypted())
                        throw new ApplicationException(pdfPath + " is encrypted.");

                    var pdfParser = new PdfReaderContentParser(pdfReader);

                    while (instance._currentPage <= pdfReader.NumberOfPages)
                    {
                        pdfParser.ProcessContent(instance._currentPage, instance);

                        instance._currentPage++;
                    }
                }

                return instance._imageCount;
            }

            #region Implementation of IRenderListener

            public void BeginTextBlock() { }
            public void EndTextBlock() { }
            public void RenderText(TextRenderInfo renderInfo) { }

            public void RenderImage(ImageRenderInfo renderInfo)
            {
                var imageObject = renderInfo.GetImage();

                var imageFileName = String.Format("{0}_{1}_{2}.{3}", _outputFilePrefix, _currentPage, _imageCount, imageObject.GetFileType());
                var imagePath = Path.Combine(_outputFolder, imageFileName);

                if (_overwriteExistingFiles || !File.Exists(imagePath))
                {
                    var imageRawBytes = imageObject.GetImageAsBytes();

                    File.WriteAllBytes(imagePath, imageRawBytes);

                }

                // Subtle: Always increment even if file is not written. This ensures consistency should only some
                //   of a PDF file's images actually exist.
                _imageCount++;
            }

            #endregion // Implementation of IRenderListener

        }
    }
}

