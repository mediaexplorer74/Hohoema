// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Dictionaries.Recent.Word
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System.Runtime.Serialization;

#nullable disable
namespace Mntone.Nico2.Dictionaries.Recent
{
  [DataContract]
  public sealed class Word
  {
    internal Word()
    {
    }

    public Category Category { get; private set; }

    [DataMember(Name = "category")]
    private char CategoryImpl
    {
      get => this.Category.ToCategoryChar();
      set => this.Category = value.ToCategory();
    }

    [DataMember(Name = "title")]
    public string Title { get; private set; }

    [DataMember(Name = "view_title")]
    public string ViewTitle { get; private set; }

    [DataMember(Name = "summary")]
    public string Summary { get; private set; }
  }
}
