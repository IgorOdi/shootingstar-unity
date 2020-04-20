﻿using DG.Tweening;
using PeixeAbissal.Controller;
using PeixeAbissal.Service;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PeixeAbissal.UI.Conteiner {

    public class WishController : MonoBehaviour {

        [SerializeField]
        private UIElement wishInputField;
        [SerializeField]
        private ButtonController makeWishButton;
        [SerializeField]
        private UIElement sentWishFeedback;

        [SerializeField]
        private TextController textController;

        public void ShowMakeWishField () {

            wishInputField.GetComponent<TMP_InputField> ().text = "";
            wishInputField.SetState (ActiveState.ENABLE, 0.75f, 1f, Ease.InOutSine, () => {
                makeWishButton.SetState (ActiveState.ENABLE, 1f, 1f, Ease.InOutSine, null);
            });
            makeWishButton.OnClick (MakeAWish);
        }

        public void HideMakeWishField (string starMessage) {

            wishInputField.SetState (ActiveState.DISABLE, null, () => {
                makeWishButton.SetState (ActiveState.DISABLE, null, () => {

                    textController.ShowText (starMessage, () => textController.HideText ());
                });
            });
        }

        private void MakeAWish () {

            TMP_InputField wishField = wishInputField.GetComponent<TMP_InputField> ();
            if (wishField.text.Length <= 3) {

                //TODO: Your wish is too short
                Debug.LogWarning ("Your wish is too short");
            } else {

                WishService wishService = new WishService ();
                wishService.SendWish (wishField.text, this);

                StarService starService = new StarService ();
                starService.GetCurrentStar (this, (currentStar) => {

                    PlayerController.SetPlayerStatus (PlayerStatus.HAS_WISHED);
                    PlayerController.SetPlayerLastStar (currentStar.starIndex);
                    HideMakeWishField (currentStar.starMessage);
                });
                makeWishButton.RemoveAllListeners ();
            }
        }

        private Tween GetOpenTween (UIElement target) {

            float xDuration = 0.5f;
            float yDuration = 0.5f;
            var customTween = target.transform.DOScaleX (endValue: 1f, xDuration)
                .From (0f)
                .SetEase (Ease.InOutSine)
                .OnStart (() => {
                    target.transform.DOScaleY (endValue: 0.25f, xDuration)
                        .From (0f);
                    target.transform.DOScaleY (endValue: 1f, yDuration)
                        .SetDelay (xDuration)
                        .SetEase (Ease.InOutSine);
                });
            return customTween;
        }

        private Tween GetCloseTween (Transform target) {

            float xDuration = 0.5f;
            float yDuration = 0.5f;
            var customTween = target.transform.DOScaleX (endValue: 0f, xDuration)
                .SetEase (Ease.InOutSine)
                .OnStart (() => {
                    target.transform.DOScaleY (endValue: 0.25f, xDuration);
                    target.transform.DOScaleY (endValue: 0f, yDuration)
                        .SetDelay (xDuration)
                        .SetEase (Ease.InOutSine);
                });
            return customTween;
        }
    }
}