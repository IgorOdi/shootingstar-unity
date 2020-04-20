using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PeixeAbissal.UI {

    [RequireComponent (typeof (Button))]
    public class ButtonController : UIElement {

        private Button _button;
        private Button button {
            get {

                if (_button == null) _button = GetComponent<Button> ();
                return _button;
            }

            set { }
        }

        public void AddClickEvent (Action clickEvent) {

            button.onClick.AddListener (() => clickEvent ());
        }

        public void OnClick (Action clickEvent) {

            RemoveAllListeners ();
            AddClickEvent (clickEvent);
        }

        public void RemoveAllListeners () {

            button.onClick.RemoveAllListeners ();
        }
    }
}