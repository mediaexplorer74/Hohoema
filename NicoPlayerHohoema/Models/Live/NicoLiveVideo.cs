﻿using Mntone.Nico2;
using Mntone.Nico2.Live;
using Mntone.Nico2.Live.PlayerStatus;
using Mntone.Nico2.Videos.Comment;
using NicoPlayerHohoema.Util;
using NicoVideoRtmpClient;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Media.Core;

namespace NicoPlayerHohoema.Models.Live
{
	public class NicoLiveVideo : BindableBase, IDisposable
	{
		public HohoemaApp HohoemaApp { get; private set; }


		/// <summary>
		/// 生放送コンテンツID
		/// </summary>
		public string LiveId { get; private set; }


		/// <summary>
		/// 生放送の配信・視聴のためのメタ情報
		/// </summary>
		public PlayerStatusResponse PlayerStatusResponse { get; private set; }


		private MediaStreamSource _VideoStreamSource;
		private AsyncLock _VideoStreamSrouceAssignLock = new AsyncLock();


		/// <summary>
		/// 生放送の動画ストリーム<br />
		/// 生放送によってはRTMPで流れてくる動画ソースの場合と、ニコニコ動画の任意動画をソースにする場合がある。
		/// </summary>
		public MediaStreamSource VideoStreamSource
		{
			get { return _VideoStreamSource; }
			set { SetProperty(ref _VideoStreamSource, value); }
		}



		private ObservableCollection<Chat> _LiveComments;

		/// <summary>
		/// 受信した生放送コメント<br />
		/// </summary>
		/// <remarks>NicoLiveCommentClient.CommentRecieved</remarks>
		public ReadOnlyObservableCollection<Chat> LiveComments { get; private set; }




		private uint _CommentCount;
		public uint CommentCount
		{
			get { return _CommentCount; }
			private set { SetProperty(ref _CommentCount, value); }
		}

		private uint _WatchCount;
		public uint WatchCount
		{
			get { return _WatchCount; }
			private set { SetProperty(ref _WatchCount, value); }
		}


		
		private LiveStatusType? _LiveStatusType;
		public LiveStatusType? LiveStatusType
		{
			get { return _LiveStatusType; }
			private set { SetProperty(ref _LiveStatusType, value); }
		}

		/// <summary>
		/// 生放送動画をRTMPで受け取るための通信クライアント<br />
		/// RTMPで正常に動画が受信できる状態になった場合 VideoStreamSource にインスタンスが渡される
		/// </summary>
		NicovideoRtmpClient _RtmpClient;


		



		/// <summary>
		/// 生放送コメント関連の通信クライアント<br />
		/// 生放送コメントの受信と送信<br />
		/// 接続を維持して有効なコメント送信を行うためのハートビートタスクの実行
		/// </summary>
		NicoLiveCommentClient _NicoLiveCommentClient;



		AsyncLock _LiveSubscribeLock = new AsyncLock();
		

		public NicoLiveVideo(string liveId, HohoemaApp hohoemaApp)
		{
			LiveId = liveId;
			HohoemaApp = hohoemaApp;

			_LiveComments = new ObservableCollection<Chat>();
			LiveComments = new ReadOnlyObservableCollection<Chat>(_LiveComments);
		}

		public void Dispose()
		{
			EndLiveSubscribe().ConfigureAwait(false);
		}

		public async Task<LiveStatusType?> UpdateLiveStatus()
		{
			LiveStatusType = null;

			try
			{
				PlayerStatusResponse = await HohoemaApp.NiconicoContext.Live.GetPlayerStatusAsync(LiveId);
			}
			catch (Exception ex)
			{
				if (ex.HResult == NiconicoHResult.ELiveNotFound)
				{
					LiveStatusType = Live.LiveStatusType.NotFound;
				}
				else if (ex.HResult == NiconicoHResult.ELiveClosed)
				{
					LiveStatusType = Live.LiveStatusType.Closed;
				}
				else if (ex.HResult == NiconicoHResult.ELiveComingSoon)
				{
					LiveStatusType = Live.LiveStatusType.ComingSoon;
				}
				else if (ex.HResult == NiconicoHResult.EMaintenance)
				{
					LiveStatusType = Live.LiveStatusType.Maintenance;
				}
				else if (ex.HResult == NiconicoHResult.ELiveCommunityMemberOnly)
				{
					LiveStatusType = Live.LiveStatusType.CommunityMemberOnly;
				}
				else if (ex.HResult == NiconicoHResult.ELiveFull)
				{
					LiveStatusType = Live.LiveStatusType.Full;
				}
				else if (ex.HResult == NiconicoHResult.ELivePremiumOnly)
				{
					LiveStatusType = Live.LiveStatusType.PremiumOnly;
				}
				else if (ex.HResult == NiconicoHResult.ENotSigningIn)
				{
					LiveStatusType = Live.LiveStatusType.NotLogin;
				}
			}

			if (LiveStatusType != null)
			{
				await EndLiveSubscribe();
			}

			return LiveStatusType;
		}


		public async Task<LiveStatusType?> SetupLive()
		{
			if (PlayerStatusResponse != null)
			{
				PlayerStatusResponse = null;

				await EndLiveSubscribe();

				await Task.Delay(TimeSpan.FromSeconds(1));
			}

			var status = await UpdateLiveStatus();

			if (PlayerStatusResponse != null && status == null)
			{
				await OpenRtmpConnection(PlayerStatusResponse);

				await StartLiveSubscribe();
			}

			return status;
		}

