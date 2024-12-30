// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.HttpOutputDownloadParameters
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  [DataContract]
  public class HttpOutputDownloadParameters
  {
    [DataMember(Name = "file_extension")]
    public string FileExtension { get; set; }

    [DataMember(Name = "transfer_preset")]
    public string TransferPreset { get; set; }

    [DataMember(Name = "use_ssl")]
    public string UseSsl { get; set; }

    [DataMember(Name = "use_well_known_port")]
    public string UseWellKnownPort { get; set; }
  }
}
