// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.NiconicoUrls
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;

#nullable disable
namespace Mntone.Nico2
{
  public static class NiconicoUrls
  {
    private const string DomainBase = ".nicovideo.jp/";
    private const string LiveApiForCommunityUrlBase = "http://watch.live.nicovideo.jp/api/";
    private const string LiveApiForOfficialOrChannelUrlBase = "http://ow.live.nicovideo.jp/api/";
    private const string LiveApiForExternalUrlBase = "http://ext.live.nicovideo.jp/api/";
    private const string AuthenticationBase = "https://secure.nicovideo.jp/secure/";
    internal static readonly string AccountApiBase = string.Format("https://account{0}api/", (object) ".nicovideo.jp/");
    internal static readonly string AccountApiV1 = string.Format("{0}v1/", (object) NiconicoUrls.AccountApiBase);
    internal static readonly string LogOnApiUrl = string.Format("{0}login", (object) NiconicoUrls.AccountApiV1);
    internal static readonly string MultiFactorAuthenticationPageBase = string.Format("https://account{0}", (object) ".nicovideo.jp/");
    internal static readonly string MultiFactorAuthenticationPage = string.Format("{0}mfa", (object) NiconicoUrls.MultiFactorAuthenticationPageBase);
    internal static readonly string BackupCodeMultiFactorAuthenticationPage = string.Format("{0}/backup_code", (object) NiconicoUrls.MultiFactorAuthenticationPage);
    internal const string VideoUrlBase = "http://www.nicovideo.jp/";
    internal const string VideoApiUrlBase = "http://www.nicovideo.jp/api/";
    internal const string VideoFlapiUrlBase = "http://flapi.nicovideo.jp/api/";
    public static string VideoLoginUrl = "http://www.nicovideo.jp/login";
    private const string ExtUrlBase = "http://ext.nicovideo.jp/";
    private const string ExtAPIUrlBase = "http://ext.nicovideo.jp/api/";
    private const string ExtSearchUrlBase = "http://ext.nicovideo.jp/api/search/";
    public static string VideoKeywordSearchApiUrl = "http://ext.nicovideo.jp/api/search/search/";
    public static string VideoTagSearchApiUrl = "http://ext.nicovideo.jp/api/search/tag/";
    public static string RelatedVideoApiUrl = "http://api.ce.nicovideo.jp/nicoapi/v1/video.relation";
    private const string LiveUrlBase = "http://live.nicovideo.jp/";
    private const string LiveApiUrlBase = "http://live.nicovideo.jp/api/";
    private const string Live2BaseUrl = "http://live2.nicovideo.jp/";
    internal static string Live2WatchPageUrl = "http://live2.nicovideo.jp/watch/";
    private const string ImageUrlBase = "http://seiga.nicovideo.jp/";
    private const string ImageApiUrlBase = "http://seiga.nicovideo.jp/api/";
    private const string ImageExtApiUrlBase = "http://ext.seiga.nicovideo.jp/api/";
    private const string SearchApiUrlBase = "http://api.search.nicovideo.jp/api/";
    private const string DictionaryUrlBase = "http://dic.nicovideo.jp/";
    private const string DictionaryApiUrlBase = "http://api.nicodic.jp/";
    private const string AppUrlBase = "http://app.nicovideo.jp/";
    private const string CommunityUrlBase = "http://com.nicovideo.jp/";
    public static string CommynitySearchPageUrl = "http://com.nicovideo.jp/search/";
    public static string CommynitySammaryPageUrl = "http://com.nicovideo.jp/community/";
    public static string CommynityVideoPageUrl = "http://com.nicovideo.jp/video/";
    public static string CommunityJoinPageUrl = "http://com.nicovideo.jp/motion/";
    public static string CommunityLeavePageUrl = "http://com.nicovideo.jp/leave/";
    public static string UserPageUrlBase = string.Format("{0}user/", (object) "http://www.nicovideo.jp/");
    private static string UserFavApiBase = "http://www.nicovideo.jp/api/watchitem/";
    public static string UserFavUserPageUrl = NiconicoUrls.UserPageUrl + "/fav/user";
    public static string UserFavMylistPageUrl = NiconicoUrls.UserPageUrl + "/fav/mylist";
    public static string UserFavTagPageUrl = NiconicoUrls.UserPageUrl + "/fav/tag";
    public static string UserFavCommunityPageUrl = NiconicoUrls.UserPageUrl + "/community";
    public const string UserFavTagBase = "http://www.nicovideo.jp/api/favtag/";
    public const string UserNGCommentUrl = "http://flapi.nicovideo.jp/api/configurengclient";
    public const string VideoPostKeyUrl = "http://flapi.nicovideo.jp/api/getpostkey";
    public static string MylistDefListUrlBase = "http://www.nicovideo.jp/api/deflist/";
    public static string MylistDeflistListUrl = NiconicoUrls.MylistDefListUrlBase + "list";
    public static string MylistDeflistAddUrl = NiconicoUrls.MylistDefListUrlBase + "add";
    public static string MylistDeflistUpdateUrl = NiconicoUrls.MylistDefListUrlBase + "update";
    public static string MylistDeflistRemoveUrl = NiconicoUrls.MylistDefListUrlBase + "delete";
    public static string MylistDeflistMoveUrl = NiconicoUrls.MylistDefListUrlBase + "move";
    public static string MylistDeflistCopyUrl = NiconicoUrls.MylistDefListUrlBase + "copy";
    public static string MylistGroupUrlBase = "http://www.nicovideo.jp/api/mylistgroup/";
    public static string MylistGroupListUrl = NiconicoUrls.MylistGroupUrlBase + "list";
    public static string MylistGroupGetUrl = NiconicoUrls.MylistGroupUrlBase + "get";
    public static string MylistGroupAddUrl = NiconicoUrls.MylistGroupUrlBase + "add";
    public static string MylistGroupUpdateUrl = NiconicoUrls.MylistGroupUrlBase + "update";
    public static string MylistGroupRemoveUrl = NiconicoUrls.MylistGroupUrlBase + "delete";
    public static string MylistGroupSortUrl = NiconicoUrls.MylistGroupUrlBase + "sort";
    public static string MylistGroupDetailApi = "http://api.ce.nicovideo.jp/nicoapi/v1/mylistgroup.get";
    public static string MylistListlApi = "http://api.ce.nicovideo.jp/nicoapi/v1/mylist.list";
    public static string MylistMyPageUrl = "http://www.nicovideo.jp/my/mylist";
    public static string MylistUrlBase = "http://www.nicovideo.jp/api/mylist/";
    public static string MylistListUrl = NiconicoUrls.MylistUrlBase + "list";
    public static string MylistAddUrl = NiconicoUrls.MylistUrlBase + "add";
    public static string MylistUpdateUrl = NiconicoUrls.MylistUrlBase + "update";
    public static string MylistRemoveUrl = NiconicoUrls.MylistUrlBase + "delete";
    public static string MylistMoveUrl = NiconicoUrls.MylistUrlBase + "move";
    public static string MylistCopyUrl = NiconicoUrls.MylistUrlBase + "copy";
    public static string WatchItemUrlBase = "http://www.nicovideo.jp/api/watchitem/";
    public static string WatchItemListUrl = NiconicoUrls.WatchItemUrlBase + "list";
    public static string WatchItemExistUrl = NiconicoUrls.WatchItemUrlBase + "exist";
    public static string WatchItemAddUrl = NiconicoUrls.WatchItemUrlBase + "add";
    public static string WatchItemRemoveUrl = NiconicoUrls.WatchItemUrlBase + "remove";
    public const string NICOVIDEO_CE_NICOAPI_BASE = "http://api.ce.nicovideo.jp/nicoapi/";
    public const string NICOVIDEO_CE_NICOAPI_V1 = "http://api.ce.nicovideo.jp/nicoapi/v1/";
    public const string NICOVIDEO_CE_NICOAPI_V1_MYLISTGROUP = "http://api.ce.nicovideo.jp/nicoapi/v1/mylistgroup";
    public const string NICOVIDEO_CE_NICOAPI_V1_MYLISTGROUP_GET = "http://api.ce.nicovideo.jp/nicoapi/v1/mylistgroup.get";
    public const string NICOVIDEO_CE_NICOAPI_V1_MYLIST = "http://api.ce.nicovideo.jp/nicoapi/v1/mylist";
    public const string NICOVIDEO_CE_NICOAPI_V1_MYLIST_SEARCH = "http://api.ce.nicovideo.jp/nicoapi/v1/mylist.search";
    public const string NICOVIDEO_CE_NICOAPI_V1_MYLIST_LIST = "http://api.ce.nicovideo.jp/nicoapi/v1/mylist.list";
    public const string NICOVIDEO_CE_NICOAPI_V1_VIDEO = "http://api.ce.nicovideo.jp/nicoapi/v1/video";
    public const string NICOVIDEO_CE_NICOAPI_V1_VIDEO_INFO = "http://api.ce.nicovideo.jp/nicoapi/v1/video.info";
    public const string NICOVIDEO_CE_NICOAPI_V1_VIDEO_SEARCH = "http://api.ce.nicovideo.jp/nicoapi/v1/video.search";
    public const string NICOVIDEO_CE_NICOAPI_V1_TAG = "http://api.ce.nicovideo.jp/nicoapi/v1/tag";
    public const string NICOVIDEO_CE_NICOAPI_V1_TAG_SEARCH = "http://api.ce.nicovideo.jp/nicoapi/v1/tag.search";
    public const string NICOVIDEO_CE_API_BASE = "http://api.ce.nicovideo.jp/api/";
    public const string NICOVIDEO_CE_API_V1 = "http://api.ce.nicovideo.jp/api/v1/";
    public const string NICOVIDEO_CE_API_V1_COMMUNITY = "http://api.ce.nicovideo.jp/api/v1/community";
    public const string NICOVIDEO_CE_API_V1_COMMUNITY_INFO = "http://api.ce.nicovideo.jp/api/v1/community.info";
    public const string NICOVIDEO_CE_LIVEAPI_BASE = "http://api.ce.nicovideo.jp/liveapi/";
    public const string NICOVIDEO_CE_LIVEAPI_BASE_V1 = "http://api.ce.nicovideo.jp/liveapi/v1/";
    public const string NICOVIDEO_CE_LIVEAPI_V1_COMMUNITY_VIDEO = "http://api.ce.nicovideo.jp/liveapi/v1/community.video";
    public const string NICOVIDEO_CE_LIVEAPI_V1_VIDEO_SEARCH = "http://api.ce.nicovideo.jp/liveapi/v1/video.search.solr";

