// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Searches.Video.VideoSearchClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Searches.Video
{
  public sealed class VideoSearchClient
  {
    public static async Task<string> GetVideoDataAsync(NiconicoContext context, string videoId)
    {
      return await context.GetStringAsync("http://api.ce.nicovideo.jp/nicoapi/v1/video.info", new Dictionary<string, string>()
      {
        {
          "__format",
          "json"
        },
        {
          "v",
          videoId
        }
      });
    }

    private static VideoInfoResponse ParseVideoInfoResponseJson(string json)
    {
      return JsonSerializerExtensions.Load<VideoInfoResponseContainer>(json).NicovideoVideoResponse;
    }

    public static Task<VideoInfoResponse> GetVideoInfoAsync(NiconicoContext context, string videoId)
    {
      return VideoSearchClient.GetVideoDataAsync(context, videoId).ContinueWith<VideoInfoResponse>((Func<Task<string>, VideoInfoResponse>) (prevTask => VideoSearchClient.ParseVideoInfoResponseJson(prevTask.Result)));
    }

    public static async Task<string> GetKeywordSearchDataAsync(
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
        query.Add(nameof (order), order.Value == Order.Ascending ? "a" : "d");
      if (sort.HasValue)
        query.Add(nameof (sort), sort.Value.ToShortString());
      return await context.GetStringAsync("http://api.ce.nicovideo.jp/nicoapi/v1/video.search", query);
    }

    public static async Task<string> GetTagSearchDataAsync(
      NiconicoContext context,
      string tag,
      uint from,
      uint limit,
      Sort? sort,
      Order? order)
    {
      Dictionary<string, string> query = new Dictionary<string, string>();
      query.Add("__format", "json");
      query.Add(nameof (tag), tag);
      query.Add(nameof (from), from.ToString());
      query.Add(nameof (limit), limit.ToString());
      if (order.HasValue)
        query.Add(nameof (order), order.Value == Order.Ascending ? "a" : "d");
      if (sort.HasValue)
        query.Add(nameof (sort), sort.Value.ToShortString());
      return await context.GetStringAsync("http://api.ce.nicovideo.jp/nicoapi/v1/tag.search", query);
    }

    private static VideoListingResponse ParseVideoResponseJson(string videoSearchResponseJson)
    {
      return JsonSerializerExtensions.Load<VideoListingResponseContainer>(videoSearchResponseJson).nicovideo_video_response;
    }

    public static Task<VideoListingResponse> GetKeywordSearchAsync(
      NiconicoContext context,
      string keyword,
      uint from,
      uint limit,
      Sort? sort,
      Order? order)
    {
      return VideoSearchClient.GetKeywordSearchDataAsync(context, keyword, from, limit, sort, order).ContinueWith<VideoListingResponse>((Func<Task<string>, VideoListingResponse>) (prevTask => VideoSearchClient.ParseVideoResponseJson(prevTask.Result)));
    }

    public static Task<VideoListingResponse> GetTagSearchAsync(
      NiconicoContext context,
      string tag,
      uint from,
      uint limit,
      Sort? sort,
      Order? order)
    {
      return VideoSearchClient.GetTagSearchDataAsync(context, tag, from, limit, sort, order).ContinueWith<VideoListingResponse>((Func<Task<string>, VideoListingResponse>) (prevTask => VideoSearchClient.ParseVideoResponseJson(prevTask.Result)));
    }
  }
}
