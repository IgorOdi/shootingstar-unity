using System;
using UnityEngine;

namespace PeixeAbissal.Input {

    public class InputManager : MonoBehaviour {

        public static Action OnMouseClick;
        private static bool clearAfterClick;

        public static void SetMouseClick (bool clear, Action onClick) {

            clearAfterClick = clear;
            OnMouseClick = onClick;
        }

        public static void ClearMouseListeners () {

            OnMouseClick = null;
        }

        private void Update () {

            if (UnityEngine.Input.GetMouseButtonDown (0)) {

                OnMouseClick?.Invoke ();
                if (clearAfterClick) ClearMouseListeners ();
            }
        }
    }
}