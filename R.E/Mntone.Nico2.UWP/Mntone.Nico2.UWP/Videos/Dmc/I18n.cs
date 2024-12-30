// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.I18n
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  [DataContract]
  public class I18n
  {
    [DataMember(Name = "language")]
    public string Language { get; set; }

    [DataMember(Name = "locale")]
    public string Locale { get; set; }

    [DataMember(Name = "area")]
    public string Area { get; set; }

    [DataMember(Name = "footer")]
    public object Footer { get; set; }
  }
}
