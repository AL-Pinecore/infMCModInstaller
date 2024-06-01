using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace infMCModInstaller
{
    class Program
    {
        public static void Main(string[] args)
        {
            string downloadTargetDirectory = string.Empty;
            
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

            string modId = "519860";
            string fileId = "3457539";
            
            string cUrl = $"https://api.curseforge.com/v1/mods/{modId}/files/{fileId}";
            string cAuthKey = "$2a$10$Kd.G.ZYJVXBDaDjX27Z.GumBfck7sldsODAe8utmnap8i00VBu3dS";
            string downloadLink = "";

            var client = new RestClient(cUrl);
            var request = new RestRequest();
            
            // request.AddHeader("User-Agent","InfinitumC/infMCModInstaller/1.56.0 (2592415035@qq.com)");
            // request.AddQueryParameter("algorithm", "sha1");
            request.AddHeader("x-api-key", cAuthKey);
            
            string response = client.Get(request).Content;
            
            // JObject jsonResponse = JObject.Parse(response);
            // downloadLink = jsonResponse["files"][0]["url"].ToString();

            JObject jsonResponse = JObject.Parse(response);
            downloadLink = jsonResponse["data"]["downloadUrl"].ToString();
            // Console.WriteLine(jsonResponse.ToString());

            Console.WriteLine(downloadLink);

            WebClient webClient = new WebClient();
            webClient.Credentials = CredentialCache.DefaultCredentials;
            webClient.DownloadFile(downloadLink, downloadTargetDirectory + "/" + Path.GetFileName(downloadLink));
            
            
            Console.WriteLine("安装完毕! ");
        }
    }
}