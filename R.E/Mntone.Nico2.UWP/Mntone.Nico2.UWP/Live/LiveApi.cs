// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.LiveApi
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Live.CKey;
using Mntone.Nico2.Live.Description;
using Mntone.Nico2.Live.Heartbeat;
using Mntone.Nico2.Live.Leave;
using Mntone.Nico2.Live.MyPage;
using Mntone.Nico2.Live.OnAirStreams;
using Mntone.Nico2.Live.OtherStreams;
using Mntone.Nico2.Live.PlayerStatus;
using Mntone.Nico2.Live.PostKey;
using Mntone.Nico2.Live.Reservations;
using Mntone.Nico2.Live.ReservationsInDetail;
using Mntone.Nico2.Live.TagRevision;
using Mntone.Nico2.Live.Tags;
using Mntone.Nico2.Live.Vote;
using Mntone.Nico2.Live.Watch;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Live
{
  public sealed class LiveApi
  {
    private NiconicoContext _context;

    internal LiveApi(NiconicoContext context) => this._context = context;

    public Task<string> GetCKeyAsync(string refererId, string requestId)
    {
      return CKeyClient.GetCKeyAsync(this._context, refererId, requestId);
    }

    public Task<DescriptionResponse> GetDescriptionAsync(string requestId)
    {
      return DescriptionClient.GetDescriptionAsync(this._context, requestId);
    }

    public Task<HeartbeatResponse> HeartbeatAsync(string requestId)
    {
      return HeartbeatClient.HeartbeatAsync(this._context, requestId);
    }

    public Task<PlayerStatusResponse> GetPlayerStatusAsync(string requestId)
    {
      return PlayerStatusClient.GetPlayerStatusAsync(this._context, requestId);
    }

    public Task<bool> LeaveAsync(string requestId)
    {
      return LeaveClient.LeaveAsync(this._context, requestId);
    }

    public Task<OnAirStreamsResponse> GetOnAirStreamsIndexAsync()
    {
      return OnAirStreamsClient.GetOnAirStreamsIndexAsync(this._context);
    }

    public Task<OnAirStreamsResponse> GetOnAirStreamsIndexAsync(ushort pageIndex)
    {
      return OnAirStreamsClient.GetOnAirStreamsIndexAsync(this._context, pageIndex);
    }

    public Task<OnAirStreamsResponse> GetOnAirStreamsRecentAsync(
      ushort pageIndex,
      Category category)
    {
      return OnAirStreamsClient.GetOnAirStreamsRecentAsync(this._context, pageIndex, category);
    }

    public Task<OnAirStreamsResponse> GetOnAirStreamsRecentAsync(
      ushort pageIndex,
      Category category,
      Order direction,
      SortType type)
    {
      return OnAirStreamsClient.GetOnAirStreamsRecentAsync(this._context, pageIndex, category, direction, type);
    }

    public Task<OtherStreamsResponse> GetOtherStreamsAsync(StatusType status)
    {
      return OtherStreamsClient.GetOtherStreamsAsync(this._context, status, (ushort) 1);
    }

    public Task<OtherStreamsResponse> GetOtherStreamsAsync(StatusType status, ushort pageIndex)
    {
      return OtherStreamsClient.GetOtherStreamsAsync(this._context, status, pageIndex);
    }

    public Task<IReadOnlyList<string>> GetReservationsAsync()
    {
      return ReservationsClient.GetReservationsAsync(this._context);
    }

    public Task<ReservationsInDetailResponse> GetReservationsInDetailAsync()
    {
      return ReservationsInDetailClient.GetReservationsInDetailAsync(this._context);
    }

    public Task<ushort> GetTagRevisionAsync(string requestId)
    {
      return TagRevisionClient.GetTagRevisionAsync(this._context, requestId);
    }

    public Task<TagsResponse> GetTagsAsync(string requestId)
    {
      return TagsClient.GetTagsAsync(this._context, requestId);
    }

    public Task<MyPageResponse> GetMyPageAsync() => MyPageClient.GetMyPageAsync(this._context);

    public Task<string> GetPostKeyAsync(uint threadId, uint blockNo)
    {
      return PostKeyClient.GetPostKeyAsync(this._context, threadId, blockNo);
    }

    public Task<bool> VoteAsync(string requestId, ushort choiceNumber)
    {
      return VoteClient.VoteAsync(this._context, requestId, choiceNumber);
    }

    public Task<LeoPlayerProps> GetLeoPlayerPropsAsync(string liveId)
    {
      return WatchClient.GetLeoPlayerPropsAsync(this._context, liveId);
    }
  }
}
