using PeixeAbissal.Audio;
using PeixeAbissal.Input;
using PeixeAbissal.Model;
using PeixeAbissal.Service;
using PeixeAbissal.UI;
using PeixeAbissal.UI.Conteiner;
using PeixeAbissal.Utils;
using UnityEngine;

namespace PeixeAbissal.Controller {

    public class CutsceneController : MonoBehaviour {

        [SerializeField]
        private TextController textController;
        [SerializeField]
        private WishController wishController;
        [SerializeField]
        private StarStatusController starStatusController;
        [SerializeField]
        private ResultsController resultsController;

        private StarService starService = new StarService ();
        private ConfigService configService = new ConfigService ();

        [SerializeField]
        private ButtonController yesButton, noButton, makeWishButton;

        private string starPresentationCutsceneText = "Hello sweetheart, I'm passing by this beautiful sky and I need your help indeed. I need to complete my journey but i don't have enough energy. Can you help me? You only need to make a wish based on {1} energy";
        private string starYes = "Oh! You have a good heart!";
        private string starNo = "Please? I don't have much time left";

        private const string ALREADY_WISHED = "You've already made a wish for the star {0}. Come Back later to know what happened with the star and your wish";

        public void InitializeGame () {

            starService.GetCurrentStar (this, (currentStar) => {

                var playerStatus = PlayerController.GetPlayerStatus ();
                if (playerStatus.Equals (PlayerStatus.NEVER_PLAYED)) {

                    StarPresentationCutscene (currentStar);
                } else {

                    Debug.Log ($"The current star is {currentStar.starName}");

                    int lastPlayerStar = PlayerController.GetPlayerLastStar ();
                    int actualStar = currentStar.starIndex;

                    if (actualStar > lastPlayerStar) {

                        ShowLastStarResults ();
                    } else {

                        string text = string.Format (ALREADY_WISHED, currentStar.starName);
                        textController.ShowText (text, () => textController.HideText ());
                    }
                }
            });

            AudioManager.Instance.PlayMusic (Music.WISH);
            starStatusController.StartUpdatingTime (onCompleteTimeCycle: ShowLastStarResults);
        }

        public void StarPresentationCutscene (CurrentStar star) {

            ConfigService configService = new ConfigService ();
            configService.GetConfig (this, (config) => {

                string textToShow = string.Format (starPresentationCutsceneText, star.starName, star.starProperty, config.wishesNeeded - star.wishesReceived);
                textController.ShowText (textToShow, onComplete : ConfigureYesOrNoButtons);
            });
        }

        public void ConfigureYesOrNoButtons () {

            yesButton.SetState (ActiveState.ENABLE);
            noButton.SetState (ActiveState.ENABLE);
            yesButton.AddClickEvent (() => { ConfigureYesOrNoQuestion (true); });
            noButton.AddClickEvent (() => { ConfigureYesOrNoQuestion (false); });
        }

        public void ConfigureYesOrNoQuestion (bool answeredYes) {

            textController.ShowText (answeredYes ? starYes : starNo, onComplete: () => { makeWishButton.SetState (ActiveState.ENABLE); });
            yesButton.SetState (ActiveState.DISABLE);
            noButton.SetState (ActiveState.DISABLE);
            makeWishButton.AddClickEvent (() => {

                textController.HideText ();
                makeWishButton.SetState (ActiveState.DISABLE);
                this.RunDelayed (1f, wishController.ShowMakeWishField);
            });
        }

        public void ShowLastStarResults () {

            if (PlayerController.GetPlayerStatus ().Equals (PlayerStatus.NEVER_PLAYED)) {

                InitializeGame ();
            }

            wishController.HideMakeWishField ();
            HideAllButtons ();
            textController.HideText (() => {

                int lastSeenStar = PlayerController.GetPlayerLastStar ();
                starService.GetStarResult (lastSeenStar, this, (result) => {

                    AudioManager.Instance.PlaySFX (result.starSurvived ? SFX.VICTORY : SFX.DEFEAT);
                    resultsController.ShowResults (result);
                });
            });
        }

        private void HideAllButtons () {

            yesButton.SetState (ActiveState.DISABLE);
            noButton.SetState (ActiveState.DISABLE);
            makeWishButton.SetState (ActiveState.DISABLE);
        }
    }
}