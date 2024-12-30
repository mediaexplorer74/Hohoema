// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.UserApi
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Mylist;
using Mntone.Nico2.Users.Deflist;
using Mntone.Nico2.Users.Follow;
using Mntone.Nico2.Users.FollowCommunity;
using Mntone.Nico2.Users.Icon;
using Mntone.Nico2.Users.Info;
using Mntone.Nico2.Users.MylistGroup;
using Mntone.Nico2.Users.MylistItem;
using Mntone.Nico2.Users.NG;
using Mntone.Nico2.Users.User;
using Mntone.Nico2.Users.Video;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Users
{
  public sealed class UserApi
  {
    private NiconicoContext _context;

    internal UserApi(NiconicoContext context) => this._context = context;

    public Task<byte[]> GetIconAsync(uint requestUserId)
    {
      return IconClient.GetIconAsync(this._context, requestUserId);
    }

    public Task<InfoResponse> GetInfoAsync() => InfoClient.GetInfoAsync(this._context);

    public Task<Mntone.Nico2.Users.User.User> GetUserAsync(string requestUserId)
    {
      return UserClient.GetUserAsync(this._context, requestUserId);
    }

    public Task<List<FollowData>> GetFollowUsersAsync()
    {
      return FollowClient.GetFollowUsersAsync(this._context);
    }

    public Task<List<string>> GetFollowTagsAsync()
    {
      return FollowClient.GetFollowTagsAsync(this._context);
    }

    public Task<List<FollowData>> GetFollowMylistsAsync()
    {
      return FollowClient.GetFollowMylistAsync(this._context);
    }

    public Task<ContentManageResult> ExistUserFollowAsync(NiconicoItemType itemType, string item_id)
    {
      return FollowClient.ExistFollowAsync(this._context, itemType, item_id);
    }

    public Task<ContentManageResult> AddUserFollowAsync(NiconicoItemType itemType, string item_id)
    {
      return FollowClient.AddFollowAsync(this._context, itemType, item_id);
    }

    public Task<ContentManageResult> RemoveUserFollowAsync(
      NiconicoItemType itemType,
      string item_id)
    {
      return FollowClient.RemoveFollowAsync(this._context, itemType, item_id);
    }

    public Task<ContentManageResult> AddFollowTagAsync(string tag)
    {
      return FollowClient.AddFollowTagAsync(this._context, tag);
    }

    public Task<ContentManageResult> RemoveFollowTagAsync(string tag)
    {
      return FollowClient.RemoveFollowTagAsync(this._context, tag);
    }

    public Task<UserDetail> GetUserDetail(uint userId) => this.GetUserDetail(userId.ToString());

    public Task<UserDetail> GetUserDetail(string userId)
    {
      return UserClient.GetUserDetailAsync(this._context, userId);
    }

    public Task<UserVideoResponse> GetUserVideos(
      uint userId,
      uint page,
      Sort sortMethod = Sort.FirstRetrieve,
      Order sortDir = Order.Descending)
    {
      return UserVideoClient.GetUserAsync(this._context, userId, page, sortMethod, sortDir);
    }

    public Task<NGCommentResponse> GetNGComment() => NGClient.GetNGCommentAsync(this._context);

    public Task<NGCommentResponseCore> AddNGComment(NGCommentType type, string source)
    {
      return NGClient.AddNGCommentAsync(this._context, type, source);
    }

    public Task<NGCommentResponseCore> DeleteNGComment(NGCommentType type, string source)
    {
      return NGClient.DeleteNGCommentAsync(this._context, type, source);
    }

    public Task<List<MylistGroupData>> GetMylistGroupListAsync()
    {
      return MylistGroupClient.GetMylistGroupListAsync(this._context);
    }

    public Task<ContentManageResult> CreateMylistGroupAsync(
      string name,
      string description,
      bool is_public,
      MylistDefaultSort default_sort,
      IconType iconType)
    {
      return MylistGroupClient.AddMylistGroupAsync(this._context, name, description, is_public, default_sort, iconType);
    }

    public Task<ContentManageResult> CreateMylistGroupAsync(MylistGroupData groupData)
    {
      return MylistGroupClient.AddMylistGroupAsync(this._context, groupData.Name, groupData.Description, groupData.GetIsPublic(), MylistDefaultSort.FirstRetrieve_Descending, groupData.GetIconType());
    }

    public Task<ContentManageResult> UpdateMylistGroupAsync(
      string group_id,
      string name,
      string description,
      bool is_public,
      MylistDefaultSort default_sort,
      IconType iconType)
    {
      return MylistGroupClient.UpdateMylistGroupAsync(this._context, group_id, name, description, is_public, default_sort, iconType);
    }

    public Task<ContentManageResult> UpdateMylistGroupAsync(MylistGroupData groupData)
    {
      return MylistGroupClient.UpdateMylistGroupAsync(this._context, groupData.Id, groupData.Name, groupData.Description, groupData.GetIsPublic(), MylistDefaultSort.FirstRetrieve_Descending, groupData.GetIconType());
    }

    public Task<ContentManageResult> RemoveMylistGroupAsync(string group_id)
    {
      return MylistGroupClient.RemoveMylistGroupAsync(this._context, group_id);
    }

    public Task<ContentManageResult> RemoveMylistGroupAsync(MylistGroupData groupData)
    {
      return MylistGroupClient.RemoveMylistGroupAsync(this._context, groupData.Id);
    }

    public Task<NicoVideoResponse> GetMylistListAsync(
      string group_id,
      uint from = 0,
      uint limit = 50,
      Sort sortMethod = Sort.FirstRetrieve,
      Order sortDir = Order.Descending)
    {
      return MylistItemClient.GetMylistListAsync(this._context, group_id, from, limit, sortMethod, sortDir);
    }

    public Task<List<MylistData>> GetMylistItemListAsync(string group_id)
    {
      return MylistGroupData.IsDeflist(group_id) ? DeflistClient.GetDeflistAsync(this._context) : MylistItemClient.GetMylistItemAsync(this._context, group_id);
    }

    public Task<ContentManageResult> AddMylistItemAsync(
      string group_id,
      NiconicoItemType item_type,
      string item_id,
      string description)
    {
      return MylistGroupData.IsDeflist(group_id) ? DeflistClient.AddDeflistAsync(this._context, item_type, item_id, description) : MylistItemClient.AddMylistItemAsync(this._context, group_id, item_type, item_id, description);
    }

    public Task<ContentManageResult> AddMylistItemAsync(string group_id, MylistData mylistData)
    {
      return MylistItemClient.AddMylistItemAsync(this._context, mylistData.GroupId, mylistData.ItemType, mylistData.ItemId, mylistData.Description);
    }

    public Task<ContentManageResult> UpdateMylistItemAsync(
      string group_id,
      NiconicoItemType item_type,
      string item_id,
      string description)
    {
      return MylistGroupData.IsDeflist(group_id) ? DeflistClient.UpdateDeflistAsync(this._context, item_type, item_id, description) : MylistItemClient.UpdateMylistItemAsync(this._context, group_id, item_type, item_id, description);
    }

    public Task<ContentManageResult> UpdateMylistItemAsync(string group_id, MylistData mylistData)
    {
      return this.UpdateMylistItemAsync(mylistData.GroupId, mylistData.ItemType, mylistData.ItemId, mylistData.Description);
    }

    public Task<ContentManageResult> RemoveMylistItemAsync(
      string group_id,
      NiconicoItemType item_type,
      params string[] itemIdList)
    {
      return MylistGroupData.IsDeflist(group_id) ? DeflistClient.RemoveDeflistAsync(this._context, item_type, itemIdList) : MylistItemClient.RemoveMylistItemAsync(this._context, group_id, item_type, itemIdList);
    }

    public Task<ContentManageResult> CopyMylistItemAsync(
      string group_id,
      string target_group_id,
      NiconicoItemType itemType,
      params string[] itemIdList)
    {
      if (group_id == target_group_id)
        return Task.FromResult<ContentManageResult>(ContentManageResult.Success);
      if (MylistGroupData.IsDeflist(target_group_id))
        throw new NotSupportedException("not support mylist item copy to Deflist(とりあえずマイリスト)");
      return MylistGroupData.IsDeflist(group_id) ? DeflistClient.CopyDeflistAsync(this._context, target_group_id, itemType, itemIdList) : MylistItemClient.CopyMylistItemAsync(this._context, group_id, target_group_id, itemType, itemIdList);
    }

    public Task<ContentManageResult> MoveMylistItemAsync(
      string group_id,
      string target_group_id,
      NiconicoItemType itemType,
      params string[] itemIdList)
    {
      if (group_id == target_group_id)
        return Task.FromResult<ContentManageResult>(ContentManageResult.Success);
      if (MylistGroupData.IsDeflist(target_group_id))
        throw new NotSupportedException("not support mylist item move to Deflist(とりあえずマイリスト)");
      return MylistGroupData.IsDeflist(group_id) ? DeflistClient.MoveDeflistAsync(this._context, target_group_id, itemType, itemIdList) : MylistItemClient.MoveMylistItemAsync(this._context, group_id, target_group_id, itemType, itemIdList);
    }

    public Task<MylistGroupDetail> GetMylistGroupDetailAsync(string group_id)
    {
      return MylistGroupClient.GetMylistGroupDetailAsync(this._context, group_id);
    }

    public Task<FollowCommunityResponse> GetFollowCommunityAsync(int page)
    {
      return FollowCommunityClient.GetFollowCommunityAsync(this._context, page);
    }

    public Task<bool> AddFollowCommunityAsync(
      string communityId,
      string title = "",
      string comment = "",
      bool notify = false)
    {
      return FollowCommunityClient.AddFollowCommunity(this._context, communityId, title, comment, notify);
    }

    public Task<CommunityLeaveToken> GetFollowCommunityLeaveTokenAsync(string communityId)
    {
      return FollowCommunityClient.GetCommunityLeaveToken(this._context, communityId);
    }

    public Task<bool> RemoveFollowCommunityAsync(CommunityLeaveToken leaveToken)
    {
      return FollowCommunityClient.RemoveFollowCommunity(this._context, leaveToken);
    }
  }
}
