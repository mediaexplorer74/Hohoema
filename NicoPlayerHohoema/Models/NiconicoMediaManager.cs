﻿using Mntone.Nico2.Videos.Comment;
using Mntone.Nico2.Videos.Thumbnail;
using Mntone.Nico2.Videos.WatchAPI;
using NicoPlayerHohoema.Util;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;

namespace NicoPlayerHohoema.Models
{
	/// <summary>
	/// ニコニコ動画の動画やサムネイル画像、
	/// 動画情報など動画に関わるメディアを管理します
	/// </summary>
	public class NiconicoMediaManager : BindableBase, IDisposable
	{
		static internal async Task<NiconicoMediaManager> Create(HohoemaApp app)
		{
			var man = new NiconicoMediaManager(app);
			man.Context = await VideoDownloadContext.Create(app);

			// ダウンロードリクエストされたアイテムのNicoVideoオブジェクトの作成
			// 及び、リクエストの再構築
			var list = await VideoDownloadContext.LoadDownloadRequestItems().ConfigureAwait(false);
			foreach (var req in list)
			{
				var nicoVideo = await man.GetNicoVideo(req.RawVideoid);
				await nicoVideo.RequestCache(req.Quality);
			}

			Debug.WriteLine($"{list.Count}件のダウンロード待ち状況を復元しました。");

			// キャッシュ済みアイテムのNicoVideoオブジェクトの作成
			var saveFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("video", CreationCollisionOption.OpenIfExists);
			var files = await saveFolder.GetFilesAsync();
			files
				.AsParallel()
				.Where(x => x.Name.EndsWith("_info.json"))
				.Select(x => new String(x.Name.TakeWhile(y => y != '_').ToArray()))
				.ForAll(x => man.GetNicoVideo(x).ConfigureAwait(false));

			return man;
		}

		

		private NiconicoMediaManager(HohoemaApp app)
		{
			_HohoemaApp = app;

			VideoIdToNicoVideo = new Dictionary<string, NicoVideo>();

			_NicoVideoSemaphore = new SemaphoreSlim(1, 1);
		}

		


		


		public static async Task<StorageFolder> GetLocalVideoCacheFolderAsync()
		{
			return await ApplicationData.Current.LocalFolder.CreateFolderAsync("video", CreationCollisionOption.OpenIfExists);
		}

		public async Task DownloadVideoAsync(string rawVideoId)
		{
			var saveFolder = await GetLocalVideoCacheFolderAsync();


			var videoInfo = await _HohoemaApp.NiconicoContext.Video.GetWatchApiAsync(rawVideoId, false);
			var file = await saveFolder.CreateFileAsync(videoInfo.videoDetail.title + ".mp4", CreationCollisionOption.ReplaceExisting);

			System.Diagnostics.Debug.WriteLine($"{videoInfo.videoDetail.title}のダウンロードを開始します。");
			System.Diagnostics.Debug.WriteLine($"{videoInfo.VideoUrl}");



			var downloadTask = DownloadVideo(file, videoInfo.VideoUrl);
			downloadTask.Progress = (progress, current) => 
			{
				var parcent = (float)current.Progress / current.VideoSize * 100.0f;
				System.Diagnostics.Debug.WriteLine($"{current.Progress}/{current.VideoSize}({parcent:0.##})%");

				if (current.VideoSize == current.Progress)
				{
					System.Diagnostics.Debug.WriteLine("done poi");
				}

				_HohoemaApp.NiconicoContext.User.GetInfoAsync();
			};

			downloadTask.Completed = (x, y) => 
			{
				System.Diagnostics.Debug.WriteLine("download done.");

				if (downloadTask.ErrorCode != null)
				{
					System.Diagnostics.Debug.WriteLine(downloadTask.ErrorCode.Message);
				}
			};
		}

		

		public async Task<NicoVideo> GetNicoVideo(string rawVideoId)
		{
			try
			{
				await _NicoVideoSemaphore.WaitAsync().ConfigureAwait(false);

				if (VideoIdToNicoVideo.ContainsKey(rawVideoId))
				{
					return VideoIdToNicoVideo[rawVideoId];
				}
				else
				{
					var nicoVideo = await NicoVideo.Create(_HohoemaApp, rawVideoId, Context);
					VideoIdToNicoVideo.Add(rawVideoId, nicoVideo);
					return nicoVideo;
				}
			}
			finally
			{
				_NicoVideoSemaphore.Release();
			}
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="file"></param>
		/// <param name="videoId"></param>
		/// <returns>progress max is 1000</returns>
		internal IAsyncActionWithProgress<VideoDownloadProgressEventArgs> DownloadVideo(StorageFile file, Uri videoUri)
		{ 

			return AsyncInfo.Run<VideoDownloadProgressEventArgs>(async (token, task) =>
			{
				var stream = await Util.HttpRandomAccessStream.CreateAsync(_HohoemaApp.NiconicoContext.HttpClient, videoUri);

				var arg = new VideoDownloadProgressEventArgs();


				using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
				{
					var buffer = new byte[262144].AsBuffer();

					arg.VideoSize = (uint)stream.Size;

					
					while (stream.Position < arg.VideoSize)
					{
						var downloadSize = (uint) Math.Min(buffer.Capacity, stream.Size);
						await stream.ReadAsync(buffer, downloadSize, Windows.Storage.Streams.InputStreamOptions.None);

						await fileStream.WriteAsync(buffer);

						arg.Progress = (uint)stream.Position;

						task.Report(arg);

						await Task.Delay(1000);
					}
				}
			});
		}



		public void Dispose()
		{
			Context.Dispose();
		}


		private SemaphoreSlim _NicoVideoSemaphore;

		public Dictionary<string, NicoVideo> VideoIdToNicoVideo { get; private set; }

		public VideoDownloadContext Context { get; private set; }
		HohoemaApp _HohoemaApp;
	}


