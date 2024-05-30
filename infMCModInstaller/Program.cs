using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using AltoHttp;
using System.Text.RegularExpressions;
using AppKit;

namespace infMCModInstaller
{
    class Program
    {
        public static void Main(string[] args)
        {
            HttpDownloader httpDownloader;
            
            string downloadTargetDirectory = string.Empty;

            // var nsop = new NSOpenPanel();
            // nsop.Title = "请选择你要安装至的模组文件夹目录";
            // nsop.CanChooseFiles = false;
            // nsop.CanChooseDirectories = true;
            // nsop.AllowsMultipleSelection = false;
            //
            // if (nsop.RunModal() == (long)NSModalResponse.OK && nsop.Urls.Length > 0)
            // {
            //     downloadTargetDirectory = nsop.Urls[0].Path;
            // }
            // else
            // {
            //     Console.WriteLine("No valid directory selected");
            // }
            
            Console.WriteLine("请输入你要安装至的模组文件夹目录");
            downloadTargetDirectory = Console.ReadLine();
            // Test is folder exists
            if (!Directory.Exists(downloadTargetDirectory))
            {
                Console.WriteLine("请输入一个有效的文件夹！");
                return;
            }
            
            
            // mUrl-For Modrinth link   cUrl-For Curseforge link //
            
            // Modrinth
            // Get file base on id
            // string id = "xYL9nIiR";
            // string url = $"https://api.modrinth.com/v2/version/{id}";
            
            // Get file base on hash
            // string hash = "99658ba320539c41e4c4c4de5f2e8c1f40985bff";
            // string mUrl = $"https://api.modrinth.com/v2/version_file/{hash}";

            string modId = "59621";
            string fileId = "3990329";
            
            string cUrl = $"https://api.curseforge.com/v1/mods/{modId}/files/{fileId}";
            string cAuthKey = "$2a$10$Kd.G.ZYJVXBDaDjX27Z.GumBfck7sldsODAe8utmnap8i00VBu3dS";
            string downloadLink = "";

            var client = new RestClient(cUrl);
            var request = new RestRequest();
            
            // request.AddHeader("User-Agent","InfinitumC/infMCModInstaller/1.56.0 (2592415035@qq.com)");
            // request.AddQueryParameter("algorithm", "sha1");
            request.AddHeader("x-api-key", cAuthKey);
            
            string response = client.Get(request).Content;
            // string pattern = "\"url\":\"(?<url>[^\"]+)\"";
            string pattern = "\"downloadUrl\":\"(?<url>[^\"]+)\"";
            
            Match match = Regex.Match(response, pattern);
            if (match.Success)
            {
                downloadLink = match.Groups["url"].Value;
                Console.WriteLine(downloadLink);
            }
            
            httpDownloader = new HttpDownloader(downloadLink, $"{downloadTargetDirectory}/{Path.GetFileName(downloadLink)}");
            httpDownloader.Start();
            
            Console.WriteLine("安装完毕! ");
        }
    }
}