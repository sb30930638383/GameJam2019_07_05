using System;
using System.Collections.Generic;

namespace GameJam2019
{
    public class HFSM
    {
        private Dictionary<string, State> mStateDict = new Dictionary<string, State>();
        private List<State> mCurStatePath = new List<State>();
        private State mCurState = null;
        private State mRootState = new State();

        public State CurState { get { return mCurState; } }

        public HFSM()
        {
            mRootState.Name = "Root";
            mStateDict.Add("Root", mRootState);
            mCurStatePath.Add(mRootState);
            mCurState = mRootState;
        }

        public void AddState(string stateName, State state)
        {
            AddState(stateName, state, "Root");
        }

        public void AddState(string stateName, State state, string fatherStateName)
        {
            State fatherState;
            fatherState = mStateDict.TryGetValue(fatherStateName, out fatherState) ? fatherState : null;
            System.Diagnostics.Debug.Assert(fatherState != null, string.Format("指定的 FatherState 不存在，stateName = {0}, fatherStateName = {1}", stateName, fatherStateName));
            System.Diagnostics.Debug.Assert(!mStateDict.ContainsKey(stateName), string.Format("当前HFSM中存在同名State，stateName = {0}", stateName));
            state.Name = stateName;
            state.FatherState = fatherState;
            mStateDict.Add(stateName, state);
        }

        public void Init(string stateName)
        {
            Transform(new HFSMEvent(stateName));
        }

        public bool Post(string msg)
        {
            return Post(new HFSMEvent(msg));
        }

        public bool Post(HFSMEvent evt)
        {
            //HFSMEvent nextStateEvt = null;
            //try
            //{
            //    nextStateEvt = mCurState.PostEvent(evt);
            //}
            //catch (Exception)
            //{
            //    UnityEngine.Debug.LogError(string.Format("出错!当前状态{0}, 要切换的状态{1}, 对象是{2}", master.GetCurState().Name, evt.msg, master.name));
            //    throw;
            //}
            HFSMEvent nextStateEvt = mCurState.PostEvent(evt);

            if (nextStateEvt.msg != "")
            {
                Transform(nextStateEvt);
                return true;
            }
            return false;
        }

        public void Update()
        {
            State state = mCurState;
            while (state != null)
            {
                state.Update();
                state = state.FatherState;
            }
            //if (mCurState == null) {
            //    UnityEngine.Debug.Log("lalala");
            //}
            //UnityEngine.Debug.Log("CurStateName = " + mCurState.Name);
            //mCurState.Update();
        }

        private void Transform(HFSMEvent evt)
        {
            if (evt.msg == mCurState.Name)
                return;

            List<State> curPathList = GetStatePath(mCurState.Name);
            List<State> aimPathList = GetStatePath(evt.msg);
            SplitSamePath(ref curPathList, ref aimPathList);

            for (int i = curPathList.Count - 1; i >= 0; i--)
            {
                curPathList[i].OnOver();
                mCurState = curPathList[i].FatherState;
            }
            for (int i = 0; i < aimPathList.Count; i++)
            {
                mCurState = aimPathList[i];
                aimPathList[i].OnStart(evt.obj0, evt.obj1, evt.obj2, evt.obj3, evt.obj4, evt.obj5);
            }
        }

        private List<State> GetStatePath(string stateName)
        {
            List<State> statePath = new List<State>();
            State curState;
            if (mStateDict.TryGetValue(stateName, out curState))
            {
                while (curState != null)
                {
                    statePath.Insert(0, curState);
                    curState = curState.FatherState;
                }
            }
            return statePath;
        }

        private void SplitSamePath(ref List<State> curPathList, ref List<State> aimPathList)
        {
            int minCount = Math.Min(curPathList.Count, aimPathList.Count);
            for (int i = 0; i < minCount; i++)
            {
                if (curPathList[0] == aimPathList[0])
                {
                    curPathList.RemoveAt(0);
                    aimPathList.RemoveAt(0);
                }
                else
                {
                    break;
                }
            }
        }
    }
}

