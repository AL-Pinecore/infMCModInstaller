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
            if (!Directory.Exists(downloadTargetDirectory))
            {
                Console.WriteLine("请输入一个有效的文件夹！");
                return;
            }
            
            
            // For Modrinth
            string id = "M7yKwUqa";
            string url = $"https://api.modrinth.com/v2/version/{id}";
            string downloadLink = "";

            var client = new RestClient(url);
            var request = new RestRequest();

            request.AddHeader("User-Agent","InfinitumC/infMCModInstaller/1.56.0 (2592415035@qq.com)");
            
            string response = client.Get(request).Content;
            string pattern = "\"url\":\"(?<url>[^\"]+)\"";
            
            Match match = Regex.Match(response, pattern);
            if (match.Success)
            {
                downloadLink = match.Groups["url"].Value;
            }
            
            httpDownloader = new HttpDownloader(downloadLink, $"{downloadTargetDirectory}/{Path.GetFileName(downloadLink)}");
            httpDownloader.Start();
            
            Console.WriteLine("安装完毕!");
        }
    }
}