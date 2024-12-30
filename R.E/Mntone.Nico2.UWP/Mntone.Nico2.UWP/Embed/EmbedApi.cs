// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Embed.EmbedApi
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Embed.Ichiba;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Embed
{
  public sealed class EmbedApi
  {
    private NiconicoContext _context { get; }

    internal EmbedApi(NiconicoContext context) => this._context = context;

    public Task<IchibaResponse> GetIchiba(string contentId)
    {
      return IchibaClient.GetIchibaAsync(this._context, contentId);
    }
  }
}
