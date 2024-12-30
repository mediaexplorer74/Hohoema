// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Channels.ChannelApi
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Channels.Icon;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Channels
{
  public sealed class ChannelApi
  {
    private NiconicoContext _context;

    internal ChannelApi(NiconicoContext context) => this._context = context;

    public Task<byte[]> GetIconAsync(string requestChannelId)
    {
      return IconClient.GetIconAsync(this._context, requestChannelId);
    }
  }
}
