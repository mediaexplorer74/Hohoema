﻿using Hohoema.Models.Domain;
using Hohoema.Models.Domain.Niconico.Video;
using Hohoema.Models.Domain.VideoCache;
using Hohoema.Models.Helpers;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;

namespace Hohoema.Models.UseCase.VideoCache
{
    public sealed class VideoCacheFolderManager
    {
        public const string CACHE_FOLDER_NAME = "Hohoema_Videos";

        private readonly VideoCacheManager _videoCacheManager;
        private readonly NicoVideoProvider _nicoVideoProvider;

        public VideoCacheFolderManager(
            VideoCacheManager vIdeoCacheManager,
            NicoVideoProvider nicoVideoProvider
            )
        {
            _videoCacheManager = vIdeoCacheManager;
            _nicoVideoProvider = nicoVideoProvider;

            _messenger = WeakReferenceMessenger.Default;
        }

        IMessenger _messenger;

        public StorageFolder VideoCacheFolder => _videoCacheManager.VideoCacheFolder;

        public async Task InitializeAsync()
        {
            // キャッシュフォルダの指定
            if (StorageApplicationPermissions.FutureAccessList.ContainsItem(CACHE_FOLDER_NAME))
            {
                _videoCacheManager.VideoCacheFolder = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(CACHE_FOLDER_NAME);
            }
            else
            {
                var folder = await DownloadsFolder.CreateFolderAsync(CACHE_FOLDER_NAME, CreationCollisionOption.GenerateUniqueName);
                StorageApplicationPermissions.FutureAccessList.AddOrReplace(CACHE_FOLDER_NAME, folder);
                _videoCacheManager.VideoCacheFolder = folder;
            }

            // キャッシュの暗号化を初期化
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/VideoCache_EncryptionKey_32byte.txt"));
            var bytes = await file.ReadBytesAsync();
            _videoCacheManager.SetXts(XTSSharp.XtsAes128.Create(bytes.Take(32).ToArray()));


            // キャッシュファイル名の解決方法を設定
            VideoCacheManager.ResolveVideoTitle = async (id) =>
            {
                var item = await _nicoVideoProvider.GetNicoVideoInfo(id);
                return item.Title;
            };
        }

        public async Task ChangeVideoCacheFolder()
        {
            // フォルダを取得
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Downloads;
            folderPicker.FileTypeFilter.Add("*");

            var newFolder = await folderPicker.PickSingleFolderAsync();
            if (newFolder == null) { return; }

            await _messenger.Send<Events.StartCacheSaveFolderChangingAsyncRequestMessage>();

            try
            {
                // 現在進行中のキャッシュ情報を取得
                // 全てのキャッシュ更新を一時停止
                var resumeInfo = await _videoCacheManager.PauseAllDownloadOperationAsync();

                // 新しいフォルダに存在するキャッシュ済みファイルを取り込む
                await ImportCacheRequestFromNewFolderItems(newFolder);

                // フォルダのアイテムの移動を開始
                var oldFolder = _videoCacheManager.VideoCacheFolder;
                await MoveFolderItemsToDestination(oldFolder, newFolder);

                // VideoCacheManagerのフォルダ指定を変更
                _videoCacheManager.VideoCacheFolder = newFolder;

                // 停止したキャッシュを再開
                foreach (var resume in resumeInfo.PausedVideoIdList)
                {
                    await _videoCacheManager.PushCacheRequestAsync(resume, NicoVideoQuality.Unknown);
                }

                // 新しい指定フォルダをFutureAccessListへ登録
                StorageApplicationPermissions.FutureAccessList.AddOrReplace(CACHE_FOLDER_NAME, newFolder);
            }
            finally
            {
                _messenger.Send<Events.EndCacheSaveFolderChangingMessage>();
            }
        }

        private async Task ImportCacheRequestFromNewFolderItems(StorageFolder folder)
        {
            var query = folder.CreateFileQueryWithOptions(new (Windows.Storage.Search.CommonFileQuery.DefaultQuery, new[] { VideoCacheManager.HohoemaVideoCacheExt }));

            List<Exception> exceptions = new List<Exception>();
            var count = await query.GetItemCountAsync();
            var progressCount = 0;
            while (count > progressCount)
            {
                var files = await query.GetFilesAsync((uint)progressCount, 50);
                foreach (var file in files)
                {
                    try
                    {
                        if (ExtractionVideoIdFromVideoFileName(file, out var videoid, out var quality))
                        {
                            await _videoCacheManager.ImportCacheRequestAsync(videoid, quality, file);
                        }
                    }
                    catch (Exception e)
                    {
                        exceptions.Add(e);
                    }
                }

                progressCount += files.Count;
            }
        }

        private bool ExtractionVideoIdFromVideoFileName(StorageFile file, out string outVideoId, out NicoVideoQuality quality)
        {
            var woExtName = Path.ChangeExtension(file.Name, null);
            var startBracket = woExtName.LastIndexOf('[');
            var endBracket = woExtName.LastIndexOf(']');
            if (startBracket != -1 && endBracket != -1)
            {
                var idAndQuality = woExtName.Substring(startBracket + 1, endBracket - startBracket - 1);
                var splited = idAndQuality.Split('-');
                outVideoId = splited[0];
                quality = splited[1].ToLower() switch
                {
                    "mobile" => NicoVideoQuality.Mobile,
                    "low" => NicoVideoQuality.Low,
                    "midium" => NicoVideoQuality.Midium,
                    "high" => NicoVideoQuality.High,
                    "superhigh" => NicoVideoQuality.SuperHigh,
                    _ => NicoVideoQuality.Unknown,
                };
                return true;
            }
            else
            {
                outVideoId = null;
                quality = NicoVideoQuality.Unknown;
                return false;
            }
        }




        private async Task MoveFolderItemsToDestination(StorageFolder source, StorageFolder dest)
        {
            var query = source.CreateFileQueryWithOptions(new (
                Windows.Storage.Search.CommonFileQuery.DefaultQuery, 
                new[] { VideoCacheManager.HohoemaVideoCacheExt, VideoCacheManager.HohoemaVideoCacheHashExt })
                );

            List<Exception> exceptions = new List<Exception>();
            var count = await query.GetItemCountAsync();
            var progressCount = 0;
            while (count > progressCount)
            {
                var files = await query.GetFilesAsync((uint)progressCount, 50);
                foreach (var file in files)
                {
                    try
                    {
                        await file.MoveAsync(dest);
                    }
                    catch (Exception e)
                    {
                        exceptions.Add(e);

                        // TODO: 移動に失敗したファイルのDBアイテムを更新してFailedにマークしておく
                    }
                }

                progressCount += files.Count;
            }
        }
    }
}