	public delegate void VideoDownloadProgressEvent(VideoDownloadProgressEventArgs args);

	public class VideoDownloadProgressEventArgs
	{
		public uint VideoSize { get; set; }
		public uint Progress { get; set; }
	}

	public sealed class VideoDownloadContext : BindableBase, IDisposable
	{
		static internal async Task<VideoDownloadContext> Create(HohoemaApp hohoemaApp)
		{
			var context = new VideoDownloadContext(hohoemaApp);


			return context;
		}


		

		private VideoDownloadContext(HohoemaApp hohoemaApp)
		{
			_HohoemaApp = hohoemaApp;
			_CacheRequestStack = new ObservableCollection<NicoVideoCacheRequest>();
			CacheRequestStack = new ReadOnlyObservableCollection<NicoVideoCacheRequest>(_CacheRequestStack);

			CurrentPlayingStream = null;
			CurrentDownloadStream = null;
		}

		public async Task<NicoVideoCachedStream> GetPlayingStream(string rawVideoId, NicoVideoQuality quality)
		{
			CloseCurrentPlayingStream();

			// 再生用のストリームを取得します
			// すでに開始されたダウンロードタスクは一旦中断し、再生用ストリームに回線を明け渡します。
			// 中断されたダウンロードタスクは、ダウンロードスタックに積み、再生用ストリームのダウンロード完了を確認して
			// ダウンロードを再開させます。		

			// 再生用対象をダウンロード中の場合は
			// キャッシュモードを無視してダウンロード中のストリームをそのまま帰す
			if (_CurrentDownloadStream != null && 
				_CurrentDownloadStream.RawVideoId == rawVideoId)
			{
				CurrentPlayingStream = _CurrentDownloadStream;
			}
			
			// 再生ストリームを作成します
			if (_CurrentPlayingStream == null)
			{
				// 現在のダウンロードタスクは後でダウンロードするようにスタックへ積み直します
				if (_CurrentDownloadStream != null)
				{
					PushToTopCurrentDownloadRequest();
				}

				CurrentPlayingStream = await CreateDownloadStream(rawVideoId, quality, isRequireCache:false);
				
				
				if (!_CurrentPlayingStream.IsCacheComplete)
				{
					await AssignDownloadStream(_CurrentPlayingStream);
				}
				else 
				{
					// call NextDownload 
					await TryBeginNextDownloadRequest();
				}
			}

			return _CurrentPlayingStream;
		}

		

		public async Task RequestDownload(string rawVideoId, NicoVideoQuality quority)
		{
			PushDownloadRequest(rawVideoId, quority);

			await TryBeginNextDownloadRequest();
		}

		public void ClosePlayingStream(string rawVideoId)
		{
			if (_CurrentPlayingStream != null &&
				_CurrentPlayingStream.VideoId == rawVideoId)
			{
				CloseCurrentPlayingStream();
			}
		}

		private void CloseCurrentPlayingStream()
		{
			if (_CurrentPlayingStream != null && !_CurrentPlayingStream.IsRequireCache)
			{
				if (_CurrentDownloadStream == _CurrentPlayingStream)
				{
					CloseCurrentDownloadStream();

					TryBeginNextDownloadRequest().ConfigureAwait(false);
				}
			}

			_CurrentPlayingStream = null;
		}


		private void CloseCurrentDownloadStream()
		{
			if (_CurrentDownloadStream != null)
			{
				OnCacheCompleted?.Invoke(_CurrentDownloadStream.RawVideoId, _CurrentDownloadStream.Quality, false);
				_CurrentDownloadStream.OnCacheComplete -= DownloadCompleteAction;
				_CurrentDownloadStream.Dispose();
				_CurrentDownloadStream = null;
			}
		}


		public void CacnelDownloadRequest(string rawVideoId, NicoVideoQuality quality)
		{
			var req = _CacheRequestStack.SingleOrDefault(x => x.RawVideoid == rawVideoId && x.Quality == quality);
			if (req != null)
			{
				OnCacheCompleted?.Invoke(req.RawVideoid, req.Quality, false);
			}
		}


