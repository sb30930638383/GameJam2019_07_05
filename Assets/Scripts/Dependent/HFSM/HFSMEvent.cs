using System;

namespace GameJam2019
{
    public class HFSMEvent : EventArgs
    {
        public string msg = "";
        public readonly object obj0 = null;
        public readonly object obj1 = null;
        public readonly object obj2 = null;
        public readonly object obj3 = null;
        public readonly object obj4 = null;
        public readonly object obj5 = null;

        //public string Msg{get{ return mMsg;}}
        //public object Obj1{get{ return mObj1;}}
        //public object Obj2{get{ return mObj2;}}
        //public object Obj3{get{ return mObj3;}}
        //public object Obj4{get{ return mObj4;}}

        public HFSMEvent(string msg, object obj0 = null, object obj1 = null, object obj2 = null, object obj3 = null, object obj4 = null, object obj5 = null)
        {
            this.msg = msg;
            this.obj0 = obj0;
            this.obj1 = obj1;
            this.obj2 = obj2;
            this.obj3 = obj3;
            this.obj4 = obj4;
            this.obj5 = obj5;
        }
    }
}

