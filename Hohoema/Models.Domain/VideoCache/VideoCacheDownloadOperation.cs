﻿using Hohoema.Models.Helpers;
using Hohoema.Models.Domain.Player.Video;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Espresso3389.HttpStream;

namespace Hohoema.Models.Domain.VideoCache
{
    public class VideoCacheDownloadOperation : IDisposable, IVideoCacheDownloadOperation
    {
        public string VideoId => VideoCacheItem.VideoId;

        public VideoCacheItem VideoCacheItem { get; }

        private readonly VideoCacheManager _videoCacheManager;
        private readonly DmcVideoStreamingSession _dmcVideoStreamingSession;
        private IVideoCacheDownloadOperationOutput _videoCacheDownloadOperationOutput;
        private CancellationTokenSource _cancellationTokenSource;

        public event EventHandler Started;
        public event EventHandler Paused;
        public event EventHandler<VideoCacheDownloadOperationProgress> Progress;
        public event EventHandler Completed;

        internal VideoCacheDownloadOperation(VideoCacheManager videoCacheManager, VideoCacheItem videoCacheItem, DmcVideoStreamingSession dmcVideoStreamingSession, IVideoCacheDownloadOperationOutput videoCacheDownloadOperationOutput)
        {
            _videoCacheManager = videoCacheManager;
            VideoCacheItem = videoCacheItem;
            _dmcVideoStreamingSession = dmcVideoStreamingSession;
            _videoCacheDownloadOperationOutput = videoCacheDownloadOperationOutput;

            _dmcVideoStreamingSession.StopStreamingFromOwnerShipReturned += _dmcVideoStreamingSession_StopStreamingFromOwnerShipReturned;
        }

        private void _dmcVideoStreamingSession_StopStreamingFromOwnerShipReturned(object sender, EventArgs e)
        {
            if (_cancellationTokenSource.IsCancellationRequested is false)
            {
                _cancellationTokenSource.Cancel();

                Paused?.Invoke(this, EventArgs.Empty);
            }
        }

        public async Task DownloadAsync()
        {
            Stream downloadStream = null;
            try
            {
                var uri = await _dmcVideoStreamingSession.GetDownloadUrlAndSetupDownloadSession();
                downloadStream = new HttpStreamForWindows(uri, _dmcVideoStreamingSession.NiconicoSession.Context.HttpClient);
            }
            catch
            {
                downloadStream?.Dispose();
                throw;
            }

            Started?.Invoke(this, EventArgs.Empty);

            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                await _videoCacheDownloadOperationOutput.CopyStreamAsync(downloadStream, new Progress<VideoCacheDownloadOperationProgress>(x => Progress?.Invoke(this, x)), _cancellationTokenSource.Token);
                Completed?.Invoke(this, EventArgs.Empty);
            }
            catch (OperationCanceledException)
            {

            }
            catch (FileLoadException)
            {
                throw;
            }
            catch (Exception)
            {
                // ニコ動サーバー側からタイムアウトで切られた場合は一時停止扱い
                Paused?.Invoke(this, EventArgs.Empty);
            }
            finally
            {
                downloadStream.Dispose();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }

        public async Task StopAndDeleteDownloadedAsync()
        {
            if (_cancellationTokenSource is not null)
            {
                _cancellationTokenSource.Cancel();
            }

            await _videoCacheDownloadOperationOutput.DeleteAsync();
        }

        void IDisposable.Dispose()
        {
            _dmcVideoStreamingSession.StopStreamingFromOwnerShipReturned -= _dmcVideoStreamingSession_StopStreamingFromOwnerShipReturned;
            _dmcVideoStreamingSession.Dispose();
        }

        public Task PauseAsync()
        {
            if (_cancellationTokenSource is not null)
            {
                _cancellationTokenSource.Cancel();

                Paused?.Invoke(this, EventArgs.Empty);
            }

            return Task.CompletedTask;
        }
    }
}
