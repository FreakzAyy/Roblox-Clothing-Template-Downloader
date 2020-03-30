using System;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace Roblox_Clothing_Template_Downloader
{
    class Program
    { 
        // http://assetgame.roblox.com/Asset/?id=
        static void Main(string[] args)
        {
            Start:
            Console.Title = "Roblox Clothing Template Downloader / By: Freakz";
            Console.WriteLine("Clothing ID: ");
            string ID = Console.ReadLine();
            WebClient webClient = new WebClient();
            webClient.DownloadFile("http://assetgame.roblox.com/Asset/?id=" + ID, @"ToConvert\dl");
            Console.WriteLine(webClient.Headers.ToString());
            
            string fileContents = File.ReadAllText(@"ToConvert\dl");
            if (fileContents.Contains("ShirtGraphic"))
            {
                Console.WriteLine("\nDetected Asset Type: T-Shirt");
            }
            else if(fileContents.Contains("Pants"))
            {
                Console.WriteLine("\nDetected Asset Type: Pants");
            }
            else if (fileContents.Contains("Shirt"))
            {
                Console.WriteLine("\nDetected Asset Type: Shirt");
            }
            else
            {
                Console.WriteLine("\nDetected Asset Type: Unknown");
            }
            Console.WriteLine("\nFinding Asset URL...");
            Regex regex = new Regex(@"<url>(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})</url>");
            Match match = regex.Match(fileContents);
            if (match.Success)
            {
                Console.WriteLine("\nAsset URL Found! Downloading Template...");
                webClient.DownloadFile(match.Value.ToString().Replace("<url>", null).Replace("</url>", null), @"Finished\Template" + DateTime.Now.ToString().Replace(":", "-") + ".png");
                Console.WriteLine("\n\nFinished Downloading! Saved in the Finished directory.\nPress ENTER to Restart");
                File.Delete(@"ToConvert\dl");
                Console.ReadLine();
                Console.Clear();
                goto Start;
            }
            else
            {
                Console.WriteLine("\nAsset URL NOT Found! :(");
                System.Threading.Thread.Sleep(3000);
            }
        }
    }
}