		public bool CheckCacheRequested(string rawVideoId, NicoVideoQuality quality)
		{
			return _CacheRequestStack.Any(x => x.RawVideoid == rawVideoId && x.Quality == quality);
		}

		public bool CheckVideoDownloading(string rawVideoId, NicoVideoQuality quality)
		{
			if (CurrentDownloadStream == null) { return false; }

			return CurrentDownloadStream.VideoId == rawVideoId && CurrentDownloadStream.Quality == quality;
		}

		private async Task<bool> TryBeginNextDownloadRequest()
		{
			if (_CurrentDownloadStream != null)
			{
				await _CurrentDownloadStream.StopDownload();
				await _CurrentDownloadStream.Download();
				Debug.WriteLine("ダウンロードがすでに実行中のためリクエスト処理は実行できません");
				return false;
			}

			if (_CacheRequestStack.Count == 0)
			{
				return false;
			}

			while(_CacheRequestStack.Count > 0)
			{
				var req = _CacheRequestStack.Last();
				_CacheRequestStack.Remove(req);

				Debug.WriteLine($"{req.RawVideoid}のダウンロードリクエストを処理しています");

				var stream = await CreateDownloadStream(req.RawVideoid, req.Quality, isRequireCache:true);

				if (!stream.IsCacheComplete)
				{
					Debug.WriteLine($"{req.RawVideoid}のダウンロードを開始");

					await AssignDownloadStream(stream);
					break;
				}
				else
				{
					Debug.WriteLine($"{req.RawVideoid}はダウンロード済みのため処理をスキップ");
					stream.Dispose();
				}
			}

			return true;
		}

		private async void DownloadCompleteAction(string rawVideoid)
		{
			if (_CurrentDownloadStream != null &&
				_CurrentDownloadStream.RawVideoId == rawVideoid)
			{
				if (_CurrentDownloadStream != _CurrentPlayingStream)
				{
					_CurrentDownloadStream.Dispose();
				}

				var quality = _CurrentDownloadStream.Quality;
				_CurrentDownloadStream.OnCacheComplete -= DownloadCompleteAction;
				CurrentDownloadStream = null;

				Debug.WriteLine($"{rawVideoid} のダウンロード完了");
				OnCacheCompleted?.Invoke(rawVideoid, quality, true);

				await SaveDownloadRequestItems().ConfigureAwait(false);

				await TryBeginNextDownloadRequest().ConfigureAwait(false);
			}
			else
			{
				throw new Exception("ダウンロードタスクの解除処理が異常");
			}

		}



		private async Task<NicoVideoCachedStream> CreateDownloadStream(string rawVideoid, WatchApiResponse res, NicoVideoQuality quality, bool isRequireCache)
		{
			var saveFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("video", CreationCollisionOption.OpenIfExists);
			return await NicoVideoCachedStream.Create(this._HohoemaApp.NiconicoContext.HttpClient, rawVideoid, res, saveFolder, quality, isRequireCache);
		}


		private async Task<NicoVideoCachedStream> CreateDownloadStream(string rawVideoid, NicoVideoQuality quality, bool isRequireCache)
		{
			var res = await _HohoemaApp.NiconicoContext.Video.GetWatchApiAsync(rawVideoid, quality == NicoVideoQuality.Low);
			return await CreateDownloadStream(rawVideoid, res, quality, isRequireCache);
		}


		private async Task AssignDownloadStream(NicoVideoCachedStream downloadStream)
		{
			CurrentDownloadStream = downloadStream;
			if (!_CurrentDownloadStream.IsCacheComplete)
			{
				_CurrentDownloadStream.OnCacheComplete += DownloadCompleteAction;
				_CurrentDownloadStream.OnCacheProgress += _CurrentDownloadStream_OnCacheProgress;

				OnCacheStarted?.Invoke(downloadStream.RawVideoId, downloadStream.Quality);
			}

			await _CurrentDownloadStream.Download();
		}

		private void _CurrentDownloadStream_OnCacheProgress(string rawVideoId, NicoVideoQuality quality, uint totalSize, uint size)
		{
			OnCacheProgress?.Invoke(rawVideoId, quality, totalSize, size);
		}

		private void PushToTopCurrentDownloadRequest()
		{
			if (_CurrentDownloadStream != null)
			{
				_CacheRequestStack.Add(new NicoVideoCacheRequest()
				{
					RawVideoid = _CurrentDownloadStream.RawVideoId,
					Quality = _CurrentDownloadStream.Quality,
				});

				_CurrentDownloadStream.Dispose();
				CurrentDownloadStream = null;
			}
		}
		private void PushDownloadRequest(string rawVideoid, NicoVideoQuality quality)
		{
			var alreadyRequest = _CacheRequestStack.SingleOrDefault(x => x.RawVideoid == rawVideoid);
			if (alreadyRequest != null)
			{
				_CacheRequestStack.Remove(alreadyRequest);
			}

			_CacheRequestStack.Insert(0, new NicoVideoCacheRequest()
			{
				RawVideoid = rawVideoid,
				Quality = quality,
			});
		}


