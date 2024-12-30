// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.ReservationsInDetail.ReservationsInDetailClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;
using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Live.ReservationsInDetail
{
  internal sealed class ReservationsInDetailClient
  {
    public static Task<string> GetReservationsInDetailDataAsync(NiconicoContext context)
    {
      return context.GetClient().GetConvertedStringAsync(NiconicoUrls.LiveWatchingReservationDetailListUrl);
    }

    public static ReservationsInDetailResponse ParseReservationsInDetailData(
      string reservationsInDatailData)
    {
      XElement documentRootNode = XDocument.Parse(reservationsInDatailData).GetDocumentRootNode();
      XElement xelement = !(documentRootNode.GetName() != "nicolive_video_response") ? documentRootNode.GetFirstChildNode() : throw new Exception("Parse Error: Node name is invalid.");
      return !(xelement.GetName() != "timeshift_reserved_detail_list") ? new ReservationsInDetailResponse(xelement) : throw new Exception("Parse Error: Node name is invalid.");
    }

    public static Task<ReservationsInDetailResponse> GetReservationsInDetailAsync(
      NiconicoContext context)
    {
      return ReservationsInDetailClient.GetReservationsInDetailDataAsync(context).ContinueWith<ReservationsInDetailResponse>((Func<Task<string>, ReservationsInDetailResponse>) (prevTask => ReservationsInDetailClient.ParseReservationsInDetailData(prevTask.Result)));
    }
  }
}
