using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PeixeAbissal.UI {

    public class UIElement : MonoBehaviour {

        public virtual void SetState (ActiveState state, float finalValue, float tweenDuration = 1, Ease ease = Ease.InOutSine, Action callback = null) {

            var tween = state.Equals (ActiveState.ENABLE) ? EnableAnimation (GetComponent<Image> (), finalValue, tweenDuration) :
                DisableAnimation (GetComponent<Image> (), tweenDuration);
            tween.SetEase (ease);
            tween.OnComplete (() => callback?.Invoke ());
            tween.Play ();
        }

        public virtual void SetState (ActiveState state, Tween customTween, Action callback = null) {

            if (state.Equals (ActiveState.ENABLE)) gameObject.SetActive (true);

            var tween = customTween != null ? customTween : DisableAnimation (GetComponent<Image> (), 1f);
            tween.OnComplete (() => {
                callback?.Invoke ();
                if (state.Equals (ActiveState.DISABLE)) gameObject.SetActive (false);
            });
            tween.Play ();
        }

        protected virtual Tween EnableAnimation (Graphic target, float finalValue, float tweenDuration) {

            gameObject.SetActive (true);
            return target.DOFade (finalValue, tweenDuration)
                .From (0);
        }

        protected virtual Tween DisableAnimation (Graphic target, float tweenDuration) {

            return target.DOFade (0, tweenDuration)
                .OnComplete (() => gameObject.SetActive (false));
        }
    }
}