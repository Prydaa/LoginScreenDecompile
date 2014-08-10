// Type: screenspin.Program
// Assembly: screenspin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4351C7F0-BD04-4B3C-89D5-7A8D50305D31
// Assembly location: C:\QuickLoL Login Screen.exe

using System;
using System.Windows.Forms;

namespace screenspin
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new Form1());
    }
  }
}
