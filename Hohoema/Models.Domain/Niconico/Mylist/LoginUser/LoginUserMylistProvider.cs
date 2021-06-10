﻿using I18NPortable;
using LiteDB;
using Hohoema.Models.Domain.Niconico.Video;
using Hohoema.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NiconicoToolkit.Mylist;
using Uno.Extensions;
using NiconicoToolkit.Account;
using NiconicoToolkit.Mylist.LoginUser;
using NiconicoToolkit.Video;

namespace Hohoema.Models.Domain.Niconico.Mylist.LoginUser
{
    public sealed class LoginUserMylistProvider : ProviderBase
    {
        public sealed class LoginUserMylistItemIdRepository : LiteDBServiceBase<LoginUserMylistItemIdEntry>
        {
            public LoginUserMylistItemIdRepository(LiteDatabase liteDatabase) : base(liteDatabase)
            {
                _collection.EnsureIndex(x => x.VideoId);
                _collection.EnsureIndex(x => x.MylistGroupId);
            }

            public void AddItem(string itemId, string mylistId, string videoId)
            {
                _collection.Upsert(new LoginUserMylistItemIdEntry() { ItemId = itemId, MylistGroupId = mylistId, VideoId = videoId });
            }

            public string GetItemId(string mylistId, string videoId)
            {
                return _collection.FindOne(x => x.MylistGroupId == mylistId && x.VideoId == videoId)?.ItemId;
            }
        }

        public sealed class LoginUserMylistItemIdEntry
        {
            [BsonId]
            public string ItemId { get; set; }

            [BsonField]
            public string MylistGroupId { get; set; }

            [BsonField]
            public string VideoId { get; set; }
        }



        private readonly NicoVideoCacheRepository _nicoVideoRepository;
        private readonly NicoVideoProvider _nicoVideoProvider;
        private readonly LoginUserMylistItemIdRepository _loginUserMylistItemIdRepository;

        public LoginUserMylistProvider(
            NiconicoSession niconicoSession,
            NicoVideoProvider nicoVideoProvider,
            LoginUserMylistItemIdRepository loginUserMylistItemIdRepository
            )
            : base(niconicoSession)
        {
            _nicoVideoProvider = nicoVideoProvider;
            _loginUserMylistItemIdRepository = loginUserMylistItemIdRepository;            
        }


        private async Task<LoginUserMylistPlaylist> GetDefaultMylistAsync()
        {
            if (!_niconicoSession.IsLoggedIn) { throw new System.Exception("");  }
            
            var defMylist = await _niconicoSession.ToolkitContext.Mylist.LoginUser.GetWatchAfterItemsAsync(0, 3, MylistSortKey.AddedAt, MylistSortOrder.Asc);

            // TODO: とりあえずマイリストのSortやOrderの取得

            return new LoginUserMylistPlaylist(MylistPlaylistExtension.DefailtMylistId, this) 
            {
                Label = "WatchAfterMylist".Translate(),
                Count = (int)defMylist.Data.Mylist.TotalItemCount,
                UserId = _niconicoSession.UserIdString,
                ThumbnailImages = defMylist.Data.Mylist.Items.Take(3).Select(x => x.Video.Thumbnail.ListingUrl).ToArray(),
            };
        }

        public async Task<List<LoginUserMylistPlaylist>> GetLoginUserMylistGroups()
        {
            using var _ = await _niconicoSession.SigninLock.LockAsync();
            
            if (!_niconicoSession.IsLoggedIn)
            {
                return null;
            }

            List<LoginUserMylistPlaylist> mylistGroups = new List<LoginUserMylistPlaylist>();

            var defaultMylist = await GetDefaultMylistAsync();

            mylistGroups.Add(defaultMylist);

            var res = await _niconicoSession.ToolkitContext.Mylist.LoginUser.GetMylistGroupsAsync(sampleItemCount: 1);

            if (res.Meta.Status != 200)
            {
                return mylistGroups;
            }
            
            foreach (var mylistGroup in res.Data.Mylists)
            {
                var mylist = new LoginUserMylistPlaylist(mylistGroup.Id.ToString(), this)
                {
                    Label = mylistGroup.Name,
                    Count = (int)mylistGroup.ItemsCount,
                    UserId = mylistGroup.Owner.Id,
                    Description = mylistGroup.Description,
                    IsPublic = mylistGroup.IsPublic,
                    //IconType = mylistGroup.co,
                    DefaultSortKey = mylistGroup.DefaultSortKey,
                    DefaultSortOrder = mylistGroup.DefaultSortOrder,
                    SortIndex = res.Data.Mylists.IndexOf(mylistGroup),
                    ThumbnailImages = mylistGroup.SampleItems.Take(3).Select(x => x.Video.Thumbnail.ListingUrl).ToArray(),
                };

                mylistGroups.Add(mylist);
            }

            return mylistGroups;
        }

