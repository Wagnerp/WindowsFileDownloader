using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileDownloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Queue<string> allDownloadUrls = new Queue<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            String downloadUrl = url.Text; //downloadUrl = "http://www.cs.sfu.ca/CourseCentral/101.MACM/abulatov";

            //parse the html
            HtmlWeb hw = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc = hw.Load(downloadUrl);

            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                String web_url = link.Attributes["href"].Value;
                if (web_url.Contains("pdf"))
                {
                    urlsListBox.Items.Add(web_url);//add to done box
                    allDownloadUrls.Enqueue(web_url);//add all urls to queue
                }
            }
            //images in progress
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//img[@src]"))
            {
                String web_url = link.Attributes["src"].Value;
                urlsListBox.Items.Add(web_url);
                if (web_url.Contains("pdf"))
                {
                    //urlsListBox.Items.Add(web_url);//add to done box
                    //allDownloadUrls.Enqueue(web_url);//add all urls to queue
                }
            }
            DownloadFile();
        }

        private void DownloadFile()
        {
            if (allDownloadUrls.Any())
            {
                WebClient client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

                var thisUrl = allDownloadUrls.Dequeue();
                String fileName = Path.GetFileName(thisUrl);

                currentUrlDownloading.Text = "Downloading: "+thisUrl;

                // Starts the download
                client.DownloadFileAsync(new Uri(thisUrl), @"C:\Users\ghans\Documents\Visual Studio 2013\Projects\FileDownloader\FileDownloader\bin\Debug\downloads\"+fileName+"");
            }
            // End of the download
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;

            downloadProgress.Value = int.Parse(Math.Truncate(percentage).ToString());
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //start the next
            DownloadFile();
        }
    }
}
