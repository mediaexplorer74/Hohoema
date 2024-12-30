// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.SingleOrArrayConverter`1
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

#nullable disable
namespace Mntone.Nico2
{
  public class SingleOrArrayConverter<T> : JsonConverter
  {
    public virtual bool CanConvert(Type objectType)
    {
      return (object) objectType == (object) typeof (List<T>);
    }

    public virtual object ReadJson(
      JsonReader reader,
      Type objectType,
      object existingValue,
      JsonSerializer serializer)
    {
      JToken jtoken = JToken.Load(reader);
      if (jtoken.Type == 2)
        return (object) jtoken.ToObject<List<T>>();
      return (object) new List<T>() { jtoken.ToObject<T>() };
    }

    public virtual bool CanWrite => false;

    public virtual void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }
  }
}
