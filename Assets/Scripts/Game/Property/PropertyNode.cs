using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class PropertyNode
    {
        public float ValueFixed { get { return valueFixed; } }

        private Action<float> onValueChanged;
        private float valueAddAdd;
        private float valueAddMul;
        private float valueBase;
        private float valueFixed;

        public void Init(float value, Action<float> onValueChanged)
        {
            valueBase = value;
            this.onValueChanged = onValueChanged;
            Reset();
            RefreshValueFixed();
        }

        public void AddValueAdd(float addV)
        {
            valueAddAdd += addV;
            RefreshValueFixed();
        }

        public void AddValueMul(float addV)
        {
            valueAddMul += addV;
            RefreshValueFixed();
        }

        public void Reset()
        {
            valueAddMul = 1;
            valueAddAdd = 0;
            RefreshValueFixed();
        }

        public void ResetValueBase(float value)
        {
            valueBase = value;
            RefreshValueFixed();
        }

        private void RefreshValueFixed()
        {
            valueFixed = (valueBase + valueAddAdd) * valueAddMul;
            if (onValueChanged != null)
                onValueChanged(valueFixed);
        }
    }
}