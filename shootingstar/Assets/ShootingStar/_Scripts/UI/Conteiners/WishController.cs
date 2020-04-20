using DG.Tweening;
using PeixeAbissal.Audio;
using PeixeAbissal.Controller;
using PeixeAbissal.Model;
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

        [SerializeField]
        private Image fillImage;

        private TMP_InputField wishInputFieldComponent;

        void Awake () {

            wishInputFieldComponent = wishInputField.GetComponent<TMP_InputField> ();
        }

        public void ShowMakeWishField () {

            wishInputFieldComponent.text = "";
            wishInputField.SetState (ActiveState.ENABLE, 0.75f, 1f, Ease.InOutSine, () => {
                makeWishButton.SetState (ActiveState.ENABLE, 1f, 1f, Ease.InOutSine, null);
            });
            makeWishButton.OnClick (MakeAWish);
            AudioManager.Instance.PlaySFX (SFX.POPUP);
        }

        public void HideMakeWishField (string starMessage = "") {

            wishInputField.SetState (ActiveState.DISABLE, null, () => {
                makeWishButton.SetState (ActiveState.DISABLE, null, () => {

                    if (starMessage != "")
                        textController.ShowText (starMessage, () => textController.HideText ());
                });
            });
        }

        private void MakeAWish () {

            if (wishInputFieldComponent.text.Length <= 3) {

                //TODO: Your wish is too short
                Debug.LogWarning ("Your wish is too short");
            } else {

                WishService wishService = new WishService ();

                StarService starService = new StarService ();
                starService.GetCurrentStar (this, (currentStar) => {

                    wishService.SendWish (wishInputFieldComponent.text, currentStar.starIndex, this);
                    PlayerController.SetPlayerStatus (PlayerStatus.HAS_WISHED);
                    PlayerController.SetPlayerLastStar (currentStar.starIndex);
                    HideMakeWishField (currentStar.starMessage);
                });
                makeWishButton.RemoveAllListeners ();
                AudioManager.Instance.PlaySFX (SFX.SEND_WISH);
            }
        }

        public void OnType () {

            fillImage.fillAmount = (float) wishInputFieldComponent.text.Length / (float) wishInputFieldComponent.characterLimit;
        }
    }
}