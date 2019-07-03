using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using GameJam2019;

public static class SystemUtil
{
    /// <summary>
    /// 从一个类型中获取拥有指定特性的方法.
    /// </summary>
    public static Dictionary<MessageEnum, MethodInfo> GetMessageMethodListByClass(Type type)
    {
        var result = new Dictionary<MessageEnum, MethodInfo>();
        var methodArr = type.GetMethods();
        MessageHandlerAttribute att;
        for (int i = 0; i < methodArr.Length; i++)
        {
            att = methodArr[i].GetCustomAttribute<MessageHandlerAttribute>();
            if (ReferenceEquals(att, null)) continue;
            System.Diagnostics.Debug.Assert(result.ContainsKey(att.Message), "error: MessageManager attribute register error.");
            result.Add(att.Message, methodArr[i]);
        }
        return result;
    }
}
