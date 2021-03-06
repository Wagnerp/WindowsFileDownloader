﻿using HtmlAgilityPack;
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

        public string GENERIC_ERR = "some_generic_error";
        public Form1()
        {
            InitializeComponent();
        }

        private Queue<string> _downloadUrls = new Queue<string>();

        private string mFilesSavePath = "";

        private bool downloadClicked = false;
        private bool newSite = true;

        private async void init_download_Click(object sender, EventArgs e)
        {
            String pageUrl = page_url.Text;
            if (pageUrl == "") pageUrl = "http://gurinderhans.me/";

            if (!downloadClicked)
            {
                //(maybe a default save path - Desktop unless user clickes download while holding some key to change the path
                //then this opens) --- LATER
                var result = choose_folder.ShowDialog();
                // OK button was pressed. 
                if (result == DialogResult.OK)
                {
                    mFilesSavePath = choose_folder.SelectedPath;

                    init_download.Text = "Scanning Site";
                    var doc = await DownloadPageAsync(pageUrl);
                    init_download.Text = "Loaded";
                    allUrlsListBox.BeginUpdate();

                    try
                    {
                        foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                        {
                            String fileUrl = fixUrl(pageUrl, link.Attributes["href"].Value);
                            if (fileUrl != GENERIC_ERR)
                            {
                                allUrlsListBox.Items.Add(fileUrl);//add to done box
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
                        Console.WriteLine("Parsing Error");
                    }
                    allUrlsListBox.EndUpdate();
                    DownloadFile();
                    downloadClicked = true;
                    newSite = false;
                }
                else
                {
                    //tell user to pick folder again
                    MessageBox.Show("Please choose a folder where you want to save the files.");
                }
            }
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
                if (newSite) current_download.Text = "Loading";
                //here if scanning new site then set current_download.Text = "Loading";
                var contentType = await fileType(thisUrl); //NOTE: This line can take time
                if (contentType.Contains(file_type.Text))// check content type here
                {
                    WebClient client = new WebClient();
                    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

                    current_download.Text = "Downloading: " + thisUrl;

                    // Starts the download
                    //we dont need to check for path null because only way we get here is if we have a path
                    if (file_type.Text.Contains("htm"))
                    {
                        client.DownloadFileAsync(new Uri(thisUrl), @"" + mFilesSavePath + "\\" + fileName + "."+file_type.Text);
                    }
                    else client.DownloadFileAsync(new Uri(thisUrl), @"" + mFilesSavePath + "\\" + fileName + "");
                }
                else DownloadFile();

            }
            else
            {
                current_download.Text = "Download done";
                init_download.Text = "Download";//also set this button to work again when we disable it
                download_progress.Value = 100;
                downloadClicked = false;
                newSite = true;
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
            string url = "";
            try
            {
                if (relativeUrl.Contains("http"))
                {
                    url = relativeUrl;
                }
                else
                {
                    //filter stuff such as #id links and mail links
                    if (relativeUrl[0] == '#' || relativeUrl[0] == '@') url = GENERIC_ERR;
                    else
                    {
                        // Create an absolute Uri from a string.
                        Uri absoluteUri = new Uri(mainurl);

                        // Create a relative Uri from a string.  allowRelative = true to allow for  
                        // creating a relative Uri.
                        Uri relativeUri = new Uri(relativeUrl, UriKind.Relative);

                        // Create a new Uri from an absolute Uri and a relative Uri.
                        Uri combinedUri = new Uri(absoluteUri, relativeUri);
                        url = combinedUri.ToString();
                    }
                }

                url = System.Net.WebUtility.HtmlDecode(url);

                //take out the link itself and to do that
                //we bring them on the same level, then compare
                if (new Uri(url) == new Uri(mainurl)) url = GENERIC_ERR;
            }
            catch
            {
                url = GENERIC_ERR;
            }

            if (!file_type.Text.Contains("htm"))//check for html pages explicitly
            {
                if (url.Contains(".htm")) url = GENERIC_ERR;
            }

            //deep scan decision here
            if(deep_scan.CheckState != CheckState.Checked)
            {
                string type = file_type.Text;
                //only get the ones with the the filetype extension
                if (type == "image")
                {
                    string[] imageFileTypes = new string[] { ".bmp", ".gif", ".jpg", ".jpeg", ".png", ".psd", ".pspimage", ".thm", ".tif", ".yuv" };
                    bool contains = false;
                    foreach(string e_type in imageFileTypes){
                        if (url.Contains(e_type))
                        {
                            contains = true;
                            break;
                        }
                    }
                    if (!contains) url = GENERIC_ERR;
                }
                else  if (!url.Contains(type)) url = GENERIC_ERR;
            }
            return url;
        }

        private void allUrlsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(allUrlsListBox.GetItemText(allUrlsListBox.SelectedItem));//check for nullity
        }

        private void deep_scan_CheckedChanged(object sender, EventArgs e)
        {
            if (deep_scan.CheckState == CheckState.Checked)
            {
                MessageBox.Show("Only enable if you think not everything is being downloaded");
            }
        }
    }
}