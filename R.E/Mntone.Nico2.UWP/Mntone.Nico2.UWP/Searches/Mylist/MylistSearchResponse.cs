// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Searches.Mylist.MylistSearchResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Searches.Mylist
{
  [DataContract]
  public class MylistSearchResponse
  {
    [DataMember(Name = "total_count")]
    public string __total_count { get; private set; }

    public uint GetTotalCount() => uint.Parse(this.__total_count);

    [DataMember(Name = "data_count")]
    public string __data_count { get; private set; }

    public uint GetDataCount() => uint.Parse(this.__data_count);

    [DataMember(Name = "mylistgroup")]
    [JsonConverter(typeof (SingleOrArrayConverter<MylistGroup>))]
    public IList<MylistGroup> MylistGroupItems { get; private set; }

    [DataMember(Name = "@status")]
    public string status { get; private set; }

    public bool IsOK => this.status == "ok";
  }
}
