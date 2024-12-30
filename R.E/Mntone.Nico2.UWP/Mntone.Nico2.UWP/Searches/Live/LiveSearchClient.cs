// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Searches.Live.LiveSearchClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Live;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Searches.Live
{
  public sealed class LiveSearchClient
  {
    public static Task<string> GetLiveSearchDataAsync(
      NiconicoContext context,
      string word,
      bool isTagSearch,
      CommunityType? pt = null,
      uint from = 0,
      uint limit = 30,
      Order? order = null,
      NicoliveSearchSort? sort = null,
      NicoliveSearchMode? search_mode = null)
    {
      Dictionary<string, string> query = new Dictionary<string, string>();
      string str1 = "json";
      query.Add("__format", str1);
      query.Add(nameof (word), word);
      if (isTagSearch)
      {
        string str2 = "tags";
        query.Add("kind", str2);
      }
      if (pt.HasValue)
        query.Add(nameof (pt), pt.ToString().ToLower());
      if (from > 0U)
        query.Add(nameof (from), from.ToString());
      if (limit >= 150U)
        limit = 149U;
      query.Add(nameof (limit), limit.ToString());
      if (order.HasValue)
        query.Add(nameof (order), order.Value.ToChar().ToString());
      if (sort.HasValue)
        query.Add(nameof (sort), sort.Value.ToString().ToLower());
      if (search_mode.HasValue)
        query.Add(nameof (search_mode), search_mode.Value.ToString().ToLower());
      return context.GetStringAsync("http://api.ce.nicovideo.jp/liveapi/v1/video.search.solr", query);
    }

    private static NicoliveVideoResponse ParseLiveSearchJson(string json)
    {
      return JsonSerializerExtensions.Load<NicoliveVideoResponseContainer>(json).NicoliveVideoResponse;
    }

    public static Task<NicoliveVideoResponse> GetLiveSearchAsync(
      NiconicoContext context,
      string word,
      bool isTagSearch,
      CommunityType? provider = null,
      uint from = 0,
      uint length = 30,
      Order? order = null,
      NicoliveSearchSort? sort = null,
      NicoliveSearchMode? mode = null)
    {
      return LiveSearchClient.GetLiveSearchDataAsync(context, word, isTagSearch, provider, from, length, order, sort, mode).ContinueWith<NicoliveVideoResponse>((Func<Task<string>, NicoliveVideoResponse>) (prevTask => LiveSearchClient.ParseLiveSearchJson(prevTask.Result)));
    }
  }
}
