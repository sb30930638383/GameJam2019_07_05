using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam2019
{
    public class DoCoroutine
    {
        public static MonoBehaviour Mono;

        public static Coroutine StartCoroutine(IEnumerator iAct)
        {
            return Mono.StartCoroutine(iAct);
        }

        public static void StopCoroutine(Coroutine cor)
        {
            Mono.StopCoroutine(cor);
        }

        public static void StopAllCoroutine()
        {
            Mono.StopAllCoroutines();
        }
    }
}