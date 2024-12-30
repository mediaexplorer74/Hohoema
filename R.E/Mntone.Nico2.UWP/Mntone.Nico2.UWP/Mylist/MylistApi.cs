// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Mylist.MylistApi
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Mylist.MylistGroup;
using Mntone.Nico2.Mylist.UserMylist;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Mylist
{
  public sealed class MylistApi
  {
    private NiconicoContext _context;

    internal MylistApi(NiconicoContext context) => this._context = context;

    public Task<List<MylistGroupData>> GetUserMylistGroupAsync(string userId)
    {
      return UserMylistClient.GetUserMylistAsync(this._context, userId);
    }

    public Task<MylistGroupDetailResponse> GetMylistGroupDetailAsync(
      string group_id,
      bool isNeedDetail = true)
    {
      return MylistGroupClient.GetMylistGroupDetailAsync(this._context, group_id, isNeedDetail);
    }

    public Task<MylistGroupVideoResponse> GetMylistGroupVideoAsync(
      string group_id,
      uint from = 0,
      uint limit = 30,
      Sort sort = Sort.FirstRetrieve,
      Order order = Order.Descending)
    {
      return MylistGroupClient.GetMylistGroupVideoAsync(this._context, group_id, from, limit, sort, order);
    }
  }
}
