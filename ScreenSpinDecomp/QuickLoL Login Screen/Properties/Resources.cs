// Type: screenspin.Properties.Resources
// Assembly: screenspin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4351C7F0-BD04-4B3C-89D5-7A8D50305D31
// Assembly location: C:\QuickLoL Login Screen.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace screenspin.Properties
{
  [DebuggerNonUserCode]
  [CompilerGenerated]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) Resources.resourceMan, (object) null))
          Resources.resourceMan = new ResourceManager("screenspin.Properties.Resources", typeof (Resources).Assembly);
        return Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return Resources.resourceCulture;
      }
      set
      {
        Resources.resourceCulture = value;
      }
    }

    internal static byte[] Newtonsoft_Json
    {
      get
      {
        return (byte[]) Resources.ResourceManager.GetObject("Newtonsoft_Json", Resources.resourceCulture);
      }
    }

    internal Resources()
    {
    }
  }
}
