// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Communities.Live.LiveInfoClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Communities.Live
{
  public sealed class LiveInfoClient
  {
    public static Task<string> GetCommunityLiveInfoDataAsync(
      NiconicoContext context,
      string community_id)
    {
      return context.GetStringAsync("http://api.ce.nicovideo.jp/liveapi/v1/community.video", new Dictionary<string, string>()
      {
        {
          "__format",
          "json"
        },
        {
          nameof (community_id),
          community_id
        }
      });
    }

    private static NicoliveVideoResponse ParseCommunityLiveInfoJson(string json)
    {
      return JsonSerializerExtensions.Load<CommunityLiveInfoResponse>(json).NicoliveVideoResponse;
    }

    public static Task<NicoliveVideoResponse> GetCommunityLiveInfo(
      NiconicoContext context,
      string communityId)
    {
      return LiveInfoClient.GetCommunityLiveInfoDataAsync(context, communityId).ContinueWith<NicoliveVideoResponse>((Func<Task<string>, NicoliveVideoResponse>) (prevTask => LiveInfoClient.ParseCommunityLiveInfoJson(prevTask.Result)));
    }
  }
}
