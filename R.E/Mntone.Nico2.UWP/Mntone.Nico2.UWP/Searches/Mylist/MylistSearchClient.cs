// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Searches.Mylist.MylistSearchClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Searches.Mylist
{
  public sealed class MylistSearchClient
  {
    public static Task<string> GetMylistSearchDataAsync(
      NiconicoContext context,
      string str,
      uint from,
      uint limit,
      Sort? sort,
      Order? order)
    {
      Dictionary<string, string> query = new Dictionary<string, string>();
      query.Add("__format", "json");
      query.Add(nameof (str), str);
      query.Add(nameof (from), from.ToString());
      query.Add(nameof (limit), limit.ToString());
      if (order.HasValue)
        query.Add(nameof (order), order.Value.ToChar().ToString());
      if (sort.HasValue)
        query.Add(nameof (sort), sort.Value.ToShortString());
      return context.GetStringAsync("http://api.ce.nicovideo.jp/nicoapi/v1/mylist.search", query);
    }

    private static MylistSearchResponse ParseVideoResponseJson(string mylistSearchResponseJson)
    {
      return JsonSerializerExtensions.Load<MylistSearchResponseContainer>(mylistSearchResponseJson).nicovideo_mylist_response;
    }

    public static Task<MylistSearchResponse> GetMylistSearchAsync(
      NiconicoContext context,
      string keyword,
      uint from,
      uint limit,
      Sort? sort,
      Order? order)
    {
      return MylistSearchClient.GetMylistSearchDataAsync(context, keyword, from, limit, sort, order).ContinueWith<MylistSearchResponse>((Func<Task<string>, MylistSearchResponse>) (prevTask => MylistSearchClient.ParseVideoResponseJson(prevTask.Result)));
    }
  }
}
