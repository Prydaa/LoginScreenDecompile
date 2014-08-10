// Type: screenspin.Form1
// Assembly: screenspin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4351C7F0-BD04-4B3C-89D5-7A8D50305D31
// Assembly location: C:\QuickLoL Login Screen.exe

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using screenspin.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace screenspin
{
  public class Form1 : Form
  {
    private string baseurl = "http://quicklol.net/";
    private string current = "loginBraum";
    private static bool didCancel;
    private static bool Garena;
    private string json;
    private string fullPath;
    private IContainer components;
    private Button button1;
    private Label label1;
    private TableLayoutPanel tableLayoutPanel1;
    private Panel panel1;
    private LinkLabel linkLabel1;
    private Label label2;
    private GroupBox groupBox1;
    private GroupBox groupBox2;
    private ComboBox comboBox1;
    private SerialPort serialPort1;
    private PictureBox pictureBox1;
    private Label label3;

    static Form1()
    {
    }

    public Form1()
    {
      this.InitializeComponent();
      Dictionary<string, Assembly> loaded = new Dictionary<string, Assembly>();
      AppDomain.CurrentDomain.AssemblyResolve += (ResolveEventHandler) ((sender, args) =>
      {
        string local_1_1 = (args.Name.Contains(",") ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "")).Replace(".", "_");
        Assembly local_0;
        if (!loaded.ContainsKey(local_1_1))
        {
          if (local_1_1.EndsWith("_resources"))
            return (Assembly) null;
          local_0 = Assembly.Load((byte[]) new ResourceManager(this.GetType().Namespace + ".Properties.Resources", Assembly.GetExecutingAssembly()).GetObject(local_1_1));
          loaded.Add(local_1_1, local_0);
        }
        else
          local_0 = loaded[local_1_1];
        return local_0;
      });
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      try
      {
        FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
        if (Settings.Default.LeaguePath == null || Settings.Default.LeaguePath == "")
        {
          int num = (int) MessageBox.Show("We can't find your League installation.\nYou'll need to show us where it is.", "Initial Setup", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
          Form1.RequestPath();
        }
        else if (System.IO.File.Exists(Settings.Default.LeaguePath + "\\lol.launcher.exe"))
          Form1.Garena = false;
        else if (System.IO.File.Exists(Settings.Default.LeaguePath + "\\lol.exe"))
        {
          Form1.Garena = true;
        }
        else
        {
          int num = (int) MessageBox.Show("We can't find your League installation.\nYou'll need to show us where it is.", "Initial Setup", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
          Form1.RequestPath();
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Something went wrong.", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
      try
      {
        this.json = new WebClient().DownloadString(this.baseurl + "loginscreens.json");
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Couldn't load login screens data from quicklol.", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        Process.Start("http://quicklol.net/contact");
        Application.Exit();
      }
      JObject jobject = (JObject) JsonConvert.DeserializeObject(this.json);
      if (((object) jobject.get_Item("message")).ToString() != "")
      {
        int num1 = (int) MessageBox.Show(((object) jobject.get_Item("message")).ToString(), "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      if (((object) jobject.get_Item("url")).ToString() != "")
        Process.Start(((object) jobject.get_Item("url")).ToString());
      using (IEnumerator<JToken> enumerator = ((IEnumerable<JToken>) jobject.get_Item("data")).GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
          this.comboBox1.Items.Add((object) ((JObject) enumerator.Current).get_Item("name"));
      }
      this.current = ((object) jobject.get_Item("latest")).ToString();
      try
      {
        DateTime dateTime = new DateTime(1900, 1, 1);
        string str = (string) null;
        foreach (string path in Directory.GetDirectories(Settings.Default.LeaguePath + "/RADS/projects/lol_air_client/releases"))
        {
          DateTime lastWriteTime = new DirectoryInfo(path).LastWriteTime;
          if (lastWriteTime > dateTime)
          {
            str = path;
            dateTime = lastWriteTime;
          }
        }
        this.fullPath = str + "/deploy/mod/lgn/themes";
      }
      catch (Exception ex)
      {
        int num2 = (int) MessageBox.Show("Unable to locate League of Legends. Application will exit now.", "Can't Find League", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        Application.Exit();
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (this.comboBox1.Text != "")
      {
        if (!Directory.Exists(this.fullPath + "/" + this.current + "_backup"))
        {
          Form1.DirectoryCopy(this.fullPath + "/" + this.current, this.fullPath + "/" + this.current + "_backup", true);
          ((Control) new Form2(this.getLsPath(this.comboBox1.Text), this.fullPath + "/" + this.current)).Show();
        }
        else
          ((Control) new Form2(this.getLsPath(this.comboBox1.Text), this.fullPath + "/" + this.current)).Show();
      }
      else
      {
        int num = (int) MessageBox.Show("You must select login screen.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("http://quicklol.net/");
    }

    private string getLsPath(string name)
    {
      using (IEnumerator<JToken> enumerator = ((IEnumerable<JToken>) ((JObject) JsonConvert.DeserializeObject(this.json)).get_Item("data")).GetEnumerator())
      {
        while (((IEnumerator) enumerator).MoveNext())
        {
          JObject jobject = (JObject) enumerator.Current;
          if (((object) jobject.get_Item("name")).ToString() == name)
            return this.baseurl + ((object) jobject.get_Item("path")).ToString();
        }
      }
      return this.baseurl + "data";
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        using (WebResponse response = WebRequest.Create(this.getLsPath(this.comboBox1.Text) + "/preview.jpg").GetResponse())
        {
          using (Stream responseStream = response.GetResponseStream())
          {
            this.pictureBox1.BackgroundImage = Image.FromStream(responseStream);
            this.label3.Hide();
          }
        }
      }
      catch (Exception ex)
      {
        this.label3.Text = "Failed";
        int num = (int) MessageBox.Show("Couldn't load preview for " + this.comboBox1.Text + ".", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private static void RequestPath()
    {
      bool flag = true;
      Form form = new Form();
      ((Control) form).Show();
      form.Hide();
      while (flag)
      {
        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        folderBrowserDialog.ShowNewFolderButton = false;
        folderBrowserDialog.Description = "Please select the path to your League installation. e.g.\nC:\\Riot Games\\League of Legends\nC:\\Program Files (x86)\\GarenaLoL\\GameData\\Apps\\LoL";
        if (folderBrowserDialog.ShowDialog((IWin32Window) new Form()
        {
          TopMost = true,
          TopLevel = true
        }) == DialogResult.OK)
        {
          if (System.IO.File.Exists(folderBrowserDialog.SelectedPath + "\\lol.launcher.exe") || System.IO.File.Exists(folderBrowserDialog.SelectedPath + "\\lol.exe"))
          {
            Settings.Default.LeaguePath = folderBrowserDialog.SelectedPath;
            Settings.Default.Save();
            flag = false;
          }
          else
          {
            int num = (int) MessageBox.Show("Unable to locate League of Legends. Please try again.", "Can't Find League", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
          }
        }
        else
        {
          Form1.didCancel = true;
          flag = false;
        }
      }
    }

    private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
    {
      DirectoryInfo directoryInfo1 = new DirectoryInfo(sourceDirName);
      DirectoryInfo[] directories = directoryInfo1.GetDirectories();
      if (!directoryInfo1.Exists)
        throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName);
      if (!Directory.Exists(destDirName))
        Directory.CreateDirectory(destDirName);
      foreach (FileInfo fileInfo in directoryInfo1.GetFiles())
      {
        string destFileName = Path.Combine(destDirName, fileInfo.Name);
        fileInfo.CopyTo(destFileName, false);
      }
      if (!copySubDirs)
        return;
      foreach (DirectoryInfo directoryInfo2 in directories)
      {
        string destDirName1 = Path.Combine(destDirName, directoryInfo2.Name);
        Form1.DirectoryCopy(directoryInfo2.FullName, destDirName1, copySubDirs);
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Form1));
      this.button1 = new Button();
      this.label1 = new Label();
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.panel1 = new Panel();
      this.linkLabel1 = new LinkLabel();
      this.label2 = new Label();
      this.groupBox1 = new GroupBox();
      this.comboBox1 = new ComboBox();
      this.groupBox2 = new GroupBox();
      this.label3 = new Label();
      this.pictureBox1 = new PictureBox();
      this.serialPort1 = new SerialPort(this.components);
      this.tableLayoutPanel1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.pictureBox1.BeginInit();
      this.SuspendLayout();
      this.button1.Dock = DockStyle.Fill;
      this.button1.Location = new Point(3, 265);
      this.button1.Name = "button1";
      this.button1.Size = new Size(307, 24);
      this.button1.TabIndex = 0;
      this.button1.Text = "Change";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.label1.Location = new Point(0, 0);
      this.label1.Name = "label1";
      this.label1.Size = new Size(100, 23);
      this.label1.TabIndex = 0;
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.Controls.Add((Control) this.button1, 0, 2);
      this.tableLayoutPanel1.Controls.Add((Control) this.panel1, 0, 3);
      this.tableLayoutPanel1.Controls.Add((Control) this.groupBox1, 0, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.groupBox2, 0, 1);
      this.tableLayoutPanel1.Dock = DockStyle.Fill;
      this.tableLayoutPanel1.Location = new Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 4;
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 57f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 25f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 42f));
      this.tableLayoutPanel1.Size = new Size(313, 317);
      this.tableLayoutPanel1.TabIndex = 5;
      this.panel1.Controls.Add((Control) this.linkLabel1);
      this.panel1.Controls.Add((Control) this.label2);
      this.panel1.Dock = DockStyle.Right;
      this.panel1.Location = new Point(92, 295);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(218, 19);
      this.panel1.TabIndex = 7;
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Location = new Point(131, 1);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new Size(88, 13);
      this.linkLabel1.TabIndex = 1;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "www.quicklol.net";
      this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      this.label2.AutoSize = true;
      this.label2.Location = new Point(3, 1);
      this.label2.Name = "label2";
      this.label2.Size = new Size(136, 13);
      this.label2.TabIndex = 0;
      this.label2.Text = "For more League tools visit ";
      this.groupBox1.Controls.Add((Control) this.comboBox1);
      this.groupBox1.Dock = DockStyle.Fill;
      this.groupBox1.Location = new Point(3, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Padding = new Padding(10);
      this.groupBox1.Size = new Size(307, 51);
      this.groupBox1.TabIndex = 8;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Select";
      this.comboBox1.Dock = DockStyle.Bottom;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Location = new Point(10, 20);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new Size(287, 21);
      this.comboBox1.TabIndex = 0;
      this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
      this.groupBox2.Controls.Add((Control) this.label3);
      this.groupBox2.Controls.Add((Control) this.pictureBox1);
      this.groupBox2.Dock = DockStyle.Fill;
      this.groupBox2.Location = new Point(3, 60);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Padding = new Padding(6);
      this.groupBox2.Size = new Size(307, 199);
      this.groupBox2.TabIndex = 9;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Preview";
      this.label3.Dock = DockStyle.Fill;
      this.label3.Location = new Point(6, 19);
      this.label3.Name = "label3";
      this.label3.Size = new Size(295, 174);
      this.label3.TabIndex = 1;
      this.label3.Text = "Waiting";
      this.label3.TextAlign = ContentAlignment.MiddleCenter;
      this.pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
      this.pictureBox1.Dock = DockStyle.Fill;
      this.pictureBox1.Location = new Point(6, 19);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(295, 174);
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(313, 317);
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.Controls.Add((Control) this.label1);
      this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MinimumSize = new Size(240, 300);
      this.Name = "Form1";
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "quicklol login screen";
      this.Load += new EventHandler(this.Form1_Load);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.pictureBox1.EndInit();
      this.ResumeLayout(false);
    }
  }
}
