// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Videos.Comment.NMSG_ResponseJsonConverter
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Mntone.Nico2.Videos.Comment
{
  public class NMSG_ResponseJsonConverter : JsonConverter
  {
    private Type[] CanConvertTypes = new Type[5]
    {
      typeof (NGMS_Thread_ResponseItem),
      typeof (NGMS_Leaf),
      typeof (NGMS_GlobalNumRes),
      typeof (NMSG_Chat),
      typeof (PingItem)
    };

    public virtual bool CanWrite => false;

    public virtual bool CanRead => true;

    public virtual bool CanConvert(Type objectType)
    {
      return ((IEnumerable<Type>) this.CanConvertTypes).Any<Type>((Func<Type, bool>) (x => (object) x == (object) objectType));
    }

    public virtual object ReadJson(
      JsonReader reader,
      Type objectType,
      object existingValue,
      JsonSerializer serializer)
    {
      JObject jobject = JObject.Load(reader);
      if (jobject["ping"] != null)
      {
        PingItem pingItem = new PingItem();
        serializer.Populate(reader, (object) pingItem);
        return (object) pingItem;
      }
      if (jobject["thread"] == null)
        throw new NotImplementedException();
      NGMS_Thread_ResponseItem threadResponseItem = new NGMS_Thread_ResponseItem();
      serializer.Populate(reader, (object) threadResponseItem);
      return (object) threadResponseItem;
    }

    public virtual void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }
  }
}
