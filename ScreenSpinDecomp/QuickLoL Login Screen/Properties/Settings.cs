// Type: screenspin.Properties.Settings
// Assembly: screenspin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4351C7F0-BD04-4B3C-89D5-7A8D50305D31
// Assembly location: C:\QuickLoL Login Screen.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace screenspin.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        return Settings.defaultInstance;
      }
    }

    [DefaultSettingValue("")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public string LeaguePath
    {
      get
      {
        return (string) this["LeaguePath"];
      }
      set
      {
        this["LeaguePath"] = (object) value;
      }
    }

    static Settings()
    {
    }
  }
}
