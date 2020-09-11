﻿using Mntone.Nico2.Videos.Ranking;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Commands;
using Hohoema.Models.Domain;
using System.Reactive.Linq;
using Microsoft.Toolkit.Uwp.UI;
using Prism.Navigation;
using Hohoema.Presentation.Services.Page;
using Windows.UI.Xaml.Data;
using Prism.Mvvm;
using Prism.Events;
using Reactive.Bindings.Extensions;
using System.Diagnostics;
using Hohoema.Models.UseCase;
using I18NPortable;
using System.Threading.Tasks;
using Hohoema.Models.Domain.Niconico.Video;
using Hohoema.Models.Domain.PageNavigation;
using Hohoema.Models.Domain.Application;

namespace Hohoema.Presentation.ViewModels
{
    public class RankingCategoryListPageViewModel : HohoemaViewModelBase, INavigatedAwareAsync
    {
        public RankingCategoryListPageViewModel(
            ApplicationLayoutManager applicationLayoutManager,
            PageManager pageManager,
            Services.DialogService dialogService,
            VideoRankingSettings rankingSettings,
            IEventAggregator eventAggregator,
            AppFlagsRepository appFlagsRepository,
            RankingProvider rankingProvider
            )
        {
            ApplicationLayoutManager = applicationLayoutManager;
            PageManager = pageManager;
            HohoemaDialogService = dialogService;
            RankingSettings = rankingSettings;
            _eventAggregator = eventAggregator;
            _appFlagsRepository = appFlagsRepository;
            _rankingProvider = rankingProvider;


            // TODO: ログインユーザーが成年であればR18ジャンルを表示するように
            _RankingGenreItemsSource = new List<RankingGenreItem>();

            FavoriteItems = new ObservableCollection<RankingItem>(GetFavoriteRankingItems());
            _favoriteRankingGenreGroupItem = new FavoriteRankingGenreGroupItem()
            {
                Label = "FavoriteRankingTag".Translate(),
                IsDisplay = true,
                Items = new AdvancedCollectionView(FavoriteItems),
            };
            _RankingGenreItemsSource.Add(_favoriteRankingGenreGroupItem);
            

            var sourceGenreItems = Enum.GetValues(typeof(RankingGenre)).Cast<RankingGenre>()
                .Where(x => x != RankingGenre.R18)
                .Select(genre =>
                {
                    var rankingItems = ToRankingItem(genre, _rankingProvider.GetRankingGenreTagsFromCache(genre));
                    var acv = new AdvancedCollectionView(new ObservableCollection<RankingItem>(rankingItems), isLiveShaping: true)
                    {
                        Filter = (item) => (item as RankingItem).IsDisplay,
                    };
                    acv.ObserveFilterProperty(nameof(RankingItem.IsDisplay));

                    return new RankingGenreItem()
                    {
                        Genre = genre,
                        Label = genre.Translate(),
                        IsDisplay = !RankingSettings.IsHiddenGenre(genre),
                        Items = acv
                    };
                });

            foreach (var genreItem in sourceGenreItems)
            {
                _RankingGenreItemsSource.Add(genreItem);
            }


            foreach (var item in _RankingGenreItemsSource)
            {
                if (item.Genre == null)
                {
                    _RankingGenreItems.Add(item);
                }
                else if (!RankingSettings.IsHiddenGenre(item.Genre.Value))
                {
                    _RankingGenreItems.Add(item);
                }
            }
            

            {
                RankingGenreItems = new CollectionViewSource()
                {
                    Source = _RankingGenreItems,
                    ItemsPath = new Windows.UI.Xaml.PropertyPath(nameof(RankingGenreItem.Items)),
                    IsSourceGrouped = true,
                };
            }

            _eventAggregator.GetEvent<Events.RankingGenreShowRequestedEvent>()
                .Subscribe((args) =>
                {
                    // Tagの指定が無い場合はジャンル自体の非表示として扱う
                    var genreItem = _RankingGenreItemsSource.First(x => x.Genre == args.RankingGenre);
                    if (string.IsNullOrEmpty(args.Tag))
                    {
                        genreItem.IsDisplay = true;
                        RankingSettings.RemoveHiddenGenre(args.RankingGenre);
                        
                        _RankingGenreItems.Clear();
                        foreach (var item in _RankingGenreItemsSource)
                        {
                            if (item.Genre == null)
                            {
                                _RankingGenreItems.Add(item);
                            }
                            else if (!RankingSettings.IsHiddenGenre(item.Genre.Value))
                            {
                                _RankingGenreItems.Add(item);
                            }
                        }

                        System.Diagnostics.Debug.WriteLine($"Genre Show: {args.RankingGenre}");
                    }
                    else
                    {
                        var sameTagItem = genreItem.Items.SourceCollection.Cast<RankingItem>().FirstOrDefault(x => x.Tag == args.Tag);
                        if (sameTagItem != null)
                        {
                            sameTagItem.IsDisplay = true;
                            RankingSettings.RemoveHiddenTag(args.RankingGenre, args.Tag);
                            
                            System.Diagnostics.Debug.WriteLine($"Tag Show: {args.Tag}");
                        }
                    }
                })
                .AddTo(_CompositeDisposable);

            _eventAggregator.GetEvent<Events.RankingGenreHiddenRequestedEvent>()
                .Subscribe((args) => 
                {
                    // Tagの指定が無い場合はジャンル自体の非表示として扱う
                    var genreItem = _RankingGenreItemsSource.First(x => x.Genre == args.RankingGenre);
                    if (string.IsNullOrEmpty(args.Tag))
                    {
                        genreItem.IsDisplay = false;
                        RankingSettings.AddHiddenGenre(args.RankingGenre);
                        
                        _RankingGenreItems.Clear();
                        foreach (var item in _RankingGenreItemsSource)
                        {
                            if (item.Genre == null)
                            {
                                _RankingGenreItems.Add(item);
                            }
                            else if (!RankingSettings.IsHiddenGenre(item.Genre.Value))
                            {
                                _RankingGenreItems.Add(item);
                            }
                        }

                        System.Diagnostics.Debug.WriteLine($"Genre Hidden: {args.RankingGenre}");
                    }
                    else
                    {
                        var sameTagItem = genreItem.Items.SourceCollection.Cast<RankingItem>().FirstOrDefault(x => x.Tag == args.Tag);
                        if (sameTagItem != null)
                        {
                            sameTagItem.IsDisplay = false;
                            RankingSettings.AddHiddenTag(sameTagItem.Genre.Value, sameTagItem.Tag, sameTagItem.Label);
                            
                            System.Diagnostics.Debug.WriteLine($"Tag Hidden: {args.Tag}");
                        }
                    }                    
                })
                .AddTo(_CompositeDisposable);

            _eventAggregator.GetEvent<Events.RankingGenreFavoriteRequestedEvent>()
                .Subscribe((args) =>
                {
                    if (false == FavoriteItems.Any(x => x.Genre == args.RankingGenre && x.Tag == args.Tag))
                    {
                        var addedItem = new RankingItem()
                        {
                            Genre = args.RankingGenre,
                            IsDisplay = true,
                            IsFavorite = true,
                            Label = args.Label,
                            Tag = args.Tag
                        };

                        FavoriteItems.Add(addedItem);
                        RankingSettings.AddFavoriteTag(addedItem.Genre.Value, addedItem.Tag, addedItem.Label);
                        
                        System.Diagnostics.Debug.WriteLine($"Favorite added: {args.Label}");
                    }
                })
                .AddTo(_CompositeDisposable);

            _eventAggregator.GetEvent<Events.RankingGenreUnFavoriteRequestedEvent>()
                .Subscribe((args) =>
                {
                    var unFavoriteItem = FavoriteItems.FirstOrDefault(x => x.Genre == args.RankingGenre && x.Tag == args.Tag);
                    if (unFavoriteItem != null)
                    {
                        FavoriteItems.Remove(unFavoriteItem);
                        RankingSettings.RemoveFavoriteTag(unFavoriteItem.Genre.Value, unFavoriteItem.Tag);
                        
                        System.Diagnostics.Debug.WriteLine($"Favorite removed: {args.RankingGenre} {args.Tag}");
                    }
                })
                .AddTo(_CompositeDisposable);
        }

