﻿using Hohoema.Models.Domain;
using Hohoema.Models.Domain.Application;
using Hohoema.Models.Domain.Niconico.Video;
using Hohoema.Models.Domain.Player.Video;
using Hohoema.Models.Domain.Player.Video.Cache;
using Hohoema.Models.Domain.VideoCache;
using Hohoema.Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage.AccessCache;

namespace Hohoema.Models.UseCase.Migration
{
    internal sealed class VideoCacheDatabaseMigration_V_0_29_0 : IMigrateAsync
    {
        private readonly AppFlagsRepository _appFlagsRepository;
        private readonly CacheRequestRepository _cacheRequestRepositoryLegacy;
        private readonly VideoCacheManager _videoCacheManager;
        private readonly DialogService _dialogService;

        public VideoCacheDatabaseMigration_V_0_29_0(
            Domain.Application.AppFlagsRepository appFlagsRepository,
            Domain.Player.Video.Cache.CacheRequestRepository cacheRequestRepositoryLegacy,
            Domain.VideoCache.VideoCacheManager videoCacheManager,
            Presentation.Services.DialogService dialogService
            )
        {
            _appFlagsRepository = appFlagsRepository;
            _cacheRequestRepositoryLegacy = cacheRequestRepositoryLegacy;
            _videoCacheManager = videoCacheManager;
            _dialogService = dialogService;
        }

        public async Task MigrateAsync()
        {
            if (_appFlagsRepository.IsCacheVideosMigrated_V_0_29_0 == true)
            {
                return;
            }

            _appFlagsRepository.IsCacheVideosMigrated_V_0_29_0 = true;

            // 保存先フォルダを移行
            try
            {
                if (StorageApplicationPermissions.FutureAccessList.ContainsItem(VideoCacheSaveFolderManager.FolderAccessToken))
                {
                    var folder = await StorageApplicationPermissions.FutureAccessList.GetFolderAsync(VideoCacheSaveFolderManager.FolderAccessToken);
                    StorageApplicationPermissions.FutureAccessList.Remove(VideoCacheSaveFolderManager.FolderAccessToken);

                    StorageApplicationPermissions.FutureAccessList.AddOrReplace(VideoCache.VideoCacheFolderManager.CACHE_FOLDER_NAME, folder);
                }
            }
            catch { }

            // DB的な統合をやる
            foreach (var regacyItem in _cacheRequestRepositoryLegacy.GetRange(0, int.MaxValue))
            {
                await _videoCacheManager.PushCacheRequest_Legacy(regacyItem.VideoId, ToNewQuality(regacyItem.PriorityQuality));
            }
        }


        private static NicoVideoQuality ToNewQuality(NicoVideoQuality_Legacy legacy)
        {
            return legacy switch
            {
                NicoVideoQuality_Legacy.Dmc_SuperHigh => NicoVideoQuality.SuperHigh,
                NicoVideoQuality_Legacy.Dmc_High => NicoVideoQuality.High,
                NicoVideoQuality_Legacy.Dmc_Midium => NicoVideoQuality.Midium,
                NicoVideoQuality_Legacy.Dmc_Low => NicoVideoQuality.Low,
                NicoVideoQuality_Legacy.Dmc_Mobile => NicoVideoQuality.Mobile,
                NicoVideoQuality_Legacy.Smile_Low => NicoVideoQuality.Low,
                NicoVideoQuality_Legacy.Smile_Original => NicoVideoQuality.SuperHigh,
                _ => NicoVideoQuality.Unknown,
            };
        }
    }
}
