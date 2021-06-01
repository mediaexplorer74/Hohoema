﻿using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using NiconicoToolkit.Account;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
#if WINDOWS_UWP
using Windows.Web;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using Windows.Web.Http.Headers;
#else
using System.Net;
using System.Net.Http;
#endif

namespace NiconicoToolkit.Follow
{

    public sealed class FollowClient
    {
        private readonly NiconicoContext _context;
        private readonly JsonSerializerOptions _defaultOptions;

        public TagsFollowSubClient Tag { get; }
        public UserFollowSubClient User { get; }
        public MylistFollowSubClient Mylist { get; }
        public ChannelFollowSubClient Channel { get; }
        public CommunityFollowSubClient Community { get; }

        public FollowClient(NiconicoContext context)
        {
            _context = context;

            _defaultOptions = new JsonSerializerOptions() 
            { 
                Converters =
                {
                    new JsonStringEnumMemberConverter()
                }
            };
            
            Tag = new TagsFollowSubClient(this, _context, _defaultOptions);
            User = new UserFollowSubClient(this, _context, _defaultOptions);
            Mylist = new MylistFollowSubClient(this, _context, _defaultOptions);
            Channel = new ChannelFollowSubClient(this, _context, _defaultOptions);
            Community = new CommunityFollowSubClient(this, _context, _defaultOptions);
        }

        public sealed class TagsFollowSubClient
        {
            private readonly FollowClient _followClient;
            private readonly NiconicoContext _context;
            private readonly JsonSerializerOptions _options;

            internal TagsFollowSubClient(FollowClient followClient, NiconicoContext context, JsonSerializerOptions options)
            {
                _followClient = followClient;
                _context = context;
                _options = options;
            }


            public async Task<FollowTagsResponse> GetFollowTagsAsync()
            {
                var uri = $"https://nvapi.nicovideo.jp/v1/users/me/following/tags";
                await _context.PrepareCorsAsscessAsync(HttpMethod.Get, uri);
                return await _context.GetJsonAsAsync<FollowTagsResponse>(uri, _options);
            }



            public Task<ContentManageResult> AddFollowTagAsync(string tag)
            {
                return _followClient.AddFollowInternalAsync($"https://nvapi.nicovideo.jp/v1/users/me/following/tags?tag={tag}");
            }

            public Task<ContentManageResult> RemoveFollowTagAsync(string tag)
            {
                return _followClient.RemoveFollowInternalAsync($"https://nvapi.nicovideo.jp/v1/users/me/following/tags?tag={tag}");
            }

            /*
            public static Task<bool> IsFollowingTagAsync(string tag)
            {
                return _followClient..GetFollowedInternalAsync(context, $"https://nvapi.nicovideo.jp/v1/users/me/following/tags?tag={Uri.EscapeDataString(tag)}");
            }
            */

        }


        public sealed class UserFollowSubClient
        {
            private readonly FollowClient _followClient;
            private readonly NiconicoContext _context;
            private readonly JsonSerializerOptions _options;

            internal UserFollowSubClient(FollowClient followClient, NiconicoContext context, JsonSerializerOptions options)
            {
                _followClient = followClient;
                _context = context;
                _options = options;
            }


            public async Task<FollowUsersResponse> GetFollowUsersAsync(uint pageSize, FollowUsersResponse lastUserResponse = null)
            {
                var uri = $"https://nvapi.nicovideo.jp/v1/users/me/following/users?pageSize={pageSize}";
                if (lastUserResponse != null)
                {
                    uri += "&cursor=" + lastUserResponse.Data.Summary.Cursor;
                }

                await _context.PrepareCorsAsscessAsync(HttpMethod.Get, uri);
                return await _context.GetJsonAsAsync<FollowUsersResponse>(uri, _options);
            }


            public Task<ContentManageResult> AddFollowUserAsync(string userId)
            {
                return _followClient.AddFollowInternalAsync($"https://public.api.nicovideo.jp/v1/user/followees/niconico-users/{userId}.json");
            }

            public Task<ContentManageResult> RemoveFollowUserAsync(string userId)
            {
                return _followClient.RemoveFollowInternalAsync($"https://public.api.nicovideo.jp/v1/user/followees/niconico-users/{userId}.json");
            }

