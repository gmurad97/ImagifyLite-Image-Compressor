using ImageMagick;
using System;
using System.IO;

namespace ImagifyLite
{
    internal class EntryPoint
    {
        static void Main()
        {
            string inputDirectory = "input_images";
            string outputDirectory = "output_images";

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }
            if (!Directory.Exists(inputDirectory))
            {
                Directory.CreateDirectory(inputDirectory);
            }

            string[] inputFiles = Directory.GetFiles(inputDirectory);


            foreach (string inputFile in inputFiles)
            {
                string outputFileName = Path.Combine(outputDirectory, Path.GetFileName(inputFile));
                using (var image = new MagickImage(inputFile))
                {
                    image.Quality = 50;
                    image.Write(outputFileName);
                }
            }
        }
    }
}