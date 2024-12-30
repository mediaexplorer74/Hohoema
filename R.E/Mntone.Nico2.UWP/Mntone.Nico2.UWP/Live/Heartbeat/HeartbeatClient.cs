// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.Heartbeat.HeartbeatClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Web.Http;

#nullable disable
namespace Mntone.Nico2.Live.Heartbeat
{
  internal sealed class HeartbeatClient
  {
    public static Task<string> HeartbeatDataAsync(NiconicoContext context, string requestId)
    {
      if (!NiconicoRegex.IsLiveId(requestId))
        throw new ArgumentException();
      return WindowsRuntimeSystemExtensions.AsTask<string, HttpProgress>(context.GetClient().GetStringAsync(new Uri(string.Format("{0}?v={1}", (object) NiconicoUrls.LiveHeartbeatUrl, (object) requestId))));
    }

    public static HeartbeatResponse ParseHeartbeatData(string heartbeatData)
    {
      XElement documentRootNode = XDocument.Parse(heartbeatData).GetDocumentRootNode();
      if (documentRootNode.GetName() != "heartbeat")
        throw new Exception("Parse Error: Node name is invalid.");
      if (documentRootNode.GetNamedAttributeText("status") != "ok")
      {
        XElement firstChildNode = documentRootNode.GetFirstChildNode();
        string namedChildNodeText1 = firstChildNode.GetNamedChildNodeText("code");
        string namedChildNodeText2 = firstChildNode.GetNamedChildNodeText("description");
        firstChildNode.GetNamedChildNodeText("reject").ToBooleanFromString();
        throw new Exception("Parse Error: " + namedChildNodeText2 + " (" + namedChildNodeText1 + ")");
      }
      return new HeartbeatResponse(documentRootNode);
    }

    public static Task<HeartbeatResponse> HeartbeatAsync(NiconicoContext context, string requestId)
    {
      return HeartbeatClient.HeartbeatDataAsync(context, requestId).ContinueWith<HeartbeatResponse>((Func<Task<string>, HeartbeatResponse>) (prevTask => HeartbeatClient.ParseHeartbeatData(prevTask.Result)));
    }
  }
}
