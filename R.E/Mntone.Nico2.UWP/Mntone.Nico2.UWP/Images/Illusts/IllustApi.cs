// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Images.Illusts.IllustApi
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Mntone.Nico2.Images.Illusts.BlogParts;
using Mntone.Nico2.Images.Illusts.BlogPartsRanking;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Images.Illusts
{
  public sealed class IllustApi
  {
    private NiconicoContext _context;

    internal IllustApi(NiconicoContext context) => this._context = context;

    public Task<BlogPartsResponse> GetClipAsync(uint requestClipId)
    {
      return BlogPartsClient.GetClipAsync(this._context, requestClipId);
    }

    public Task<BlogPartsResponse> GetUserAsync(uint requestUserId)
    {
      return BlogPartsClient.GetUserAsync(this._context, requestUserId);
    }

    public Task<BlogPartsRankingResponse> GetRankingAsync(
      DurationType targetDuration,
      GenreOrCategory targetGenreOrCategory)
    {
      return BlogPartsRankingClient.GetRankingAsync(this._context, targetDuration, targetGenreOrCategory);
    }
  }
}
