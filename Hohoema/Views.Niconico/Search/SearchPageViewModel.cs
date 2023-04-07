﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hohoema.Models;
using System.Collections.ObjectModel;
using Reactive.Bindings;
using CommunityToolkit.Mvvm.Input;
using System.Reactive.Linq;
using System.Diagnostics;
using Reactive.Bindings.Extensions;
using Hohoema.Services.PageNavigation;
using Hohoema.Services;
using NiconicoSession = Hohoema.Models.Niconico.NiconicoSession;
using Hohoema.Models.Niconico.Search;
using Hohoema.Models.PageNavigation;
using Hohoema.Navigations;
using I18NPortable;
using Hohoema.Views.Pages.Niconico.Search;
using Hohoema.ViewModels.Niconico.Search;
using Hohoema.Models.Pins;
using Hohoema.Helpers;
using System.Reactive.Concurrency;

namespace Hohoema.ViewModels.Pages.Niconico.Search
{
    public class SearchPageViewModel : HohoemaPageViewModelBase, ITitleUpdatablePage, IPinablePage
    {
		public HohoemaPin GetPin()
		{
			if (_LastKeyword == null) { return null; }

			return new HohoemaPin()
			{
				Label = _LastKeyword + $" - {_LastSelectedTarget.Translate()}",
				PageType = HohoemaPageType.Search,
				Parameter = $"keyword={System.Net.WebUtility.UrlEncode(_LastKeyword)}&service={SelectedTarget.Value}",
			};
		}

		public ApplicationLayoutManager ApplicationLayoutManager { get; }
		public NiconicoSession NiconicoSession { get; }
		public SearchProvider SearchProvider { get; }
		public PageManager PageManager { get; }

        private readonly IScheduler _scheduler;
        private readonly SearchHistoryRepository _searchHistoryRepository;


		public ISearchPagePayloadContent RequireSearchOption { get; private set; }


		public ReactiveCommand DoSearchCommand { get; private set; }

		public ReactiveProperty<string> SearchText { get; private set; }
		public List<SearchTarget> TargetListItems { get; private set; }
		public ReactiveProperty<SearchTarget> SelectedTarget { get; private set; }

		private static SearchTarget _LastSelectedTarget;
		private static string _LastKeyword;

		public ReactiveProperty<bool> IsNavigationFailed { get; }
		public ReactiveProperty<string> NavigationFailedReason { get; }



		public ObservableCollection<SearchHistoryListItemViewModel> SearchHistoryItems { get; private set; } = new ObservableCollection<SearchHistoryListItemViewModel>();


		private RelayCommand _ShowSearchHistoryCommand;
		public RelayCommand ShowSearchHistoryCommand
		{
			get
			{
				return _ShowSearchHistoryCommand
					?? (_ShowSearchHistoryCommand = new RelayCommand(() =>
					{
						PageManager.OpenPage(HohoemaPageType.Search);
					}));
			}
		}


		private RelayCommand _DeleteAllSearchHistoryCommand;
		public RelayCommand DeleteAllSearchHistoryCommand
		{
			get
			{
				return _DeleteAllSearchHistoryCommand
					?? (_DeleteAllSearchHistoryCommand = new RelayCommand(() =>
					{
						_searchHistoryRepository.Clear();

						SearchHistoryItems.Clear();
						OnPropertyChanged(nameof(SearchHistoryItems));
					},
					() => _searchHistoryRepository.Count() > 0
					));
			}
		}

		private RelayCommand<SearchHistoryListItemViewModel> _SearchHistoryItemCommand;
		public RelayCommand<SearchHistoryListItemViewModel> SearchHistoryItemCommand
		{
			get
			{
				return _SearchHistoryItemCommand
					?? (_SearchHistoryItemCommand = new RelayCommand<SearchHistoryListItemViewModel>((item) =>
					{
						SearchText.Value = item.Keyword;
						if (DoSearchCommand.CanExecute())
                        {
							DoSearchCommand.Execute();
						}
					}
					));
			}
		}


		private RelayCommand<SearchHistory> _DeleteSearchHistoryItemCommand;
		public RelayCommand<SearchHistory> DeleteSearchHistoryItemCommand
		{
			get
			{
				return _DeleteSearchHistoryItemCommand
					?? (_DeleteSearchHistoryItemCommand = new RelayCommand<SearchHistory>((item) =>
					{
						_searchHistoryRepository.Remove(item.Keyword, item.Target);
						var itemVM = SearchHistoryItems.FirstOrDefault(x => x.Keyword == item.Keyword && x.Target == item.Target);
						if (itemVM != null)
						{
							SearchHistoryItems.Remove(itemVM);
						}
					}
					));
			}
		}


