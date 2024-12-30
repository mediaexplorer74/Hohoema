// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.WatchAPI.WatchAPIClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

#nullable disable
namespace Mntone.Nico2.Videos.WatchAPI
{
  internal sealed class WatchAPIClient
  {
    public static async Task<string> GetWatchApiDataAsync(
      NiconicoContext context,
      string requestId,
      bool forceQuality,
      HarmfulContentReactionType harmfulReactType)
    {
      NiconicoRegex.IsVideoId(requestId);
      Dictionary<string, string> dict = new Dictionary<string, string>();
      string str1 = string.Format("{0}{1}", (object) NiconicoUrls.VideoWatchPageUrl, (object) requestId);
      if (forceQuality)
        dict.Add("eco", "1");
      if (harmfulReactType != HarmfulContentReactionType.None)
        dict.Add("watch_harmful", ((uint) harmfulReactType).ToString());
      string uri = str1 + "?" + HttpQueryExtention.DictionaryToQuery((IDictionary<string, string>) dict);
      string watchApiDataAsync;
      try
      {
        HttpClient client = context.GetClient();
        HttpCookiePairHeaderValue cookiePairHeaderValue1 = new HttpCookiePairHeaderValue("watch_html5", "1");
        if (((ICollection<HttpCookiePairHeaderValue>) client.DefaultRequestHeaders.Cookie).Contains(cookiePairHeaderValue1))
          ((ICollection<HttpCookiePairHeaderValue>) client.DefaultRequestHeaders.Cookie).Remove(cookiePairHeaderValue1);
        ((ICollection<HttpCookiePairHeaderValue>) client.DefaultRequestHeaders.Cookie).Add(cookiePairHeaderValue1);
        HttpCookiePairHeaderValue cookiePairHeaderValue2 = new HttpCookiePairHeaderValue("watch_flash", "1");
        HttpCookiePairHeaderValue cookiePairHeaderValue3 = ((IEnumerable<HttpCookiePairHeaderValue>) client.DefaultRequestHeaders.Cookie).SingleOrDefault<HttpCookiePairHeaderValue>((Func<HttpCookiePairHeaderValue, bool>) (x => x.Name == "watch_flash"));
        if (cookiePairHeaderValue3 != null)
          ((ICollection<HttpCookiePairHeaderValue>) client.DefaultRequestHeaders.Cookie).Remove(cookiePairHeaderValue3);
        ((ICollection<HttpCookiePairHeaderValue>) client.DefaultRequestHeaders.Cookie).Add(cookiePairHeaderValue2);
        HttpResponseMessage async = await context.GetClient().GetAsync(uri);
        if (async.StatusCode == 403)
          throw new WebException("require payment.");
        string str2 = await WindowsRuntimeSystemExtensions.AsTask<string, ulong>(async.Content.ReadAsStringAsync());
        HtmlDocument htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(str2);
        watchApiDataAsync = htmlDocument.GetElementbyId("PAGECONTAINER") == null ? WebUtility.HtmlDecode(htmlDocument.GetElementbyId("watchAPIDataContainer").InnerText) : throw new ContentZoningException("access once blocked, maybe harmful video.");
      }
      catch (ContentZoningException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new WebException("access failed watch/" + requestId, ex);
      }
      return watchApiDataAsync;
    }

    public static WatchApiResponse ParseWatchApi(string flvData)
    {
      JsonSerializer jsonSerializer = new JsonSerializer();
      jsonSerializer.NullValueHandling = (NullValueHandling) 0;
      jsonSerializer.Error += new EventHandler<ErrorEventArgs>(WatchAPIClient.JsonSerializer_Error);
      jsonSerializer.DefaultValueHandling = (DefaultValueHandling) 0;
      return jsonSerializer.Deserialize<WatchApiResponse>((JsonReader) new JsonTextReader((TextReader) new StringReader(flvData)));
    }

    public static Task<WatchApiResponse> GetWatchApiAsync(
      NiconicoContext context,
      string requestId,
      bool forceLowQuality,
      HarmfulContentReactionType harmfulReactType)
    {
      return WatchAPIClient.GetWatchApiDataAsync(context, requestId, forceLowQuality, harmfulReactType).ContinueWith<WatchApiResponse>((Func<Task<string>, WatchApiResponse>) (prevTask => WatchAPIClient.ParseWatchApi(prevTask.Result)));
    }

    private static void JsonSerializer_Error(object sender, ErrorEventArgs e)
    {
    }
  }
}