		public async Task Suspending()
		{
			PushToTopCurrentDownloadRequest();

			await SaveDownloadRequestItems().ConfigureAwait(false);

		}

		public async Task Resume()
		{
			await TryBeginNextDownloadRequest();
		}

		public void Dispose()
		{
			Suspending();
		}

		const string DOWNLOAD_QUEUE_FILENAME = "download_queue.json";

		public async Task SaveDownloadRequestItems()
		{
			var videoFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("video", CreationCollisionOption.OpenIfExists);
			if (_CacheRequestStack.Count > 0)
			{
				var file = await videoFolder.CreateFileAsync(DOWNLOAD_QUEUE_FILENAME, CreationCollisionOption.OpenIfExists);
				var jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(_CacheRequestStack.ToArray());
				await FileIO.WriteTextAsync(file, jsonText);

				Debug.WriteLine("ダウンロード待ち状況を保存しました。");
			}
			else if (File.Exists(Path.Combine(videoFolder.Path, DOWNLOAD_QUEUE_FILENAME)))
			{
				var file = await videoFolder.GetFileAsync(DOWNLOAD_QUEUE_FILENAME);
				await file.DeleteAsync();

				Debug.WriteLine("ダウンロード待ちがないため、状況ファイルを削除しました。");
			}
		}

		public static async Task<List<NicoVideoCacheRequest>> LoadDownloadRequestItems()
		{

			try
			{
				var videoFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("video", CreationCollisionOption.OpenIfExists);
				if (!File.Exists(Path.Combine(videoFolder.Path, DOWNLOAD_QUEUE_FILENAME)))
				{
					return new List<NicoVideoCacheRequest>();
				}

				var file = await videoFolder.GetFileAsync(DOWNLOAD_QUEUE_FILENAME);
				var jsonText = await FileIO.ReadTextAsync(file);
				return Newtonsoft.Json.JsonConvert.DeserializeObject<NicoVideoCacheRequest[]>(jsonText).ToList();
			}
			catch (FileNotFoundException)
			{
				return new List<NicoVideoCacheRequest>();
			}
		}

		private ObservableCollection<NicoVideoCacheRequest> _CacheRequestStack;
		public ReadOnlyObservableCollection<NicoVideoCacheRequest> CacheRequestStack { get; private set; }

		private NicoVideoCachedStream _CurrentPlayingStream;
		public NicoVideoCachedStream CurrentPlayingStream
		{
			get
			{
				return _CurrentPlayingStream;
			}
			private set
			{
				SetProperty(ref _CurrentPlayingStream, value);
			}
		}

		private NicoVideoCachedStream _CurrentDownloadStream;
		public NicoVideoCachedStream CurrentDownloadStream
		{
			get
			{
				return _CurrentDownloadStream;
			}
			private set
			{
				SetProperty(ref _CurrentDownloadStream, value);
			}
		}

		// TODO: アプリ終了時にDownloadStreamを中断して、_CacheRequestStackに積む
		// TODO: _CacheRequestStackの内容を終了時に未ダウンロードアイテムとしてvideoフォルダに書き出す

		public event CacheStartedHandler OnCacheStarted;
		public event CacheCompleteHandler OnCacheCompleted;
		public event CacheProgressHandler OnCacheProgress;

		HohoemaApp _HohoemaApp;
	}

	public delegate void CacheStartedHandler(string rawVideoId, NicoVideoQuality quality);
	public delegate void CacheCompleteHandler(string videoId, NicoVideoQuality quality, bool isSuccess);
	public delegate void CacheProgressHandler(string rawVideoId, NicoVideoQuality quality, uint totalSize, uint size);


	public class NicoVideoCacheRequest : IEquatable<NicoVideoCacheRequest>
	{
		public string RawVideoid { get; set; }
		public NicoVideoQuality Quality { get; set; }

		public override int GetHashCode()
		{
			return RawVideoid.GetHashCode() ^ Quality.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj is NicoVideoCacheRequest)
			{
				return Equals(obj as NicoVideoCacheRequest);
			}
			else
			{
				return false;
			}
		}

		public bool Equals(NicoVideoCacheRequest other)
		{
			return this.RawVideoid == other.RawVideoid && this.Quality == other.Quality;
		}
	}

