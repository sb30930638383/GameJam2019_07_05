using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class ResManager
    {
        public static ResManager Inst { get { return inst; } }
        private static ResManager inst = new ResManager();

        public T Load<T>(string resName, ResTypeEnum resType) where T : Object
        {
            string resPath = string.Format("{0}/{1}", resType.ToString(), resName);
            return Load<T>(resPath);
        }

        public T Load<T>(string resPath) where T : Object
        {
            var resPrefab = Resources.Load<T>(resPath);
            System.Diagnostics.Debug.Assert(resPrefab == null, "error: res load failed, path is " + resPath);
            return resPrefab;
        }
    }
}