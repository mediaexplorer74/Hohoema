// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.PlayerStatus.User
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Xml.Linq;

#nullable disable
namespace Mntone.Nico2.Live.PlayerStatus
{
  public sealed class User
  {
    internal User(XElement streamXml, XElement userXml)
    {
      this.Id = userXml.GetNamedChildNodeText("user_id").ToUInt();
      this.Name = userXml.GetNamedChildNodeText("nickname");
      this.IsPremium = userXml.GetNamedChildNodeText("is_premium").ToBooleanFrom1();
      this.Age = userXml.GetNamedChildNodeText("userAge").ToUShort();
      this.Sex = userXml.GetNamedChildNodeText("userSex").ToBooleanFrom1() ? Sex.Male : Sex.Female;
      this.Domain = userXml.GetNamedChildNodeText("userDomain");
      this.Prefecture = (Prefecture) userXml.GetNamedChildNodeText("userPrefecture").ToInt();
      this.Language = userXml.GetNamedChildNodeText("userLanguage");
      this.HKey = streamXml.GetNamedChildNodeText("hkey");
      this.IsOwner = streamXml.GetNamedChildNodeText("is_owner").ToBooleanFrom1();
      this.IsJoin = userXml.GetNamedChildNodeText("is_join").ToBooleanFrom1();
      this.IsReserved = streamXml.GetNamedChildNodeText("is_timeshift_reserved").ToBooleanFrom1();
      this.IsPrefecturePreferential = streamXml.GetNamedChildNodeText("is_priority_prefecture").ToBooleanFrom1();
      string namedChildNodeText = streamXml.GetNamedChildNodeText("product_purchased");
      if (!string.IsNullOrEmpty(namedChildNodeText))
      {
        this.IsPurchased = namedChildNodeText.ToBooleanFrom1();
        this.IsSerialUsing = streamXml.GetNamedAttributeText("is_serial_stream").ToBooleanFrom1();
      }
      this.Twitter = new UserTwitter(userXml.GetNamedChildNode("twitter_info"));
    }

    public uint Id { get; private set; }

    public string Name { get; private set; }

    public bool IsPremium { get; private set; }

    public ushort Age { get; private set; }

    public bool IsMale => this.Sex == Sex.Male;

    public bool IsFemale => this.Sex == Sex.Female;

    public Sex Sex { get; private set; }

    public string Domain { get; private set; }

    public Prefecture Prefecture { get; private set; }

    public string Language { get; private set; }

    public string HKey { get; private set; }

    public bool IsOwner { get; private set; }

    public bool IsJoin { get; private set; }

    public bool IsReserved { get; private set; }

    public bool IsPrefecturePreferential { get; private set; }

    public bool IsPurchased { get; private set; }

    public bool IsSerialUsing { get; private set; }

    public UserTwitter Twitter { get; private set; }
  }
}
