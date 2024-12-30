// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Dictionaries.Exist.ExistClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Dictionaries.Exist
{
  internal sealed class ExistClient
  {
    public static Task<string> ExistDataAsync(
      NiconicoContext context,
      Category targetCategory,
      string targetWord)
    {
      return context.GetClient().GetStringAsync(string.Format("{0}{1}/{2}", (object) NiconicoUrls.DictionaryExistUrl, (object) targetCategory.ToCategoryChar(), (object) Uri.EscapeUriString(targetWord)));
    }

    public static bool ParseExistData(string existData) => existData.ToBooleanFrom1();

    public static Task<bool> ExistAsync(
      NiconicoContext context,
      Category targetCategory,
      string targetWord)
    {
      return ExistClient.ExistDataAsync(context, targetCategory, targetWord).ContinueWith<bool>((Func<Task<string>, bool>) (prevTask => ExistClient.ParseExistData(prevTask.Result)));
    }
  }
}