        FavoriteRankingGenreGroupItem _favoriteRankingGenreGroupItem;
        public ObservableCollection<RankingItem> FavoriteItems { get; set; }

        IEnumerable<RankingItem> GetFavoriteRankingItems()
        {
            return RankingSettings.FavoriteTags
                 .Select(y => new RankingItem()
                 {
                     Genre = y.Genre,
                     IsDisplay = true,
                     Label = y.Label,
                     Tag = y.Tag,
                     IsFavorite = true
                 });
        }


        static RankingCategoryListPageViewModel()
        {
           
        }

        public ApplicationLayoutManager ApplicationLayoutManager { get; }
        public PageManager PageManager { get; }

        public Services.DialogService HohoemaDialogService { get; }
        public VideoRankingSettings RankingSettings { get; }


        public CollectionViewSource RankingGenreItems { get; }
        ObservableCollection<RankingGenreItem> _RankingGenreItems = new ObservableCollection<RankingGenreItem>();
        List<RankingGenreItem> _RankingGenreItemsSource = new List<RankingGenreItem>();

        private DelegateCommand<RankingItem> _OpenRankingPageCommand;
        public DelegateCommand<RankingItem> OpenRankingPageCommand => _OpenRankingPageCommand
            ?? (_OpenRankingPageCommand = new DelegateCommand<RankingItem>(OnRankingCategorySelected));
        
