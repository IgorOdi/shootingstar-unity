using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PeixeAbissal.UI {

    public class UIElement : MonoBehaviour {

        public virtual void SetState (ActiveState state, float finalValue, float tweenDuration = 1, Ease ease = Ease.InOutSine, Action callback = null) {

            var tweens = state.Equals (ActiveState.ENABLE) ? EnableAnimation (GetComponent<Image> (), finalValue, tweenDuration) :
                DisableAnimation (GetComponent<Image> (), tweenDuration);

            for (int i = 0; i < tweens.Count; i++) {
                tweens[i].SetEase (ease);
                if (i == tweens.Count - 1)
                    tweens[i].OnComplete (() => callback?.Invoke ());
                tweens[i].Play ();
            }
        }

        public virtual void SetState (ActiveState state, Tween customTween, Action callback = null) {

            if (state.Equals (ActiveState.ENABLE)) gameObject.SetActive (true);

            var tweens = customTween != null ? new List<Tween> () { customTween } : DisableAnimation (GetComponent<Image> (), 1f);
            for (int i = 0; i < tweens.Count; i++) {

                if (i == tweens.Count - 1)
                    tweens[i].OnComplete (() => {
                        callback?.Invoke ();
                        if (state.Equals (ActiveState.DISABLE)) gameObject.SetActive (false);
                    });
                tweens[i].Play ();
            }
        }

        protected virtual List<Tween> EnableAnimation (Graphic target, float finalValue, float tweenDuration) {

            List<Tween> tweens = new List<Tween> ();
            foreach (Graphic g in GetComponentsInChildren<Graphic> ()) {

                gameObject.SetActive (true);
                tweens.Add (g.DOFade (finalValue, tweenDuration)
                    .From (0)
                    .Play ()
                );
            }
            return tweens;
        }

        protected virtual List<Tween> DisableAnimation (Graphic target, float tweenDuration) {

            List<Tween> tweens = new List<Tween> ();
            foreach (Graphic g in GetComponentsInChildren<Graphic> ()) {
                tweens.Add (g.DOFade (0, tweenDuration)
                    .OnComplete (() => gameObject.SetActive (false)).Play ());
            }
            return tweens;
        }
    }
}