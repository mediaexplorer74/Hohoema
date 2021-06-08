﻿using Hohoema.Models.Domain.Niconico.Recommend.LoginUser;
using Hohoema.Models.Domain.Niconico.Video;
using Hohoema.Models.Domain.PageNavigation;
using Hohoema.Models.Helpers;
using Hohoema.Models.UseCase;
using Hohoema.Models.UseCase.NicoVideos;
using Hohoema.Models.UseCase.PageNavigation;
using Hohoema.Presentation.ViewModels.VideoListPage;
using Microsoft.Toolkit.Collections;
using Mntone.Nico2.Videos.Recommend;
using Prism.Commands;
using Prism.Navigation;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Hohoema.Presentation.ViewModels.Pages.Niconico.VideoRecommend
{
    public class RecommendPageViewModel : HohoemaListingPageViewModelBase<RecommendVideoListItem>, INavigatedAwareAsync
    {
        public RecommendPageViewModel(
            ApplicationLayoutManager applicationLayoutManager,
            LoginUserRecommendProvider loginUserRecommendProvider,
            HohoemaPlaylist hohoemaPlaylist,
            PageManager pageManager,
            NicoVideoProvider nicoVideoProvider
            )
        {
            ApplicationLayoutManager = applicationLayoutManager;
            LoginUserRecommendProvider = loginUserRecommendProvider;
            HohoemaPlaylist = hohoemaPlaylist;
            PageManager = pageManager;
            _nicoVideoProvider = nicoVideoProvider;
        }

        public ApplicationLayoutManager ApplicationLayoutManager { get; }
        public LoginUserRecommendProvider LoginUserRecommendProvider { get; }
        public HohoemaPlaylist HohoemaPlaylist { get; }
        public PageManager PageManager { get; }
        public ReadOnlyObservableCollection<NicoVideoTag> RecommendSourceTags { get; private set; }
        
        protected override (int, IIncrementalSource<RecommendVideoListItem>) GenerateIncrementalSource()
        {
            var source = new RecommendVideoIncrementalLoadingSource(LoginUserRecommendProvider, _nicoVideoProvider);
            RecommendSourceTags = source.RecommendSourceTags
               .ToReadOnlyReactiveCollection(x => new NicoVideoTag(x));
            RaisePropertyChanged(nameof(RecommendSourceTags));
            return (RecommendVideoIncrementalLoadingSource.OneTimeLoadCount, source);
        }

        private DelegateCommand<string> _OpenTagCommand;
        private readonly NicoVideoProvider _nicoVideoProvider;

        public DelegateCommand<string> OpenTagCommand
        {
            get
            {
                return _OpenTagCommand
                    ?? (_OpenTagCommand = new DelegateCommand<string>((tag) =>
                    {
                        PageManager.Search(SearchTarget.Tag, tag);
                    }));
            }
        }
    }


    public sealed class RecommendVideoListItem : VideoListItemControlViewModel
    {
        Mntone.Nico2.Videos.Recommend.Item _Item;

        public string RecommendSourceTag { get; private set; }


        public RecommendVideoListItem(
            Mntone.Nico2.Videos.Recommend.Item item
            )
            : base(item.Id, item.ParseTitle(), item.ThumbnailUrl, item.ParseLengthToTimeSpan(), item.ParseForstRetroeveToDateTimeOffset().DateTime)
        {
            _Item = item;
            RecommendSourceTag = _Item.AdditionalInfo?.Sherlock.Tag;
        }
    }

    public sealed class RecommendVideoIncrementalLoadingSource : IIncrementalSource<RecommendVideoListItem>
    {
        public RecommendVideoIncrementalLoadingSource(
            LoginUserRecommendProvider loginUserRecommendProvider,
            NicoVideoProvider nicoVideoProvider
            )
        {
            LoginUserRecommendProvider = loginUserRecommendProvider;
            _nicoVideoProvider = nicoVideoProvider;
        }

        public const int OneTimeLoadCount = 18;

        public LoginUserRecommendProvider LoginUserRecommendProvider { get; }
        
        private RecommendResponse _RecommendResponse;
        private RecommendContent _PrevRecommendContent;

        private HashSet<string> _RecommendSourceTagsHashSet = new HashSet<string>();
        public ObservableCollection<string> RecommendSourceTags { get; } = new ObservableCollection<string>();

        private bool _EndOfRecommend = false;
        private readonly NicoVideoProvider _nicoVideoProvider;


        private void AddRecommendTags(IEnumerable<string> tags)
        {
            foreach (var tag in tags)
            {
                if (!_RecommendSourceTagsHashSet.Contains(tag))
                {
                    _RecommendSourceTagsHashSet.Add(tag);
                    RecommendSourceTags.Add(tag);
                }
            }
        }

       async Task<IEnumerable<RecommendVideoListItem>> IIncrementalSource<RecommendVideoListItem>.GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken ct)
        {
            if (_EndOfRecommend)
            {
                return Enumerable.Empty<RecommendVideoListItem>();
            }

            try
            {
                // 初回はページアクセスで得られるデータを使う
                if (_PrevRecommendContent == null)
                {
                    _RecommendResponse = await LoginUserRecommendProvider.GetRecommendFirstAsync();
                    if (_RecommendResponse.FirstData.Status == "ok")
                    {
                        _EndOfRecommend = _RecommendResponse.FirstData.RecommendInfo.EndOfRecommend;

                        AddRecommendTags(
                            _RecommendResponse.FirstData.Items.Select(x => x.AdditionalInfo?.Sherlock.Tag)
                            .Where(x => x != null)
                            );

                    }
                    else
                    {
                        _EndOfRecommend = true;
                    }

                    _PrevRecommendContent = _RecommendResponse?.FirstData;
                }
                else
                {
                    _PrevRecommendContent = await LoginUserRecommendProvider.GetRecommendAsync(_RecommendResponse, _PrevRecommendContent);
                }

                ct.ThrowIfCancellationRequested();

                if (_PrevRecommendContent == null || _PrevRecommendContent.Status != "ok")
                {
                    _EndOfRecommend = true;
                    return Enumerable.Empty<RecommendVideoListItem>();
                }

                _EndOfRecommend = _PrevRecommendContent?.RecommendInfo.EndOfRecommend ?? true;

                AddRecommendTags(
                    _PrevRecommendContent.Items.Select(x => x.AdditionalInfo?.Sherlock.Tag)
                    .Where(x => x != null)
                    );

                return _PrevRecommendContent.Items.Select(item =>
                {
                    var video = _nicoVideoProvider.UpdateCache(item.Id, video =>
                    {
                        video.ThumbnailUrl = item.ThumbnailUrl;
                        video.Title = item.ParseTitle();
                        video.Length = item.ParseLengthToTimeSpan();
                        video.PostedAt = item.ParseForstRetroeveToDateTimeOffset().DateTime;
                        return (false, default);
                    });

                    var vm = new RecommendVideoListItem(item);
                    vm.ViewCount = item.ViewCounter;
                    vm.MylistCount = item.MylistCounter;
                    vm.CommentCount = item.NumRes;

                    return vm;
                });
            }
            catch (Exception ex)
            {
                ErrorTrackingManager.TrackError(ex);
                _EndOfRecommend = true;
                return Enumerable.Empty<RecommendVideoListItem>();
            }
        }
    }
}
