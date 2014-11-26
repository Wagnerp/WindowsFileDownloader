namespace FileDownloader
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.url = new System.Windows.Forms.TextBox();
            this.download_files = new System.Windows.Forms.Button();
            this.urlsListBox = new System.Windows.Forms.ListBox();
            this.downloadProgress = new System.Windows.Forms.ProgressBar();
            this.currentUrlDownloading = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // url
            // 
            this.url.Location = new System.Drawing.Point(12, 12);
            this.url.Name = "url";
            this.url.Size = new System.Drawing.Size(168, 20);
            this.url.TabIndex = 0;
            // 
            // download_files
            // 
            this.download_files.Location = new System.Drawing.Point(197, 9);
            this.download_files.Name = "download_files";
            this.download_files.Size = new System.Drawing.Size(75, 23);
            this.download_files.TabIndex = 1;
            this.download_files.Text = "Download";
            this.download_files.UseVisualStyleBackColor = true;
            this.download_files.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.urlsListBox.FormattingEnabled = true;
            this.urlsListBox.Location = new System.Drawing.Point(12, 38);
            this.urlsListBox.Name = "listBox1";
            this.urlsListBox.Size = new System.Drawing.Size(516, 290);
            this.urlsListBox.TabIndex = 2;
            // 
            // progressBar1
            // 
            this.downloadProgress.Location = new System.Drawing.Point(12, 383);
            this.downloadProgress.Name = "progressBar1";
            this.downloadProgress.Size = new System.Drawing.Size(516, 23);
            this.downloadProgress.TabIndex = 3;
            // 
            // label1
            // 
            this.currentUrlDownloading.AutoSize = true;
            this.currentUrlDownloading.Location = new System.Drawing.Point(12, 353);
            this.currentUrlDownloading.Name = "label1";
            this.currentUrlDownloading.Size = new System.Drawing.Size(35, 13);
            this.currentUrlDownloading.TabIndex = 4;
            this.currentUrlDownloading.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 430);
            this.Controls.Add(this.currentUrlDownloading);
            this.Controls.Add(this.downloadProgress);
            this.Controls.Add(this.urlsListBox);
            this.Controls.Add(this.download_files);
            this.Controls.Add(this.url);
            this.Name = "Form1";
            this.Text = "File Downloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox url;
        private System.Windows.Forms.Button download_files;
        private System.Windows.Forms.ListBox urlsListBox;
        private System.Windows.Forms.ProgressBar downloadProgress;
        private System.Windows.Forms.Label currentUrlDownloading;
    }
}

