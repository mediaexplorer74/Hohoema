// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.Item
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  [DataContract]
  public class Item
  {
    [DataMember(Name = "id")]
    public string Id { get; set; }

    [DataMember(Name = "requestId")]
    public object RequestId { get; set; }

    [DataMember(Name = "title")]
    public string Title { get; set; }

    [DataMember(Name = "thumbnailURL")]
    public string ThumbnailURL { get; set; }

    [DataMember(Name = "viewCounter")]
    public string ViewCounter { get; set; }

    [DataMember(Name = "numRes")]
    public object NumRes { get; set; }

    [DataMember(Name = "mylistCounter")]
    public string MylistCounter { get; set; }

    [DataMember(Name = "firstRetrieve")]
    public string FirstRetrieve { get; set; }

    [DataMember(Name = "lengthSeconds")]
    public string LengthSeconds { get; set; }

    [DataMember(Name = "threadUpdateTime")]
    public object ThreadUpdateTime { get; set; }

    [DataMember(Name = "createTime")]
    public object CreateTime { get; set; }

    [DataMember(Name = "width")]
    public int? Width { get; set; }

    [DataMember(Name = "height")]
    public int? Height { get; set; }

    [DataMember(Name = "isTranslated")]
    public bool IsTranslated { get; set; }

    [DataMember(Name = "mylistComment")]
    public object MylistComment { get; set; }

    [DataMember(Name = "tkasType")]
    public object TkasType { get; set; }

    [DataMember(Name = "hasData")]
    public bool HasData { get; set; }
  }
}
