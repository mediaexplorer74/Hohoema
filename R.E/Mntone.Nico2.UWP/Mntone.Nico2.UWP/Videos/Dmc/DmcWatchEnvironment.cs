// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.DmcWatchEnvironment
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  [DataContract]
  public class DmcWatchEnvironment
  {
    [DataMember(Name = "updated")]
    public Updated Updated { get; set; }

    [DataMember(Name = "baseURL")]
    public BaseURL BaseURL { get; set; }

    [DataMember(Name = "playlistToken")]
    public string PlaylistToken { get; set; }

    [DataMember(Name = "i18n")]
    public I18n I18n { get; set; }

    [DataMember(Name = "urls")]
    public Urls Urls { get; set; }

    [DataMember(Name = "isMonitoringLogUser")]
    public bool? IsMonitoringLogUser { get; set; }
  }
}
