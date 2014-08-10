// Type: screenspin.Form2
// Assembly: screenspin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4351C7F0-BD04-4B3C-89D5-7A8D50305D31
// Assembly location: C:\QuickLoL Login Screen.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace screenspin
{
  public class Form2 : Form
  {
    private string baselink;
    private string savePath;
    private string workingon;
    private IContainer components;
    private ProgressBar progressBar1;
    private Label label1;

    public Form2(string baselink, string savePath)
    {
      this.InitializeComponent();
      this.baselink = baselink;
      this.savePath = savePath;
      this.download();
    }

    private void Form2_Load(object sender, EventArgs e)
    {
    }

    private void download()
    {
      this.workingon = "Music";
      WebClient webClient = new WebClient();
      webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.Completed);
      webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.ProgressChanged);
      webClient.DownloadFileAsync(new Uri(this.baselink + "/music.mp3"), this.savePath + "/music/LoginScreenLoop.mp3");
    }

    private void download2()
    {
      this.workingon = "Animation";
      WebClient webClient = new WebClient();
      webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.Completed2);
      webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.ProgressChanged);
      webClient.DownloadFileAsync(new Uri(this.baselink + "/login.swf"), this.savePath + "/cs_bg_champions.png");
    }

    private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
      this.progressBar1.Value = e.ProgressPercentage;
      this.label1.Text = this.workingon + (object) " - " + (string) (object) e.BytesReceived + " bytes out of " + (string) (object) e.TotalBytesToReceive + " bytes downloaded.";
    }

    private void Completed(object sender, AsyncCompletedEventArgs e)
    {
      System.IO.File.Copy(this.savePath + "/music/LoginScreenLoop.mp3", this.savePath + "/music/LoginScreenIntro.mp3", true);
      this.download2();
    }

    private void Completed2(object sender, AsyncCompletedEventArgs e)
    {
      System.IO.File.Copy(this.savePath + "/cs_bg_champions.png", this.savePath + "/login.swf", true);
      this.Hide();
      int num = (int) MessageBox.Show("Download completed." + Environment.NewLine + Environment.NewLine + "To enable music, click on \"Disable Menu Animations\".", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Form2));
      this.progressBar1 = new ProgressBar();
      this.label1 = new Label();
      this.SuspendLayout();
      this.progressBar1.Location = new Point(12, 12);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new Size(359, 23);
      this.progressBar1.TabIndex = 0;
      this.label1.Dock = DockStyle.Bottom;
      this.label1.Location = new Point(6, 41);
      this.label1.Margin = new Padding(6);
      this.label1.Name = "label1";
      this.label1.Size = new Size(368, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "0 bytes out of 0 bytes downloaded.";
      this.label1.TextAlign = ContentAlignment.BottomCenter;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(380, 60);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.progressBar1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = "Form2";
      this.Padding = new Padding(6);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Download";
      this.Load += new EventHandler(this.Form2_Load);
      this.ResumeLayout(false);
    }
  }
}
