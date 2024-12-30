// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Dmc.DmcClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using HtmlAgilityPack;
using Mntone.Nico2.Videos.WatchAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

#nullable disable
namespace Mntone.Nico2.Videos.Dmc
{
  internal sealed class DmcClient
  {
    public static async Task<string> GetDmcWatchJsonDataAsync(
      NiconicoContext context,
      string requestId,
      string playlistToken)
    {
      NiconicoRegex.IsVideoId(requestId);
      Dictionary<string, string> dict = new Dictionary<string, string>();
      string str = string.Format("{0}{1}", (object) NiconicoUrls.VideoWatchPageUrl, (object) requestId);
      dict.Add("mode", "pc_html5");
      dict.Add("eco", "0");
      dict.Add("playlist_token", playlistToken);
      dict.Add("watch_harmful", 2U.ToString());
      dict.Add("continue_watching", "1");
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(str + "?" + HttpQueryExtention.DictionaryToQuery((IDictionary<string, string>) dict)));
      ((IDictionary<string, string>) httpRequestMessage.Headers).Add("Accept", "*/*");
      string watchJsonDataAsync;
      try
      {
        HttpResponseMessage httpResponseMessage = await context.GetClient().SendRequestAsync(httpRequestMessage);
        if (httpResponseMessage.StatusCode == 403)
          throw new WebException("require payment.");
        watchJsonDataAsync = await WindowsRuntimeSystemExtensions.AsTask<string, ulong>(httpResponseMessage.Content.ReadAsStringAsync());
      }
      catch (ContentZoningException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new WebException("access failed watch/" + requestId, ex);
      }
      return watchJsonDataAsync;
    }

    public static DmcWatchResponse ParseDmcWatchJsonData(string htmlString)
    {
      return new JsonSerializer()
      {
        NullValueHandling = ((NullValueHandling) 0),
        DefaultValueHandling = ((DefaultValueHandling) 0)
      }.Deserialize<DmcWatchResponse>((JsonReader) new JsonTextReader((TextReader) new StringReader(htmlString)));
    }

    public static Task<DmcWatchResponse> GetDmcWatchJsonAsync(
      NiconicoContext context,
      string requestId,
      string playlistToken)
    {
      return DmcClient.GetDmcWatchJsonDataAsync(context, requestId, playlistToken).ContinueWith<DmcWatchResponse>((Func<Task<string>, DmcWatchResponse>) (prevTask => DmcClient.ParseDmcWatchJsonData(prevTask.Result)));
    }

