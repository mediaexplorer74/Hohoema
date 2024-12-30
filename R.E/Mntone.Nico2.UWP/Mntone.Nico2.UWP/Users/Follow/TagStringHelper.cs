// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Users.Follow.TagStringHelper
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Mntone.Nico2.Users.Follow
{
  public static class TagStringHelper
  {
    private static Dictionary<char, char> NumberZenkakuToHankaku = new Dictionary<char, char>();

    static TagStringHelper()
    {
      TagStringHelper.NumberZenkakuToHankaku.Add('０', '0');
      TagStringHelper.NumberZenkakuToHankaku.Add('１', '1');
      TagStringHelper.NumberZenkakuToHankaku.Add('２', '2');
      TagStringHelper.NumberZenkakuToHankaku.Add('３', '3');
      TagStringHelper.NumberZenkakuToHankaku.Add('４', '4');
      TagStringHelper.NumberZenkakuToHankaku.Add('５', '5');
      TagStringHelper.NumberZenkakuToHankaku.Add('６', '6');
      TagStringHelper.NumberZenkakuToHankaku.Add('７', '7');
      TagStringHelper.NumberZenkakuToHankaku.Add('８', '8');
      TagStringHelper.NumberZenkakuToHankaku.Add('９', '9');
    }

    public static string ToEnsureHankakuNumberTagString(this string tag)
    {
      bool flag = false;
      foreach (char key in tag)
      {
        if (TagStringHelper.NumberZenkakuToHankaku.ContainsKey(key))
        {
          flag = true;
          break;
        }
      }
      return flag ? new string(tag.Select<char, char>((Func<char, char>) (x => !TagStringHelper.NumberZenkakuToHankaku.ContainsKey(x) ? x : TagStringHelper.NumberZenkakuToHankaku[x])).ToArray<char>()) : tag;
    }
  }
}
