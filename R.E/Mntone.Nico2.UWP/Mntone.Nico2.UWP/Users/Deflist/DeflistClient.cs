// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.Deflist.DeflistClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Mylist;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Users.Deflist
{
  internal sealed class DeflistClient
  {
    public static Task<string> GetDeflistDataAsync(NiconicoContext context)
    {
      return context.PostAsync(NiconicoUrls.MylistDeflistListUrl);
    }

    public static async Task<string> AddDeflistDataAsync(
      NiconicoContext context,
      NiconicoItemType item_type,
      string item_id,
      string description)
    {
      Dictionary<string, string> dict = new Dictionary<string, string>();
      string group_id = "default";
      MylistAdditionInfo mylistToken = await context.GetMylistToken(group_id, item_id);
      dict.Add("group_id", group_id);
      dict.Add(nameof (item_type), ((uint) item_type).ToString());
      dict.Add(nameof (item_id), mylistToken.ItemId);
      dict.Add(nameof (description), description);
      dict.Add("token", mylistToken.Token);
      return await context.PostAsync(NiconicoUrls.MylistDeflistAddUrl, dict, false);
    }

    public static Task<string> UpdateDeflistDataAsync(
      NiconicoContext context,
      NiconicoItemType item_type,
      string item_id,
      string description)
    {
      Dictionary<string, string> keyvalues = new Dictionary<string, string>();
      keyvalues.Add(nameof (item_id), item_id);
      if (item_type != NiconicoItemType.Video)
        keyvalues.Add(nameof (item_type), ((uint) item_type).ToString());
      if (!string.IsNullOrWhiteSpace(description))
        keyvalues.Add(nameof (description), description);
      return context.PostAsync(NiconicoUrls.MylistDeflistUpdateUrl, keyvalues);
    }

    public static Task<string> RemoveDeflistDataAsync(
      NiconicoContext context,
      NiconicoItemType itemType,
      params string[] itemIdList)
    {
      Dictionary<string, string> keyvalues = new Dictionary<string, string>();
      string key = NiconicoQueryHelper.Make_idlist_QueryKeyString(itemType);
      foreach (string itemId in itemIdList)
        keyvalues.Add(key, itemId);
      return context.PostAsync(NiconicoUrls.MylistDeflistRemoveUrl, keyvalues);
    }

    public static Task<string> MoveDeflistDataAsync(
      NiconicoContext context,
      string target_group_id,
      NiconicoItemType itemType,
      params string[] itemIdList)
    {
      Dictionary<string, string> keyvalues = new Dictionary<string, string>();
      keyvalues.Add(nameof (target_group_id), target_group_id);
      string key = NiconicoQueryHelper.Make_idlist_QueryKeyString(itemType);
      foreach (string itemId in itemIdList)
        keyvalues.Add(key, itemId);
      return context.PostAsync(NiconicoUrls.MylistDeflistMoveUrl, keyvalues);
    }

    public static Task<string> CopyDeflistDataAsync(
      NiconicoContext context,
      string target_group_id,
      NiconicoItemType itemType,
      params string[] itemIdList)
    {
      Dictionary<string, string> keyvalues = new Dictionary<string, string>();
      keyvalues.Add(nameof (target_group_id), target_group_id);
      string key = NiconicoQueryHelper.Make_idlist_QueryKeyString(itemType);
      foreach (string itemId in itemIdList)
        keyvalues.Add(key, itemId);
      return context.PostAsync(NiconicoUrls.MylistDeflistCopyUrl, keyvalues);
    }

    public static Task<List<MylistData>> GetDeflistAsync(NiconicoContext context)
    {
      return DeflistClient.GetDeflistDataAsync(context).ContinueWith<List<MylistData>>((Func<Task<string>, List<MylistData>>) (prevTask => MylistJsonSerializeHelper.ParseMylistItemResponse(prevTask.Result)));
    }

    public static Task<ContentManageResult> AddDeflistAsync(
      NiconicoContext context,
      NiconicoItemType item_type,
      string item_id,
      string description)
    {
      return DeflistClient.AddDeflistDataAsync(context, item_type, item_id, description).ContinueWith<ContentManageResult>((Func<Task<string>, ContentManageResult>) (prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result)));
    }

    public static Task<ContentManageResult> UpdateDeflistAsync(
      NiconicoContext context,
      NiconicoItemType item_type,
      string item_id,
      string description)
    {
      return DeflistClient.UpdateDeflistDataAsync(context, item_type, item_id, description).ContinueWith<ContentManageResult>((Func<Task<string>, ContentManageResult>) (prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result)));
    }

    public static Task<ContentManageResult> RemoveDeflistAsync(
      NiconicoContext context,
      NiconicoItemType item_type,
      params string[] itemIdList)
    {
      return DeflistClient.RemoveDeflistDataAsync(context, item_type, itemIdList).ContinueWith<ContentManageResult>((Func<Task<string>, ContentManageResult>) (prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result)));
    }

    public static Task<ContentManageResult> MoveDeflistAsync(
      NiconicoContext context,
      string target_group_id,
      NiconicoItemType item_type,
      params string[] itemIdList)
    {
      return DeflistClient.MoveDeflistDataAsync(context, target_group_id, item_type, itemIdList).ContinueWith<ContentManageResult>((Func<Task<string>, ContentManageResult>) (prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result)));
    }

    public static Task<ContentManageResult> CopyDeflistAsync(
      NiconicoContext context,
      string target_group_id,
      NiconicoItemType item_type,
      params string[] itemIdList)
    {
      return DeflistClient.CopyDeflistDataAsync(context, target_group_id, item_type, itemIdList).ContinueWith<ContentManageResult>((Func<Task<string>, ContentManageResult>) (prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result)));
    }
  }
}