            public Task<bool> IsFollowingUserAsync(uint userId)
            {
                return _followClient.GetFollowedInternalAsync($"https://public.api.nicovideo.jp/v1/user/followees/niconico-users/{userId}.json");
            }
        }

        public sealed class MylistFollowSubClient
        {
            private readonly FollowClient _followClient;
            private readonly NiconicoContext _context;
            private readonly JsonSerializerOptions _options;

            internal MylistFollowSubClient(FollowClient followClient, NiconicoContext context, JsonSerializerOptions options)
            {
                _followClient = followClient;
                _context = context;
                _options = options;
            }


            public async Task<FollowMylistResponse> GetFollowMylistsAsync(uint sampleItemCount = 3)
            {
                var uri = $"https://nvapi.nicovideo.jp/v1/users/me/following/mylists?sampleItemCount={sampleItemCount}";
                await _context.PrepareCorsAsscessAsync(HttpMethod.Get, uri);
                return await _context.GetJsonAsAsync<FollowMylistResponse>(uri, _options);
            }





            public Task<ContentManageResult> AddFollowMylistAsync(string mylistId)
            {
                return _followClient.AddFollowInternalAsync($"https://nvapi.nicovideo.jp/v1/users/me/following/mylists/{mylistId}");
            }

            public Task<ContentManageResult> RemoveFollowMylistAsync(string mylistId)
            {
                return _followClient.RemoveFollowInternalAsync($"https://nvapi.nicovideo.jp/v1/users/me/following/mylists/{mylistId}");
            }

            /*
            public static Task<bool> IsFollowingMylistAsync(string mylistId)
            {
                return _followClient.GetFollowedInternalAsync(context, $"https://nvapi.nicovideo.jp/v1/users/me/following/mylists/{mylistId}");
            }
            */
        }


        public sealed class ChannelFollowSubClient
        {
            private readonly FollowClient _followClient;
            private readonly NiconicoContext _context;
            private readonly JsonSerializerOptions _options;

            internal ChannelFollowSubClient(FollowClient followClient, NiconicoContext context, JsonSerializerOptions options)
            {
                _followClient = followClient;
                _context = context;
                _options = options;
            }


            public async Task<FollowChannelResponse> GetFollowChannelAsync(uint offset = 0, uint limit = 25)
            {
                var uri = $"https://public.api.nicovideo.jp/v1/user/followees/channels.json?limit={limit}&offset={offset}";
                await _context.PrepareCorsAsscessAsync(HttpMethod.Get, uri);
                return await _context.GetJsonAsAsync<FollowChannelResponse>(uri, _options);
            }




            public async Task<ChannelAuthorityResponse> GetChannelAuthorityAsync(uint channelNumberId)
            {
                return await _context.GetJsonAsAsync<ChannelAuthorityResponse>(
                    $"https://public.api.nicovideo.jp/v1/channel/channelapp/channels/{channelNumberId}.json", _options
                    );
            }


            struct ChannelFollowApiInfo
            {
                public string AddApi { get; set; }
                public string DeleteApi { get; set; }
                public string Params { get; set; }
            }


            private async Task<ChannelFollowApiInfo> GetFollowChannelApiInfo(string channelId)
            {
                bool isScreenName = true;
                if (channelId.StartsWith("ch") && char.IsDigit(channelId.Last()))
                {
                    isScreenName = false;
                }
                else if (channelId.All(c => char.IsDigit(c)))
                {
                    channelId = "ch" + channelId;
                    isScreenName = false;
                }

                var res = await _context.GetAsync(isScreenName
                    ? $"http://ch.nicovideo.jp/{channelId}"
                    : $"http://ch.nicovideo.jp/channels/{channelId}"
                    );

                var htmlParser = new HtmlParser();

                using var stream = await res.Content.ReadAsInputStreamAsync();
                using var document = await htmlParser.ParseDocumentAsync(stream.AsStreamForRead());
                var bookmarkAnchorNode = document.QuerySelector(".bookmark");
                return new ChannelFollowApiInfo()
                {
                    AddApi = bookmarkAnchorNode.Attributes["api_add"].Value,
                    DeleteApi = bookmarkAnchorNode.Attributes["api_delete"].Value,
                    Params = System.Net.WebUtility.HtmlDecode(bookmarkAnchorNode.Attributes["params"].Value)
                };
            }

