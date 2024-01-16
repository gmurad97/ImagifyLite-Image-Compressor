using System;
using System.IO;
using ImageMagick;

namespace ImagifyLite
{
    internal class ImagifyLite
    {
        public readonly string inputFolderPath = null;
        public readonly string outputFolderPath = null;

        public ImagifyLite(string inputFolderPath, string outputFolderPath)
        {
            if (!Directory.Exists(inputFolderPath) || !Directory.Exists(outputFolderPath))
            {
                Directory.CreateDirectory(inputFolderPath);
                Directory.CreateDirectory(outputFolderPath);
            }
            this.inputFolderPath = inputFolderPath;
            this.outputFolderPath = outputFolderPath;
        }

        public void HelloWorld(bool dataShow)
        {
            Console.Title = "ImagifyLite";
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(new string('=', 80));
            Console.WriteLine(
                "" +
                "██╗███╗   ███╗ █████╗  ██████╗ ██╗███████╗██╗   ██╗██╗     ██╗████████╗███████╗\n" +
                "██║████╗ ████║██╔══██╗██╔════╝ ██║██╔════╝╚██╗ ██╔╝██║     ██║╚══██╔══╝██╔════╝\n" +
                "██║██╔████╔██║███████║██║  ███╗██║█████╗   ╚████╔╝ ██║     ██║   ██║   █████╗\n" +
                "██║██║╚██╔╝██║██╔══██║██║   ██║██║██╔══╝    ╚██╔╝  ██║     ██║   ██║   ██╔══╝\n" +
                "██║██║ ╚═╝ ██║██║  ██║╚██████╔╝██║██║        ██║   ███████╗██║   ██║   ███████╗\n" +
                "╚═╝╚═╝     ╚═╝╚═╝  ╚═╝ ╚═════╝ ╚═╝╚═╝        ╚═╝   ╚══════╝╚═╝   ╚═╝   ╚══════╝");
            Console.WriteLine(new string('=', 80));
            if (dataShow)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(
                    ">Author:\tGMURAD97\n" +
                    ">Version:\t0.2.1"
                    );
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(new string('=', 80));
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void ProcessImage(string inputImagePath, int quality)
        {
            try
            {
                using (MagickImage magickImage = new MagickImage(inputImagePath))
                {
                    magickImage.Quality = quality;
                    magickImage.Write(Path.Combine(outputFolderPath, Path.GetFileName(inputImagePath)));
                }
            }
            catch
            {
                Console.WriteLine("Error!This is no image!" + Path.GetFileName(inputImagePath));
            }
        }
    }
}