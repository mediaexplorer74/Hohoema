// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Embed.Ichiba.IchibaResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Embed.Ichiba
{
  [DataContract]
  public sealed class IchibaResponse
  {
    private List<IchibaItem> _MainItems;
    private List<IchibaItem> _PickupItems;

    [DataMember(Name = "pickup")]
    public string Pickup { get; set; }

    [DataMember(Name = "main")]
    public string Main { get; set; }

    [DataMember(Name = "polling")]
    public Polling Polling { get; set; }

    public List<IchibaItem> GetMainIchibaItems()
    {
      return this._MainItems ?? (this._MainItems = IchibaResponse.ParseIchibaHtml(this.Main));
    }

    public List<IchibaItem> GetPickupIchibaItems()
    {
      return this._PickupItems ?? (this._PickupItems = IchibaResponse.ParseIchibaHtml(this.Pickup));
    }

    private static List<IchibaItem> ParseIchibaHtml(string html)
    {
      HtmlDocument htmlDocument = new HtmlDocument();
      htmlDocument.LoadHtml(html);
      HtmlNode documentNode = htmlDocument.DocumentNode;
      List<IchibaItem> ichibaHtml = new List<IchibaItem>();
      foreach (HtmlNode node in documentNode.FirstChild.GetElementsByClassName("ichiba_mainitem"))
      {
        try
        {
          IchibaItem ichibaItem = IchibaResponse.IchibaItemFromXmlNode(node);
          ichibaHtml.Add(ichibaItem);
        }
        catch
        {
        }
      }
      return ichibaHtml;
    }

    private static IchibaItem IchibaItemFromXmlNode(HtmlNode node)
    {
      IchibaItem ichibaItem = new IchibaItem();
      HtmlNode elementByTagName1 = node.GetElementByTagName("div");
      ichibaItem.Id = ((IEnumerable<string>) elementByTagName1.Id.Split('_')).Last<string>();
      string attributeValue1 = elementByTagName1.GetElementByClassName("thumbnail").GetElementByTagName("div").GetElementByTagName("a").GetElementByTagName("img")?.GetAttributeValue("src", "");
      if (attributeValue1 != null)
        ichibaItem.ThumbnailUrl = new Uri(attributeValue1);
      HtmlNode elementByTagName2 = elementByTagName1.GetElementByClassName("itemname").GetElementByTagName("a");
      string innerText1 = elementByTagName2.InnerText;
      string attributeValue2 = elementByTagName2.GetAttributeValue("href", "");
      ichibaItem.AmazonItemLink = new Uri(attributeValue2);
      ichibaItem.Title = innerText1;
      string innerText2 = elementByTagName1.GetElementByClassName("maker").InnerText;
      ichibaItem.Maker = innerText2;
      HtmlNode elementByClassName1 = elementByTagName1.GetElementByClassName("price");
      if (elementByClassName1 != null)
      {
        ichibaItem.Price = elementByClassName1.InnerText;
        HtmlNode elementByTagName3 = elementByClassName1.GetElementByTagName("span");
        if (elementByTagName3 != null)
          ichibaItem.DiscountText = elementByTagName3.InnerText;
      }
      HtmlNode elementByClassName2 = elementByTagName1.GetElementByClassName("release");
      HtmlNode elementByTagName4 = elementByClassName2 != null ? elementByClassName2.GetElementByTagName("span") : (HtmlNode) null;
      if (elementByTagName4 != null)
        ichibaItem.Reservation = new IchibaItemReservation()
        {
          ReleaseDate = elementByTagName4.InnerText
        };
      HtmlNode elementByClassName3 = elementByTagName1.GetElementByClassName("action");
      if (elementByClassName3 != null)
      {
        IchibaItemSellBase ichibaItemSellBase;
        if (ichibaItem.Reservation != null)
        {
          ichibaItemSellBase = (IchibaItemSellBase) ichibaItem.Reservation;
          HtmlNode elementByClassName4 = elementByClassName3.GetElementByClassName("reservation");
          if (elementByClassName4 != null)
            ichibaItem.Reservation.ReservationActionText = elementByClassName4.InnerText;
          HtmlNode elementByClassName5 = elementByClassName3.GetElementByClassName("reservationYesterday");
          if (elementByClassName5 != null)
            ichibaItem.Reservation.YesterdayReservationActionText = elementByClassName5.InnerText;
        }
        else
        {
          ichibaItemSellBase = (IchibaItemSellBase) (ichibaItem.Sell = new IchibaItemSell());
          HtmlNode elementByClassName6 = elementByClassName3.GetElementByClassName("buy");
          if (elementByClassName6 != null)
            ichibaItem.Sell.BuyActionText = elementByClassName6.InnerText;
          HtmlNode elementByClassName7 = elementByClassName3.GetElementByClassName("buyYesterday");
          if (elementByClassName7 != null)
            ichibaItem.Sell.YesterdayBuyActionText = elementByClassName7.InnerText;
        }
        HtmlNode elementByClassName8 = elementByClassName3.GetElementByClassName("click");
        if (elementByClassName8 != null)
          ichibaItemSellBase.ClickActionText = elementByClassName8.InnerText;
        HtmlNode htmlNode = elementByClassName3.GetElementsByTagName("span").LastOrDefault<HtmlNode>();
        if (htmlNode != null && !htmlNode.Attributes.Contains("class"))
          ichibaItemSellBase.ClickInThisContentText = htmlNode.InnerText;
      }
      HtmlNode elementByClassName9 = elementByTagName1.GetElementByClassName("goIchiba");
      HtmlNode elementByTagName5 = elementByClassName9 != null ? elementByClassName9.GetElementByTagName("a") : (HtmlNode) null;
      if (elementByTagName5 != null)
      {
        string attributeValue3 = elementByTagName5.GetAttributeValue("href", "");
        ichibaItem.IchibaUrl = new Uri(attributeValue3);
      }
      return ichibaItem;
    }
  }
}