	public class NicoVideo : BindableBase
	{
		internal static async Task<NicoVideo> Create(HohoemaApp app, string rawVideoid, VideoDownloadContext context)
		{
			Debug.WriteLine("start initialize : " + rawVideoid);
			var nicoVideo = new NicoVideo(app, rawVideoid, context);

			await nicoVideo.GetThumbnailInfo();
			await nicoVideo.SetupVideoInfoFromLocal();
			await nicoVideo.CheckCacheStatus();

			nicoVideo.VideoId = nicoVideo.CachedThumbnailInfo.Id;
			nicoVideo.Title = nicoVideo.CachedWatchApiResponse?.videoDetail.title ?? nicoVideo.CachedThumbnailInfo.Title;
			return nicoVideo;

		}

		private NicoVideo(HohoemaApp app, string rawVideoid, VideoDownloadContext context)
		{
			HohoemaApp = app;
			RawVideoId = rawVideoid;
			_Context = context;

			_ThumbnailInfoFileWriteSemaphore = new SemaphoreSlim(1, 1);
			_VideoInfoFileWriteSemaphore = new SemaphoreSlim(1, 1);
			_CommentFileWriteSemaphore = new SemaphoreSlim(1, 1);

			OriginalQualityCacheState = NicoVideoCacheState.Incomplete;
			LowQualityCacheState = NicoVideoCacheState.Incomplete;
		}



		public async Task CheckCacheStatus()
		{
			// すでにダウンロード済みのキャッシュファイルをチェック
			var saveFolder = await NiconicoMediaManager.GetLocalVideoCacheFolderAsync();

			if (NicoVideoCachedStream.ExistOriginalQuorityVideo(Title, VideoId, saveFolder))
			{
				OriginalQualityCacheState = NicoVideoCacheState.Cached;
			}
			else if(_Context.CheckCacheRequested(this.RawVideoId, NicoVideoQuality.Original))
			{
				OriginalQualityCacheState = NicoVideoCacheState.CacheRequested;
			}
			else if (_Context.CheckVideoDownloading(this.RawVideoId, NicoVideoQuality.Original))
			{
				OriginalQualityCacheState = NicoVideoCacheState.NowDownloading;
			}
			else // if (NicoVideoCachedStream.ExistIncompleteOriginalQuorityVideo(CachedWatchApiResponse, saveFolder))
			{
				OriginalQualityCacheState = NicoVideoCacheState.Incomplete;
			}

			CanRequestDownloadOriginalQuality = OriginalQualityCacheState == NicoVideoCacheState.Incomplete;


			if (NicoVideoCachedStream.ExistLowQuorityVideo(Title, VideoId, saveFolder))
			{
				LowQualityCacheState = NicoVideoCacheState.Cached;
			}
			else if (_Context.CheckCacheRequested(this.RawVideoId, NicoVideoQuality.Low))
			{
				LowQualityCacheState = NicoVideoCacheState.CacheRequested;
			}
			else if (_Context.CheckVideoDownloading(this.RawVideoId, NicoVideoQuality.Low))
			{
				LowQualityCacheState = NicoVideoCacheState.NowDownloading;
			}
			else // if (NicoVideoCachedStream.ExistIncompleteOriginalQuorityVideo(CachedWatchApiResponse, saveFolder))
			{
				LowQualityCacheState = NicoVideoCacheState.Incomplete;
			}

			CanRequestDownloadLowQuality = LowQualityCacheState == NicoVideoCacheState.Incomplete;

		}

		// コメントのキャッシュまたはオンラインからの取得と更新
		public async Task<CommentResponse> GetComment(bool requierLatest = false)
		{
			if (_CachedCommentResponse == null || requierLatest)
			{
				var comment = await GetCommentFromOnline();

				if (comment != null)
				{
					_CachedCommentResponse = comment;
				}
			}

			if (_CachedCommentResponse == null)
			{
				_CachedCommentResponse = await GetCommentFromLocal();
			}

			return _CachedCommentResponse;
		}

		public async Task<CommentResponse> GetCommentFromOnline()
		{
			CommentResponse comment = null;
			try
			{
				comment = await ConnectionRetryUtil.TaskWithRetry(async () => 
				{
					return await this.HohoemaApp.NiconicoContext.Video
						.GetCommentAsync(CachedWatchApiResponse);
				});
			}
			catch { }

			return comment;
		}

		public async Task<CommentResponse> GetCommentFromLocal()
		{
			var fileName = $"{RawVideoId}_comment.json";
			var saveFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("video", CreationCollisionOption.OpenIfExists);

			if (!System.IO.File.Exists(Path.Combine(saveFolder.Path, fileName)))
			{
				return null;
			}

			CommentResponse comment = null;
			StorageFile commentFile;
			commentFile = await saveFolder.GetFileAsync(fileName);
			string jsonText;
			try
			{
				await _CommentFileWriteSemaphore.WaitAsync();
				jsonText = await FileIO.ReadTextAsync(commentFile);
			}
			finally
			{
				_CommentFileWriteSemaphore.Release();
			}

			comment = Newtonsoft.Json.JsonConvert.DeserializeObject<CommentResponse>(jsonText);

			return comment;
		}

