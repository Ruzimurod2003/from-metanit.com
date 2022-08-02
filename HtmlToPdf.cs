using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ForTest
{
    public class HtmlToPdf
    {
        public string? Url { get; set; } = "https://metanit.com/sharp/aspnet5/";
        public string? FilePath { get; set; } = @"c:\TEMP\";
        public bool SavePdfCheck(string url, string output)
        {
            bool check = false;
            try
            {
                var chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
                using (var p = new Process())
                {
                    p.StartInfo.FileName = chromePath;
                    p.StartInfo.Arguments = $"--headless --disable-gpu --run-all-compositor-stages-before-draw  --print-to-pdf-no-header --print-to-pdf={output} --no-margins {url}";
                    p.Start();
                    p.WaitForExit();
                }
                check = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                check = false;
            }
            return check;
        }
        public bool checkWebsite(string URL)
        {
            bool check = false;
            try
            {
                var webGet = new HtmlWeb();
                var document = webGet.Load(URL);
                var title = document.DocumentNode.SelectSingleNode("html/head/title").InnerText;
                check = true;
            }
            catch
            {
                check = false;
            }
            return check;
        }
        public void Progression()
        {
            string newUrl = "";
            string directoryPath = "";
            string filePath = "";
            for (int i = 1; i <= 40; i++)
            {
                for (int j = 1; j <= 20; j++)
                {
                    newUrl = Url + "" + i + "." + j + ".php";
                    if (checkWebsite(newUrl))
                    {
                        var directory = GetSubDirectoryName(newUrl);
                        if (directory != "")
                        {
                            directoryPath = FilePath + directory;
                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }
                        }
                        var title = GetTitleUrl(newUrl);
                        if (title != "")
                        {
                            filePath = "\"" + directoryPath + "\\" + title + ".pdf" + "\"";
                            SavePdfCheck(newUrl, filePath);
                        }
                        Console.WriteLine("Progress: " + directory + " " + title);
                        Thread.Sleep(TimeSpan.FromSeconds(3));
                    }
                    else
                    {
                        break;
                    }
                }
            }
            Console.WriteLine("Tamom");
        }
        public string GetTitleUrl(string url)
        {
            try
            {
                var webGet = new HtmlWeb();
                var document = webGet.Load(url);
                HtmlNode sell1 = document.DocumentNode.SelectSingleNode(@"//h2[contains(text(), 'ASP.NET Core - новая эпоха в развитии ASP.NET')]");
                var title = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/h2[1]").InnerText;
                return title;
            }
            catch
            {
                return "";
            }
        }
        public string GetSubDirectoryName(string url)
        {
            try
            {
                var webGet = new HtmlWeb();
                var document = webGet.Load(url);
                HtmlNode sell1 = document.DocumentNode.SelectSingleNode(@"//h1[contains(text(), 'Введение в ASP.NET Core')]");
                var title = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/h1[1]").InnerText;
                return title;
            }
            catch
            {
                return "";
            }
        }

        public void SaveHtmlFileFromUrl(string url)
        {
            var webGet = new HtmlWeb();
            var document = webGet.Load(url);
            HtmlNode sell1 = document.DocumentNode.SelectSingleNode(@"//h2[contains(text(), 'ASP.NET Core - новая эпоха в развитии ASP.NET')]");
            var title = document.DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/h2[1]").InnerText;

            //HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            //myRequest.Method = "GET";
            //WebResponse myResponse = myRequest.GetResponse();
            //StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            //string result = sr.ReadToEnd();
            //sr.Close();
            //myResponse.Close();

            //WebClient myClient = new WebClient();
            //string myPageHTML = null;
            //byte[] requestHTML;
            //// Gets the url of the page
            //string currentPageUrl = Request.Url.ToString();

            //UTF8Encoding utf8 = new UTF8Encoding();

            //// by setting currentPageUrl to url it will fetch the source (html) 
            //// of the url and put it in the myPageHTML variable. 

            //// currentPageUrl = "url"; 

            //requestHTML = myClient.DownloadData(currentPageUrl);

            //myPageHTML = utf8.GetString(requestHTML);

            //System.IO.File.WriteAllText(@"C:\yoursite.html", myPageHTML);
        }
    }
}