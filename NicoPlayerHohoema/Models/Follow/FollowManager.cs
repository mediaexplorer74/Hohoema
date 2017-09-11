﻿using Mntone.Nico2;
using Mntone.Nico2.Users.Follow;
using Mntone.Nico2.Users.Video;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Foundation;
using System.Runtime.InteropServices.WindowsRuntime;
using Prism.Mvvm;
using Windows.UI.Core;
using NicoPlayerHohoema.Util;

namespace NicoPlayerHohoema.Models
{
	public class FollowManager : IBackgroundUpdateable
	{

		#region Niconico follow constants

		// Note: 2016/10/31 から お気に入りユーザー枠は一般プレミアムどちらも600に変更
		public const uint FOLLOW_USER_MAX_COUNT = 600;
		public const uint PREMIUM_FOLLOW_USER_MAX_COUNT = 600;

		public const uint FOLLOW_MYLIST_MAX_COUNT = 20;
		public const uint PREMIUM_FOLLOW_MYLIST_MAX_COUNT = 50;

		public const uint FOLLOW_TAG_MAX_COUNT = 10;
		public const uint PREMIUM_FOLLOW_TAG_MAX_COUNT = 10;

		public const uint FOLLOW_COMMUNITY_MAX_COUNT = 50;
		public const uint PREMIUM_FOLLOW_COMMUNITY_MAX_COUNT = 300;

		#endregion


		public static Task<FollowManager> Create(HohoemaApp hohoemaApp, uint userId)
		{
			var followManager = new FollowManager(hohoemaApp, userId);

			return Task.FromResult(followManager);
		}


		#region Properties 

		public uint UserId { get; set; }


		public IFollowInfoGroup Tag { get; private set; }
		public IFollowInfoGroup Mylist { get; private set; }
		public IFollowInfoGroup User { get; private set; }
		public IFollowInfoGroup Community { get; private set; }


		public IReadOnlyList<IFollowInfoGroup> GetAllFollowInfoGroups() => new[] 
		{
			Tag,
			Mylist,
			User,
			Community
		};


        #endregion

        #region Event 

        public event BackgroundUpdateCompletedEventHandler Completed;

        #endregion

        #region Fields

        HohoemaApp _HohoemaApp;

        AsyncLock _SyncLock = new AsyncLock();

        #endregion

        internal FollowManager(HohoemaApp hohoemaApp, uint userId)
		{
			_HohoemaApp = hohoemaApp;
			UserId = userId;

            Tag = new TagFollowInfoGroup(_HohoemaApp);
            Mylist = new MylistFollowInfoGroup(_HohoemaApp);
            User = new UserFollowInfoGroup(_HohoemaApp);
            Community = new CommunityFollowInfoGroup(_HohoemaApp);
        }


        #region interface IBackgroundUpdateable

        public IAsyncAction BackgroundUpdate(CoreDispatcher uiDispatcher)
		{
			return AsyncInfo.Run(SyncAll);
		}

		#endregion

		private IFollowInfoGroup GetFollowInfoGroup(FollowItemType itemType)
		{
			switch (itemType)
			{
				case FollowItemType.Tag:
					return Tag;
				case FollowItemType.Mylist:
					return Mylist;
				case FollowItemType.User:
					return User;
				case FollowItemType.Community:
					return Community;
				default:
					throw new Exception();
			}
		}

		public bool CanMoreAddFollow(FollowItemType itemType)
		{
			return GetFollowInfoGroup(itemType).CanMoreAddFollow();
		}

		

		public bool IsFollowItem(FollowItemType itemType, string id)
		{
			var group = GetFollowInfoGroup(itemType);

			if (itemType == FollowItemType.Tag)
			{
				id = TagStringHelper.ToEnsureHankakuNumberTagString(id);
			}

			return group.IsFollowItem(id);
		}


	

		public async Task SyncAll(CancellationToken token)
		{
            using (var releaser = await _SyncLock.LockAsync())
            {
                foreach (var followInfoGroup in GetAllFollowInfoGroups())
                {
                    token.ThrowIfCancellationRequested();

                    await followInfoGroup.SyncFollowItems();

                    token.ThrowIfCancellationRequested();

                    await Task.Delay(250);
                }

                Completed?.Invoke(this);
            }
        }

		public FollowItemInfo FindFollowInfo(FollowItemType itemType, string id)
		{
			return GetFollowInfoGroup(itemType).FollowInfoItems.SingleOrDefault(x => x.Id == id);
		}

		public async Task<ContentManageResult> AddFollow(FollowItemType itemType, string id, string name, object token = null)
		{
			var group = GetFollowInfoGroup(itemType);

			var result = await group.AddFollow(name, id, token);
		
			return result;
		}

		public async Task<ContentManageResult> RemoveFollow(FollowItemType itemType, string id)
		{
			var group = GetFollowInfoGroup(itemType);

			var result = await group.RemoveFollow(id);

			return result;
		}

	}

}