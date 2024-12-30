// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Comment.CommandTypesHelper
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Mntone.Nico2.Videos.Comment
{
  public static class CommandTypesHelper
  {
    public static IEnumerable<CommandType> ParseCommentCommandTypes(string mail)
    {
      if (mail == null)
        return Enumerable.Empty<CommandType>();
      CommandType result;
      return ((IEnumerable<string>) mail.Split(' ')).Select<string, CommandType?>((Func<string, CommandType?>) (x => Enum.TryParse<CommandType>(x, out result) ? new CommandType?(result) : new CommandType?())).Where<CommandType?>((Func<CommandType?, bool>) (x => x.HasValue)).Select<CommandType?, CommandType>((Func<CommandType?, CommandType>) (x => x.Value));
    }
  }
}
