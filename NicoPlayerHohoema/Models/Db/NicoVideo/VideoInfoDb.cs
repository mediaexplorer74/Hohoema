﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Mntone.Nico2;
using Mntone.Nico2.Videos.Thumbnail;
using Mntone.Nico2.Videos.WatchAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinRTXamlToolkit.Async;

namespace NicoPlayerHohoema.Models.Db
{
	public static class VideoInfoDb
	{
		public static readonly AsyncLock _AsyncLock = new AsyncLock();

		public static void UpdateNicoVideoInfo(NicoVideoInfo info, ThumbnailResponse thumbnailRes)
		{
			info.VideoId = thumbnailRes.Id;
			info.HighSize = (uint)thumbnailRes.SizeHigh;
			info.LowSize = (uint)thumbnailRes.SizeLow;
			info.Length = thumbnailRes.Length;
			info.MovieType = thumbnailRes.MovieType;
			info.PostedAt = thumbnailRes.PostedAt.DateTime;
			info.UserId = thumbnailRes.UserId;
			info.UserType = thumbnailRes.UserType;
			info.Description = thumbnailRes.Description;
			info.ThumbnailUrl = thumbnailRes.ThumbnailUrl.AbsoluteUri;

			info.Title = thumbnailRes.Title;
			info.ViewCount = thumbnailRes.ViewCount;
			info.MylistCount = thumbnailRes.MylistCount;
			info.CommentCount = thumbnailRes.CommentCount;
			info.SetTags(thumbnailRes.Tags.Value.ToList());

		}


		public static void UpdateNicoVideoInfo(NicoVideoInfo info, WatchApiResponse watchApiRes)
		{
			info.DescriptionWithHtml = watchApiRes.videoDetail.description;

			info.ThreadId = watchApiRes.ThreadId.ToString();
			info.ViewCount = (uint)watchApiRes.videoDetail.viewCount.Value;
			info.MylistCount = (uint)watchApiRes.videoDetail.mylistCount.Value;
			info.ViewCount = (uint)watchApiRes.videoDetail.commentCount.Value;

			info.PrivateReasonType = watchApiRes.PrivateReason;
		}

		public static IReadOnlyList<NicoVideoInfo> GetAll()
		{
			using (var db = new NicoVideoDbContext())
			{
				return db.VideoInfos.ToList();
			}
		}

		public static async Task<NicoVideoInfo> GetEnsureNicoVideoInfoAsync(string rawVideoId)
		{
			using (var db = new NicoVideoDbContext())
			{
				var info = db.VideoInfos.SingleOrDefault(x => x.RawVideoId == rawVideoId);

				if (info == null)
				{
					info = new NicoVideoInfo()
					{
						RawVideoId = rawVideoId,
						LastUpdated = DateTime.Now
					};

					using (var releaser = await _AsyncLock.LockAsync())
					{
						db.VideoInfos.Add(info);
						await db.SaveChangesAsync();
					}
				}

				return info;
			}
		}

		public static async Task UpdateWithThumbnailAsync(NicoVideoInfo info, ThumbnailResponse thumbnailRes)
		{
			using (var releaser = await _AsyncLock.LockAsync())
			using (var db = new NicoVideoDbContext())
			{
				UpdateNicoVideoInfo(info, thumbnailRes);

				db.VideoInfos.Update(info);

				info.LastUpdated = DateTime.Now;

				await db.SaveChangesAsync();
			}
		}


		public static async Task UpdateWithWatchApiResponseAsync(NicoVideoInfo info, WatchApiResponse res)
		{
			using (var releaser = await _AsyncLock.LockAsync())
			using (var db = new NicoVideoDbContext())
			{
				UpdateNicoVideoInfo(info, res);

				info.LastUpdated = DateTime.Now;

				db.VideoInfos.Update(info);

				await db.SaveChangesAsync();
			}
		}


		public static void Deleted(string rawVideoId)
		{
			using (var db = new NicoVideoDbContext())
			{
				var info = db.VideoInfos.SingleOrDefault(x => x.RawVideoId == rawVideoId);

				if (info != null)
				{
					info.IsDeleted = true;
					db.VideoInfos.Update(info);
					db.SaveChanges();
				}
			}
		}

		public static NicoVideoInfo Get(string rawVideoId)
		{
			using (var db = new NicoVideoDbContext())
			{
				return db.VideoInfos.SingleOrDefault(x => x.RawVideoId == rawVideoId);
			}
		}

		public static async Task<NicoVideoInfo> GetAsync(string threadId)
		{
			using (var releaser = await _AsyncLock.LockAsync())
			using (var db = new NicoVideoDbContext())
			{
				return await db.VideoInfos.SingleOrDefaultAsync(x => x.RawVideoId == threadId);
			}
		}


		public static void Remove(string rawVideoId)
		{
			var nicoVideoInfo = Get(rawVideoId);

			using (var db = new NicoVideoDbContext())
			{
				db.VideoInfos.Remove(nicoVideoInfo);
				db.SaveChanges();
			}
		}

		public static async Task RemoveAsync(NicoVideoInfo nicoVideoInfo)
		{
			using (var releaser = await _AsyncLock.LockAsync())
			using (var db = new NicoVideoDbContext())
			{
				db.Entry(nicoVideoInfo).State = EntityState.Deleted;
				await db.SaveChangesAsync();
			}
		}

		public static async Task RemoveRangeAsync(IEnumerable<NicoVideoInfo> removeTargets)
		{
			using (var releaser = await _AsyncLock.LockAsync())
			using (var db = new NicoVideoDbContext())
			{
				db.VideoInfos.RemoveRange(removeTargets);
				await db.SaveChangesAsync();
			}
		}

	}
}
