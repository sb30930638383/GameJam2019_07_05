using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public static class MathUtil
    {
        /// <summary>
        /// 获取dir2相对于dir1的偏差角。返回值范围[0, 180]
        /// </summary>
        /// <param name="dir2"></param>
        /// <param name="dir1"></param>
        /// <returns></returns>
        public static float GetAngleWithNoSign(Vector2 dir2, Vector2 dir1)
        {
            float cosTheta = Vector2.Dot(dir2, dir1) / (dir2.magnitude * dir1.magnitude);
            return Mathf.Rad2Deg * Mathf.Acos(cosTheta);
        }
    }
}