        public async Task<List<(MylistItem MylistItem, NicoVideo NicoVideo)>> GetLoginUserMylistItemsAsync(IMylist mylist, int page, int pageSize, MylistSortKey sortKey, MylistSortOrder sortOrder)
        {
            if (mylist.UserId != _niconicoSession.UserIdString)
            {
                throw new ArgumentException();
            }

            if (mylist.IsDefaultMylist())
            {
                var mylistItemsRes = await _niconicoSession.ToolkitContext.Mylist.LoginUser.GetWatchAfterItemsAsync(page, pageSize, sortKey, sortOrder);
                var res = mylistItemsRes.Data.Mylist;
                var items = res.Items;
                foreach (var item in items)
                {
                    _loginUserMylistItemIdRepository.AddItem(item.ItemId.ToString(), mylist.Id, item.WatchId);
                }

                return items.Select(x => (x, MylistDataToNicoVideoData(x))).ToList();

            }
            else
            {
                var mylistItemsRes = await _niconicoSession.ToolkitContext.Mylist.LoginUser.GetMylistItemsAsync(mylist.Id, (int)page, (int)pageSize, sortKey, sortOrder);
                var res = mylistItemsRes.Data.Mylist;
                var items = res.Items;
                foreach (var item in items)
                {
                    _loginUserMylistItemIdRepository.AddItem(item.ItemId.ToString(), mylist.Id, item.WatchId);
                }

                return items.Select(x => (x, MylistDataToNicoVideoData(x))).ToList();
            }
        }


        static public bool IsDefaultMylist(IMylist mylist)
        {
            return MylistPlaylistExtension.IsDefaultMylistId(mylist?.Id);
        }


        private NicoVideo MylistDataToNicoVideoData(MylistItem item)
        {
            return _nicoVideoProvider.UpdateCache(item.WatchId, item.Video, item.IsDeleted);
        }


        public async Task<string> AddMylist(string name, string description, bool isPublic, MylistSortKey sortKey, MylistSortOrder sortOrder)
        {
            var result = await _niconicoSession.ToolkitContext.Mylist.LoginUser.CreateMylistAsync(name, description, isPublic, sortKey, sortOrder);
            return result.Data.MylistId.ToString();
        }

        public async Task<bool> UpdateMylist(string mylistId, string name, string description, bool isPublic, MylistSortKey sortKey, MylistSortOrder sortOrder)
        {
            return await _niconicoSession.ToolkitContext.Mylist.LoginUser.UpdateMylistAsync(mylistId, name, description, isPublic, sortKey, sortOrder);
        }


        public async Task<bool> RemoveMylist(string group_id)
        {
            return await _niconicoSession.ToolkitContext.Mylist.LoginUser.RemoveMylistAsync(group_id);
        }




        public Task<ContentManageResult> AddMylistItem(string mylistGroupId, string videoId, string mylistComment = "")
        {
            if (MylistPlaylistExtension.IsDefaultMylistId(mylistGroupId))
            {
                return _niconicoSession.ToolkitContext.Mylist.LoginUser.AddWatchAfterMylistItemAsync(
                    videoId
                    , mylistComment
                    );
            }
            else
            {
                return _niconicoSession.ToolkitContext.Mylist.LoginUser.AddMylistItemAsync(
                    mylistGroupId
                    , videoId
                    , mylistComment
                    );
            }
        }


        public async Task<ContentManageResult> RemoveMylistItem(string mylistGroupId, string videoId)
        {
            var itemId = _loginUserMylistItemIdRepository.GetItemId(mylistGroupId, videoId);
            if (itemId == null) { return ContentManageResult.Failed; }
            if (MylistPlaylistExtension.IsDefaultMylistId(mylistGroupId))
            {
                return await _niconicoSession.ToolkitContext.Mylist.LoginUser.RemoveWatchAfterItemsAsync(new[] { itemId });
            }
            else
            {
                return await _niconicoSession.ToolkitContext.Mylist.LoginUser.RemoveMylistItemsAsync(mylistGroupId, new[] { itemId });
            }
        }

        public async Task<MoveOrCopyMylistItemsResponse> CopyMylistTo(string sourceMylistGroupId, string targetGroupId, params string[] videoIdList)
        {
            var items = videoIdList.Select(x => _loginUserMylistItemIdRepository.GetItemId(sourceMylistGroupId, x));
            return await _niconicoSession.ToolkitContext.Mylist.LoginUser.CopyMylistItemsAsync(sourceMylistGroupId, targetGroupId, items.ToArray());
        }


        public async Task<MoveOrCopyMylistItemsResponse> MoveMylistTo(string sourceMylistGroupId, string targetGroupId, params string[] videoIdList)
        {
            var items = videoIdList.Select(x => _loginUserMylistItemIdRepository.GetItemId(sourceMylistGroupId, x));
            return await _niconicoSession.ToolkitContext.Mylist.LoginUser.MoveMylistItemsAsync(sourceMylistGroupId, targetGroupId, items.ToArray());
        }
    }

}