        internal void OnRankingCategorySelected(RankingItem info)
        {
            Debug.WriteLine("OnRankingCategorySelected" + info.Genre);

            var p = new NavigationParameters
            {
                { "genre", info.Genre.ToString() },
                { "tag", info.Tag }
            };
            _prevSelectedGenre = info.Genre;

            PageManager.OpenPage(HohoemaPageType.RankingCategory, p);
        }

        private RankingGenre? _prevSelectedGenre;
        private readonly IEventAggregator _eventAggregator;
        private readonly AppFlagsRepository _appFlagsRepository;
        private readonly RankingProvider _rankingProvider;

        public async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            if (!_appFlagsRepository.IsRankingInitialUpdate)
            {
                try
                {
                    foreach (var genreItem in _RankingGenreItems)
                    {
                        if (genreItem.Genre == null) { continue; }
                        var genre = genreItem.Genre.Value;
                        var tags = await _rankingProvider.GetRankingGenreTagsAsync(genre, true);

                        using (var refresh = genreItem.Items.DeferRefresh())
                        {
                            var rankingItems = tags
                                .Where(x => !string.IsNullOrEmpty(x.Tag) && x.Tag != "all")
                                .Select(y => new RankingItem()
                                {
                                    Genre = genre,
                                    IsDisplay = !RankingSettings.IsHiddenTag(genre, y.Tag),
                                    Label = y.Label,
                                    Tag = y.Tag
                                }
                            );
                            genreItem.Items.Clear();
                            foreach (var tag in rankingItems)
                            {
                                genreItem.Items.Add(tag);
                            }
                        }

                        await Task.Delay(500);
                    }
                }
                finally
                {
                    _appFlagsRepository.IsRankingInitialUpdate = true;
                }
            }
            else
            {
                if (_prevSelectedGenre != null)
                {
                    var updateTargetGenre = _RankingGenreItems.FirstOrDefault(x => x.Genre == _prevSelectedGenre);
                    if (updateTargetGenre != null)
                    {
                        updateTargetGenre.Items.Clear();
                        var items = await _rankingProvider.GetRankingGenreTagsAsync(updateTargetGenre.Genre.Value, true);

                        if (items?.Any() ?? false)
                        {
                            using (updateTargetGenre.Items.DeferRefresh())
                            {
                                var genre = updateTargetGenre.Genre.Value;
                                foreach (var a in ToRankingItem(genre, items))
                                {
                                    updateTargetGenre.Items.Add(a);
                                }
                            }
                        }
                    }
                }
            }
        }


