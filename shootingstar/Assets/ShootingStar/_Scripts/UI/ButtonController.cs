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

        public override void SetState (ActiveState state, float finalValue, float tweenDuration = 1, Ease ease = Ease.InOutSine, Action callback = null) {

            base.SetState (state, finalValue, tweenDuration, ease, callback);
            var tween = state.Equals (ActiveState.ENABLE) ? EnableAnimation (GetComponentInChildren<TextMeshProUGUI> (), finalValue, tweenDuration) :
                DisableAnimation (GetComponentInChildren<TextMeshProUGUI> (), tweenDuration);
            tween.SetEase (ease);
            tween.OnComplete (() => callback?.Invoke ());
            tween.Play ();
        }

        public override void SetState (ActiveState state, Tween customTween, Action callback = null) {

            base.SetState (state, customTween, callback);

            var tween = customTween != null ? customTween : DisableAnimation (GetComponentInChildren<TextMeshProUGUI> (), 1f);
            tween.OnComplete (() => callback?.Invoke ());
            tween.Play ();
        }
    }
}