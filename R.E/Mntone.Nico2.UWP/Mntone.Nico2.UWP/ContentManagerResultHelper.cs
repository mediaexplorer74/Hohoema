// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.ContentManagerResultHelper
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Newtonsoft.Json;

#nullable disable
namespace Mntone.Nico2
{
  public static class ContentManagerResultHelper
  {
    public static ContentManageResult ParseJsonResult(string json)
    {
      try
      {
        ContentManageResultData manageResultData = JsonConvert.DeserializeObject<ContentManageResultData>(json);
        if (manageResultData.status == "ok")
          return ContentManageResult.Success;
        if (manageResultData.error?.code == "EXIST")
          return ContentManageResult.Exist;
      }
      catch
      {
      }
      return ContentManageResult.Failed;
    }
  }
}
