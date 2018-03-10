﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using NicoPlayerHohoema.Helpers;
using NicoPlayerHohoema.Models;

namespace NicoPlayerHohoema.ViewModels.PlayerSidePaneContent
{
    public sealed class RelatedVideosSidePaneContentViewModel : SidePaneContentViewModelBase
    {
        string CurrentVideoId { get; }
        public List<VideoInfoControlViewModel> Videos { get; private set; }

        public VideoInfoControlViewModel CurrentVideo { get; private set; }

        Models.VideoRelatedInfomation _VideoViewerHelpInfo;

        public NicoVideo Video { get; }

        public RelatedVideosSidePaneContentViewModel(NicoVideo video)
        {
            Video = video;
            CurrentVideoId = video.RawVideoId;
            _VideoViewerHelpInfo = video.GetVideoRelatedInfomationWithVideoDescription(); ;

            HasVideoDescription = _VideoViewerHelpInfo != null;

            InitializeRelatedVideos().ConfigureAwait(false);
        }

        public bool HasVideoDescription { get; private set; }
        public ObservableCollection<VideoInfoControlViewModel> OtherVideos { get; } = new ObservableCollection<VideoInfoControlViewModel>();
        public VideoInfoControlViewModel NextVideo { get; private set; }

        public ObservableCollection<MylistGroupListItem> Mylists { get; } = new ObservableCollection<MylistGroupListItem>();

        public AsyncLock _InitializeLock = new AsyncLock();

        private bool _IsInitialized = false;
        const double _SeriesVideosTitleSimilarityValue = 0.7;

        public async Task InitializeRelatedVideos()
        {
            if (!HasVideoDescription) { return; }

            using (var releaser = await _InitializeLock.LockAsync())
            {
                if (_IsInitialized) { return; }

                var sourceVideo = Database.NicoVideoDb.Get(CurrentVideoId);

                var videoIds = _VideoViewerHelpInfo.GetVideoIds();
                var hohoemaApp = App.Current.Container.Resolve<Models.HohoemaApp>();

                // 再生中アイテムのタイトルと投稿者説明文に含まれる動画IDの動画タイトルを比較して
                // タイトル文字列が近似する動画をシリーズ動画として取り込む
                // 違うっぽい動画も投稿者が提示したい動画として確保
                List<Database.NicoVideo> seriesVideos = new List<Database.NicoVideo>();
                seriesVideos.Add(sourceVideo);
                foreach (var id in videoIds)
                {
                    var video = await hohoemaApp.ContentProvider.GetNicoVideoInfo(id, requireLatest: true);

                    var titleSimilarity = sourceVideo.Title.CalculateSimilarity(video.Title);
                    if (titleSimilarity > _SeriesVideosTitleSimilarityValue)
                    {
                        seriesVideos.Add(video);
                    }
                    else
                    {
                        OtherVideos.Add(new VideoInfoControlViewModel(video, requireLatest: false));
                    }
                }


                // シリーズ動画として集めたアイテムを投稿日が新しいものが最後尾になるよう並び替え
                // シリーズ動画に番兵として仕込んだ現在再生中のアイテムの位置と動画数を比べて
                // 動画数が上回っていた場合は次動画が最後尾にあると決め打ちで取得する
                var orderedSeriesVideos = seriesVideos.OrderBy(x => x.PostedAt).ToList();
                var currentVideoIndex = orderedSeriesVideos.IndexOf(sourceVideo);
                if (orderedSeriesVideos.Count - 1 > currentVideoIndex)
                {
                    var nextVideo = orderedSeriesVideos.Last();
                    NextVideo = new VideoInfoControlViewModel(nextVideo, requireLatest: false);
                    orderedSeriesVideos.Remove(nextVideo);

                    RaisePropertyChanged(nameof(NextVideo));
                }

                // 次動画を除いてシリーズ動画っぽいアイテムを投稿者が提示したい動画として優先表示されるようにする
                orderedSeriesVideos.Remove(sourceVideo);
                orderedSeriesVideos.Reverse();
                foreach (var video in orderedSeriesVideos)
                {
                    OtherVideos.Insert(0, new VideoInfoControlViewModel(video, requireLatest: false));
                }

                RaisePropertyChanged(nameof(OtherVideos));

                // マイリスト
                var pageManager = App.Current.Container.Resolve<Models.PageManager>();
                var relatedMylistIds = _VideoViewerHelpInfo.GetMylistIds();
                foreach (var mylistId in relatedMylistIds)
                {
                    var mylistDetails = await hohoemaApp.ContentProvider.GetMylistGroupDetail(mylistId);
                    if (mylistDetails.IsOK)
                    {
                        Mylists.Add(new MylistGroupListItem(mylistDetails.MylistGroup, pageManager));
                    }
                }

                RaisePropertyChanged(nameof(Mylists));

                var videos = await Video.GetRelatedVideos();
                Videos = videos.Select(x => new VideoInfoControlViewModel(x, requireLatest: false)).ToList();
                CurrentVideo = Videos.FirstOrDefault(x => x.RawVideoId == CurrentVideoId);

                RaisePropertyChanged(nameof(Videos));
                RaisePropertyChanged(nameof(CurrentVideo));


                _IsInitialized = true;
            }
        }
    }
}