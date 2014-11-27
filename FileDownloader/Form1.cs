using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileDownloader
{
    public partial class Form1 : Form
    {

        public string GENERIC_ERR = "network_error";
        public Form1()
        {
            InitializeComponent();
        }

        private Queue<string> _downloadUrls = new Queue<string>();

        private async void init_download_Click(object sender, EventArgs e)
        {
            String pageUrl = page_url.Text;
            if (pageUrl == "") pageUrl = "http://gurinderhans.me/";

            init_download.Text = "Scanning Site";
            var doc = await DownloadPageAsync(pageUrl);
            init_download.Text = "Loaded";

            try
            {
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    String fileUrl = fixUrl(pageUrl, link.Attributes["href"].Value);
                    if (fileUrl != GENERIC_ERR)
                    {
                        allUrlsListBox.Items.Add(link.Attributes["href"].Value);//add to done box
                        _downloadUrls.Enqueue(fileUrl);//add all urls to queue
                    }
                }
                //images in progress
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//img[@src]"))
                {
                    String fileUrl = fixUrl(pageUrl, link.Attributes["src"].Value);
                    if (fileUrl != GENERIC_ERR)
                    {
                        allUrlsListBox.Items.Add(fileUrl);
                        _downloadUrls.Enqueue(fileUrl);//add all urls to queue
                    }
                }
            }
            catch
            {
                //MessageBox.Show("Parsing Exception " + ex.Message);
            }
            DownloadFile();
        }

        private async Task<HtmlAgilityPack.HtmlDocument> DownloadPageAsync(string url)
        {
            string result;
            // ... Use HttpClient.
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(url))
            using (HttpContent content = response.Content)
            {
                // ... Read the string.
                result = await content.ReadAsStringAsync();
            }
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(result);
            return doc;
        }

        private async Task<string> fileType(string url)
        {
            try
            {
                // Create a 'WebRequest' with the specified url.
                WebRequest myWebRequest = WebRequest.Create(url);
                myWebRequest.Method = "HEAD";
                // Send the 'WebRequest' and wait for response.
                WebResponse myWebResponse = await myWebRequest.GetResponseAsync();
                myWebResponse.Close();
                return myWebResponse.ContentType;
            }
            catch
            {
                return GENERIC_ERR;
            }
        }

        private async void DownloadFile()
        {
            if (_downloadUrls.Any())
            {
                var thisUrl = _downloadUrls.Dequeue();

                String fileName = Path.GetFileName(thisUrl);

                download_progress.Value = 10;
                //here if scanning new site then set current_download.Text == "Loading";
                var contentType = await fileType(thisUrl); //NOTE: This line can take 
                if (contentType.Contains(file_type.Text))// check content type here
                {
                    WebClient client = new WebClient();
                    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

                    current_download.Text = "Downloading: " + thisUrl;

                    // Starts the download
                    client.DownloadFileAsync(new Uri(thisUrl), @"C:\Users\ghans\Desktop\downloads\" + fileName + "");
                }
                else DownloadFile();

            }
            else
            {
                current_download.Text = "Download done";
                init_download.Text = "Download";//also set this button to work again when we disable it
            } 
            // else all downloads are finished
            // End of the download
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;

            int progressVal = int.Parse(Math.Truncate(percentage).ToString());
            
            //limiting b/w min & max as sometimes progress value is very largely negative
            if (progressVal < 0) progressVal = 0;
            if (progressVal > 100) progressVal = 100;

            download_progress.Value = progressVal;
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            DownloadFile(); //start the next
        }

        private string fixUrl(string mainurl, string relativeUrl)
        {
            try
            {
                if (relativeUrl.Contains("http")) return relativeUrl;
                else
                {
                    // Create an absolute Uri from a string.
                    Uri absoluteUri = new Uri(mainurl);

                    // Create a relative Uri from a string.  allowRelative = true to allow for  
                    // creating a relative Uri.
                    Uri relativeUri = new Uri(relativeUrl, UriKind.Relative);

                    // Create a new Uri from an absolute Uri and a relative Uri.
                    Uri combinedUri = new Uri(absoluteUri, relativeUri);
                    return combinedUri.ToString();
                }
            }
            catch
            {
                return GENERIC_ERR;
            }
        }
    }
}