        List<RankingItem> ToRankingItem(RankingGenre genre, List<RankingGenreTag> tags)
        {
            return tags
                .Where(x => !string.IsNullOrEmpty(x.Tag) && x.Tag != "all")
                .Select(y => new RankingItem()
                {
                    Genre = genre,
                    IsDisplay = !RankingSettings.IsHiddenTag(genre, y.Tag),
                    Label = y.Label,
                    Tag = y.Tag
                }
            ).ToList();
        }



        DelegateCommand _ShowDisplayGenreSelectDialogCommand;
        public DelegateCommand ShowDisplayGenreSelectDialogCommand => _ShowDisplayGenreSelectDialogCommand
            ?? (_ShowDisplayGenreSelectDialogCommand = new DelegateCommand(async () => 
            {
                var rankingGenres = Enum.GetValues(typeof(RankingGenre)).Cast<RankingGenre>().Where(x => x != RankingGenre.R18);
                var allItems = rankingGenres.Select(x => new HiddenGenreItem() { Label = x.Translate(), Genre = x }).ToArray();

                var selectedItems = rankingGenres.Where(x => !RankingSettings.HiddenGenres.Contains(x)).Select(x => allItems.First(y => y.Genre == x));

                var result = await HohoemaDialogService.ShowMultiChoiceDialogAsync<HiddenGenreItem>(
                    "SelectDisplayRankingGenre".Translate(), 
                    allItems, selectedItems,
                    nameof(HiddenGenreItem.Label)
                    );

                if (result == null)
                {
                    return;
                }

                var hiddenGenres = rankingGenres.Where(x => !result.Any(y => x == y.Genre));
                RankingSettings.ResetHiddenGenre(hiddenGenres);

                _RankingGenreItems.Clear();
                _RankingGenreItems.Add(_favoriteRankingGenreGroupItem);
                foreach (var displayGenre in _RankingGenreItemsSource)
                {
                    if (result.Any(x => x.Genre == displayGenre.Genre))
                    {
                        _RankingGenreItems.Add(displayGenre);
                    }
                }
            }));

        DelegateCommand _ShowDisplayGenreTagSelectDialogCommand;
        public DelegateCommand ShowDisplayGenreTagSelectDialogCommand => _ShowDisplayGenreTagSelectDialogCommand
            ?? (_ShowDisplayGenreTagSelectDialogCommand = new DelegateCommand(async () =>
            {
                var result = await HohoemaDialogService.ShowMultiChoiceDialogAsync<RankingGenreTag>(
                    "SelectReDisplayHiddenRankingTags".Translate(),
                    RankingSettings.HiddenTags, Enumerable.Empty<RankingGenreTag>(),
                    nameof(RankingGenreTag.Label)
                    );

                if (result == null) { return; }

                foreach (var removeHiddenTag in result)
                {
                    RankingSettings.RemoveHiddenTag(removeHiddenTag.Genre, removeHiddenTag.Tag);
                }

                foreach (var displayTag in result)
                {
                    var genreGroup =  _RankingGenreItemsSource.FirstOrDefault(x => x.Genre == displayTag.Genre);
                    if (genreGroup != null)
                    {
                        var tagItem = genreGroup.Items.SourceCollection.Cast<RankingItem>().FirstOrDefault(x => x.Tag == displayTag.Tag);
                        if (tagItem is RankingItem item)
                        {
                            item.IsDisplay = true;
                        }
                    }
                }
            }));
    }


    public class HiddenGenreItem
    {
        public string Label { get; set; }
        public RankingGenre Genre { get; set; }
        public string Tag { get; set; }
    }

    public class RankingItem : BindableBase
    {
        public string Label { get; set; }

        public RankingGenre? Genre { get; set; }
        public string Tag { get; set; }

        private bool _IsDisplay;
        public bool IsDisplay
        {
            get => _IsDisplay;
            set => SetProperty(ref _IsDisplay, value);
        }

        private bool _IsFavorite;
        public bool IsFavorite
        {
            get => _IsFavorite;
            set => SetProperty(ref _IsFavorite, value);
        }

    }

    public class RankingGenreItem : RankingItem
    {
        public AdvancedCollectionView Items { get; set; }
    }

    public class FavoriteRankingGenreGroupItem : RankingGenreItem
    {
        
    }
}