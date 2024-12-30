// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Flv.FlvClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Http;

#nullable disable
namespace Mntone.Nico2.Videos.Flv
{
  internal sealed class FlvClient
  {
    public static async Task<string> GetFlvDataAsync(NiconicoContext context, string requestId)
    {
      HttpResponseMessage async = await context.GetClient().GetAsync(string.Format("{0}{1}", (object) NiconicoUrls.VideoWatchPageUrl, (object) requestId));
      await Task.Delay(1000);
      return await context.GetClient().GetStringAsync(string.Format("{0}{1}?as3=1", (object) NiconicoUrls.VideoFlvUrl, (object) requestId));
    }

    public static async Task<string> GetFlvDataAsync(
      NiconicoContext context,
      string requestId,
      string cKey)
    {
      HttpResponseMessage async = await context.GetClient().GetAsync(string.Format("{0}{1}", (object) NiconicoUrls.VideoWatchPageUrl, (object) requestId));
      await Task.Delay(1000);
      return await context.GetClient().GetStringAsync(string.Format("{0}{1}?as3=1", (object) NiconicoUrls.VideoFlvUrl, (object) requestId));
    }

    public static FlvResponse ParseFlvData(string flvData)
    {
      Dictionary<string, string> dictionary = ((IEnumerable<string>) flvData.Split('&')).ToDictionary<string, string, string>((Func<string, string>) (source => source.Substring(0, source.IndexOf('='))), (Func<string, string>) (source => Uri.UnescapeDataString(source.Substring(source.IndexOf('=') + 1))));
      return !dictionary.ContainsKey("error") ? new FlvResponse(dictionary) : throw new Exception("Parse Error: " + dictionary["error"]);
    }

    public static Task<FlvResponse> GetFlvAsync(NiconicoContext context, string requestId)
    {
      return FlvClient.GetFlvDataAsync(context, requestId).ContinueWith<FlvResponse>((Func<Task<string>, FlvResponse>) (prevTask => FlvClient.ParseFlvData(prevTask.Result)));
    }

    public static Task<FlvResponse> GetFlvAsync(
      NiconicoContext context,
      string requestId,
      string cKey)
    {
      return FlvClient.GetFlvDataAsync(context, requestId, cKey).ContinueWith<FlvResponse>((Func<Task<string>, FlvResponse>) (prevTask => FlvClient.ParseFlvData(prevTask.Result)));
    }
  }
}
