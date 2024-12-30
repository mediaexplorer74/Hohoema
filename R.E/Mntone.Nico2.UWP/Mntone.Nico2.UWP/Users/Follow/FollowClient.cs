// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.Follow.FollowClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Users.Follow
{
  internal sealed class FollowClient
  {
    public static Task<string> GetFavUsersDataAsync(NiconicoContext context)
    {
      return context.GetClient().GetStringAsync(string.Format("{0}", (object) NiconicoUrls.UserFavListApiUrl));
    }

    public static Task<string> ExistFollowDataAsync(
      NiconicoContext context,
      NiconicoItemType item_type,
      string item_id)
    {
      return context.GetClient().GetStringAsync(string.Format("{0}?{1}={2}&{3}={4}", (object) NiconicoUrls.UserFavExistApiUrl, (object) nameof (item_type), (object) (uint) item_type, (object) nameof (item_id), (object) item_id));
    }

    public static Task<string> AddFollowDataAsync(
      NiconicoContext context,
      NiconicoItemType item_type,
      string item_id)
    {
      Dictionary<string, string> keyvalues = new Dictionary<string, string>()
      {
        {
          nameof (item_type),
          ((uint) item_type).ToString()
        },
        {
          nameof (item_id),
          item_id
        }
      };
      return context.PostAsync(NiconicoUrls.UserFavAddApiUrl, keyvalues);
    }

    public static Task<string> RemoveFollowDataAsync(
      NiconicoContext context,
      NiconicoItemType itemType,
      string item_id)
    {
      Dictionary<string, string> keyvalues = new Dictionary<string, string>()
      {
        {
          NiconicoQueryHelper.Make_idlist_QueryKeyString(itemType),
          NiconicoQueryHelper.RemoveIdPrefix(item_id)
        }
      };
      return context.PostAsync(NiconicoUrls.UserFavRemoveApiUrl, keyvalues);
    }

    public static Task<string> GetFollowTagsDataAsync(NiconicoContext context)
    {
      Dictionary<string, string> keyvalues = new Dictionary<string, string>()
      {
        {
          "cache",
          "false"
        },
        {
          "dataType",
          "json"
        }
      };
      return context.PostAsync(NiconicoUrls.UserFavTagListUrl, keyvalues, false);
    }

    public static Task<string> AddFollowTagDataAsync(NiconicoContext context, string tag)
    {
      Dictionary<string, string> keyvalues = new Dictionary<string, string>()
      {
        {
          nameof (tag),
          tag.ToEnsureHankakuNumberTagString()
        }
      };
      return context.PostAsync(NiconicoUrls.UserFavTagAddUrl, keyvalues);
    }

    public static Task<string> RemoveFollowTagDataAsync(NiconicoContext context, string tag)
    {
      Dictionary<string, string> keyvalues = new Dictionary<string, string>()
      {
        {
          nameof (tag),
          tag.ToEnsureHankakuNumberTagString()
        }
      };
      return context.PostAsync(NiconicoUrls.UserFavTagRemoveUrl, keyvalues);
    }

    public static Task<string> GetFollowMylistDataAsync(NiconicoContext context)
    {
      return context.GetStringAsync(NiconicoUrls.UserFavMylistPageUrl);
    }

    public static List<FollowData> ParseWatchItemFollowData(string json)
    {
      WatchItemResponse watchItemResponse = JsonSerializerExtensions.Load<WatchItemResponse>(json);
      return watchItemResponse.status == "ok" ? watchItemResponse.watchitem.Select<Watchitem, FollowData>((Func<Watchitem, FollowData>) (x => new FollowData()
      {
        ItemType = NiconicoItemType.User,
        ItemId = x.item_id,
        Title = x.item_data.nickname
      })).ToList<FollowData>() : new List<FollowData>();
    }

    public static List<FollowData> ParseFollowPageHtml(string html, NiconicoItemType item_type)
    {
      HtmlDocument htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(html);
      return htmlDocument.DocumentNode.Descendants("div").Single<HtmlNode>((Func<HtmlNode, bool>) (x => x.Attributes.Contains("class") && x.Attributes["class"].Value == "content")).GetElementByClassName("articleBody").GetElementsByClassName("outer").Select<HtmlNode, FollowData>((Func<HtmlNode, FollowData>) (x =>
      {
        FollowData followPageHtml = new FollowData();
        HtmlNode childNode = x.ChildNodes["h5"].ChildNodes["a"];
        string attributeValue = childNode.GetAttributeValue("href", "");
        followPageHtml.ItemId = ((IEnumerable<string>) attributeValue.Split('/')).Last<string>();
        followPageHtml.Title = childNode.InnerText;
        followPageHtml.ItemType = item_type;
        return followPageHtml;
      })).ToList<FollowData>();
    }

    public static List<string> ParseFollowTagJson(string json)
    {
      FollowTagResponse followTagResponse = JsonSerializerExtensions.Load<FollowTagResponse>(json);
      return followTagResponse.status == "ok" ? followTagResponse.favtag_items.Select<FollowTagItem, string>((Func<FollowTagItem, string>) (x => x.tag.ToEnsureHankakuNumberTagString())).ToList<string>() : new List<string>();
    }

    public static Task<List<FollowData>> GetFollowUsersAsync(NiconicoContext context)
    {
      return FollowClient.GetFavUsersDataAsync(context).ContinueWith<List<FollowData>>((Func<Task<string>, List<FollowData>>) (prevTask => FollowClient.ParseWatchItemFollowData(prevTask.Result)));
    }

    public static Task<List<string>> GetFollowTagsAsync(NiconicoContext context)
    {
      return FollowClient.GetFollowTagsDataAsync(context).ContinueWith<List<string>>((Func<Task<string>, List<string>>) (prevTask => FollowClient.ParseFollowTagJson(prevTask.Result)));
    }

    public static Task<List<FollowData>> GetFollowMylistAsync(NiconicoContext context)
    {
      return FollowClient.GetFollowMylistDataAsync(context).ContinueWith<List<FollowData>>((Func<Task<string>, List<FollowData>>) (prevTask => FollowClient.ParseFollowPageHtml(prevTask.Result, NiconicoItemType.Mylist)));
    }

    public static Task<ContentManageResult> ExistFollowAsync(
      NiconicoContext context,
      NiconicoItemType item_type,
      string item_id)
    {
      return FollowClient.ExistFollowDataAsync(context, item_type, item_id).ContinueWith<ContentManageResult>((Func<Task<string>, ContentManageResult>) (prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result)));
    }

    public static Task<ContentManageResult> AddFollowAsync(
      NiconicoContext context,
      NiconicoItemType item_type,
      string item_id)
    {
      return FollowClient.AddFollowDataAsync(context, item_type, item_id).ContinueWith<ContentManageResult>((Func<Task<string>, ContentManageResult>) (prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result)));
    }

    public static Task<ContentManageResult> RemoveFollowAsync(
      NiconicoContext context,
      NiconicoItemType item_type,
      string item_id)
    {
      return FollowClient.RemoveFollowDataAsync(context, item_type, item_id).ContinueWith<ContentManageResult>((Func<Task<string>, ContentManageResult>) (prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result)));
    }

    public static Task<ContentManageResult> AddFollowTagAsync(NiconicoContext context, string tag)
    {
      return FollowClient.AddFollowTagDataAsync(context, tag).ContinueWith<ContentManageResult>((Func<Task<string>, ContentManageResult>) (prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result)));
    }

    public static Task<ContentManageResult> RemoveFollowTagAsync(
      NiconicoContext context,
      string tag)
    {
      return FollowClient.RemoveFollowTagDataAsync(context, tag).ContinueWith<ContentManageResult>((Func<Task<string>, ContentManageResult>) (prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result)));
    }
  }
}
