// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.MylistItem.MylistItemClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Mylist;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

#nullable disable
namespace Mntone.Nico2.Users.MylistItem
{
  internal sealed class MylistItemClient
  {
    public static Task<string> GetMylistItemDataAsync(NiconicoContext context, string group_id)
    {
      return context.PostAsync(NiconicoUrls.MylistListUrl, new Dictionary<string, string>()
      {
        {
          nameof (group_id),
          group_id
        }
      });
    }

    public static async Task<string> AddMylistItemDataAsync(
      NiconicoContext context,
      string group_id,
      NiconicoItemType item_type,
      string item_id,
      string description)
    {
      Dictionary<string, string> dict = new Dictionary<string, string>();
      MylistAdditionInfo mylistToken = await context.GetMylistToken(group_id, item_id);
      dict.Add(nameof (group_id), group_id);
      dict.Add(nameof (item_type), ((uint) item_type).ToString());
      dict.Add(nameof (item_id), mylistToken.ItemId);
      if (mylistToken.Values.ContainsKey("item_amc"))
        dict.Add("item_amc", mylistToken.ItemAmc);
      dict.Add(nameof (description), description);
      dict.Add("token", mylistToken.Token);
      return await context.PostAsync(NiconicoUrls.MylistAddUrl, dict, false);
    }

    public static Task<string> UpdateMylistItemDataAsync(
      NiconicoContext context,
      string group_id,
      NiconicoItemType item_type,
      string item_id,
      string description)
    {
      return context.PostAsync(NiconicoUrls.MylistUpdateUrl, new Dictionary<string, string>()
      {
        {
          nameof (group_id),
          group_id
        },
        {
          nameof (item_type),
          ((uint) item_type).ToString()
        },
        {
          nameof (item_id),
          item_id
        },
        {
          nameof (description),
          description
        }
      });
    }

    public static Task<string> RemoveMylistItemDataAsync(
      NiconicoContext context,
      string group_id,
      NiconicoItemType item_type,
      string[] itemIdList)
    {
      Dictionary<string, string> keyvalues = new Dictionary<string, string>();
      keyvalues.Add(nameof (group_id), group_id);
      string key = NiconicoQueryHelper.Make_idlist_QueryKeyString(item_type);
      foreach (string itemId in itemIdList)
        keyvalues.Add(key, itemId);
      return context.PostAsync(NiconicoUrls.MylistRemoveUrl, keyvalues);
    }

    public static Task<string> CopyMylistDataAsync(
      NiconicoContext context,
      string group_id,
      string target_group_id,
      NiconicoItemType itemType,
      params string[] itemIdList)
    {
      Dictionary<string, string> keyvalues = new Dictionary<string, string>();
      keyvalues.Add(nameof (group_id), group_id);
      keyvalues.Add(nameof (target_group_id), target_group_id);
      string key = NiconicoQueryHelper.Make_idlist_QueryKeyString(itemType);
      foreach (string itemId in itemIdList)
        keyvalues.Add(key, itemId);
      return context.PostAsync(NiconicoUrls.MylistCopyUrl, keyvalues);
    }

    public static Task<string> MoveMylistDataAsync(
      NiconicoContext context,
      string group_id,
      string target_group_id,
      NiconicoItemType itemType,
      params string[] itemIdList)
    {
      Dictionary<string, string> keyvalues = new Dictionary<string, string>();
      keyvalues.Add(nameof (group_id), group_id);
      keyvalues.Add(nameof (target_group_id), target_group_id);
      string key = NiconicoQueryHelper.Make_idlist_QueryKeyString(itemType);
      foreach (string itemId in itemIdList)
        keyvalues.Add(key, itemId);
      return context.PostAsync(NiconicoUrls.MylistMoveUrl, keyvalues);
    }

    public static Task<string> GetMylistListDataAsync(
      NiconicoContext context,
      string group_id,
      uint from,
      uint limit,
      Sort sortMethod,
      Order sortDir)
    {
      return context.GetStringAsync(NiconicoUrls.MylistListlApi, new Dictionary<string, string>()
      {
        {
          nameof (group_id),
          group_id
        },
        {
          nameof (from),
          from.ToString()
        },
        {
          nameof (limit),
          limit.ToString()
        },
        {
          nameof (sortMethod),
          sortMethod.ToShortString()
        },
        {
          nameof (sortDir),
          sortDir.ToShortString()
        }
      });
    }

    private static NicoVideoResponse ParseMylistListXml(string xml)
    {
      using (StringReader stringReader = new StringReader(xml))
        return (NicoVideoResponse) new XmlSerializer(typeof (NicoVideoResponse)).Deserialize((TextReader) stringReader);
    }

    public static Task<NicoVideoResponse> GetMylistListAsync(
      NiconicoContext context,
      string group_id,
      uint from,
      uint limit,
      Sort sortMethod,
      Order sortDir)
    {
      return MylistItemClient.GetMylistListDataAsync(context, group_id, from, limit, sortMethod, sortDir).ContinueWith<NicoVideoResponse>((Func<Task<string>, NicoVideoResponse>) (prevTask => MylistItemClient.ParseMylistListXml(prevTask.Result)));
    }

    public static Task<List<MylistData>> GetMylistItemAsync(
      NiconicoContext context,
      string group_id)
    {
      return MylistItemClient.GetMylistItemDataAsync(context, group_id).ContinueWith<List<MylistData>>((Func<Task<string>, List<MylistData>>) (prevTask => MylistJsonSerializeHelper.ParseMylistItemResponse(prevTask.Result)));
    }

    public static Task<ContentManageResult> AddMylistItemAsync(
      NiconicoContext context,
      string group_id,
      NiconicoItemType item_type,
      string item_id,
      string description)
    {
      return MylistItemClient.AddMylistItemDataAsync(context, group_id, item_type, item_id, description).ContinueWith<ContentManageResult>((Func<Task<string>, ContentManageResult>) (prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result)));
    }

    public static Task<ContentManageResult> UpdateMylistItemAsync(
      NiconicoContext context,
      string group_id,
      NiconicoItemType item_type,
      string item_id,
      string description)
    {
      return MylistItemClient.UpdateMylistItemDataAsync(context, group_id, item_type, item_id, description).ContinueWith<ContentManageResult>((Func<Task<string>, ContentManageResult>) (prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result)));
    }

    public static Task<ContentManageResult> RemoveMylistItemAsync(
      NiconicoContext context,
      string group_id,
      NiconicoItemType item_type,
      params string[] itemIdList)
    {
      return MylistItemClient.RemoveMylistItemDataAsync(context, group_id, item_type, itemIdList).ContinueWith<ContentManageResult>((Func<Task<string>, ContentManageResult>) (prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result)));
    }

    public static Task<ContentManageResult> CopyMylistItemAsync(
      NiconicoContext context,
      string group_id,
      string target_group_id,
      NiconicoItemType itemType,
      params string[] itemIdList)
    {
      return MylistItemClient.CopyMylistDataAsync(context, group_id, target_group_id, itemType, itemIdList).ContinueWith<ContentManageResult>((Func<Task<string>, ContentManageResult>) (prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result)));
    }

    public static Task<ContentManageResult> MoveMylistItemAsync(
      NiconicoContext context,
      string group_id,
      string target_group_id,
      NiconicoItemType itemType,
      params string[] itemIdList)
    {
      return MylistItemClient.MoveMylistDataAsync(context, group_id, target_group_id, itemType, itemIdList).ContinueWith<ContentManageResult>((Func<Task<string>, ContentManageResult>) (prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result)));
    }
  }
}
