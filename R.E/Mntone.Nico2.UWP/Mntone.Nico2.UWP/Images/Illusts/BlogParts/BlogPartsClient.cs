// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Images.Illusts.BlogParts.BlogPartsClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Images.Illusts.BlogParts
{
  internal sealed class BlogPartsClient
  {
    public static Task<string> GetClipDataAsync(NiconicoContext context, uint requestClipId)
    {
      return context.GetClient().GetStringAsync(string.Format("{0}clip&key={1}", (object) NiconicoUrls.ImageBlogPartsUrl, (object) requestClipId));
    }

    public static Task<string> GetUserDataAsync(NiconicoContext context, uint requestUserId)
    {
      return context.GetClient().GetStringAsync(string.Format("{0}user&key={1}", (object) NiconicoUrls.ImageBlogPartsUrl, (object) requestUserId));
    }

    public static BlogPartsResponse ParseBlogPartsData(string blogPartsData)
    {
      XElement documentRootNode = XDocument.Parse(blogPartsData).GetDocumentRootNode();
      return !(documentRootNode.GetName() != "response") ? new BlogPartsResponse(documentRootNode) : throw new Exception("Parse Error: Node name is invalid.");
    }

    public static Task<BlogPartsResponse> GetClipAsync(NiconicoContext context, uint requestClipId)
    {
      return BlogPartsClient.GetClipDataAsync(context, requestClipId).ContinueWith<BlogPartsResponse>((Func<Task<string>, BlogPartsResponse>) (prevTask => BlogPartsClient.ParseBlogPartsData(prevTask.Result)));
    }

    public static Task<BlogPartsResponse> GetUserAsync(NiconicoContext context, uint requestUserId)
    {
      return BlogPartsClient.GetUserDataAsync(context, requestUserId).ContinueWith<BlogPartsResponse>((Func<Task<string>, BlogPartsResponse>) (prevTask => BlogPartsClient.ParseBlogPartsData(prevTask.Result)));
    }
  }
}