    public static string TopPageUrl => "http://www.nicovideo.jp/";

    internal static string LogOnUrl => "https://secure.nicovideo.jp/secure/login?site=niconico";

    internal static string LogOffUrl => "https://secure.nicovideo.jp/secure/logout";

    public static string VideoTopPageUrl => "http://www.nicovideo.jp/my/top";

    public static string VideoMyPageUrl => "http://www.nicovideo.jp/video_top";

    public static string VideoWatchPageUrl => "http://www.nicovideo.jp/watch/";

    internal static string VideoFlvUrl => "http://flapi.nicovideo.jp/api/getflv/";

    internal static string VideoThumbInfoUrl => "http://ext.nicovideo.jp/api/getthumbinfo/";

    internal static string VideoHistoryUrl => "http://www.nicovideo.jp/api/videoviewhistory/list";

    internal static string VideoRemoveUrl
    {
      get => "http://www.nicovideo.jp/api/videoviewhistory/remove?token=";
    }

    internal static string VideoThreadKeyApiUrl
    {
      get => "http://flapi.nicovideo.jp/api/getthreadkey?thread=";
    }

    public static string MakeKeywordSearchUrl(
      string keyword,
      uint pageCount,
      string sortMethod,
      string sortDir)
    {
      return string.Format("{0}{1}?mode=watch&page={2}&sort={3}&order={4}", (object) NiconicoUrls.VideoKeywordSearchApiUrl, (object) keyword, (object) pageCount, (object) sortMethod, (object) sortDir);
    }

