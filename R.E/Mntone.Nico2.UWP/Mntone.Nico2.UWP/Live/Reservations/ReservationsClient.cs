// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.Reservations.ReservationsClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Live.Reservations
{
  internal sealed class ReservationsClient
  {
    public static Task<string> GetReservationsDataAsync(NiconicoContext context)
    {
      return context.GetClient().GetStringAsync(NiconicoUrls.LiveWatchingReservationListUrl);
    }

    public static IReadOnlyList<string> ParseReservationsData(string reservationsInDatailData)
    {
      XElement documentRootNode = XDocument.Parse(reservationsInDatailData).GetDocumentRootNode();
      XElement node = !(documentRootNode.GetName() != "nicolive_video_response") ? documentRootNode.GetFirstChildNode() : throw new Exception("Parse Error: Node name is invalid.");
      if (node.GetName() != "timeshift_reserved_list")
        throw new Exception("Parse Error: Node name is invalid.");
      return node.GetFirstChildNode() != null ? (IReadOnlyList<string>) node.GetChildNodes().Select<XElement, string>((Func<XElement, string>) (vidXml => "lv" + vidXml.GetText())).ToList<string>() : (IReadOnlyList<string>) new List<string>();
    }

    public static Task<IReadOnlyList<string>> GetReservationsAsync(NiconicoContext context)
    {
      return ReservationsClient.GetReservationsDataAsync(context).ContinueWith<IReadOnlyList<string>>((Func<Task<string>, IReadOnlyList<string>>) (prevTask => ReservationsClient.ParseReservationsData(prevTask.Result)));
    }
  }
}
