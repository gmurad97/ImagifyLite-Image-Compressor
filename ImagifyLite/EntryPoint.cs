using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using ImageMagick;

namespace ImagifyLite
{
    internal class EntryPoint
    {
        private static List<Task> listTasks = new List<Task>();

        private static string inputFolderName = "Input";
        private static string outputFolderName = "Output";

        private static int allFilesCount = 0;
        private static int goodFilesCount = 0;
        private static int badFilesCount = 0;

        private static double percentageExecution = 0;

        static async Task Main()
        {
            Console.Title =
                $"ImagifyLite - " +
                $"[Total:{(goodFilesCount + badFilesCount)}/{allFilesCount} | Progress:{percentageExecution:F2}%]" +
                $"[Good:{goodFilesCount}]" +
                $"[Bad:{badFilesCount}]";
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
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(
                "[>]Author:\tgmurad97\n" +
                "[>]Version:\t0.3.2"
                );
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(new string('=', 80));

            if (!Directory.Exists(inputFolderName) || !Directory.Exists(outputFolderName))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Directory.CreateDirectory(inputFolderName);
                Directory.CreateDirectory(outputFolderName);
                Console.WriteLine("[>]The folders have been successfully created. Please add images to the \"Input\" folder and restart the program.");
            }

            string[] filesPathArray = Directory.GetFiles(inputFolderName);
            allFilesCount = filesPathArray.Length;

            foreach (string filePath in filesPathArray)
            {
                listTasks.Add(ProcessImageAsync(filePath));
            }

            await Task.WhenAll(listTasks);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("[>]All images have been successfully processed.");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("[>]To exit the program, press the 'ESC'.");
            while (true)
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    Environment.Exit(0);
        }

        static async Task ProcessImageAsync(string imagePath)
        {
            try
            {
                using (MagickImage magickImage = new MagickImage(imagePath))
                {
                    magickImage.Quality = 50;
                    await magickImage.WriteAsync(Path.Combine(outputFolderName, Path.GetFileName(imagePath)));
                }
                Interlocked.Increment(ref goodFilesCount);
                percentageExecution = ((double)(goodFilesCount + badFilesCount) / allFilesCount) * 100;
                Console.Title =
                    $"ImagifyLite - " +
                    $"[Total:{(goodFilesCount + badFilesCount)}/{allFilesCount} | Progress:{percentageExecution:F2}%]" +
                    $"[Good:{goodFilesCount}]" +
                    $"[Bad:{badFilesCount}]";
            }
            catch
            {
                Interlocked.Increment(ref badFilesCount);
                percentageExecution = ((double)(goodFilesCount + badFilesCount) / allFilesCount) * 100;
                Console.Title =
                    $"ImagifyLite - " +
                    $"[Total:{(goodFilesCount + badFilesCount)}/{allFilesCount} | Progress:{percentageExecution:F2}%]" +
                    $"[Good:{goodFilesCount}]" +
                    $"[Bad:{badFilesCount}]";
            }
        }
    }
}