    public static string MakeTagSearchUrl(
      string tag,
      uint pageCount,
      string sortMethod,
      string sortDir)
    {
      return string.Format("{0}{1}?mode=watch&page={2}&sort={3}&order={4}", (object) NiconicoUrls.VideoTagSearchApiUrl, (object) tag, (object) pageCount, (object) sortMethod, (object) sortDir);
    }

    public static string LiveTopPageUrl => "http://live.nicovideo.jp/";

    public static string LiveMyPageUrl => "http://live.nicovideo.jp/my";

    public static string LiveWatchPageUrl => "http://live.nicovideo.jp/watch/";

    public static string LiveGatePageUrl => "http://live.nicovideo.jp/gate/";

    internal static string LiveCKeyUrl => "http://live.nicovideo.jp/api/getckey";

    internal static string LivePlayerStatusUrl => "http://live.nicovideo.jp/api/getplayerstatus/";

    internal static string LivePostKeyUrl => "http://live.nicovideo.jp/api/getpostkey";

    internal static string LiveVoteUrl => "http://live.nicovideo.jp/api/vote";

    internal static string LiveHeartbeatUrl => "http://live.nicovideo.jp/api/heartbeat";

    internal static string LiveLeaveUrl => "http://live.nicovideo.jp/api/leave";

