// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Thumbnail.ThumbnailClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Videos.Thumbnail
{
  internal sealed class ThumbnailClient
  {
    public static Task<string> GetThumbnailDataAsync(NiconicoContext context, string requestId)
    {
      NiconicoRegex.IsVideoId(requestId);
      return context.GetClient().GetStringAsync(string.Format("{0}{1}", (object) NiconicoUrls.VideoThumbInfoUrl, (object) requestId));
    }

    public static ThumbnailResponse ParseThumbnailData(string thumbnailData)
    {
      XElement documentRootNode = XDocument.Parse(thumbnailData).GetDocumentRootNode();
      if (documentRootNode.GetName() != "nicovideo_thumb_response")
        throw new Exception("Parse Error: Node name is invalid.");
      if (documentRootNode.GetNamedAttributeText("status") != "ok")
      {
        XElement firstChildNode = documentRootNode.GetFirstChildNode();
        string namedChildNodeText = firstChildNode.GetNamedChildNodeText("code");
        throw new Exception("Parse Error: " + firstChildNode.GetNamedChildNodeText("description") + " (" + namedChildNodeText + ")");
      }
      return new ThumbnailResponse(documentRootNode.GetFirstChildNode());
    }

    public static Task<ThumbnailResponse> GetThumbnailAsync(
      NiconicoContext context,
      string requestId)
    {
      return ThumbnailClient.GetThumbnailDataAsync(context, requestId).ContinueWith<ThumbnailResponse>((Func<Task<string>, ThumbnailResponse>) (prevTask => ThumbnailClient.ParseThumbnailData(prevTask.Result)));
    }
  }
}
