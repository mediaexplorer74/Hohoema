// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Dictionaries.WordExist.WordExistClient
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Threading.Tasks;

#nullable disable
namespace Mntone.Nico2.Dictionaries.WordExist
{
  internal sealed class WordExistClient
  {
    public static Task<string> WordExistDataAsync(NiconicoContext context, string targetWord)
    {
      return context.GetClient().GetStringAsync(NiconicoUrls.DictionaryWordExistUrl + Uri.EscapeUriString(targetWord));
    }

    public static bool ParseWordExistData(string wordExistData)
    {
      return wordExistData.Substring(1, 1).ToBooleanFrom1();
    }

    public static Task<bool> WordExistAsync(NiconicoContext context, string targetWord)
    {
      return WordExistClient.WordExistDataAsync(context, targetWord).ContinueWith<bool>((Func<Task<string>, bool>) (prevTask => WordExistClient.ParseWordExistData(prevTask.Result)));
    }
  }
}
