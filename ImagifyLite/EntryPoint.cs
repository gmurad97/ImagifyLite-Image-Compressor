using ImageMagick;
using System;
using System.IO;
using ImagifyLite;
using System.Threading.Tasks;

namespace ImagifyLite
{
    internal class EntryPoint
    {
        private double percent = 0;
        static void Main()
        {
            ImagifyLite imagifyLite = new ImagifyLite("input_images", "output_images");
            imagifyLite.HelloWorld(true);
            string[] inputFiles = Directory.GetFiles(imagifyLite.inputFolderPath);
            Parallel.ForEach(inputFiles, inputFile =>
            {
                imagifyLite.ProcessImage(inputFile, 70);
            });
            while (true)
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    Environment.Exit(0);
        }
    }
}