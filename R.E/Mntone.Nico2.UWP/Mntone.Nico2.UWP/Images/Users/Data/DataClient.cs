// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Images.Users.Data.DataClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Images.Users.Data
{
  internal sealed class DataClient
  {
    public static Task<string> GetDataDataAsync(NiconicoContext context, uint requestUserId)
    {
      return context.GetClient().GetStringAsync(string.Format("{0}{1}", (object) NiconicoUrls.ImageUserDataUrl, (object) requestUserId));
    }

    public static DataResponse ParseDataData(string dataData)
    {
      XElement documentRootNode = XDocument.Parse(dataData, LoadOptions.None).GetDocumentRootNode();
      return !(documentRootNode.GetName() != "response") ? new DataResponse(documentRootNode) : throw new Exception("Parse Error: Node name is invalid.");
    }

    public static Task<DataResponse> GetDataAsync(NiconicoContext context, uint requestUserId)
    {
      return DataClient.GetDataDataAsync(context, requestUserId).ContinueWith<DataResponse>((Func<Task<string>, DataResponse>) (prevTask => DataClient.ParseDataData(prevTask.Result)));
    }
  }
}
