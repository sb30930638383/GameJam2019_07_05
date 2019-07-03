using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameJam2019
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MessageHandlerAttribute : Attribute
    {
        public MessageEnum Message;

        public MessageHandlerAttribute(MessageEnum msg)
        {
            Message = msg;
        }
    }
}