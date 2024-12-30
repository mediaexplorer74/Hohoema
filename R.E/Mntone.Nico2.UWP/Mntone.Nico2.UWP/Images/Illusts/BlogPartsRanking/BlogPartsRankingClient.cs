// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Images.Illusts.BlogPartsRanking.BlogPartsRankingClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Images.Illusts.BlogPartsRanking
{
  internal sealed class BlogPartsRankingClient
  {
    public static Task<string> GetRankingDataAsync(
      NiconicoContext context,
      DurationType targetDuration,
      GenreOrCategory targetGenreOrCategory)
    {
      return context.GetClient().GetStringAsync(string.Format("{0}ranking&key={1}%2c{2}", (object) NiconicoUrls.ImageBlogPartsUrl, (object) targetDuration.ToDurationTypeString(), (object) targetGenreOrCategory.ToGenreAndCategoryString()));
    }

    public static BlogPartsRankingResponse ParseRankingData(string rankingData)
    {
      XElement documentRootNode = XDocument.Parse(rankingData).GetDocumentRootNode();
      return !(documentRootNode.GetName() != "response") ? new BlogPartsRankingResponse(documentRootNode) : throw new Exception("Parse Error: Node name is invalid.");
    }

    public static Task<BlogPartsRankingResponse> GetRankingAsync(
      NiconicoContext context,
      DurationType targetDuration,
      GenreOrCategory targetGenreOrCategory)
    {
      return BlogPartsRankingClient.GetRankingDataAsync(context, targetDuration, targetGenreOrCategory).ContinueWith<BlogPartsRankingResponse>((Func<Task<string>, BlogPartsRankingResponse>) (prevTask => BlogPartsRankingClient.ParseRankingData(prevTask.Result)));
    }
  }
}
