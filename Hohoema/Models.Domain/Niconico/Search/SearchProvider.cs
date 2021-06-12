﻿using Mntone.Nico2;
using Mntone.Nico2.Searches.Community;
using Mntone.Nico2.Searches.Video;
using Hohoema.Models.Domain.Niconico.Mylist;
using Hohoema.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NiconicoToolkit.SearchWithCeApi.Video;

namespace Hohoema.Models.Domain.Niconico.Search
{
    public sealed class SearchProvider : ProviderBase
    {
        private readonly MylistProvider _mylistProvider;

        // TODO: タグによる生放送検索を別メソッドに分ける

        public SearchProvider(
            NiconicoSession niconicoSession,
            MylistProvider mylistProvider
            )
            : base(niconicoSession)
        {
            _mylistProvider = mylistProvider;
        }



        public Task<NiconicoToolkit.SearchWithCeApi.Video.VideoListingResponse> GetKeywordSearch(string keyword, int from, int limit, VideoSortKey sort = VideoSortKey.FirstRetrieve, VideoSortOrder order = VideoSortOrder.Desc)
        {
            return _niconicoSession.ToolkitContext.SearchWithCeApi.Video.KeywordSearchAsync(keyword, from, limit, sort, order);
        }

        public Task<NiconicoToolkit.SearchWithCeApi.Video.VideoListingResponse> GetTagSearch(string tag, int from, int limit, VideoSortKey sort = VideoSortKey.FirstRetrieve, VideoSortOrder order = VideoSortOrder.Desc)
        {
            return _niconicoSession.ToolkitContext.SearchWithCeApi.Video.TagSearchAsync(tag, from, limit, sort, order);
        }

        public async Task<NiconicoToolkit.SearchWithPage.Live.LiveSearchPageScrapingResult> LiveSearchAsync(
            NiconicoToolkit.SearchWithPage.Live.LiveSearchOptionsQuery query
            )
        {
            return await ContextActionWithPageAccessWaitAsync(async context =>
            {
                return await _niconicoSession.ToolkitContext.SearchWithPage.Live.GetLiveSearchPageScrapingResultAsync(query, CancellationToken.None);
            });
            
        }


        public async Task<Mntone.Nico2.Searches.Suggestion.SuggestionResponse> GetSearchSuggestKeyword(string keyword)
        {
            return await ContextActionAsync(async context =>
            {
                return await context.Search.GetSuggestionAsync(keyword);
            });
            
        }







        public async Task<CommunitySearchResponse> SearchCommunity(
            string keyword
            , uint page
            , CommunitySearchSort sort = CommunitySearchSort.CreatedAt
            , Order order = Order.Descending
            , CommunitySearchMode mode = CommunitySearchMode.Keyword
            )
        {
            return await ContextActionAsync(async context =>
            {
                return await context.Search.CommunitySearchAsync(keyword, page, sort, order, mode);
            });
            
        }

        public class MylistSearchResult
        {
            public bool IsSuccess { get; set; }
            public List<MylistPlaylist> Items { get; set; }
            public int TotalCount { get; set; }
        }

        public async Task<MylistSearchResult> MylistSearchAsync(string keyword, uint head, uint count, Sort? sort, Order? order)
        {
            var res = await ContextActionAsync(async context =>
            {
                return await context.Search.MylistSearchAsync(keyword, head, count, sort, order);
            });

            if (res.MylistGroupItems?.Any() ?? false)
            {
                var items = res.MylistGroupItems
                    .Select(x => new MylistPlaylist(x.Id, _mylistProvider) 
                    {
                        Label = x.Name,
                        Count = (int)x.ItemCount,
                        Description = x.Description, 
                        CreateTime= x.UpdateTime 
                    }
                    )
                    .ToList();

                return new MylistSearchResult()
                {
                    IsSuccess = true,
                    Items = items,
                    TotalCount = (int)res.GetTotalCount()
                };
            }
            else
            {
                return new MylistSearchResult() { IsSuccess = false };
            }
                
        }
    }
}
