// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Communities.Info.InfoClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Communities.Info
{
  public sealed class InfoClient
  {
    public static Task<string> GetCommunityInfoDataAsync(NiconicoContext context, string id)
    {
      return context.GetStringAsync("http://api.ce.nicovideo.jp/api/v1/community.info", new Dictionary<string, string>()
      {
        {
          "__format",
          "json"
        },
        {
          nameof (id),
          id
        }
      });
    }

    private static NicovideoCommunityResponse ParseCommunityInfoResponseJson(string json)
    {
      return JsonSerializerExtensions.Load<CommunityInfoResponseContainer>(json).NicovideoCommunityResponse;
    }

    public static Task<NicovideoCommunityResponse> GetCommunityInfoAsync(
      NiconicoContext context,
      string communityId)
    {
      return InfoClient.GetCommunityInfoDataAsync(context, communityId).ContinueWith<NicovideoCommunityResponse>((Func<Task<string>, NicovideoCommunityResponse>) (prevTask => InfoClient.ParseCommunityInfoResponseJson(prevTask.Result)));
    }
  }
}