    internal static string LiveTagRevisionUrl => "http://live.nicovideo.jp/api/tagrev/";

    internal static string LiveZappingListIndexUrl
    {
      get => "http://live.nicovideo.jp/api/getzappinglist?zroute=index";
    }

    internal static string LiveZappingListRecentUrl
    {
      get => "http://live.nicovideo.jp/api/getzappinglist?zroute=recent";
    }

    internal static string LiveIndexZeroStreamListUrl
    {
      get => "http://live.nicovideo.jp/api/getindexzerostreamlist?status=";
    }

    internal static string LiveWatchingReservationListUrl
    {
      get => "http://live.nicovideo.jp/api/watchingreservation?mode=list";
    }

    internal static string LiveWatchingReservationDetailListUrl
    {
      get => "http://live.nicovideo.jp/api/watchingreservation?mode=detaillist";
    }

    public static string ImageTopPageUrl => "http://seiga.nicovideo.jp/";

    public static string ImageMyPageUrl => "http://seiga.nicovideo.jp/my";

    public static string ImageThemeTopPageUrl => "http://seiga.nicovideo.jp/theme/";

    public static string ImageIllustTopPageUrl => "http://seiga.nicovideo.jp/illust/";

    public static string ImageIllustAdultTopPageUrl => "http://seiga.nicovideo.jp/shunga/";

    public static string ImageMangaTopPageUrl => "http://seiga.nicovideo.jp/manga/";

    public static string ImageElectronicBookTopPageUrl => "http://seiga.nicovideo.jp/book/";

    internal static string ImageBlogPartsUrl
    {
      get => "http://ext.seiga.nicovideo.jp/api/illust/blogparts?mode=";
    }

    internal static string ImageUserInfoUrl => "http://seiga.nicovideo.jp/api/user/info?id=";

    internal static string ImageUserDataUrl => "http://seiga.nicovideo.jp/api/user/data?id=";

    internal static string SearchSuggestionUrl
    {
      get => "http://sug.search.nicovideo.jp/suggestion/expand/";
    }

    public static string DictionaryTopPageUrl => "http://dic.nicovideo.jp/";

    internal static string DictionaryWordExistUrl => "http://api.nicodic.jp/e/json/";

