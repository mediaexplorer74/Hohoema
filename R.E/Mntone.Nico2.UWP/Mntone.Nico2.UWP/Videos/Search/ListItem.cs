// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Search.ListItem
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Videos.Search
{
  [DataContract]
  public class ListItem
  {
    [DataMember(Name = "id")]
    public string id { get; set; }

    [DataMember(Name = "title")]
    public string title { get; set; }

    [DataMember(Name = "first_retrieve")]
    public string _first_retrieve { get; set; }

    [DataMember(Name = "view_counter")]
    public int view_counter { get; set; }

    [DataMember(Name = "mylist_counter")]
    public int mylist_counter { get; set; }

    [DataMember(Name = "thumbnail_url")]
    public string thumbnail_url { get; set; }

    [DataMember(Name = "num_res")]
    public int num_res { get; set; }

    [DataMember(Name = "last_res_body")]
    public string last_res_body { get; set; }

    [DataMember(Name = "length")]
    public string _length { get; set; }

    [DataMember(Name = "title_short")]
    public string title_short { get; set; }

    [DataMember(Name = "description_short")]
    public string description_short { get; set; }

    [DataMember(Name = "thumbnail_style")]
    public ThumbnailStyle thumbnail_style { get; set; }

    [DataMember(Name = "is_middle_thumbnail")]
    public bool is_middle_thumbnail { get; set; }

    public DateTime FirstRetrieve { get; private set; }

    public TimeSpan Length { get; private set; }

    [OnDeserialized]
    private void SetValuesOnDeserialized(StreamingContext context)
    {
      this.FirstRetrieve = DateTime.Parse(this._first_retrieve);
      IEnumerable<string> strings = ((IEnumerable<string>) this._length.Split(':')).Reverse<string>();
      int num1 = 0;
      int num2 = 0;
      foreach (string s in strings)
      {
        num1 += int.Parse(s) * (num2 == 0 ? 1 : num2 * 60);
        ++num2;
      }
      this.Length = TimeSpan.FromSeconds((double) num1);
    }
  }
}
