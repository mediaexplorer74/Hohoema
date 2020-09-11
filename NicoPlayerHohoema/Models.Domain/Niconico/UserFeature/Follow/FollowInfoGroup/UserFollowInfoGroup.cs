﻿using Mntone.Nico2;
using Mntone.Nico2.Users.Follow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hohoema.Models.Domain.Niconico.UserFeature.Follow
{
	public class UserFollowInfoGroup : FollowInfoGroupBaseTemplate<UserFollowItem>
    {
		public UserFollowInfoGroup(
            NiconicoSession niconicoSession, 
            UserFollowProvider userFollowProvider
            )
        {
            NiconicoSession = niconicoSession;
            UserFollowProvider = userFollowProvider;
        }
    

		public override FollowItemType FollowItemType => FollowItemType.User;

		public override uint MaxFollowItemCount =>
            NiconicoSession.IsPremiumAccount ? FollowManager.PREMIUM_FOLLOW_USER_MAX_COUNT : FollowManager.FOLLOW_USER_MAX_COUNT;

        public NiconicoSession NiconicoSession { get; }
        public UserFollowProvider UserFollowProvider { get; }


        protected override async Task<List<UserFollowItem>> GetFollowSource()
		{
            return await UserFollowProvider.GetAllAsync();

        }

		protected override async Task<ContentManageResult> AddFollow_Internal(string id, object token)
		{
            return await UserFollowProvider.AddFollowAsync(id);
		}
		protected override async Task<ContentManageResult> RemoveFollow_Internal(string id)
		{
            return await UserFollowProvider.RemoveFollowAsync(id);
        }

        protected override string FollowSourceToItemId(UserFollowItem source)
        {
            return source.Id.ToString();
        }

        protected override FollowItemInfo ConvertToFollowInfo(UserFollowItem source)
        {
            return new FollowItemInfo()
            {
                FollowItemType = FollowItemType,
                Id = source.Id.ToString(),
                Name = source.Nickname,
                ThumbnailUrl = source.Icons.Small.OriginalString,
            };
        }
    }
}