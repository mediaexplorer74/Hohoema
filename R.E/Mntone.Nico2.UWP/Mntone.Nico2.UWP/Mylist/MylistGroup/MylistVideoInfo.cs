// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Mylist.MylistGroup.MylistVideoInfo
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Searches.Video;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Mylist.MylistGroup
{
  [DataContract]
  public class MylistVideoInfo : VideoInfo
  {
    [DataMember(Name = "mylist")]
    public Mntone.Nico2.Mylist.Mylist Mylist { get; set; }
  }
}
