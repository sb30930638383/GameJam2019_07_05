using System;

namespace GameJam2019
{
	public class State
	{
		protected string mName = "";
		protected State mFatherState = null;
        protected Action<object[]> onStartWithParams;
		protected Action onStart;
		protected Action onOver;

		public string Name{ get { return mName; } set { mName = value; } }
		public State FatherState{get{return mFatherState;}set{mFatherState = value;}}

		public virtual void OnStart(params object[] objs){
            if (onStartWithParams != null)
                onStartWithParams.Invoke(objs);
			if (onStart != null)
				onStart.Invoke ();
		}

		public virtual void OnOver(){
			if (onOver != null)
				onOver.Invoke ();
		}

		public virtual void Update(){
			
		}

        public void AddActionOnStart(Action<object[]> action) {
            onStartWithParams += action;
        }

		public void AddActionOnStart(Action action){
			onStart += action;
		}

		public void AddActionOnOver(Action action){
			onOver += action;
		}

        public virtual HFSMEvent PostEvent(HFSMEvent evt) {
            return new HFSMEvent("");
        }

		//public virtual string PostEvent(HFSMEvent evt){
		//	return "";
		//}
	}
}

