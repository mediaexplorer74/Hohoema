// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Mylist.LoginUserMylistGroup
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Mylist
{
  [DataContract]
  public class LoginUserMylistGroup : MylistGroupData
  {
    [DataMember(Name = "default_sort")]
    public string DefaultSort { get; set; }

    [DataMember(Name = "create_time")]
    public long CreateTime { get; set; }

    [DataMember(Name = "update_time")]
    public long UpdateTime { get; set; }

    [DataMember(Name = "sort_order")]
    public string SortOrder { get; set; }

    public int ItemCount { get; set; }

    public MylistDefaultSort GetDefaultSort() => (MylistDefaultSort) int.Parse(this.DefaultSort);
  }
}
