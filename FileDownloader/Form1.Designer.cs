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
            this.page_url = new System.Windows.Forms.TextBox();
            this.init_download = new System.Windows.Forms.Button();
            this.allUrlsListBox = new System.Windows.Forms.ListBox();
            this.download_progress = new System.Windows.Forms.ProgressBar();
            this.current_download = new System.Windows.Forms.Label();
            this.progress_num = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // page_url
            // 
            this.page_url.Location = new System.Drawing.Point(12, 12);
            this.page_url.Name = "page_url";
            this.page_url.Size = new System.Drawing.Size(168, 20);
            this.page_url.TabIndex = 0;
            // 
            // init_download
            // 
            this.init_download.Location = new System.Drawing.Point(186, 9);
            this.init_download.Name = "init_download";
            this.init_download.Size = new System.Drawing.Size(75, 23);
            this.init_download.TabIndex = 1;
            this.init_download.Text = "Download";
            this.init_download.UseVisualStyleBackColor = true;
            this.init_download.Click += new System.EventHandler(this.init_download_Click);
            // 
            // allUrlsListBox
            // 
            this.allUrlsListBox.FormattingEnabled = true;
            this.allUrlsListBox.Location = new System.Drawing.Point(12, 38);
            this.allUrlsListBox.Name = "allUrlsListBox";
            this.allUrlsListBox.Size = new System.Drawing.Size(516, 290);
            this.allUrlsListBox.TabIndex = 2;
            // 
            // download_progress
            // 
            this.download_progress.Location = new System.Drawing.Point(12, 383);
            this.download_progress.Name = "download_progress";
            this.download_progress.Size = new System.Drawing.Size(516, 23);
            this.download_progress.TabIndex = 3;
            // 
            // current_download
            // 
            this.current_download.AutoSize = true;
            this.current_download.Location = new System.Drawing.Point(9, 352);
            this.current_download.Name = "current_download";
            this.current_download.Size = new System.Drawing.Size(54, 13);
            this.current_download.TabIndex = 4;
            this.current_download.Text = "Loading...";
            // 
            // progress_num
            // 
            this.progress_num.AutoSize = true;
            this.progress_num.Location = new System.Drawing.Point(493, 352);
            this.progress_num.Name = "progress_num";
            this.progress_num.Size = new System.Drawing.Size(24, 13);
            this.progress_num.TabIndex = 5;
            this.progress_num.Text = "0 %";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 430);
            this.Controls.Add(this.progress_num);
            this.Controls.Add(this.current_download);
            this.Controls.Add(this.download_progress);
            this.Controls.Add(this.allUrlsListBox);
            this.Controls.Add(this.init_download);
            this.Controls.Add(this.page_url);
            this.Name = "Form1";
            this.Text = "File Downloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox page_url;
        private System.Windows.Forms.Button init_download;
        private System.Windows.Forms.ListBox allUrlsListBox;
        private System.Windows.Forms.ProgressBar download_progress;
        private System.Windows.Forms.Label current_download;
        private System.Windows.Forms.Label progress_num;
    }
}

