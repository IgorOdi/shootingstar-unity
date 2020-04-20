using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PeixeAbissal.UI {

    public class UIElement : MonoBehaviour {

        public virtual void SetState (ActiveState state, float tweenDuration = 1, Ease ease = Ease.InOutSine, Action callback = null) {

            var tween = state.Equals (ActiveState.ENABLE) ? EnableAnimation (tweenDuration) : DisableAnimation (tweenDuration);
            tween.SetEase (ease);
            tween.OnComplete (() => callback?.Invoke ());
            tween.Play ();
        }

        public virtual void SetState (ActiveState state, Tween customTween, Action callback = null) {

            if (state.Equals (ActiveState.ENABLE)) gameObject.SetActive (true);
            var tween = customTween;
            tween.OnComplete (() => {
                callback?.Invoke ();
                if (state.Equals (ActiveState.DISABLE)) gameObject.SetActive (false);
            });
            tween.Play ();
        }

        protected Tween EnableAnimation (float tweenDuration) {

            gameObject.SetActive (true);
            return transform.DOScale (Vector3.one, tweenDuration)
                .From (Vector3.zero);
        }

        protected Tween DisableAnimation (float tweenDuration) {

            return transform.DOScale (Vector3.zero, tweenDuration)
                .OnComplete (() => gameObject.SetActive (false));
        }
    }
}