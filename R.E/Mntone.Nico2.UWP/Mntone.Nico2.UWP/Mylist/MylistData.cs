// Decompiled with JetBrains decompiler
// Type: Mntone.Nico2.Mylist.MylistData
// Assembly: Mntone.Nico2.UWP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F317CE1D-1E5C-4D93-BF0B-3E3C388CB2D2
// Assembly location: C:\Users\Admin\Desktop\RE\Hohoema\Mntone.Nico2.UWP.dll

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

#nullable disable
namespace Mntone.Nico2.Mylist
{
  public class MylistData
  {
    public NiconicoItemType ItemType { get; set; }

    public string ItemId { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public string Description { get; set; }

    public DateTime FirstRetrieve { get; set; }

    public TimeSpan Length { get; set; }

    public string WatchId { get; set; }

    public string GroupId { get; set; }

    public string Title { get; set; }

    public uint ViewCount { get; set; }

    public uint CommentCount { get; set; }

    public uint MylistCount { get; set; }

    public Uri ThumbnailUrl { get; set; }

    public bool IsDeleted { get; set; }

    public static List<MylistData> ParseMylistDataListFromJson(object json)
    {
      // ISSUE: reference to a compiler-generated field
      if (MylistData.\u003C\u003Eo__60.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MylistData.\u003C\u003Eo__60.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, IEnumerable<object>>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (IEnumerable<object>), typeof (MylistData)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, IEnumerable<object>> target1 = MylistData.\u003C\u003Eo__60.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, IEnumerable<object>>> p1 = MylistData.\u003C\u003Eo__60.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (MylistData.\u003C\u003Eo__60.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MylistData.\u003C\u003Eo__60.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "mylistitem", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = MylistData.\u003C\u003Eo__60.\u003C\u003Ep__0.Target((CallSite) MylistData.\u003C\u003Eo__60.\u003C\u003Ep__0, json);
      return target1((CallSite) p1, obj1).Select<object, MylistData>((Func<object, MylistData>) (x =>
      {
        // ISSUE: reference to a compiler-generated field
        if (MylistData.\u003C\u003Eo__60.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MylistData.\u003C\u003Eo__60.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, MylistData>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (MylistData), typeof (MylistData)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, MylistData> target2 = MylistData.\u003C\u003Eo__60.\u003C\u003Ep__3.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, MylistData>> p3 = MylistData.\u003C\u003Eo__60.\u003C\u003Ep__3;
        // ISSUE: reference to a compiler-generated field
        if (MylistData.\u003C\u003Eo__60.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MylistData.\u003C\u003Eo__60.\u003C\u003Ep__2 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "ParseMylistDataEntry", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = MylistData.\u003C\u003Eo__60.\u003C\u003Ep__2.Target((CallSite) MylistData.\u003C\u003Eo__60.\u003C\u003Ep__2, typeof (MylistData), x);
        return target2((CallSite) p3, obj2);
      })).ToList<MylistData>();
    }

    public static MylistData ParseMylistDataEntry(object json)
    {
      MylistData mylistDataEntry = new MylistData();
      MylistData mylistData1 = mylistDataEntry;
      // ISSUE: reference to a compiler-generated field
      if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MylistData.\u003C\u003Eo__61.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, DateTime>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (DateTime), typeof (MylistData)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, DateTime> target1 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__2.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, DateTime>> p2 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__2;
      // ISSUE: reference to a compiler-generated field
      if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MylistData.\u003C\u003Eo__61.\u003C\u003Ep__1 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, Type, object, object> target2 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, Type, object, object>> p1 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__1;
      Type type1 = typeof (DateTime);
      // ISSUE: reference to a compiler-generated field
      if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MylistData.\u003C\u003Eo__61.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "create_time", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__0.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__0, json);
      object obj2 = target2((CallSite) p1, type1, obj1);
      DateTime dateTime1 = target1((CallSite) p2, obj2);
      mylistData1.CreateTime = dateTime1;
      MylistData mylistData2 = mylistDataEntry;
      // ISSUE: reference to a compiler-generated field
      if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MylistData.\u003C\u003Eo__61.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (MylistData)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target3 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__4.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p4 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__4;
      // ISSUE: reference to a compiler-generated field
      if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MylistData.\u003C\u003Eo__61.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "description", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__3.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__3, json);
      string str1 = target3((CallSite) p4, obj3);
      mylistData2.Description = str1;
      MylistData mylistData3 = mylistDataEntry;
      // ISSUE: reference to a compiler-generated field
      if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MylistData.\u003C\u003Eo__61.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (MylistData)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target4 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__6.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p6 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__6;
      // ISSUE: reference to a compiler-generated field
      if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MylistData.\u003C\u003Eo__61.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "item_id", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__5.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__5, json);
      string str2 = target4((CallSite) p6, obj4);
      mylistData3.ItemId = str2;
      // ISSUE: reference to a compiler-generated field
      if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__7 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MylistData.\u003C\u003Eo__61.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "item_data", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj5 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__7.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__7, json);
      MylistData mylistData4 = mylistDataEntry;
      // ISSUE: reference to a compiler-generated field
      if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__10 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MylistData.\u003C\u003Eo__61.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (MylistData)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target5 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__10.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p10 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__10;
      // ISSUE: reference to a compiler-generated field
      if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__9 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MylistData.\u003C\u003Eo__61.\u003C\u003Ep__9 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "UnescapeDataString", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, Type, object, object> target6 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__9.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, Type, object, object>> p9 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__9;
      Type type2 = typeof (Uri);
      // ISSUE: reference to a compiler-generated field
      if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__8 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MylistData.\u003C\u003Eo__61.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "title", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj6 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__8.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__8, obj5);
      object obj7 = target6((CallSite) p9, type2, obj6);
      string str3 = target5((CallSite) p10, obj7);
      mylistData4.Title = str3;
      // ISSUE: reference to a compiler-generated field
      if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__11 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MylistData.\u003C\u003Eo__61.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "item_type", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__11.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__11, json) is string)
      {
        MylistData mylistData5 = mylistDataEntry;
        // ISSUE: reference to a compiler-generated field
        if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__14 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MylistData.\u003C\u003Eo__61.\u003C\u003Ep__14 = CallSite<Func<CallSite, object, NiconicoItemType>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (NiconicoItemType), typeof (MylistData)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, NiconicoItemType> target7 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__14.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, NiconicoItemType>> p14 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__14;
        // ISSUE: reference to a compiler-generated field
        if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__13 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MylistData.\u003C\u003Eo__61.\u003C\u003Ep__13 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, Type, object, object> target8 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__13.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, Type, object, object>> p13 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__13;
        Type type3 = typeof (int);
        // ISSUE: reference to a compiler-generated field
        if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__12 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MylistData.\u003C\u003Eo__61.\u003C\u003Ep__12 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "item_type", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj8 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__12.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__12, json);
        object obj9 = target8((CallSite) p13, type3, obj8);
        int num = (int) target7((CallSite) p14, obj9);
        mylistData5.ItemType = (NiconicoItemType) num;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__15 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MylistData.\u003C\u003Eo__61.\u003C\u003Ep__15 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "item_type", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__15.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__15, json) is double)
        {
          MylistData mylistData6 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__17 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__17 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, int> target9 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__17.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, int>> p17 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__17;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__16 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__16 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "item_type", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj10 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__16.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__16, json);
          int num = target9((CallSite) p17, obj10);
          mylistData6.ItemType = (NiconicoItemType) num;
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__20 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__20 = CallSite<Func<CallSite, Type, object, NotSupportedException>>.Create(Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, NotSupportedException> target10 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__20.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, NotSupportedException>> p20 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__20;
          Type type4 = typeof (NotSupportedException);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__19 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__19 = CallSite<Func<CallSite, string, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Add, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, string, object, object> target11 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__19.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, string, object, object>> p19 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__19;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__18 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__18 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "item_type", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj11 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__18.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__18, json);
          object obj12 = target11((CallSite) p19, "not support MylistItemType:", obj11);
          throw target10((CallSite) p20, type4, obj12);
        }
      }
      switch (mylistDataEntry.ItemType)
      {
        case NiconicoItemType.Video:
          MylistData mylistData7 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__22 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__22 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, string> target12 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__22.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, string>> p22 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__22;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__21 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__21 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "watch_id", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj13 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__21.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__21, json);
          string str4 = target12((CallSite) p22, obj13);
          mylistData7.WatchId = str4;
          MylistData mylistData8 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__25 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__25 = CallSite<Func<CallSite, object, DateTime>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (DateTime), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, DateTime> target13 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__25.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, DateTime>> p25 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__25;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__24 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__24 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target14 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__24.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p24 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__24;
          Type type5 = typeof (DateTime);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__23 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__23 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "update_time", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj14 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__23.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__23, json);
          object obj15 = target14((CallSite) p24, type5, obj14);
          DateTime dateTime2 = target13((CallSite) p25, obj15);
          mylistData8.UpdateTime = dateTime2;
          MylistData mylistData9 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__28 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__28 = CallSite<Func<CallSite, object, DateTime>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (DateTime), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, DateTime> target15 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__28.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, DateTime>> p28 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__28;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__27 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__27 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target16 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__27.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p27 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__27;
          Type type6 = typeof (DateTime);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__26 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__26 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "first_retrieve", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj16 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__26.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__26, json);
          object obj17 = target16((CallSite) p27, type6, obj16);
          DateTime dateTime3 = target15((CallSite) p28, obj17);
          mylistData9.FirstRetrieve = dateTime3;
          MylistData mylistData10 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__32 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__32 = CallSite<Func<CallSite, object, TimeSpan>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (TimeSpan), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, TimeSpan> target17 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__32.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, TimeSpan>> p32 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__32;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__31 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__31 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "FromSeconds", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target18 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__31.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p31 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__31;
          Type type7 = typeof (TimeSpan);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__30 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__30 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target19 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__30.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p30 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__30;
          Type type8 = typeof (long);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__29 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__29 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "length_seconds", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj18 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__29.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__29, json);
          object obj19 = target19((CallSite) p30, type8, obj18);
          object obj20 = target18((CallSite) p31, type7, obj19);
          TimeSpan timeSpan = target17((CallSite) p32, obj20);
          mylistData10.Length = timeSpan;
          MylistData mylistData11 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__34 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__34 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, string> target20 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__34.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, string>> p34 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__34;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__33 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__33 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "video_id", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj21 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__33.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__33, obj5);
          string str5 = target20((CallSite) p34, obj21);
          mylistData11.GroupId = str5;
          MylistData mylistData12 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__37 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__37 = CallSite<Func<CallSite, object, uint>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (uint), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, uint> target21 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__37.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, uint>> p37 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__37;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__36 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__36 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target22 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__36.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p36 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__36;
          Type type9 = typeof (int);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__35 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__35 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "view_counter", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj22 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__35.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__35, obj5);
          object obj23 = target22((CallSite) p36, type9, obj22);
          int num1 = (int) target21((CallSite) p37, obj23);
          mylistData12.ViewCount = (uint) num1;
          MylistData mylistData13 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__40 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__40 = CallSite<Func<CallSite, object, uint>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (uint), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, uint> target23 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__40.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, uint>> p40 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__40;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__39 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__39 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target24 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__39.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p39 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__39;
          Type type10 = typeof (int);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__38 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__38 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "num_res", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj24 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__38.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__38, obj5);
          object obj25 = target24((CallSite) p39, type10, obj24);
          int num2 = (int) target23((CallSite) p40, obj25);
          mylistData13.CommentCount = (uint) num2;
          MylistData mylistData14 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__43 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__43 = CallSite<Func<CallSite, object, uint>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (uint), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, uint> target25 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__43.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, uint>> p43 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__43;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__42 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__42 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target26 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__42.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p42 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__42;
          Type type11 = typeof (int);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__41 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__41 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "mylist_counter", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj26 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__41.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__41, obj5);
          object obj27 = target26((CallSite) p42, type11, obj26);
          int num3 = (int) target25((CallSite) p43, obj27);
          mylistData14.MylistCount = (uint) num3;
          MylistData mylistData15 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__45 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__45 = CallSite<Func<CallSite, Type, object, Uri>>.Create(Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, Uri> target27 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__45.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, Uri>> p45 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__45;
          Type type12 = typeof (Uri);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__44 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__44 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "thumbnail_url", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj28 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__44.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__44, obj5);
          Uri uri1 = target27((CallSite) p45, type12, obj28);
          mylistData15.ThumbnailUrl = uri1;
          MylistData mylistData16 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__47 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__47 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, string> target28 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__47.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, string>> p47 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__47;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__46 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__46 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "deleted", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj29 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__46.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__46, json);
          int num4 = target28((CallSite) p47, obj29).ToBooleanFrom1() ? 1 : 0;
          mylistData16.IsDeleted = num4 != 0;
          break;
        case NiconicoItemType.Seiga:
          MylistData mylistData17 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__50 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__50 = CallSite<Func<CallSite, object, DateTime>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (DateTime), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, DateTime> target29 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__50.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, DateTime>> p50 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__50;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__49 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__49 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target30 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__49.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p49 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__49;
          Type type13 = typeof (DateTime);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__48 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__48 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "update_time", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj30 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__48.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__48, json);
          object obj31 = target30((CallSite) p49, type13, obj30);
          DateTime dateTime4 = target29((CallSite) p50, obj31);
          mylistData17.UpdateTime = dateTime4;
          MylistData mylistData18 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__53 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__53 = CallSite<Func<CallSite, object, DateTime>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (DateTime), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, DateTime> target31 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__53.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, DateTime>> p53 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__53;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__52 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__52 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target32 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__52.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p52 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__52;
          Type type14 = typeof (DateTime);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__51 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__51 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "first_retrieve", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj32 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__51.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__51, json);
          object obj33 = target32((CallSite) p52, type14, obj32);
          DateTime dateTime5 = target31((CallSite) p53, obj33);
          mylistData18.FirstRetrieve = dateTime5;
          MylistData mylistData19 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__56 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__56 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, string> target33 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__56.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, string>> p56 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__56;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__55 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__55 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "ToString", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, object> target34 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__55.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, object>> p55 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__55;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__54 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__54 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "id", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj34 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__54.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__54, obj5);
          object obj35 = target34((CallSite) p55, obj34);
          string str6 = target33((CallSite) p56, obj35);
          mylistData19.GroupId = str6;
          MylistData mylistData20 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__59 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__59 = CallSite<Func<CallSite, object, uint>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (uint), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, uint> target35 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__59.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, uint>> p59 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__59;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__58 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__58 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target36 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__58.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p58 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__58;
          Type type15 = typeof (int);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__57 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__57 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "view_counter", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj36 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__57.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__57, obj5);
          object obj37 = target36((CallSite) p58, type15, obj36);
          int num5 = (int) target35((CallSite) p59, obj37);
          mylistData20.ViewCount = (uint) num5;
          MylistData mylistData21 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__62 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__62 = CallSite<Func<CallSite, object, uint>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (uint), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, uint> target37 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__62.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, uint>> p62 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__62;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__61 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__61 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target38 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__61.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p61 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__61;
          Type type16 = typeof (int);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__60 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__60 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "num_res", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj38 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__60.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__60, obj5);
          object obj39 = target38((CallSite) p61, type16, obj38);
          int num6 = (int) target37((CallSite) p62, obj39);
          mylistData21.CommentCount = (uint) num6;
          MylistData mylistData22 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__65 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__65 = CallSite<Func<CallSite, object, uint>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (uint), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, uint> target39 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__65.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, uint>> p65 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__65;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__64 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__64 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target40 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__64.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p64 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__64;
          Type type17 = typeof (int);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__63 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__63 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "mylist_counter", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj40 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__63.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__63, obj5);
          object obj41 = target40((CallSite) p64, type17, obj40);
          int num7 = (int) target39((CallSite) p65, obj41);
          mylistData22.MylistCount = (uint) num7;
          MylistData mylistData23 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__67 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__67 = CallSite<Func<CallSite, Type, object, Uri>>.Create(Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, Uri> target41 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__67.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, Uri>> p67 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__67;
          Type type18 = typeof (Uri);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__66 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__66 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "thumbnail_url", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj42 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__66.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__66, obj5);
          Uri uri2 = target41((CallSite) p67, type18, obj42);
          mylistData23.ThumbnailUrl = uri2;
          break;
        case NiconicoItemType.Book:
          MylistData mylistData24 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__70 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__70 = CallSite<Func<CallSite, object, DateTime>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (DateTime), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, DateTime> target42 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__70.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, DateTime>> p70 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__70;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__69 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__69 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target43 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__69.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p69 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__69;
          Type type19 = typeof (DateTime);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__68 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__68 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "update_time", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj43 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__68.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__68, json);
          object obj44 = target43((CallSite) p69, type19, obj43);
          DateTime dateTime6 = target42((CallSite) p70, obj44);
          mylistData24.UpdateTime = dateTime6;
          MylistData mylistData25 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__73 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__73 = CallSite<Func<CallSite, object, DateTime>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (DateTime), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, DateTime> target44 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__73.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, DateTime>> p73 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__73;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__72 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__72 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target45 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__72.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p72 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__72;
          Type type20 = typeof (DateTime);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__71 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__71 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "first_retrieve", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj45 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__71.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__71, json);
          object obj46 = target45((CallSite) p72, type20, obj45);
          DateTime dateTime7 = target44((CallSite) p73, obj46);
          mylistData25.FirstRetrieve = dateTime7;
          MylistData mylistData26 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__76 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__76 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, string> target46 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__76.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, string>> p76 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__76;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__75 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__75 = CallSite<Func<CallSite, string, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Add, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, string, object, object> target47 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__75.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, string, object, object>> p75 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__75;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__74 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__74 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "id", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj47 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__74.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__74, obj5);
          object obj48 = target47((CallSite) p75, "bk", obj47);
          string str7 = target46((CallSite) p76, obj48);
          mylistData26.GroupId = str7;
          MylistData mylistData27 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__79 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__79 = CallSite<Func<CallSite, object, uint>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (uint), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, uint> target48 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__79.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, uint>> p79 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__79;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__78 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__78 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target49 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__78.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p78 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__78;
          Type type21 = typeof (int);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__77 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__77 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "num_res", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj49 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__77.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__77, obj5);
          object obj50 = target49((CallSite) p78, type21, obj49);
          int num8 = (int) target48((CallSite) p79, obj50);
          mylistData27.CommentCount = (uint) num8;
          MylistData mylistData28 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__82 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__82 = CallSite<Func<CallSite, object, uint>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (uint), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, uint> target50 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__82.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, uint>> p82 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__82;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__81 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__81 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target51 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__81.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p81 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__81;
          Type type22 = typeof (int);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__80 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__80 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "mylist_counter", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj51 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__80.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__80, obj5);
          object obj52 = target51((CallSite) p81, type22, obj51);
          int num9 = (int) target50((CallSite) p82, obj52);
          mylistData28.MylistCount = (uint) num9;
          MylistData mylistData29 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__84 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__84 = CallSite<Func<CallSite, Type, object, Uri>>.Create(Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, Uri> target52 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__84.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, Uri>> p84 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__84;
          Type type23 = typeof (Uri);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__83 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__83 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "thumbnail_url", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj53 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__83.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__83, obj5);
          Uri uri3 = target52((CallSite) p84, type23, obj53);
          mylistData29.ThumbnailUrl = uri3;
          break;
        case NiconicoItemType.Blomaga:
          MylistData mylistData30 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__87 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__87 = CallSite<Func<CallSite, object, DateTime>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (DateTime), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, DateTime> target53 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__87.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, DateTime>> p87 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__87;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__86 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__86 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target54 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__86.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p86 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__86;
          Type type24 = typeof (DateTime);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__85 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__85 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "update_time", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj54 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__85.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__85, json);
          object obj55 = target54((CallSite) p86, type24, obj54);
          DateTime dateTime8 = target53((CallSite) p87, obj55);
          mylistData30.UpdateTime = dateTime8;
          MylistData mylistData31 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__90 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__90 = CallSite<Func<CallSite, object, DateTime>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (DateTime), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, DateTime> target55 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__90.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, DateTime>> p90 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__90;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__89 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__89 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target56 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__89.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p89 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__89;
          Type type25 = typeof (DateTime);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__88 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__88 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "first_retrieve", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj56 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__88.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__88, json);
          object obj57 = target56((CallSite) p89, type25, obj56);
          DateTime dateTime9 = target55((CallSite) p90, obj57);
          mylistData31.FirstRetrieve = dateTime9;
          MylistData mylistData32 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__93 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__93 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, string> target57 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__93.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, string>> p93 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__93;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__92 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__92 = CallSite<Func<CallSite, string, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Add, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, string, object, object> target58 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__92.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, string, object, object>> p92 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__92;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__91 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__91 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "id", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj58 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__91.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__91, obj5);
          object obj59 = target58((CallSite) p92, "ar", obj58);
          string str8 = target57((CallSite) p93, obj59);
          mylistData32.GroupId = str8;
          MylistData mylistData33 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__96 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__96 = CallSite<Func<CallSite, object, uint>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (uint), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, uint> target59 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__96.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, uint>> p96 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__96;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__95 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__95 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target60 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__95.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p95 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__95;
          Type type26 = typeof (int);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__94 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__94 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "num_res", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj60 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__94.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__94, obj5);
          object obj61 = target60((CallSite) p95, type26, obj60);
          int num10 = (int) target59((CallSite) p96, obj61);
          mylistData33.CommentCount = (uint) num10;
          MylistData mylistData34 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__99 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__99 = CallSite<Func<CallSite, object, uint>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (uint), typeof (MylistData)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, uint> target61 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__99.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, uint>> p99 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__99;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__98 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__98 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, object> target62 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__98.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, object>> p98 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__98;
          Type type27 = typeof (int);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__97 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__97 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "mylist_counter", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj62 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__97.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__97, obj5);
          object obj63 = target62((CallSite) p98, type27, obj62);
          int num11 = (int) target61((CallSite) p99, obj63);
          mylistData34.MylistCount = (uint) num11;
          MylistData mylistData35 = mylistDataEntry;
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__101 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__101 = CallSite<Func<CallSite, Type, object, Uri>>.Create(Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, Type, object, Uri> target63 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__101.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, Type, object, Uri>> p101 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__101;
          Type type28 = typeof (Uri);
          // ISSUE: reference to a compiler-generated field
          if (MylistData.\u003C\u003Eo__61.\u003C\u003Ep__100 == null)
          {
            // ISSUE: reference to a compiler-generated field
            MylistData.\u003C\u003Eo__61.\u003C\u003Ep__100 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "thumbnail_url", typeof (MylistData), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj64 = MylistData.\u003C\u003Eo__61.\u003C\u003Ep__100.Target((CallSite) MylistData.\u003C\u003Eo__61.\u003C\u003Ep__100, obj5);
          Uri uri4 = target63((CallSite) p101, type28, obj64);
          mylistData35.ThumbnailUrl = uri4;
          break;
        default:
          throw new NotSupportedException();
      }
      return mylistDataEntry;
    }
  }
}
