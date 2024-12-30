// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Communities.CommunityApi
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Communities.Detail;
using Mntone.Nico2.Communities.Icon;
using Mntone.Nico2.Communities.Info;
using Mntone.Nico2.Communities.Live;
using Mntone.Nico2.Communities.Video;
using Mntone.Nico2.Videos.Ranking;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Communities
{
  public sealed class CommunityApi
  {
    private NiconicoContext _context;

    internal CommunityApi(NiconicoContext context) => this._context = context;

    public Task<byte[]> GetIconAsync(string requestCommunityId)
    {
      return IconClient.GetIconAsync(this._context, requestCommunityId);
    }

    public Task<NicovideoCommunityResponse> GetCommunifyInfoAsync(string communityId)
    {
      return InfoClient.GetCommunityInfoAsync(this._context, communityId);
    }

    public Task<CommunityDetailResponse> GetCommunityDetailAsync(string communityId)
    {
      return DetailClient.GetCommunityDetailAsync(this._context, communityId);
    }

    public Task<NicoliveVideoResponse> GetCommunityLiveInfoAsync(string communityId)
    {
      return LiveInfoClient.GetCommunityLiveInfo(this._context, communityId);
    }

    public Task<NiconicoVideoRss> GetCommunityVideoAsync(string communityId, uint page)
    {
      return CommunityVideoClient.GetCommunityVideosAsync(this._context, communityId, page);
    }
  }
}