		private async Task StartLiveSubscribe()
		{
			using (var releaser = await _LiveSubscribeLock.LockAsync())
			{
				await StartCommentClientConnection();
			}
		}

		/// <summary>
		/// ニコ生からの離脱処理<br />
		/// HeartbeatAPIへの定期アクセスの停止、及びLeaveAPIへのアクセス
		/// </summary>
		/// <returns></returns>
		public async Task EndLiveSubscribe()
		{
			using (var releaser = await _LiveSubscribeLock.LockAsync())
			{
				// ニコ生サーバーから切断
				CloseRtmpConnection();


				// HeartbeatAPIへのアクセスを停止
				await EndCommentClientConnection();

				// 放送からの離脱APIを叩く
				await HohoemaApp.NiconicoContext.Live.LeaveAsync(LiveId);
			}
		}




		// 
		public async Task<Uri> MakeLiveSummaryHtmlUri()
		{
			if (PlayerStatusResponse == null) { return null; }

			var desc = PlayerStatusResponse.Program.Description;

			return await HtmlFileHelper.PartHtmlOutputToCompletlyHtml(LiveId, desc);
		}





		#region PlayerStatusResponse projection Properties


		public string LiveTitle => PlayerStatusResponse?.Program.Title;

		public string BroadcasterName => PlayerStatusResponse?.Program.BroadcasterName;

		public CommunityType? BroadcasterCommunityType => PlayerStatusResponse?.Program.CommunityType;

		public Uri BroadcasterCommunityImageUri => PlayerStatusResponse?.Program.CommunityImageUrl;

		public string BroadcasterCommunityId => PlayerStatusResponse?.Program.CommunityId;

		

		#endregion





		#region LiveVideo RTMP

		private async Task OpenRtmpConnection(PlayerStatusResponse res)
		{
			_RtmpClient = new NicovideoRtmpClient();

			_RtmpClient.Started += _RtmpClient_Started;
			_RtmpClient.Stopped += _RtmpClient_Stopped;

			await _RtmpClient.ConnectAsync(res);
		}

		private void CloseRtmpConnection()
		{
			if (_RtmpClient != null)
			{
				_RtmpClient.Started -= _RtmpClient_Started;
				_RtmpClient.Stopped -= _RtmpClient_Stopped;

				_RtmpClient?.Dispose();
				_RtmpClient = null;
			}
		}


		private async void _RtmpClient_Started(NicovideoRtmpClientStartedEventArgs args)
		{
			await HohoemaApp.UIDispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
			{
				VideoStreamSource = args.MediaStreamSource;

				Debug.WriteLine("recieve start live stream: " + LiveId);
			});
		}

		private async void _RtmpClient_Stopped(NicovideoRtmpClientStoppedEventArgs args)
		{
			await HohoemaApp.UIDispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
			{
				using (var releaser = await _VideoStreamSrouceAssignLock.LockAsync())
				{
					VideoStreamSource = null;
				}

				Debug.WriteLine("recieve exit live stream: " + LiveId);

				await EndLiveSubscribe();
			});
		}


		#endregion


		#region Live Comment 


		private async Task PostComment(string message, string command)
		{
			if (_NicoLiveCommentClient != null)
			{
				var userId = PlayerStatusResponse.User.Id;
				await _NicoLiveCommentClient.PostComment(message, userId, command);
			}
		}

		private async Task StartCommentClientConnection()
		{
			await EndCommentClientConnection();

			var baseTime = PlayerStatusResponse.Program.BaseAt;
			_NicoLiveCommentClient = new NicoLiveCommentClient(LiveId, baseTime, PlayerStatusResponse.Comment.Server, HohoemaApp.NiconicoContext);
			_NicoLiveCommentClient.CommentServerConnected += _NicoLiveCommentReciever_CommentServerConnected;
			_NicoLiveCommentClient.CommentRecieved += _NicoLiveCommentReciever_CommentRecieved;
			_NicoLiveCommentClient.Heartbeat += _NicoLiveCommentClient_Heartbeat;
			
			await _NicoLiveCommentClient.Start();
		}

		

		private async Task EndCommentClientConnection()
		{
			if (_NicoLiveCommentClient != null)
			{
				await _NicoLiveCommentClient.Stop();

				_NicoLiveCommentClient.CommentServerConnected -= _NicoLiveCommentReciever_CommentServerConnected;
				_NicoLiveCommentClient.CommentPosted -= _NicoLiveCommentReciever_CommentPosted;

				_NicoLiveCommentClient.Dispose();

				_NicoLiveCommentClient = null;
			}
		}

		private void _NicoLiveCommentReciever_CommentServerConnected()
		{
			_NicoLiveCommentClient.CommentPosted += _NicoLiveCommentReciever_CommentPosted;
		}

		private async void _NicoLiveCommentReciever_CommentRecieved(Chat chat)
		{
			await HohoemaApp.UIDispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
			{
				_LiveComments.Add(chat);
			});
		}

		private void _NicoLiveCommentReciever_CommentPosted(bool isSuccess)
		{
			if (isSuccess)
			{
			}
			else
			{
			}
		}

		private async void _NicoLiveCommentClient_Heartbeat(uint commentCount, uint watchCount)
		{
			await HohoemaApp.UIDispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
			{
				CommentCount = commentCount;
				WatchCount = watchCount;
			});
		}


		#endregion
	}
}
