// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.Watch.WatchClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

#nullable disable
namespace Mntone.Nico2.Live.Watch
{
  public sealed class WatchClient
  {
    public static Task<string> GetLiveWatchPageHtmlAsync(
      NiconicoContext context,
      string liveId,
      bool forceHtml5 = true)
    {
      HttpClient client = context.GetClient();
      HttpCookiePairHeaderValue cookiePairHeaderValue = new HttpCookiePairHeaderValue("player_version", "leo");
      if (((ICollection<HttpCookiePairHeaderValue>) client.DefaultRequestHeaders.Cookie).Contains(cookiePairHeaderValue))
        ((ICollection<HttpCookiePairHeaderValue>) client.DefaultRequestHeaders.Cookie).Remove(cookiePairHeaderValue);
      ((ICollection<HttpCookiePairHeaderValue>) client.DefaultRequestHeaders.Cookie).Add(cookiePairHeaderValue);
      return client.GetStringAsync(NiconicoUrls.Live2WatchPageUrl + liveId);
    }

    private static LeoPlayerProps ParseLeoPlayerProps(string html)
    {
      int startIndex1 = html.IndexOf("var leoPlayerProps = {");
      if (startIndex1 == -1)
        return (LeoPlayerProps) null;
      int num1 = html.IndexOf("};", startIndex1);
      int count1 = startIndex1 + "var leoPlayerProps = {".Length;
      int num2 = num1 - 2;
      string str = new string(html.Skip<char>(count1).Take<char>(num2 - count1).ToArray<char>()).Replace("unescapeHTML(", "").Replace("JSON.parse(", "").Replace("streamQuality: LeoPlayer.getStreamQuality(window.location.hash),", "").Replace(")", "").Replace(" * 1000", "");
      Func<string, string, string> func = (Func<string, string, string>) ((text, targetString) =>
      {
        int startIndex2 = text.IndexOf(targetString);
        if (startIndex2 == -1)
          return text;
        int count2 = text.IndexOf("},", startIndex2) + 2 - startIndex2;
        return text.Remove(startIndex2, count2);
      });
      return JsonConvert.DeserializeObject<LeoPlayerPropsContainer>(string.Format("{{ leoPlayerProps : {{{0}}} }}", (object) func(func(str, "externalLayout"), "externalClient")), new JsonSerializerSettings())?.LeoPlayerProps;
    }

    public static Task<LeoPlayerProps> GetLeoPlayerPropsAsync(
      NiconicoContext context,
      string liveId)
    {
      return WatchClient.GetLiveWatchPageHtmlAsync(context, liveId).ContinueWith<LeoPlayerProps>((Func<Task<string>, LeoPlayerProps>) (prevTask => WatchClient.ParseLeoPlayerProps(prevTask.Result)));
    }
  }
}
