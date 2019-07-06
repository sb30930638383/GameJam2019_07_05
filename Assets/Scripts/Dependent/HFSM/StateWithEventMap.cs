using System;
using System.Collections.Generic;

namespace GameJam2019
{
    public class StateWithEventMap<T> : State where T : IStateable
    {
        protected Dictionary<string, Func<HFSMEvent, string>> mActionMap = new Dictionary<string, Func<HFSMEvent, string>>();
        //		protected Dictionary<string, Action<HFSMEvent>> mActionMap = new Dictionary<string, Action<HFSMEvent>> ();
        //		protected event Action onStart;
        //		protected event Action onOver;
        protected T mOwner;

        public StateWithEventMap(T owner){
            mOwner = owner;
            ConstructStart();
            ConstructOver();
            ConstructStateEvent();
            ConstructActionEvent();
        }

        protected virtual void ConstructStart() { }

        protected virtual void ConstructOver() { }

        protected virtual void ConstructStateEvent() { }

        protected virtual void ConstructActionEvent() { }

		public void AddStateEvent(string msg, string nextState){
			System.Diagnostics.Debug.Assert (!mActionMap.ContainsKey (msg));
			mActionMap.Add (msg, delegate {
				return nextState;
			});
		}

		public void AddActionEvent(string msg, Action action){
			System.Diagnostics.Debug.Assert (!mActionMap.ContainsKey (msg));
			mActionMap.Add (msg, delegate {
				action ();
				return "";
			});
		}

		public void AddActionEvent(string msg, Action<HFSMEvent> action){
			System.Diagnostics.Debug.Assert (!mActionMap.ContainsKey (msg));
			mActionMap.Add (msg, delegate(HFSMEvent evt) {
				action (evt);
				return "";
			});
		}

		public override HFSMEvent PostEvent (HFSMEvent evt)
		{
			string nextState = "";
			Func<HFSMEvent, string> func;
            if (mActionMap.TryGetValue(evt.msg, out func))
            {
                nextState = func(evt);
                return new HFSMEvent(nextState, evt.obj0, evt.obj1, evt.obj2, evt.obj3, evt.obj4, evt.obj5);
            }
            else {
                if (FatherState == null) return new HFSMEvent(nextState);
                return FatherState.PostEvent(evt);
            }
		}
	}
}

