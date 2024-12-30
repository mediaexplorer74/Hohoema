// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Images.Users.UserApi
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Images.Users.Data;
using Mntone.Nico2.Images.Users.Info;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Images.Users
{
  public sealed class UserApi
  {
    private NiconicoContext _context;

    internal UserApi(NiconicoContext context) => this._context = context;

    public Task<InfoResponse> GetInfoAsync(uint requestUserId)
    {
      return InfoClient.GetInfoAsync(this._context, requestUserId);
    }

    public Task<DataResponse> GetDataAsync(uint requestUserId)
    {
      return DataClient.GetDataAsync(this._context, requestUserId);
    }
  }
}