		public INavigationService NavigationService => SearchPage.ContentNavigationService;

		public SearchPageViewModel(
			IScheduler scheduler,
			ApplicationLayoutManager applicationLayoutManager,
			NiconicoSession niconicoSession,
            SearchProvider searchProvider,
            PageManager pageManager,
			SearchHistoryRepository searchHistoryRepository
            )
        {
            _scheduler = scheduler;
            ApplicationLayoutManager = applicationLayoutManager;
			NiconicoSession = niconicoSession;
            SearchProvider = searchProvider;
            PageManager = pageManager;
            _searchHistoryRepository = searchHistoryRepository;
            HashSet<string> HistoryKeyword = new HashSet<string>();
            foreach (var item in _searchHistoryRepository.ReadAllItems().OrderByDescending(x => x.LastUpdated))
            {
                if (HistoryKeyword.Contains(item.Keyword))
                {
                    continue;
                }

                SearchHistoryItems.Add(new SearchHistoryListItemViewModel(item, this));
                HistoryKeyword.Add(item.Keyword);
            }

            SearchText = new ReactiveProperty<string>(_LastKeyword)
                .AddTo(_CompositeDisposable);

            TargetListItems = new List<SearchTarget>()
            {
                SearchTarget.Keyword,
                SearchTarget.Tag,
                SearchTarget.Niconama,
            };

            SelectedTarget = new ReactiveProperty<SearchTarget>(_LastSelectedTarget)
                .AddTo(_CompositeDisposable);

            DoSearchCommand = new ReactiveCommand()
                .AddTo(_CompositeDisposable);
#if DEBUG
			SearchText.Subscribe(x =>
            {
                Debug.WriteLine($"検索：{x}");
            });
#endif

#if DEBUG
			DoSearchCommand.CanExecuteChangedAsObservable()
                .Subscribe(x =>
                {
                    Debug.WriteLine(DoSearchCommand.CanExecute());
                });
#endif

			

			IsNavigationFailed = new ReactiveProperty<bool>();
		    NavigationFailedReason = new ReactiveProperty<string>();
		}       

        public override async Task OnNavigatedToAsync(INavigationParameters parameters)
        {
			IsNavigationFailed.Value = false;
			NavigationFailedReason.Value = null;

			try
            {
				string keyword = null;
				if (parameters.TryGetValue("keyword", out keyword))
				{
					keyword = Uri.UnescapeDataString(keyword);
				}


				SearchTarget target = SearchTarget.Keyword;
				if (parameters.TryGetValue("service", out string modeString))
				{
					Enum.TryParse<SearchTarget>(modeString, out target);
				}
				else if (parameters.TryGetValue("service", out target))
                {

                }

				var pageName = target switch
				{
					SearchTarget.Keyword => nameof(SearchResultKeywordPage),
					SearchTarget.Tag => nameof(SearchResultTagPage),
					SearchTarget.Niconama => nameof(SearchResultLivePage),
					_ => null
				};

				if (pageName != null && keyword != null)
                {
					var result = await NavigationService.NavigateAsync(pageName, ("keyword", keyword));
					if (!result.IsSuccess)
					{
						throw result.Exception;
					}
				}

				SearchText.Value = keyword;
				SelectedTarget.Value = target;

				_LastSelectedTarget = target;
				_LastKeyword = keyword;

				DoSearchCommand.Throttle(TimeSpan.FromMilliseconds(50), _scheduler).Subscribe(_ =>
				{
					//await Task.Delay(50);

					if (SearchText.Value?.Length == 0) { return; }

					if (_LastSelectedTarget == SelectedTarget.Value && _LastKeyword == SearchText.Value) { return; }

					// 検索結果を表示
					PageManager.Search(SelectedTarget.Value, SearchText.Value);

					var searched = _searchHistoryRepository.Searched(SearchText.Value, SelectedTarget.Value);

					var oldSearchHistory = SearchHistoryItems.FirstOrDefault(x => x.Keyword == SearchText.Value);
					if (oldSearchHistory != null)
					{
						SearchHistoryItems.Remove(oldSearchHistory);
					}
					SearchHistoryItems.Insert(0, new SearchHistoryListItemViewModel(searched, this));

				})
				.AddTo(_navigationDisposables);
			}
			catch (Exception e)
            {
				IsNavigationFailed.Value = true;
#if DEBUG
				NavigationFailedReason.Value = e.Message;
#endif
				Debug.WriteLine(e.ToString());
			}

			await base.OnNavigatedToAsync(parameters);
        }

        public IObservable<string> GetTitleObservable()
        {
			return SearchText.Select(x => $"{"Search".Translate()} '{x}'");
        }

        
    }
}