		public async Task SaveComment(CommentResponse comment)
		{
			var fileName = $"{RawVideoId}_comment.json";
			var saveFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("video", CreationCollisionOption.OpenIfExists);

			var jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(comment);
			var commentFile = await saveFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
			// コメントデータをファイルに保存
			try
			{
				await _CommentFileWriteSemaphore.WaitAsync();
				await FileIO.WriteTextAsync(commentFile, jsonText);
			}
			finally
			{
				_CommentFileWriteSemaphore.Release();
			}
		}

		private async Task SetupForceLowQuality()
		{
			_CachedWatchApiResponse = await HohoemaApp.NiconicoContext.Video.GetWatchApiAsync(RawVideoId, forceLowQuality:true);
		}

		public async Task<ThumbnailResponse> GetThumbnailInfo()
		{
			if (_CachedThumbnailInfo == null || !_IsLatestThumbnailResponse)
			{
				var thumb = await GetThumbnailInfoFromOnline();

				if (thumb != null)
				{
					_CachedThumbnailInfo = thumb;
					_IsLatestThumbnailResponse = true;
				}
			}

			if (_CachedThumbnailInfo == null)
			{
				_CachedThumbnailInfo = await GetThumbnailInfoFromLocal();
				_IsLatestThumbnailResponse = false;
			}

			if (_CachedThumbnailInfo != null)
			{
				LowQualityVideoSize = (uint)_CachedThumbnailInfo.SizeLow;
				OriginalQualityVideoSize = (uint)_CachedThumbnailInfo.SizeHigh;
			}

			return _CachedThumbnailInfo;
		}
		
		private async Task<ThumbnailResponse> GetThumbnailInfoFromOnline()
		{
			ThumbnailResponse res = null;

			try
			{
				res = await Util.ConnectionRetryUtil.TaskWithRetry(async () =>
				{
					return await HohoemaApp.NiconicoContext.Video.GetThumbnailAsync(RawVideoId);
				});
			}
			catch (Exception e) when (e.Message.Contains("delete"))
			{
				IsDeleted = true;
			}


			return res;
		}

		public async Task<ThumbnailResponse> GetThumbnailInfoFromLocal()
		{
			// ファイルに保存されたデータから動画情報を再現
			var saveFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("video", CreationCollisionOption.OpenIfExists);

			ThumbnailResponse res = null;

			var fileName = $"{RawVideoId}_thumb.json";

			// ファイルが存在するか
			if (!System.IO.File.Exists(Path.Combine(saveFolder.Path, fileName)))
			{
				return null;
			}

			StorageFile videoInfoFile;
			videoInfoFile = await saveFolder.GetFileAsync(fileName);
			string jsonText;
			try
			{
				await _ThumbnailInfoFileWriteSemaphore.WaitAsync();
				jsonText = await FileIO.ReadTextAsync(videoInfoFile);
			}
			finally
			{
				_ThumbnailInfoFileWriteSemaphore.Release();
			}
			res = Newtonsoft.Json.JsonConvert.DeserializeObject<ThumbnailResponse>(jsonText);


			return res;
		}

		public async Task SaveLatestThumbnailInfo()
		{
			var thumb = await GetThumbnailInfoFromOnline();
			if (thumb != null)
			{
				await SaveThumbnailInfo(thumb);
			}
		}

		private async Task SaveThumbnailInfo(ThumbnailResponse res)
		{
			// ファイルに保存されたデータから動画情報を再現
			var saveFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("video", CreationCollisionOption.OpenIfExists);

			var fileName = $"{RawVideoId}_thumb.json";

			var jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(res);
			var videoInfoFile = await saveFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
			try
			{
				await _ThumbnailInfoFileWriteSemaphore.WaitAsync();

				// 動画情報データをファイルに保存
				await FileIO.WriteTextAsync(videoInfoFile, jsonText);
			}
			finally
			{
				_ThumbnailInfoFileWriteSemaphore.Release();
			}
		}


		// 動画情報のキャッシュまたはオンラインからの取得と更新

		public async Task<WatchApiResponse> GetVideoInfo()
		{
			if (_CachedWatchApiResponse == null || !_IsLatestWatchApiResponse)
			{
				// オンラインから動画情報を取得
				var res = await GetVideoInfoFromOnline();

				if (res != null)
				{
					_CachedWatchApiResponse = res;
					_IsLatestWatchApiResponse = true;
					NowLowQualityOnly = _CachedWatchApiResponse.VideoUrl.AbsoluteUri.EndsWith("low");
				}
			}

			if (_CachedWatchApiResponse == null)
			{
				_CachedWatchApiResponse = await GetVideoInfoFromLocal();
				_IsLatestWatchApiResponse = false;
			}

			return _CachedWatchApiResponse;
		}

		private async Task<WatchApiResponse> GetVideoInfoFromOnline()
		{
			WatchApiResponse watchApiRes = null;

			try
			{
				watchApiRes = await Util.ConnectionRetryUtil.TaskWithRetry(async () =>
				{
					return await HohoemaApp.NiconicoContext.Video.GetWatchApiAsync(RawVideoId, forceLowQuality: false);
				});
			}
			catch { }

			return watchApiRes;
		}

