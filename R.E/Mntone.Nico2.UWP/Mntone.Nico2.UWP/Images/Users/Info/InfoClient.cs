// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Images.Users.Info.InfoClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Images.Users.Info
{
  internal sealed class InfoClient
  {
    public static Task<string> GetInfoDataAsync(NiconicoContext context, uint requestUserId)
    {
      return context.GetClient().GetStringAsync(string.Format("{0}{1}", (object) NiconicoUrls.ImageUserInfoUrl, (object) requestUserId));
    }

    public static InfoResponse ParseInfoData(string infoData)
    {
      XElement documentRootNode = XDocument.Parse(infoData).GetDocumentRootNode();
      XElement xelement = !(documentRootNode.GetName() != "response") ? documentRootNode.GetFirstChildNode() : throw new Exception("Parse Error: Node name is invalid.");
      return !(xelement.GetName() != "user") ? new InfoResponse(xelement) : throw new Exception("Parse Error: Node name is invalid.");
    }

    public static Task<InfoResponse> GetInfoAsync(NiconicoContext context, uint requestUserId)
    {
      return InfoClient.GetInfoDataAsync(context, requestUserId).ContinueWith<InfoResponse>((Func<Task<string>, InfoResponse>) (prevTask => InfoClient.ParseInfoData(prevTask.Result)));
    }
  }
}
