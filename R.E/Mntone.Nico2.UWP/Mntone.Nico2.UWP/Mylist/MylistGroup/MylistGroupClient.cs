// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Mylist.MylistGroup.MylistGroupClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Mylist.MylistGroup
{
  public sealed class MylistGroupClient
  {
    public static Task<string> GetMylistGroupDetailDataAsync(
      NiconicoContext context,
      string group_id,
      bool isNeedDetail)
    {
      return context.GetStringAsync("http://api.ce.nicovideo.jp/nicoapi/v1/mylistgroup.get", new Dictionary<string, string>()
      {
        {
          "__format",
          "json"
        },
        {
          nameof (group_id),
          group_id
        },
        {
          nameof (isNeedDetail),
          isNeedDetail.ToString1Or0()
        }
      });
    }

    public static Task<string> GetMylistGroupVideoDataAsync(
      NiconicoContext context,
      string group_id,
      uint from,
      uint limit,
      Sort sort,
      Order order)
    {
      return context.GetStringAsync("http://api.ce.nicovideo.jp/nicoapi/v1/mylist.list", new Dictionary<string, string>()
      {
        {
          "__format",
          "json"
        },
        {
          nameof (group_id),
          group_id
        },
        {
          nameof (from),
          from.ToString()
        },
        {
          nameof (limit),
          limit.ToString()
        },
        {
          nameof (order),
          order.ToShortString()
        },
        {
          nameof (sort),
          sort.ToShortString()
        }
      });
    }

    private static MylistGroupDetailResponse ParseMylistGroupDetailResponseJson(string json)
    {
      return JsonSerializerExtensions.Load<MylistGroupDetailResponseContainer>(json).nicovideo_mylistgroup_response;
    }

    private static MylistGroupVideoResponse ParseMylistVideoItemsResponseJson(string json)
    {
      return JsonSerializerExtensions.Load<MylistGroupVideoResponseContainer>(json).nicovideo_video_response;
    }

    public static Task<MylistGroupDetailResponse> GetMylistGroupDetailAsync(
      NiconicoContext context,
      string group_id,
      bool isNeedDetail)
    {
      return MylistGroupClient.GetMylistGroupDetailDataAsync(context, group_id, isNeedDetail).ContinueWith<MylistGroupDetailResponse>((Func<Task<string>, MylistGroupDetailResponse>) (prevTask => MylistGroupClient.ParseMylistGroupDetailResponseJson(prevTask.Result)));
    }

    public static Task<MylistGroupVideoResponse> GetMylistGroupVideoAsync(
      NiconicoContext context,
      string group_id,
      uint from,
      uint limit,
      Sort sort,
      Order order)
    {
      return MylistGroupClient.GetMylistGroupVideoDataAsync(context, group_id, from, limit, sort, order).ContinueWith<MylistGroupVideoResponse>((Func<Task<string>, MylistGroupVideoResponse>) (prevTask => MylistGroupClient.ParseMylistVideoItemsResponseJson(prevTask.Result)));
    }
  }
}
