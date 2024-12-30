// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Mylist.MylistJsonSerializeHelper
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Users.MylistItem;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Mntone.Nico2.Mylist
{
  public static class MylistJsonSerializeHelper
  {
    public static List<MylistGroupData> ParseMylistGroupListJson(string json)
    {
      return JsonConvert.DeserializeObject<LoginUserMylistGroupData>(json).mylistgroup.Cast<MylistGroupData>().ToList<MylistGroupData>();
    }

    public static List<MylistData> ParseMylistItemResponse(string json)
    {
      MylistItemResponse mylistItemResponse = JsonConvert.DeserializeObject<MylistItemResponse>(json);
      return mylistItemResponse.status == "ok" ? mylistItemResponse.mylistitem.Select<Mntone.Nico2.Users.MylistItem.MylistItem, MylistData>((Func<Mntone.Nico2.Users.MylistItem.MylistItem, MylistData>) (x => new MylistData()
      {
        Title = x.item_data.title,
        Description = x.description,
        ItemId = x.item_id,
        WatchId = x.item_data.watch_id,
        ItemType = (NiconicoItemType) int.Parse(x.item_type),
        FirstRetrieve = DateTimeOffset.FromUnixTimeSeconds((long) x.item_data.first_retrieve).DateTime,
        ViewCount = uint.Parse(x.item_data.view_counter),
        CommentCount = uint.Parse(x.item_data.num_res),
        MylistCount = uint.Parse(x.item_data.mylist_counter),
        CreateTime = DateTimeOffset.FromUnixTimeSeconds((long) x.create_time).DateTime,
        UpdateTime = DateTimeOffset.FromUnixTimeSeconds((long) x.update_time).DateTime,
        IsDeleted = x.item_data.deleted.ToBooleanFrom1(),
        Length = TimeSpan.FromSeconds((double) int.Parse(x.item_data.length_seconds)),
        ThumbnailUrl = new Uri(x.item_data.thumbnail_url)
      })).ToList<MylistData>() : (List<MylistData>) null;
    }
  }
}
