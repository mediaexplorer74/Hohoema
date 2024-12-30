// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.MylistGroup.MylistGroupClient
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
namespace Mntone.Nico2.Users.MylistGroup
{
  internal sealed class MylistGroupClient
  {
    public static Task<string> GetMylistGroupListDataAsync(NiconicoContext context)
    {
      return context.PostAsync(NiconicoUrls.MylistGroupListUrl);
    }

    public static Task<string> GetMylistGroupDataAsync(NiconicoContext context, string group_id)
    {
      return context.PostAsync(NiconicoUrls.MylistGroupGetUrl, new Dictionary<string, string>()
      {
        {
          nameof (group_id),
          group_id
        }
      });
    }

    public static Task<string> AddMylistGroupDataAsync(
      NiconicoContext context,
      string name,
      string description,
      bool is_public,
      MylistDefaultSort default_sort,
      IconType icon_id)
    {
      return context.PostAsync(NiconicoUrls.MylistGroupAddUrl, new Dictionary<string, string>()
      {
        {
          nameof (name),
          name
        },
        {
          nameof (description),
          description
        },
        {
          nameof (is_public),
          is_public.ToString1Or0()
        },
        {
          nameof (default_sort),
          ((uint) default_sort).ToString()
        },
        {
          nameof (icon_id),
          ((uint) icon_id).ToString()
        }
      });
    }

    public static Task<string> UpdateMylistGroupDataAsync(
      NiconicoContext context,
      string group_id,
      string name,
      string description,
      bool is_public,
      MylistDefaultSort default_sort,
      IconType icon_id)
    {
      return context.PostAsync(NiconicoUrls.MylistGroupUpdateUrl, new Dictionary<string, string>()
      {
        {
          nameof (group_id),
          group_id
        },
        {
          nameof (name),
          name
        },
        {
          nameof (description),
          description
        },
        {
          nameof (is_public),
          is_public.ToString1Or0()
        },
        {
          nameof (default_sort),
          ((uint) default_sort).ToString()
        },
        {
          nameof (icon_id),
          ((uint) icon_id).ToString()
        }
      });
    }

    public static Task<string> RemoveMylistGroupDataAsync(NiconicoContext context, string group_id)
    {
      return context.PostAsync(NiconicoUrls.MylistGroupRemoveUrl, new Dictionary<string, string>()
      {
        {
          nameof (group_id),
          group_id
        }
      });
    }

    public static Task<string> GetMylistGroupDetailDataAsync(
      NiconicoContext context,
      string group_id)
    {
      return context.GetStringAsync(NiconicoUrls.MylistGroupDetailApi, new Dictionary<string, string>()
      {
        {
          nameof (group_id),
          group_id
        }
      });
    }

    private static MylistGroupDetail ParseMylistGroupDetailXml(string xml)
    {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof (MylistGroupResponse));
      MylistGroupResponse mylistGroupResponse = (MylistGroupResponse) null;
      using (StringReader stringReader = new StringReader(xml))
        mylistGroupResponse = (MylistGroupResponse) xmlSerializer.Deserialize((TextReader) stringReader);
      return mylistGroupResponse.Mylistgroup;
    }

    public static Task<List<MylistGroupData>> GetMylistGroupListAsync(NiconicoContext context)
    {
      return MylistGroupClient.GetMylistGroupListDataAsync(context).ContinueWith<List<MylistGroupData>>((Func<Task<string>, List<MylistGroupData>>) (prevTask => MylistJsonSerializeHelper.ParseMylistGroupListJson(prevTask.Result)));
    }

    public static Task<ContentManageResult> AddMylistGroupAsync(
      NiconicoContext context,
      string name,
      string description,
      bool is_public,
      MylistDefaultSort default_sort,
      IconType iconType)
    {
      return MylistGroupClient.AddMylistGroupDataAsync(context, name, description, is_public, default_sort, iconType).ContinueWith<ContentManageResult>((Func<Task<string>, ContentManageResult>) (prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result)));
    }

    public static Task<ContentManageResult> UpdateMylistGroupAsync(
      NiconicoContext context,
      string group_id,
      string name,
      string description,
      bool is_public,
      MylistDefaultSort default_sort,
      IconType iconType)
    {
      return MylistGroupClient.UpdateMylistGroupDataAsync(context, group_id, name, description, is_public, default_sort, iconType).ContinueWith<ContentManageResult>((Func<Task<string>, ContentManageResult>) (prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result)));
    }

    public static Task<ContentManageResult> RemoveMylistGroupAsync(
      NiconicoContext context,
      string group_id)
    {
      return MylistGroupClient.RemoveMylistGroupDataAsync(context, group_id).ContinueWith<ContentManageResult>((Func<Task<string>, ContentManageResult>) (prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result)));
    }

    public static Task<MylistGroupDetail> GetMylistGroupDetailAsync(
      NiconicoContext context,
      string group_id)
    {
      return MylistGroupClient.GetMylistGroupDetailDataAsync(context, group_id).ContinueWith<MylistGroupDetail>((Func<Task<string>, MylistGroupDetail>) (prevTask => MylistGroupClient.ParseMylistGroupDetailXml(prevTask.Result)));
    }
  }
}
