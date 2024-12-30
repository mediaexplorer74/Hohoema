// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.PlayerStatus.PlayerStatusClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Live.PlayerStatus
{
  internal sealed class PlayerStatusClient
  {
    public static Task<string> GetPlayerStatusDataAsync(NiconicoContext context, string requestId)
    {
      if (!NiconicoRegex.IsLiveId(requestId))
        throw new ArgumentException();
      return context.GetClient().GetConvertedStringAsync(NiconicoUrls.LivePlayerStatusUrl + requestId);
    }

    public static PlayerStatusResponse ParsePlayerStatusData(string playerStatusData)
    {
      XElement documentRootNode = XDocument.Parse(playerStatusData).GetDocumentRootNode();
      if (documentRootNode.GetName() != "getplayerstatus")
        throw new ParseException("Parse Error: Node name is invalid.");
      if (!(documentRootNode.GetNamedAttributeText("status") != "ok"))
        return new PlayerStatusResponse(documentRootNode);
      switch (documentRootNode.GetFirstChildNode().GetNamedChildNodeText("code"))
      {
        case "closed":
          throw CustomExceptionFactory.Create(-1073471486);
        case "comingsoon":
          throw CustomExceptionFactory.Create(-1073471485);
        case "full":
          throw CustomExceptionFactory.Create(-1073471471);
        case "maintenance":
          throw CustomExceptionFactory.Create(-1073479679);
        case "not_found":
          throw CustomExceptionFactory.Create(-1073471487);
        case "notlogin":
          throw CustomExceptionFactory.Create(-1073479680);
        case "premium_only":
          throw CustomExceptionFactory.Create(-1073471470);
        case "require_community_member":
          throw CustomExceptionFactory.Create(-1073471472);
        default:
          throw CustomExceptionFactory.Create(-1073471488);
      }
    }

    public static Task<PlayerStatusResponse> GetPlayerStatusAsync(
      NiconicoContext context,
      string requestId)
    {
      return PlayerStatusClient.GetPlayerStatusDataAsync(context, requestId).ContinueWith<PlayerStatusResponse>((Func<Task<string>, PlayerStatusResponse>) (prevTask => PlayerStatusClient.ParsePlayerStatusData(prevTask.Result)));
    }
  }
}