    public static async Task<string> GetDmcWatchResponseDataAsync(
      NiconicoContext context,
      string requestId,
      HarmfulContentReactionType harmfulReactType)
    {
      NiconicoRegex.IsVideoId(requestId);
      Dictionary<string, string> dict = new Dictionary<string, string>();
      string str = string.Format("{0}{1}", (object) NiconicoUrls.VideoWatchPageUrl, (object) requestId);
      if (harmfulReactType != HarmfulContentReactionType.None)
        dict.Add("watch_harmful", ((uint) harmfulReactType).ToString());
      string uriString = str + "?" + HttpQueryExtention.DictionaryToQuery((IDictionary<string, string>) dict);
      string responseDataAsync;
      try
      {
        context.GetClient();
        HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(uriString));
        HttpCookiePairHeaderValue cookiePairHeaderValue1 = new HttpCookiePairHeaderValue("watch_html5", "1");
        ((ICollection<HttpCookiePairHeaderValue>) httpRequestMessage.Headers.Cookie).Add(cookiePairHeaderValue1);
        HttpCookiePairHeaderValue cookiePairHeaderValue2 = new HttpCookiePairHeaderValue("watch_flash", "0");
        ((ICollection<HttpCookiePairHeaderValue>) httpRequestMessage.Headers.Cookie).Add(cookiePairHeaderValue2);
        HttpResponseMessage httpResponseMessage = await context.GetClient().SendRequestAsync(httpRequestMessage);
        if (httpResponseMessage.StatusCode == 403)
          throw new WebException("require payment.");
        responseDataAsync = await WindowsRuntimeSystemExtensions.AsTask<string, ulong>(httpResponseMessage.Content.ReadAsStringAsync());
      }
      catch (ContentZoningException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new WebException("access failed watch/" + requestId, ex);
      }
      return responseDataAsync;
    }

    public static DmcWatchData ParseDmcWatchResponseData(string htmlString)
    {
      HtmlDocument htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(htmlString);
      if (htmlDocument.GetElementbyId("PAGECONTAINER") != null)
        throw new ContentZoningException("access once blocked, maybe harmful video.");
      try
      {
        HtmlNode elementbyId = htmlDocument.GetElementbyId("js-initial-watch-data");
        string s1 = WebUtility.HtmlDecode(elementbyId.GetAttributeValue("data-api-data", ""));
        JsonSerializer jsonSerializer = new JsonSerializer();
        jsonSerializer.NullValueHandling = (NullValueHandling) 0;
        jsonSerializer.DefaultValueHandling = (DefaultValueHandling) 0;
        DmcWatchResponse dmcWatchResponse = jsonSerializer.Deserialize<DmcWatchResponse>((JsonReader) new JsonTextReader((TextReader) new StringReader(s1)));
        string s2 = WebUtility.HtmlDecode(elementbyId.GetAttributeValue("data-environment", ""));
        DmcWatchEnvironment watchEnvironment = (DmcWatchEnvironment) null;
        try
        {
          watchEnvironment = jsonSerializer.Deserialize<DmcWatchEnvironment>((JsonReader) new JsonTextReader((TextReader) new StringReader(s2)));
        }
        catch
        {
        }
        return new DmcWatchData()
        {
          DmcWatchResponse = dmcWatchResponse,
          DmcWatchEnvironment = watchEnvironment
        };
      }
      catch
      {
        HtmlNode elementbyId = htmlDocument.GetElementbyId("watchAPIDataContainer");
        if (elementbyId == null)
          return (DmcWatchData) null;
        WatchApiResponse watchApiResponse = new JsonSerializer()
        {
          NullValueHandling = ((NullValueHandling) 0),
          DefaultValueHandling = ((DefaultValueHandling) 0)
        }.Deserialize<WatchApiResponse>((JsonReader) new JsonTextReader((TextReader) new StringReader(WebUtility.HtmlDecode(elementbyId.InnerText))));
        DmcWatchResponse dmcWatchResponse1 = new DmcWatchResponse();
        Video video = new Video();
        video.Id = watchApiResponse.videoDetail.id;
        video.Description = watchApiResponse.videoDetail.description;
        video.OriginalDescription = watchApiResponse.videoDetail.description_original;
        video.IsDeleted = watchApiResponse.videoDetail.isDeleted;
        video.IsOfficial = watchApiResponse.videoDetail.is_official;
        video.IsR18 = watchApiResponse.videoDetail.isR18;
        video.IsNicowari = watchApiResponse.videoDetail.is_nicowari;
        video.Duration = watchApiResponse.videoDetail.length.Value;
        video.IsPublic = watchApiResponse.videoDetail.is_public;
        video.IsMonetized = watchApiResponse.videoDetail.isMonetized;
        video.Width = new int?(watchApiResponse.videoDetail.width);
        video.Height = new int?(watchApiResponse.videoDetail.height);
        int? nullable = watchApiResponse.videoDetail.viewCount;
        video.ViewCount = nullable.Value;
        nullable = watchApiResponse.videoDetail.mylistCount;
        video.MylistCount = nullable.Value;
        video.MovieType = watchApiResponse.flashvars.movie_type;
        video.OriginalTitle = watchApiResponse.videoDetail.title_original;
        video.Title = watchApiResponse.videoDetail.title;
        video.SmileInfo = new SmileInfo()
        {
          Url = watchApiResponse.VideoUrl.OriginalString
        };
        dmcWatchResponse1.Video = video;
        Thread thread = new Thread();
        thread.ServerUrl = watchApiResponse.CommentServerUrl.OriginalString;
        thread.SubServerUrl = watchApiResponse.SubCommentServerUrl.OriginalString;
        nullable = watchApiResponse.videoDetail.commentCount;
        thread.CommentCount = nullable.Value;
        thread.Ids = new Ids()
        {
          Default = watchApiResponse.ThreadId.ToString()
        };
        dmcWatchResponse1.Thread = thread;
        dmcWatchResponse1.Viewer = new Viewer()
        {
          Id = watchApiResponse.viewerInfo.id,
          Nickname = watchApiResponse.viewerInfo.nickname,
          IsPremium = watchApiResponse.viewerInfo.isPremium,
          IsPrivileged = new bool?(watchApiResponse.viewerInfo.isPrivileged)
        };
        dmcWatchResponse1.Tags = (IList<Tag>) watchApiResponse.videoDetail.tagList.Select<TagList, Tag>((Func<TagList, Tag>) (x => new Tag()
        {
          Name = x.tag,
          Id = x.id,
          IsCategory = x.cat.HasValue && x.cat.Value,
          IsLocked = x.lck.ToBooleanFrom1(),
          IsDictionaryExists = x.dic.HasValue && x.dic.Value
        })).ToList<Tag>();
        DmcWatchResponse dmcWatchResponse2 = dmcWatchResponse1;
        if (watchApiResponse.UploaderInfo != null)
          dmcWatchResponse2.Owner = new Owner()
          {
            Id = watchApiResponse.UserId.ToString(),
            Nickname = watchApiResponse.UserName,
            IconURL = watchApiResponse.UploaderInfo.icon_url,
            IsUserMyVideoPublic = watchApiResponse.UploaderInfo.is_user_myvideo_public,
            IsUserOpenListPublic = watchApiResponse.UploaderInfo.is_user_openlist_public,
            IsUserVideoPublic = watchApiResponse.UploaderInfo.is_uservideo_public
          };
        if (watchApiResponse.channelInfo != null)
        {
          DmcWatchResponse dmcWatchResponse3 = dmcWatchResponse2;
          Channel channel = new Channel();
          channel.Id = watchApiResponse.channelInfo.id;
          channel.IconURL = watchApiResponse.channelInfo.icon_url;
          nullable = watchApiResponse.channelInfo.is_favorited;
          int num = 1;
          channel.IsFavorited = nullable.GetValueOrDefault() == num && nullable.HasValue;
          channel.FavoriteToken = watchApiResponse.channelInfo.favorite_token;
          nullable = watchApiResponse.channelInfo.favorite_token_time;
          channel.FavoriteTokenTime = new int?(nullable.Value);
          channel.Name = watchApiResponse.channelInfo.name;
          dmcWatchResponse3.Channel = channel;
        }
        return new DmcWatchData()
        {
          DmcWatchResponse = dmcWatchResponse2
        };
      }
    }

    public static Task<DmcWatchData> GetDmcWatchResponseAsync(
      NiconicoContext context,
      string requestId,
      HarmfulContentReactionType harmfulReactType)
    {
      return DmcClient.GetDmcWatchResponseDataAsync(context, requestId, harmfulReactType).ContinueWith<DmcWatchData>((Func<Task<string>, DmcWatchData>) (prevTask => DmcClient.ParseDmcWatchResponseData(prevTask.Result)));
    }

    public static async Task<string> GetDmcSessionResponseDataAsync(
      NiconicoContext context,
      DmcWatchResponse watchData,
      VideoContent videoQuality = null,
      AudioContent audioQuality = null)
    {
      DmcSessionRequest dmcSessionRequest = new DmcSessionRequest();
      SessionApi sessionApi = watchData.Video.DmcInfo.SessionApi;
      Quality quality = watchData.Video.DmcInfo.Quality;
      List<string> stringList1 = new List<string>();
      if (videoQuality != null && videoQuality.Available)
        stringList1.Add(videoQuality.Id);
      VideoContent videoContent = quality.Videos.Last<VideoContent>();
      if (videoQuality.Id != videoContent.Id)
        stringList1.Add(videoContent.Id);
      List<string> stringList2 = new List<string>()
      {
        quality.Audios.Last<AudioContent>().Id
      };
      string url = string.Format("{0}?_format=json", (object) sessionApi.Urls[0].Url);
      dmcSessionRequest.Session = new RequestSession()
      {
        RecipeId = sessionApi.RecipeId,
        ContentId = sessionApi.ContentId,
        ContentType = "movie",
        ContentSrcIdSets = (IList<ContentSrcIdSet>) new List<ContentSrcIdSet>()
        {
          new ContentSrcIdSet()
          {
            ContentSrcIds = (IList<ContentSrcId>) new List<ContentSrcId>()
            {
              new ContentSrcId()
              {
                SrcIdToMux = new SrcIdToMux()
                {
                  VideoSrcIds = (IList<string>) stringList1,
                  AudioSrcIds = (IList<string>) stringList2
                }
              }
            }
          }
        },
        TimingConstraint = "unlimited",
        KeepMethod = new KeepMethod()
        {
          Heartbeat = new Heartbeat() { Lifetime = 120000 }
        },
        Protocol = new Protocol()
        {
          Name = "http",
          Parameters = new ProtocolParameters()
          {
            HttpParameters = new HttpParameters()
            {
              Parameters = new Parameters()
              {
                HttpOutputDownloadParameters = new HttpOutputDownloadParameters()
                {
                  UseSsl = "no",
                  UseWellKnownPort = "no"
                }
              }
            }
          }
        },
        ContentUri = "",
        SessionOperationAuth = new SessionOperationAuth_Request()
        {
          SessionOperationAuthBySignature = new SessionOperationAuthBySignature_Request()
          {
            Token = sessionApi.Token,
            Signature = sessionApi.Signature
          }
        },
        ContentAuth = new ContentAuth_Request()
        {
          AuthType = sessionApi.AuthTypes.Http,
          ContentKeyTimeout = sessionApi.ContentKeyTimeout,
          ServiceId = "nicovideo",
          ServiceUserId = sessionApi.ServiceUserId
        },
        ClientInfo = new ClientInfo()
        {
          PlayerId = sessionApi.PlayerId
        },
        Priority = sessionApi.Priority
      };
      string str = JsonConvert.SerializeObject((object) dmcSessionRequest, new JsonSerializerSettings()
      {
        NullValueHandling = (NullValueHandling) 1
      });
      return await context.PostAsync(url, (IHttpContent) new HttpStringContent(str, (UnicodeEncoding) 0, "application/json"));
    }

    public static DmcSessionResponse ParseDmcSessionResponse(string json)
    {
      return new JsonSerializer()
      {
        NullValueHandling = ((NullValueHandling) 0),
        DefaultValueHandling = ((DefaultValueHandling) 0)
      }.Deserialize<DmcSessionResponse>((JsonReader) new JsonTextReader((TextReader) new StringReader(json)));
    }

    public static Task<DmcSessionResponse> GetDmcSessionResponseAsync(
      NiconicoContext context,
      DmcWatchResponse watchData,
      VideoContent videoQuality = null,
      AudioContent audioQuality = null)
    {
      return DmcClient.GetDmcSessionResponseDataAsync(context, watchData, videoQuality, audioQuality).ContinueWith<DmcSessionResponse>((Func<Task<string>, DmcSessionResponse>) (prevTask => DmcClient.ParseDmcSessionResponse(prevTask.Result)));
    }

    public static async Task DmcSessionFirstHeartbeatAsync(
      NiconicoContext context,
      DmcWatchResponse watch,
      DmcSessionResponse sessionRes)
    {
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Options, new Uri(string.Format("{0}/{1}?_format=json&_method=PUT", (object) watch.Video.DmcInfo.SessionApi.Urls[0].Url, (object) sessionRes.Data.Session.Id)));
      ((IDictionary<string, string>) httpRequestMessage.Headers).Add("Access-Control-Request-Method", "POST");
      ((IDictionary<string, string>) httpRequestMessage.Headers).Add("Access-Control-Request-Headers", "content-type");
      ((ICollection<HttpProductInfoHeaderValue>) httpRequestMessage.Headers.UserAgent).Add(((IEnumerable<HttpProductInfoHeaderValue>) context.HttpClient.DefaultRequestHeaders.UserAgent).First<HttpProductInfoHeaderValue>());
      int num = (await context.GetClient().SendRequestAsync(httpRequestMessage, (HttpCompletionOption) 1)).IsSuccessStatusCode ? 1 : 0;
    }

    public static async Task DmcSessionHeartbeatAsync(
      NiconicoContext context,
      DmcWatchResponse watch,
      DmcSessionResponse sessionRes)
    {
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(string.Format("{0}/{1}?_format=json&_method=PUT", (object) watch.Video.DmcInfo.SessionApi.Urls[0].Url, (object) sessionRes.Data.Session.Id)));
      ((ICollection<HttpProductInfoHeaderValue>) httpRequestMessage.Headers.UserAgent).Add(((IEnumerable<HttpProductInfoHeaderValue>) context.HttpClient.DefaultRequestHeaders.UserAgent).First<HttpProductInfoHeaderValue>());
      ((IDictionary<string, string>) httpRequestMessage.Headers).Add("Origin", "http://www.nicovideo.jp");
      ((IDictionary<string, string>) httpRequestMessage.Headers).Add("Referer", "http://www.nicovideo.jp/watch/" + watch.Video.Id);
      ((IDictionary<string, string>) httpRequestMessage.Headers).Add("Accept", "application/json");
      string str = JsonConvert.SerializeObject((object) sessionRes.Data, new JsonSerializerSettings()
      {
        NullValueHandling = (NullValueHandling) 1
      });
      httpRequestMessage.put_Content((IHttpContent) new HttpStringContent(str, (UnicodeEncoding) 0, "application/json"));
      int num = (await context.GetClient().SendRequestAsync(httpRequestMessage, (HttpCompletionOption) 1)).IsSuccessStatusCode ? 1 : 0;
    }

    public static async Task DmcSessionLeaveAsync(
      NiconicoContext context,
      DmcWatchResponse watch,
      DmcSessionResponse sessionRes)
    {
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Options, new Uri(string.Format("{0}/{1}?_format=json&_method=DELETE", (object) watch.Video.DmcInfo.SessionApi.Urls[0].Url, (object) sessionRes.Data.Session.Id)));
      ((IDictionary<string, string>) httpRequestMessage.Headers).Add("Access-Control-Request-Method", "POST");
      ((IDictionary<string, string>) httpRequestMessage.Headers).Add("Access-Control-Request-Headers", "content-type");
      ((ICollection<HttpProductInfoHeaderValue>) httpRequestMessage.Headers.UserAgent).Add(((IEnumerable<HttpProductInfoHeaderValue>) context.HttpClient.DefaultRequestHeaders.UserAgent).First<HttpProductInfoHeaderValue>());
      int num = (await context.GetClient().SendRequestAsync(httpRequestMessage, (HttpCompletionOption) 1)).IsSuccessStatusCode ? 1 : 0;
    }

    public static async Task DmcSessionExitHeartbeatAsync(
      NiconicoContext context,
      DmcWatchResponse watch,
      DmcSessionResponse sessionRes)
    {
      HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri(string.Format("{0}/{1}?_format=json&_method=DELETE", (object) watch.Video.DmcInfo.SessionApi.Urls[0].Url, (object) sessionRes.Data.Session.Id)));
      ((IDictionary<string, string>) httpRequestMessage.Headers).Add("Access-Control-Request-Method", "POST");
      ((IDictionary<string, string>) httpRequestMessage.Headers).Add("Access-Control-Request-Headers", "content-type");
      ((ICollection<HttpProductInfoHeaderValue>) httpRequestMessage.Headers.UserAgent).Add(((IEnumerable<HttpProductInfoHeaderValue>) context.HttpClient.DefaultRequestHeaders.UserAgent).First<HttpProductInfoHeaderValue>());
      ((IDictionary<string, string>) httpRequestMessage.Headers).Add("Accept", "application/json");
      string str = JsonConvert.SerializeObject((object) sessionRes.Data, new JsonSerializerSettings()
      {
        NullValueHandling = (NullValueHandling) 1
      });
      httpRequestMessage.put_Content((IHttpContent) new HttpStringContent(str, (UnicodeEncoding) 0, "application/json"));
      int num = (await context.GetClient().SendRequestAsync(httpRequestMessage, (HttpCompletionOption) 1)).IsSuccessStatusCode ? 1 : 0;
    }
  }
}
