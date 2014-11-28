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
            this.file_type = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.choose_folder = new System.Windows.Forms.FolderBrowserDialog();
            this.deep_scan = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // page_url
            // 
            this.page_url.Location = new System.Drawing.Point(42, 12);
            this.page_url.Name = "page_url";
            this.page_url.Size = new System.Drawing.Size(220, 20);
            this.page_url.TabIndex = 0;
            // 
            // init_download
            // 
            this.init_download.Location = new System.Drawing.Point(358, 10);
            this.init_download.Name = "init_download";
            this.init_download.Size = new System.Drawing.Size(75, 23);
            this.init_download.TabIndex = 2;
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
            this.allUrlsListBox.TabIndex = 3;
            this.allUrlsListBox.SelectedIndexChanged += new System.EventHandler(this.allUrlsListBox_SelectedIndexChanged);
            // 
            // download_progress
            // 
            this.download_progress.Location = new System.Drawing.Point(12, 368);
            this.download_progress.Name = "download_progress";
            this.download_progress.Size = new System.Drawing.Size(516, 23);
            this.download_progress.TabIndex = 4;
            // 
            // current_download
            // 
            this.current_download.AutoSize = true;
            this.current_download.Location = new System.Drawing.Point(9, 342);
            this.current_download.Name = "current_download";
            this.current_download.Size = new System.Drawing.Size(54, 13);
            this.current_download.TabIndex = 5;
            this.current_download.Text = "Loading...";
            // 
            // file_type
            // 
            this.file_type.Location = new System.Drawing.Point(298, 12);
            this.file_type.Name = "file_type";
            this.file_type.Size = new System.Drawing.Size(54, 20);
            this.file_type.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(268, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "ext:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Url:";
            // 
            // deep_scan
            // 
            this.deep_scan.AutoSize = true;
            this.deep_scan.Location = new System.Drawing.Point(448, 14);
            this.deep_scan.Name = "deep_scan";
            this.deep_scan.Size = new System.Drawing.Size(80, 17);
            this.deep_scan.TabIndex = 8;
            this.deep_scan.Text = "Deep Scan";
            this.deep_scan.UseVisualStyleBackColor = true;
            this.deep_scan.CheckedChanged += new System.EventHandler(this.deep_scan_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 405);
            this.Controls.Add(this.deep_scan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.file_type);
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
        private System.Windows.Forms.TextBox file_type;
        private System.Windows.Forms.Button init_download;
        private System.Windows.Forms.ListBox allUrlsListBox;
        private System.Windows.Forms.ProgressBar download_progress;
        private System.Windows.Forms.Label current_download;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog choose_folder;
        private System.Windows.Forms.CheckBox deep_scan;
    }
}

