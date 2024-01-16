using ImageMagick;
using System;
using System.IO;
using ImagifyLite;
using System.Threading.Tasks;

namespace ImagifyLite
{
    internal class EntryPoint
    {
        private static object _locker = new object();
        static void Main()
        {
            ImagifyLite imagifyLite = new ImagifyLite("input_images", "output_images");
            imagifyLite.HelloWorld(true);
            string[] inputFiles = Directory.GetFiles(imagifyLite.inputFolderPath);
            double processedCount = 0;
            Parallel.ForEach(inputFiles, (inputFile) =>
            {
                imagifyLite.ProcessImage(inputFile, 70);
                lock (_locker)
                {
                    processedCount++;
                    Console.Title = $"ImagifyLite - Progress:{((processedCount / inputFiles.Length) * 100):F2}%";
                }
            });
            Console.WriteLine("Success!For exit press 'ESC'");
            while (true)
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    Environment.Exit(0);
        }
    }
}