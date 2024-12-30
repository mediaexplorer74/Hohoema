// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Images.ImageApi
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Images.Illusts;
using Mntone.Nico2.Images.Users;

#nullable disable
namespace Mntone.Nico2.Images
{
  public sealed class ImageApi
  {
    private IllustApi _Illust;
    private UserApi _User;
    private NiconicoContext _context;

    internal ImageApi(NiconicoContext context) => this._context = context;

    public IllustApi Illust => this._Illust ?? (this._Illust = new IllustApi(this._context));

    public UserApi User => this._User ?? (this._User = new UserApi(this._context));
  }
}