		public async Task SetupVideoInfoFromLocal()
		{
			_CachedWatchApiResponse = await GetVideoInfoFromLocal();
			_IsLatestWatchApiResponse = false;
		}

		public async Task<WatchApiResponse> GetVideoInfoFromLocal()
		{
			var fileName = $"{RawVideoId}_info.json";

			var saveFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("video", CreationCollisionOption.OpenIfExists);
			// ファイルが存在するか
			if (!System.IO.File.Exists(Path.Combine(saveFolder.Path, fileName)))
			{
				return null;
			}

			StorageFile videoInfoFile;
			videoInfoFile = await saveFolder.GetFileAsync(fileName);
			string jsonText;
			try
			{
				await _VideoInfoFileWriteSemaphore.WaitAsync();
				jsonText = await FileIO.ReadTextAsync(videoInfoFile);
			}
			finally
			{
				_VideoInfoFileWriteSemaphore.Release();
			}

			return Newtonsoft.Json.JsonConvert.DeserializeObject<WatchApiResponse>(jsonText);
		}

		public async Task SaveLatestVideoInfo()
		{
			var watchApiRes = await GetVideoInfoFromOnline();
			await SaveVideoInfo(watchApiRes);
		}

		private async Task SaveVideoInfo(WatchApiResponse watchApiRes)
		{
			var fileName = $"{RawVideoId}_info.json";

			var saveFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("video", CreationCollisionOption.OpenIfExists);

			var jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(watchApiRes);
			var videoInfoFile = await saveFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
			try
			{
				await _VideoInfoFileWriteSemaphore.WaitAsync();

				// 動画情報データをファイルに保存
				await FileIO.WriteTextAsync(videoInfoFile, jsonText);
			}
			finally
			{
				_VideoInfoFileWriteSemaphore.Release();
			}


			if (RawVideoId != VideoId)
			{
				var aliasDescriptionFilename = $"{RawVideoId}_real_id_is_[{VideoId}]";
				if (!File.Exists(Path.Combine(saveFolder.Path, aliasDescriptionFilename)))
				{
					try
					{
						await saveFolder.CreateFileAsync(aliasDescriptionFilename, CreationCollisionOption.FailIfExists);
					}
					catch { }
				}
			}
		}





		


		/// <summary>
		/// 動画ストリームの取得します
		/// </summary>
		/// <param name="cacheMode"></param>
		/// <returns></returns>
		/// <remarks>既にキャッシュ対象に指定されている場合、cacheModel.NoCacheは無視されます。</remarks>
		public async Task<IRandomAccessStream> GetVideoStream(NicoVideoQuality quality)
		{
			if (quality == NicoVideoQuality.Low)
			{
				await SetupForceLowQuality();
			}

			return await _Context.GetPlayingStream(RawVideoId, quality);
		}

		// TODO: 
		// 動画のキャッシュ要求
		public async Task RequestCache(NicoVideoQuality quality)
		{
			if (CachedWatchApiResponse == null)
			{
				await GetVideoInfo();
			}

			if (quality == NicoVideoQuality.Low || NowLowQualityOnly)
			{
				quality = NicoVideoQuality.Low;
				await SetupForceLowQuality();
			}

			await _Context.RequestDownload(RawVideoId, quality);
			_Context.OnCacheStarted += _Context_OnCacheStarted;
			_Context.OnCacheCompleted += _Context_OnCacheCompleted;
			_Context.OnCacheProgress += _Context_OnCacheProgress;
			await CheckCacheStatus();

			await GetComment(true);
			await SaveComment(CachedCommentResponse);
			await SaveVideoInfo(CachedWatchApiResponse);
			await SaveThumbnailInfo(CachedThumbnailInfo);
		}

		private void _Context_OnCacheStarted(string rawVideoId, NicoVideoQuality quality)
		{
			if (RawVideoId == rawVideoId)
			{
				switch (quality)
				{
					case NicoVideoQuality.Original:
						OriginalQualityCacheState = NicoVideoCacheState.NowDownloading;
						break;
					case NicoVideoQuality.Low:
						LowQualityCacheState = NicoVideoCacheState.NowDownloading;
						break;
					default:
						break;
				}
			}
		}

		private void _Context_OnCacheProgress(string rawVideoId, NicoVideoQuality quality, uint totalSize, uint size)
		{
			if (rawVideoId == RawVideoId)
			{
				switch (quality)
				{
					case NicoVideoQuality.Original:
						OriginalQualityCacheState = NicoVideoCacheState.NowDownloading;
						OriginalQualityCacheProgressSize = size;
						break;
					case NicoVideoQuality.Low:
						LowQualityCacheState = NicoVideoCacheState.NowDownloading;
						LowQualityCacheProgressSize = size;
						break;
					default:
						break;
				}
			}
		}

