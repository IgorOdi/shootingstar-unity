using System;
using System.Collections;
using UnityEngine;

namespace PeixeAbissal.Utils {

    public static class CoroutineUtils {

        public static void RunDelayed (this MonoBehaviour mono, float time, Action callback) {

            mono.StartCoroutine (InternalRunDelayed (time, callback));
        }

        private static IEnumerator InternalRunDelayed (float time, Action callback) {

            yield return new WaitForSeconds (time);
            callback?.Invoke ();
        }
    }
}