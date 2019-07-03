using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class MessageDataSend
    {
        public MessageEnum Msg;
        public object[] Objs;
    }

    public class MessageDataResult
    {
        public MessageEnum Msg;
        public object[] Objs;
    }

    public class MessageHandler
    {
        public static MessageHandler Inst { get { return inst; } }
        private static MessageHandler inst = new MessageHandler();

        private Dictionary<MessageEnum, Action<object[]>> funcDict = new Dictionary<MessageEnum, Action<object[]>>();
        private MessageDataSend tempFormatDataSend;
        private MessageDataResult tempFormatDataResult;
        private Action<object[]> tempFunc;

        public void Init()
        {
            InitFunc();
        }

        private void InitFunc()
        {
            funcDict.Clear();

            var atts = SystemUtil.GetMessageMethodListByClass(typeof(MessageManager));
            Action<object[]> func;
            foreach (var v in atts)
            {
                var method = v.Value;
                func = objs => method.Invoke(MessageManager.Inst, objs);
                funcDict.Add(v.Key, func);
            }
        }

        public void SendMsg(MessageEnum msg, params object[] objs)
        {
            if (true)
            {
                tempFormatDataSend = FormatDataSend(msg, GamePlayer.Inst, objs);
                ReceiveMsg(tempFormatDataSend);
            }
        }


        private void ReceiveMsg(MessageDataSend msgData)
        {
            if (funcDict.TryGetValue(msgData.Msg, out tempFunc))
            {
                tempFormatDataResult = FormatDataResult(msgData);
                HandlerMsg(tempFormatDataResult);
            }
        }

        private void HandlerMsg(MessageDataResult msgData)
        {
            if (funcDict.TryGetValue(msgData.Msg, out tempFunc))
            {
                tempFunc(msgData.Objs);
            }
        }


        private MessageDataSend FormatDataSend(MessageEnum msg, params object[] objs)
        {
            return new MessageDataSend
            {
                Msg = msg,
                Objs = objs,
            };
        }

        private MessageDataResult FormatDataResult(MessageDataSend msg)
        {
            return new MessageDataResult
            {
                Msg = msg.Msg,
                Objs = msg.Objs,
            };
        }
    }
}