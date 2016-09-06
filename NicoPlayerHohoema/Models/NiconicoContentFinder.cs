﻿using Mntone.Nico2;
using Mntone.Nico2.Mylist;
using Mntone.Nico2.Mylist.MylistGroup;
using Mntone.Nico2.Searches.Video;
using Mntone.Nico2.Users.Fav;
using Mntone.Nico2.Users.User;
using Mntone.Nico2.Users.Video;
using Mntone.Nico2.Videos.Histories;
using Mntone.Nico2.Videos.Ranking;
using Mntone.Nico2.Videos.Thumbnail;
using Mntone.Nico2.Videos.WatchAPI;
using NicoPlayerHohoema.Models.Db;
using NicoPlayerHohoema.Util;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NicoPlayerHohoema.Models
{
	/// <summary>
	/// 検索やランキングなどコンテンツを見つける機能をサポートします
	/// </summary>
	public class NiconicoContentFinder : BindableBase
	{
		public NiconicoContentFinder(HohoemaApp app)
		{
			_HohoemaApp = app;
		}


		public Task Initialize()
		{
			return Task.CompletedTask;
		}

		public async Task<ThumbnailResponse> GetThumbnailResponse(string rawVideoId)
		{
			ThumbnailResponse res = null;
		
			res = await Util.ConnectionRetryUtil.TaskWithRetry(async () =>
			{
				return await _HohoemaApp.NiconicoContext.Video.GetThumbnailAsync(rawVideoId);
			});

			if (res != null)
			{
				await UserInfoDb.AddOrReplaceAsync(res.UserId.ToString(), res.UserName, res.UserIconUrl.AbsoluteUri);
			}

			return res;
		}

		public async Task<WatchApiResponse> GetWatchApiResponse(string rawVideoId, bool forceLowQuality = false, HarmfulContentReactionType harmfulContentReaction = HarmfulContentReactionType.None)
		{
			var res = await Util.ConnectionRetryUtil.TaskWithRetry(() =>
			{
				return _HohoemaApp.NiconicoContext.Video.GetWatchApiAsync(
					rawVideoId
					, forceLowQuality: forceLowQuality
					, harmfulReactType: harmfulContentReaction
					);
			});

			if (res != null)
			{
				var uploaderInfo = res.UploaderInfo;
				await UserInfoDb.AddOrReplaceAsync(uploaderInfo.id, uploaderInfo.nickname, uploaderInfo.icon_url);
			}

			return res;
		}


		public async Task<User> GetUserInfo(string userId)
		{
			var user = await ConnectionRetryUtil.TaskWithRetry(() =>
			{
				return _HohoemaApp.NiconicoContext.User.GetUserAsync(userId);
			});

			if (user != null)
			{
				await UserInfoDb.AddOrReplaceAsync(userId, user.Nickname, user.ThumbnailUrl);
			}

			return user;
		}

		public async Task<UserDetail> GetUserDetail(string userId)
		{
			var userDetail = await ConnectionRetryUtil.TaskWithRetry(() =>
			{
				return _HohoemaApp.NiconicoContext.User.GetUserDetail(userId);
			});

			if (userDetail != null)
			{
				await UserInfoDb.AddOrReplaceAsync(userId, userDetail.Nickname, userDetail.ThumbnailUri);
			}

			return userDetail;
		}


		public async Task<NiconicoRankingRss> GetCategoryRanking(RankingCategory category, RankingTarget target, RankingTimeSpan timeSpan)
		{
			return await ConnectionRetryUtil.TaskWithRetry(async () =>
			{
				return await NiconicoRanking.GetRankingData(target, timeSpan, category);
			});
		}


		public async Task<VideoListingResponse> GetKeywordSearch(string keyword, uint from, uint limit, Sort sort = Sort.FirstRetrieve, Order order = Order.Descending)
		{
			return await ConnectionRetryUtil.TaskWithRetry(async () =>
			{
				return await _HohoemaApp.NiconicoContext.Search.VideoSearchWithKeywordAsync(keyword, from, limit, sort, order);
			});
		}

		public async Task<VideoListingResponse> GetTagSearch(string tag, uint from, uint limit, Sort sort = Sort.FirstRetrieve, Order order = Order.Descending)
		{
			return await ConnectionRetryUtil.TaskWithRetry(async () =>
			{
				return await _HohoemaApp.NiconicoContext.Search.VideoSearchWithTagAsync(tag, from, limit, sort, order)
					.ContinueWith(prevTask =>
					{
						if (!prevTask.Result.IsOK)
						{
							throw new WebException();
						}
						else
						{
							return prevTask.Result;
						}
					});
			}, retryInterval:2000);
		}

		public Task<List<LoginUserMylistGroup>> GetLoginUserMylistGroups()
		{
			return ConnectionRetryUtil.TaskWithRetry(() =>
			{
				return _HohoemaApp.NiconicoContext.User.GetMylistGroupListAsync();
			})
			.ContinueWith(prevResult => 
			{
				return prevResult.Result.Cast<LoginUserMylistGroup>().ToList();
			});
		}


		public Task<List<MylistGroupData>> GetUserMylistGroups(string userId)
		{
			return ConnectionRetryUtil.TaskWithRetry(async () =>
			{
				try
				{
					return await _HohoemaApp.NiconicoContext.Mylist.GetUserMylistGroupAsync(userId);
				}
				catch (Exception e) when (e.Message.Contains("Forbidden"))
				{
					return new List<MylistGroupData>();
				}
			})
			.ContinueWith(prevTask => 
			{
				if (prevTask.IsCompleted && prevTask.Result != null)
				{
					_CachedUserMylistGroupDatum = prevTask.Result;
					return prevTask.Result;
				}
				else
				{
					return _CachedUserMylistGroupDatum;
				}
			});
		}

		private List<MylistGroupData> _CachedUserMylistGroupDatum = null;


		public async Task<MylistGroupDetailResponse> GetMylistGroupDetail(string mylistGroupid)
		{
			return await ConnectionRetryUtil.TaskWithRetry(async () =>
			{
				return await _HohoemaApp.NiconicoContext.Mylist.GetMylistGroupDetailAsync(mylistGroupid);
			});
		}

		public async Task<MylistGroupVideoResponse> GetMylistGroupVideo(string mylistGroupid, uint from = 0, uint limit = 50)
		{
			return await ConnectionRetryUtil.TaskWithRetry(async () =>
			{
				return await _HohoemaApp.NiconicoContext.Mylist.GetMylistGroupVideoAsync(mylistGroupid, from, limit);
			});
		}




		public async Task<HistoriesResponse> GetHistory()
		{
			return await ConnectionRetryUtil.TaskWithRetry(async () =>
			{
				return await _HohoemaApp.NiconicoContext.Video.GetHistoriesAsync();
			});	
		}

		
		public async Task<List<FavData>> GetFavUsers()
		{
			return await _HohoemaApp.NiconicoContext.User.GetFavUsersAsync();
		}


		public async Task<List<string>> GetFavTags()
		{
			return await _HohoemaApp.NiconicoContext.User.GetFavTagsAsync();
		}

		public async Task<List<FavData>> GetFavMylists()
		{
			return await _HohoemaApp.NiconicoContext.User.GetFavMylistsAsync();
		}



		public async Task<UserVideoResponse> GetUserVideos(uint userId, uint page, Sort sort = Sort.FirstRetrieve, Order order = Order.Descending)
		{
			return await _HohoemaApp.NiconicoContext.User.GetUserVideos(userId, page, sort, order);
		}

		public async Task<NicoVideoResponse> GetRelatedVideos(string videoId, uint from, uint limit, Sort sort = Sort.FirstRetrieve, Order order = Order.Descending)
		{
			return await _HohoemaApp.NiconicoContext.Video.GetRelatedVideoAsync(videoId, from, limit, sort, order);
		}



		HohoemaApp _HohoemaApp;
	}
}
