// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Search.SearchClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Videos.Search
{
  internal static class SearchClient
  {
    public static async Task<string> GetKeywordSearchDataAsync(
      NiconicoContext context,
      string keyword,
      uint pageCount,
      Sort sortMethod,
      Order sortDir)
    {
      char ch = sortMethod.ToChar();
      string sortMethod1 = ch.ToString();
      ch = sortDir.ToChar();
      string sortDir1 = ch.ToString();
      return await context.GetClient().GetStringAsync(NiconicoUrls.MakeKeywordSearchUrl(keyword, pageCount, sortMethod1, sortDir1));
    }

    public static async Task<string> GetTagSearchDataAsync(
      NiconicoContext context,
      string tag,
      uint pageCount,
      Sort sortMethod,
      Order sortDir)
    {
      string shortString1 = sortMethod.ToShortString();
      string shortString2 = sortDir.ToShortString();
      return await context.GetClient().GetStringAsync(NiconicoUrls.MakeTagSearchUrl(tag, pageCount, shortString1, shortString2));
    }

    public static SearchResponse ParseSearchData(string searchJson)
    {
      return JsonSerializerExtensions.Load<SearchResponse>(searchJson);
    }

    public static Task<SearchResponse> GetKeywordSearchAsync(
      NiconicoContext context,
      string keyword,
      uint pageCount,
      Sort sortMethod = Sort.FirstRetrieve,
      Order sortDir = Order.Descending)
    {
      return SearchClient.GetKeywordSearchDataAsync(context, keyword, pageCount, sortMethod, sortDir).ContinueWith<SearchResponse>((Func<Task<string>, SearchResponse>) (prevTask => SearchClient.ParseSearchData(prevTask.Result)));
    }

    public static Task<SearchResponse> GetTagSearchAsync(
      NiconicoContext context,
      string tag,
      uint pageCount,
      Sort sortMethod = Sort.FirstRetrieve,
      Order sortDir = Order.Descending)
    {
      return SearchClient.GetTagSearchDataAsync(context, tag, pageCount, sortMethod, sortDir).ContinueWith<SearchResponse>((Func<Task<string>, SearchResponse>) (prevTask => SearchClient.ParseSearchData(prevTask.Result)));
    }
  }
}
