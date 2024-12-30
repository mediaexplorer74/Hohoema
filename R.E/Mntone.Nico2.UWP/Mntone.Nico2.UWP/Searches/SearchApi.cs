// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Searches.SearchApi
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Live;
using Mntone.Nico2.Searches.Community;
using Mntone.Nico2.Searches.Live;
using Mntone.Nico2.Searches.Mylist;
using Mntone.Nico2.Searches.Suggestion;
using Mntone.Nico2.Searches.Video;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Searches
{
  public sealed class SearchApi
  {
    private NiconicoContext _context;

    internal SearchApi(NiconicoContext context) => this._context = context;

    public Task<SuggestionResponse> GetSuggestionAsync(string targetWord)
    {
      return SuggestionClient.GetSuggestionAsync(this._context, targetWord);
    }

    public Task<VideoInfoResponse> GetVideoInfoAsync(string videoId)
    {
      return VideoSearchClient.GetVideoInfoAsync(this._context, videoId);
    }

    public Task<VideoListingResponse> VideoSearchWithKeywordAsync(
      string keyword,
      uint from = 0,
      uint limit = 30,
      Sort? sort = null,
      Order? order = null)
    {
      return VideoSearchClient.GetKeywordSearchAsync(this._context, keyword, from, limit, sort, order);
    }

    public Task<VideoListingResponse> VideoSearchWithTagAsync(
      string tag,
      uint from = 0,
      uint limit = 30,
      Sort? sort = null,
      Order? order = null)
    {
      return VideoSearchClient.GetTagSearchAsync(this._context, tag, from, limit, sort, order);
    }

    public Task<MylistSearchResponse> MylistSearchAsync(
      string keyword,
      uint from = 0,
      uint limit = 30,
      Sort? sort = null,
      Order? order = null)
    {
      return MylistSearchClient.GetMylistSearchAsync(this._context, keyword, from, limit, sort, order);
    }

    public Task<CommunitySearchResponse> CommunitySearchAsync(
      string keyword,
      uint page = 1,
      CommunitySearchSort sort = CommunitySearchSort.CreatedAt,
      Order order = Order.Descending,
      CommunitySearchMode mode = CommunitySearchMode.Keyword)
    {
      return CommunityClient.CommunitySearchAsync(this._context, keyword, page, sort, order, mode);
    }

    public Task<NicoliveVideoResponse> LiveSearchAsync(
      string word,
      bool isTagSearch,
      CommunityType? provider = null,
      uint from = 0,
      uint length = 30,
      Order? order = null,
      NicoliveSearchSort? sort = null,
      NicoliveSearchMode? mode = null)
    {
      return LiveSearchClient.GetLiveSearchAsync(this._context, word, isTagSearch, provider, from, length, order, sort, mode);
    }
  }
}
