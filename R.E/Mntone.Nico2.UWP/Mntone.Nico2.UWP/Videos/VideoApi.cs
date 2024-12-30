// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.VideoApi
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Mylist;
using Mntone.Nico2.Videos.Comment;
using Mntone.Nico2.Videos.Dmc;
using Mntone.Nico2.Videos.Flv;
using Mntone.Nico2.Videos.Histories;
using Mntone.Nico2.Videos.Related;
using Mntone.Nico2.Videos.RemoveHistory;
using Mntone.Nico2.Videos.Search;
using Mntone.Nico2.Videos.Thumbnail;
using Mntone.Nico2.Videos.WatchAPI;
using System;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Videos
{
  public sealed class VideoApi
  {
    private NiconicoContext _context;

    internal VideoApi(NiconicoContext context) => this._context = context;

    public Task<FlvResponse> GetFlvAsync(string requestId)
    {
      return FlvClient.GetFlvAsync(this._context, requestId);
    }

    public Task<ThumbnailResponse> GetThumbnailAsync(string requestId)
    {
      return ThumbnailClient.GetThumbnailAsync(this._context, requestId);
    }

    public Task<HistoriesResponse> GetHistoriesAsync()
    {
      return HistoriesClient.GetHistoriesAsync(this._context);
    }

    public Task<RemoveHistoryResponse> RemoveHistoryAsync(string token, string requestId)
    {
      return RemoveHistoryClient.RemoveHistoryAsync(this._context, token, requestId);
    }

    public Task<RemoveHistoryResponse> RemoveAllHistoriesAsync(string token)
    {
      return RemoveHistoryClient.RemoveAllHistoriesAsync(this._context, token);
    }

    public Task<WatchApiResponse> GetWatchApiAsync(
      string requestId,
      bool forceLowQuality,
      HarmfulContentReactionType harmfulReactType = HarmfulContentReactionType.None)
    {
      return WatchAPIClient.GetWatchApiAsync(this._context, requestId, forceLowQuality, harmfulReactType);
    }

    public Task<CommentResponse> GetCommentAsync(
      int userId,
      string commentServerUrl,
      int threadId,
      bool isKeyRequired)
    {
      return CommentClient.GetCommentAsync(this._context, userId, commentServerUrl, threadId, isKeyRequired);
    }

    public Task<NMSG_Response> GetNMSGCommentAsync(DmcWatchResponse dmcWatchRes)
    {
      return dmcWatchRes.Video.DmcInfo.Thread.ThreadKeyRequired ? CommentClient.GetOfficialVideoNMSGCommentAsync(this._context, (long) dmcWatchRes.Video.DmcInfo.Thread.ThreadId, (long) dmcWatchRes.Video.DmcInfo.Thread.OptionalThreadId, dmcWatchRes.Viewer.Id, dmcWatchRes.Context.Userkey, TimeSpan.FromSeconds((double) dmcWatchRes.Video.Duration)) : CommentClient.GetNMSGCommentAsync(this._context, (long) dmcWatchRes.Video.DmcInfo.Thread.ThreadId, dmcWatchRes.Viewer.Id, dmcWatchRes.Context.Userkey, TimeSpan.FromSeconds((double) dmcWatchRes.Video.Duration));
    }

    public Task<SearchResponse> GetKeywordSearchAsync(
      string keyword,
      uint pageCount,
      Sort sortMethod,
      Order sortDir = Order.Descending)
    {
      return SearchClient.GetKeywordSearchAsync(this._context, keyword, pageCount, sortMethod, sortDir);
    }

    public Task<SearchResponse> GetTagSearchAsync(
      string tag,
      uint pageCount,
      Sort sortMethod,
      Order sortDir = Order.Descending)
    {
      return SearchClient.GetTagSearchAsync(this._context, tag, pageCount, sortMethod, sortDir);
    }

    public Task<NicoVideoResponse> GetRelatedVideoAsync(
      string videoId,
      uint from,
      uint limit,
      Sort sortMethod,
      Order sortDir = Order.Descending)
    {
      return RelatedClient.GetRelatedVideoAsync(this._context, videoId, from, limit, sortMethod, sortDir);
    }

    public Task<PostCommentResponse> PostCommentAsync(
      string commentServerUrl,
      string threadId,
      string ticket,
      int commentCount,
      string comment,
      TimeSpan position,
      string commands)
    {
      return CommentClient.PostCommentAsync(this._context, commentServerUrl, threadId, ticket, commentCount, comment, position, commands);
    }

    public Task<PostCommentResponse> PostCommentAsync(
      string commentServerUrl,
      CommentThread thread,
      string comment,
      TimeSpan position,
      string commands)
    {
      return CommentClient.PostCommentAsync(this._context, commentServerUrl, thread._thread, thread.Ticket, int.Parse(thread.CommentCount) + 1, comment, position, commands);
    }

    public Task<DmcWatchData> GetDmcWatchResponseAsync(
      string requestId,
      HarmfulContentReactionType harmfulReactType = HarmfulContentReactionType.None)
    {
      return DmcClient.GetDmcWatchResponseAsync(this._context, requestId, harmfulReactType);
    }

    public Task<DmcWatchResponse> GetDmcWatchJsonAsync(string requestId, string playlistToken)
    {
      return DmcClient.GetDmcWatchJsonAsync(this._context, requestId, playlistToken);
    }

    public Task<DmcSessionResponse> GetDmcSessionResponse(
      DmcWatchResponse watchData,
      VideoContent videoQuality = null,
      AudioContent audioQuality = null)
    {
      return DmcClient.GetDmcSessionResponseAsync(this._context, watchData, videoQuality, audioQuality);
    }

    public Task DmcSessionFirstHeartbeatAsync(DmcWatchResponse watch, DmcSessionResponse sessionRes)
    {
      return DmcClient.DmcSessionFirstHeartbeatAsync(this._context, watch, sessionRes);
    }

    public Task DmcSessionHeartbeatAsync(DmcWatchResponse watch, DmcSessionResponse sessionRes)
    {
      return DmcClient.DmcSessionHeartbeatAsync(this._context, watch, sessionRes);
    }

    public Task DmcSessionLeaveAsync(DmcWatchResponse watch, DmcSessionResponse sessionRes)
    {
      return DmcClient.DmcSessionLeaveAsync(this._context, watch, sessionRes);
    }

    public Task DmcSessionExitHeartbeatAsync(DmcWatchResponse watch, DmcSessionResponse sessionRes)
    {
      return DmcClient.DmcSessionExitHeartbeatAsync(this._context, watch, sessionRes);
    }
  }
}