    internal static string DictionarySummarytUrl => "http://api.nicodic.jp/page.summary/json/a/";

    internal static string DictionaryExistUrl => "http://api.nicodic.jp/page.exist/json/";

    internal static string DictionaryRecentUrl => "http://api.nicodic.jp/page.created/json";

    public static string AppTopPageUrl => "http://app.nicovideo.jp/";

    public static string AppMyPageUrl => "http://app.nicovideo.jp/my/apps";

    public static string CommunityIconUrl => "http://icon.nimg.jp/community/{0}/co{1}.jpg";

    public static string CommunitySmallIconUrl => "http://icon.nimg.jp/community/s/{0}/co{1}.jpg";

    public static string CommunityBlankIconUrl => "http://icon.nimg.jp/404.jpg";

    public static string ChannelIconUrl => "http://icon.nimg.jp/channel/ch{0}.jpg";

    public static string ChannelSmallIconUrl => "http://icon.nimg.jp/channel/s/ch{0}.jpg";

    public static string UserPageUrl => "http://www.nicovideo.jp/my";

    public static string UserIconUrl => "http://usericon.nimg.jp/usericon/{0}/{1}.jpg";

    public static string UserSmallIconUrl => "http://usericon.nimg.jp/usericon/s/{0}/{1}.jpg";

    public static string UserBlankIconUrl => "http://uni.res.nimg.jp/img/user/thumb/blank.jpg";

    public static string MakeUserPageUrl(string user_id)
    {
      return string.Format("{0}{1}", (object) NiconicoUrls.UserPageUrlBase, (object) user_id);
    }

    public static string MakeUserMylistGroupListRssUrl(string user_id)
    {
      return string.Format("{0}/mylist?rss=2.0", (object) NiconicoUrls.MakeUserPageUrl(user_id));
    }

    public static string MakeUserVideoRssUrl(
      string userId,
      uint page,
      string sortMethod = null,
      string sortDirection = null)
    {
      string str = page > 0U ? string.Format("http://www.nicovideo.jp/user/{0}/video?rss=2.0&page={1}", (object) userId, (object) page) : throw new NotSupportedException("page is can not be lesser equal 0.");
      if (sortMethod != null)
        str += string.Format("&sort={0}", (object) sortMethod);
      if (sortDirection != null)
        str += string.Format("&order={0}", (object) sortDirection);
      return str;
    }

    public static string CE_UserApiUrl => "http://api.ce.nicovideo.jp/api/v1/user.info";

    public static string UserFavListApiUrl => NiconicoUrls.UserFavApiBase + "list";

    public static string UserFavExistApiUrl => NiconicoUrls.UserFavApiBase + "exist";

    public static string UserFavAddApiUrl => NiconicoUrls.UserFavApiBase + "add";

    public static string UserFavRemoveApiUrl => NiconicoUrls.UserFavApiBase + "delete";

    public static string UserFavTagListUrl => "http://www.nicovideo.jp/api/favtag/list";

    public static string UserFavTagAddUrl => "http://www.nicovideo.jp/api/favtag/add";

    public static string UserFavTagRemoveUrl => "http://www.nicovideo.jp/api/favtag/delete";

    public static string MakeMylistPageUrl(string group_id)
    {
      return "http://www.nicovideo.jp/mylist/" + group_id;
    }

    public static string MakeMylistCSRFTokenApiUrl(string group_id)
    {
      return string.Format("{0}/#/{1}", (object) NiconicoUrls.MylistMyPageUrl, (object) group_id);
    }

    public static string MakeMylistAddVideoTokenApiUrl(string videoId)
    {
      return string.Format("{0}mylist_add/video/{1}", (object) "http://www.nicovideo.jp/", (object) videoId);
    }

    public static string MakeTagPageUrl(string tag)
    {
      return string.Format("{0}tag/{1}", (object) "http://www.nicovideo.jp/", (object) tag);
    }
  }
}
