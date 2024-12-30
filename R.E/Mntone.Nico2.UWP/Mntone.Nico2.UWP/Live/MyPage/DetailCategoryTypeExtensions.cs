// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Live.MyPage.DetailCategoryTypeExtensions
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;

#nullable disable
namespace Mntone.Nico2.Live.MyPage
{
  internal static class DetailCategoryTypeExtensions
  {
    private const string GENERAL_IMAGE_URL = "img/10/cmn/icon/icon_common.gif?090827";
    private const string POLITICS_IMAGE_URL = "img/10/cmn/icon/icon_politics.gif?090827";
    private const string ANIMAL_IMAGE_URL = "mg/10/cmn/icon/icon_animal.gif?090827";
    private const string COOKING_IMAGE_URL = "img/10/cmn/icon/icon_cooking.gif?090827";
    private const string PERFORMANCE_IMAGE_URL = "img/10/cmn/icon/icon_play.gif?090827";
    private const string SING_IMAGE_URL = "img/10/cmn/icon/icon_sing.gif?090827";
    private const string DANCE_IMAGE_URL = "img/10/cmn/icon/icon_dance.gif?090827";
    private const string DRAW_IMAGE_URL = "img/10/cmn/icon/icon_draw.gif?090827";
    private const string LECTURE_IMAGE_URL = "img/10/cmn/icon/icon_lecture.gif?090827";
    private const string GAME_IMAGE_URL = "img/10/cmn/icon/icon_live.gif?090827";
    private const string INTRODUCTION_IMAGE_URL = "img/10/cmn/icon/icon_request.gif?090827";
    private const string ADULT_IMAGE_URL = "img/10/cmn/icon/icon__r18_.gif?090827";

    public static DetailCategoryType ToDetailCategory(this string categorySrc)
    {
      switch (categorySrc)
      {
        case "img/10/cmn/icon/icon__r18_.gif?090827":
          return DetailCategoryType.Adult;
        case "img/10/cmn/icon/icon_common.gif?090827":
          return DetailCategoryType.General;
        case "img/10/cmn/icon/icon_cooking.gif?090827":
          return DetailCategoryType.Cooking;
        case "img/10/cmn/icon/icon_dance.gif?090827":
          return DetailCategoryType.Dance;
        case "img/10/cmn/icon/icon_draw.gif?090827":
          return DetailCategoryType.Draw;
        case "img/10/cmn/icon/icon_lecture.gif?090827":
          return DetailCategoryType.Lecture;
        case "img/10/cmn/icon/icon_live.gif?090827":
          return DetailCategoryType.Game;
        case "img/10/cmn/icon/icon_play.gif?090827":
          return DetailCategoryType.Performance;
        case "img/10/cmn/icon/icon_politics.gif?090827":
          return DetailCategoryType.Politics;
        case "img/10/cmn/icon/icon_request.gif?090827":
          return DetailCategoryType.Introduction;
        case "img/10/cmn/icon/icon_sing.gif?090827":
          return DetailCategoryType.Sing;
        case "mg/10/cmn/icon/icon_animal.gif?090827":
          return DetailCategoryType.Animal;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }
}