            public async Task<ChannelFollowResult> AddFollowChannelAsync(string channelId)
            {
                var apiInfo = await GetFollowChannelApiInfo(channelId);
                return await _context.GetJsonAsAsync<ChannelFollowResult>($"{apiInfo.AddApi}?{apiInfo.Params}");
            }


            public async Task<ChannelFollowResult> DeleteFollowChannelAsync(string channelId)
            {
                var apiInfo = await GetFollowChannelApiInfo(channelId);
                return await _context.GetJsonAsAsync<ChannelFollowResult>($"{apiInfo.DeleteApi}?{apiInfo.Params}");

            }
        }


        public sealed class CommunityFollowSubClient
        {
            private readonly FollowClient _followClient;
            private readonly NiconicoContext _context;
            private readonly JsonSerializerOptions _options;

            internal CommunityFollowSubClient(FollowClient followClient, NiconicoContext context, JsonSerializerOptions options)
            {
                _followClient = followClient;
                _context = context;
                _options = options;
            }


            public async Task<FollowCommunityResponse> GetFollowCommunityAsync(int page = 0, int limit = 25)
            {
                var uri = $"https://public.api.nicovideo.jp/v1/user/followees/communities.json?limit={limit}&page={page}";
                await _context.PrepareCorsAsscessAsync(HttpMethod.Get, uri);
                return await _context.GetJsonAsAsync<FollowCommunityResponse>(uri, _options);
            }


            public Task<UserOwnedCommunityResponse> GetUserOwnedCommunitiesAsync(uint userId)
            {
                return _context.GetJsonAsAsync<UserOwnedCommunityResponse>(
                    $"https://public.api.nicovideo.jp/v1/user/{userId}/communities.json", _options
                    );
            }


            public async Task<CommunityAuthorityResponse> GetCommunityAuthorityAsync(string communityId)
            {
                var communityIdWoCo = communityId.Substring(2);
                var communityJoinPageUrl = new Uri($"https://com.nicovideo.jp/motion/{communityId}");

                return await _context.GetJsonAsAsync<CommunityAuthorityResponse>(
                    $"https://com.nicovideo.jp/api/v1/communities/{communityIdWoCo}/authority.json", _options
                    );
            }

            public async Task<ContentManageResult> AddFollowCommunityAsync(string communityId)
            {
                var communityIdWoCo = communityId.Substring(2);
                var communityJoinPageUrl = new Uri($"https://com.nicovideo.jp/motion/{communityId}");

                var uri = $"https://com.nicovideo.jp/api/v1/communities/{communityIdWoCo}/follows.json";
                //            await PrepareCorsAsscessAsync(HttpMethod.Post, uri);

                var res = await _context.SendAsync(HttpMethod.Post, uri, content: null, headers =>
                {
#if WINDOWS_UWP
                    headers.Referer = communityJoinPageUrl;
                    headers.Host = new Windows.Networking.HostName("com.nicovideo.jp");
#else
                headers.Referrer = communityJoinPageUrl;
                headers.Host = "com.nicovideo.jp";
#endif
                    headers.Add("Origin", "https://com.nicovideo.jp");
                    headers.Add("X-Requested-By", communityJoinPageUrl.OriginalString);
                });

                var result = await res.Content.ReadAsAsync<ResponseWithMeta>();
                return result.Meta.IsSuccess ? ContentManageResult.Success : ContentManageResult.Failed;
            }



            // 成功すると 200 
            // http://com.nicovideo.jp/motion/co2128730/done
            // にリダイレクトされる

            // 失敗すると
            // 404

            // 申請に許可が必要な場合は未調査


            // Communityからの登録解除
            // http://com.nicovideo.jp/leave/co2128730
            // にアクセスして、フォームから timeとcommit_keyを抽出して
            // time: UNIX_TIME
            // commit_key
            // commit
            // http://com.nicovideo.jp/leave/co2128730 にPOSTする
            // 成功したら200、失敗したら404

