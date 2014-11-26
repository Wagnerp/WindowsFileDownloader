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

        private Queue<string> _downloadUrls = new Queue<string>();

        private void init_download_Click(object sender, EventArgs e)
        {
            String pageUrl = page_url.Text;
            if (pageUrl == "") pageUrl = "http://www.cs.sfu.ca/CourseCentral/101.MACM/abulatov/";

            //parse the html
            HtmlWeb hw = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc = hw.Load(pageUrl);

            try
            {
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    String fileUrl = fixUrl(pageUrl, link.Attributes["href"].Value);
                    Console.WriteLine("URL Fixed: " + fileUrl);
                    allUrlsListBox.Items.Add(fileUrl);//add to done box
                    _downloadUrls.Enqueue(fileUrl);//add all urls to queue
                }
                //images in progress
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//img[@src]"))
                {
                    //String fileUrl = fixUrl(site_url, link.Attributes["src"].Value);
                    //urlsListBox.Items.Add(fileUrl);
                    //allDownloadUrls.Enqueue(fileUrl);//add all urls to queue
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Parsing Exception " + ex.Message);
            }
            DownloadFile();
        }

        private void DownloadFile()
        {
            if (_downloadUrls.Any())
            {
                var thisUrl = _downloadUrls.Dequeue();
                String fileName = Path.GetFileName(thisUrl);

                Console.WriteLine("MY CONTENT tYpE: " + fileType(thisUrl));
                string contentType = fileType(thisUrl);
                if (contentType.Contains("pdf"))// check content type here
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
            // End of the download
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;

            download_progress.Value = int.Parse(Math.Truncate(percentage).ToString());
            progress_num.Text = int.Parse(Math.Truncate(percentage).ToString()).ToString()+" %";

        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            DownloadFile(); //start the next
        }

        private string fixUrl(string mainurl, string relativeUrl)
        {
            String return_url = "";
            if (relativeUrl.Contains("http")) return_url = relativeUrl;
            else
            {
                // Create an absolute Uri from a string.
                Uri absoluteUri = new Uri(mainurl);

                // Create a relative Uri from a string.  allowRelative = true to allow for  
                // creating a relative Uri.
                Uri relativeUri = new Uri(relativeUrl, UriKind.Relative);

                // Create a new Uri from an absolute Uri and a relative Uri.
                Uri combinedUri = new Uri(absoluteUri, relativeUri);
                return_url = combinedUri.ToString();
            }
            return return_url;
        }

        private string fileType(string url)
        {
            // Create a 'WebRequest' with the specified url.
            WebRequest myWebRequest = WebRequest.Create(url);
            myWebRequest.Method = "HEAD";
            // Send the 'WebRequest' and wait for response.
            WebResponse myWebResponse = myWebRequest.GetResponse();
            myWebResponse.Close();
            return myWebResponse.ContentType;
        }
    }
}
