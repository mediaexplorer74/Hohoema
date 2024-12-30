// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.Info.InfoResponse
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Mntone.Nico2.Users.Info
{
  public sealed class InfoResponse
  {
    internal InfoResponse(HtmlNode bodyHtml, string language, UserMyPageJSInfo info)
    {
      try
      {
        this.Id = uint.Parse(info.Id);
      }
      catch
      {
      }
      try
      {
        this.IsPremium = info.IsPremium.ToBooleanFromString();
      }
      catch
      {
      }
      try
      {
        this.IsOver18 = uint.Parse(info.Age) >= 18U;
      }
      catch
      {
      }
      HtmlNode elementByClassName = bodyHtml.GetElementByClassName("userDetail").GetElementByClassName("profile");
      try
      {
        this.Name = elementByClassName.Element("h2").FirstChild.InnerText;
      }
      catch
      {
      }
      try
      {
        uint[] array = ((IEnumerable<HtmlNode>) elementByClassName.GetElementByClassName("stats").SelectNodes("./li/a/span")).Select<HtmlNode, uint>((Func<HtmlNode, uint>) (x => uint.Parse(string.Join<char>("", x.InnerText.Where<char>((Func<char, bool>) (y => y != ',')).TakeWhile<char>((Func<char, bool>) (y => y >= '0' && y <= '9')))))).ToArray<uint>();
        this.FavoriteCount = (ushort) array[0];
        this.StampCount = (ushort) array[1];
        this.Points = array[2];
        this.CreatorScore = array[3];
      }
      catch (Exception ex)
      {
      }
    }

    public string Name { get; private set; }

    public uint Id { get; private set; }

    public string JoinedVersion { get; private set; }

    public bool IsPremium { get; private set; }

    public bool IsOver18 { get; private set; }

    public ushort FavoriteCount { get; private set; }

    public ushort StampCount { get; private set; }

    public uint Points { get; private set; }

    public uint CreatorScore { get; private set; }
  }
}