            // コミュニティオーナーとしてフォロー解除を行うとコミュニティの解散になるため、注意が必要


            private async Task<CommunityLeaveToken> GetCommunityLeaveTokenAsync(string url, string communityId)
            {
                CommunityLeaveToken leaveToken = new CommunityLeaveToken()
                {
                    CommunityId = communityId
                };

                var res = await _context.GetAsync(url);
                var parser = new HtmlParser();
                using var stream = await res.Content.ReadAsInputStreamAsync();
                using var document = await parser.ParseDocumentAsync(stream.AsStreamForRead());

                var hiddenInputs = document.QuerySelectorAll("body > main > div > form > input");

                foreach (var hiddenInput in hiddenInputs)
                {
                    var nameAttr = hiddenInput.GetAttribute("name");
                    if (nameAttr == "time")
                    {
                        var timeValue = hiddenInput.GetAttribute("value");
                        leaveToken.Time = timeValue;
                    }
                    else if (nameAttr == "commit_key")
                    {
                        var commit_key = hiddenInput.GetAttribute("value");
                        leaveToken.CommitKey = commit_key;
                    }
                    else if (nameAttr == "commit")
                    {
                        var commit = hiddenInput.GetAttribute("value");
                        leaveToken.Commit = commit;
                    }
                }

                return leaveToken;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="communityId"></param>
            /// <remarks>コミュニティオーナーがフォロー解除を行うとコミュニティ解散という重大な操作になるため注意</remarks>
            /// <returns></returns>
            public async Task<ContentManageResult> RemoveFollowCommunityAsync(string communityId)
            {
                var url = $"https://com.nicovideo.jp/leave/{communityId}";
                var token = await GetCommunityLeaveTokenAsync(url, communityId);
                var dict = new Dictionary<string, string>();
                dict.Add("time", token.Time);
                dict.Add("commit_key", token.CommitKey);
                dict.Add("commit", token.Commit);

#if WINDOWS_UWP
                var content = new HttpFormUrlEncodedContent(dict);
#else
            var content = new FormUrlEncodedContent(dict);
#endif

                var postResult = await _context.SendAsync(HttpMethod.Post, url, content: content, headers =>
                {
                    headers.Add("Upgrade-Insecure-Requests", "1");
                    headers.Add("Referer", url);
                    headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                }, HttpCompletionOption.ResponseHeadersRead);

                Debug.WriteLine(postResult);

                return postResult.IsSuccessStatusCode ? ContentManageResult.Success : ContentManageResult.Failed;
            }

            public class CommunityLeaveToken
            {
                public string CommunityId { get; set; }
                public string Time { get; set; }
                public string CommitKey { get; set; }
                public string Commit { get; set; }
            }

        }


        private async Task<bool> GetFollowedInternalAsync(string uri)
        {
            var res= await _context.SendAsync(HttpMethod.Get, uri, content: null, headers =>
            {
                headers.Add("X-Request-With", "https://www.nicovideo.jp/my/follow");
            });

            var result = await res.Content.ReadAsAsync<FollowedResultResponce>();
            return result.Data.IsFollowing;
        }

        private async Task<ContentManageResult> AddFollowInternalAsync(string uri)
        {
            await _context.PrepareCorsAsscessAsync(HttpMethod.Post, uri);
            var res = await _context.SendAsync(HttpMethod.Post, uri, content: null, headers =>
            {
                headers.Add("X-Request-With", "https://www.nicovideo.jp/my/follow");
            }
            , HttpCompletionOption.ResponseHeadersRead
            );
            return res.IsSuccessStatusCode ? ContentManageResult.Success : ContentManageResult.Failed;
        }

        private async Task<ContentManageResult> RemoveFollowInternalAsync(string uri)
        {
            await _context.PrepareCorsAsscessAsync(HttpMethod.Delete, uri);
            var res = await _context.SendAsync(HttpMethod.Delete, uri, content: null, headers =>
            {
                headers.Add("X-Request-With", "https://www.nicovideo.jp/my/follow");
            }
            , HttpCompletionOption.ResponseHeadersRead
            );
            return res.IsSuccessStatusCode ? ContentManageResult.Success : ContentManageResult.Failed;
        }
    }


}