		private void _Context_OnCacheCompleted(string videoId, NicoVideoQuality quality, bool isSuccess)
		{
			if (videoId == RawVideoId)
			{
				CheckCacheStatus().ConfigureAwait(false);
			}
		}

		public void StopPlay()
		{
			_Context.ClosePlayingStream(VideoId);
		}


		public async Task<bool> HasOriginalQualityIncompleteVideoFile()
		{
			var saveFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("video", CreationCollisionOption.OpenIfExists);
			return NicoVideoCachedStream.ExistIncompleteOriginalQuorityVideo(Title, VideoId, saveFolder);
		}

		public async Task<bool> HasLowQualityIncompleteVideoFile()
		{
			var saveFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("video", CreationCollisionOption.OpenIfExists);
			return NicoVideoCachedStream.ExistIncompleteLowQuorityVideo(Title, VideoId, saveFolder);
		}


		public IList<TagList> GetTags()
		{
			return CachedWatchApiResponse.videoDetail.tagList;
		}




		private bool _IsLatestWatchApiResponse;

		private bool _IsLatestThumbnailResponse;

		private CommentResponse _CachedCommentResponse;
		public CommentResponse CachedCommentResponse
		{
			get
			{
				return _CachedCommentResponse;
			}
		}

		private WatchApiResponse _CachedWatchApiResponse;
		public WatchApiResponse CachedWatchApiResponse
		{
			get
			{
				return _CachedWatchApiResponse;
			}
		}

		private ThumbnailResponse _CachedThumbnailInfo;
		public ThumbnailResponse CachedThumbnailInfo
		{
			get
			{
				return _CachedThumbnailInfo;
			}
		}

		private SemaphoreSlim _ThumbnailInfoFileWriteSemaphore;

		private SemaphoreSlim _VideoInfoFileWriteSemaphore;
		private SemaphoreSlim _CommentFileWriteSemaphore;

		public string VideoId { get; private set; }
		public string RawVideoId { get; private set; }

		public string Title { get; private set; }

		public bool IsDeleted { get; private set; }


		private NicoVideoCacheState _OriginalQualityCacheState;
		public NicoVideoCacheState OriginalQualityCacheState
		{
			get { return _OriginalQualityCacheState; }
			set { SetProperty(ref _OriginalQualityCacheState, value); }
		}

		private NicoVideoCacheState _LowQualityCacheState;
		public NicoVideoCacheState LowQualityCacheState
		{
			get { return _LowQualityCacheState; }
			set { SetProperty(ref _LowQualityCacheState, value); }
		}


		private bool _NowLowQualityOnly;
		public bool NowLowQualityOnly
		{
			get { return _NowLowQualityOnly; }
			set { SetProperty(ref _NowLowQualityOnly, value); }
		}

		private bool _CanRequestDownloadLowQuality;
		public bool CanRequestDownloadLowQuality
		{
			get { return _CanRequestDownloadLowQuality && !NowOffline; }
			set { SetProperty(ref _CanRequestDownloadLowQuality, value); }
		}

		private bool _CanRequestDownloadOriginalQuality;
		public bool CanRequestDownloadOriginalQuality
		{
			get { return _CanRequestDownloadOriginalQuality && !NowLowQualityOnly && !NowOffline; }
			set { SetProperty(ref _CanRequestDownloadOriginalQuality, value); }
		}


		private uint _LowQualityCacheProgressSize;
		public uint LowQualityCacheProgressSize
		{
			get { return _LowQualityCacheProgressSize; }
			set { SetProperty(ref _LowQualityCacheProgressSize, value); }
		}

		private uint _LowQualityVideoSize;
		public uint LowQualityVideoSize
		{
			get { return _LowQualityVideoSize; }
			set { SetProperty(ref _LowQualityVideoSize, value); }
		}

		private uint _OriginalQualityCacheProgressSize;
		public uint OriginalQualityCacheProgressSize
		{
			get { return _OriginalQualityCacheProgressSize; }
			set { SetProperty(ref _OriginalQualityCacheProgressSize, value); }
		}

		private uint _OriginalQualityVideoSize;
		public uint OriginalQualityVideoSize
		{
			get { return _OriginalQualityVideoSize; }
			set { SetProperty(ref _OriginalQualityVideoSize, value); }
		}




		public bool NowOffline { get; private set; }

		public HohoemaApp HohoemaApp { get; private set; }
		VideoDownloadContext _Context;
	}

	public enum NicoVideoCacheState
	{
		Incomplete,
		CacheRequested,
		NowDownloading,
		Cached,
	}

	public enum NicoVideoCanNotDownloadReason
	{
		Unknown,
		Offline,
		NotExist,
		OnlyLowQualityWithoutPremiumUser,
	}

	public enum NicoVideoQuality
	{
		Original,
		Low,
	}
}
