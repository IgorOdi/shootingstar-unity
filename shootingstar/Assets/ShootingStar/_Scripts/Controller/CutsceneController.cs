using PeixeAbissal.Input;
using PeixeAbissal.Model;
using PeixeAbissal.Service;
using PeixeAbissal.UI.Conteiner;
using UnityEngine;

namespace PeixeAbissal.Controller {

    public class CutsceneController : MonoBehaviour {

        [SerializeField]
        private TextController textController;
        [SerializeField]
        private WishController wishController;
        [SerializeField]
        private StarStatusController starStatusController;

        [TextArea (3, 10)]
        public string starPresentationCutsceneText;

        private StarService starService = new StarService ();
        private ConfigService configService = new ConfigService ();

        private const string SURVIVED_TEXT = "THE LAST STAR SURVIVED!";
        private const string PERISHED_TEXT = "THE LAST STAR PERISHED :(";

        private const string ALREADY_WISHED = "Você já desejou para essa estrela, volte mais tarde saber o seu destino";

        public void InitializeGame (CurrentStar startingStar) {

            var playerStatus = PlayerController.GetPlayerStatus ();

            if (playerStatus.Equals (PlayerStatus.NEVER_PLAYED)) {

                StarPresentationCutscene (startingStar);
            } else {

                Debug.Log ($"The current star is {startingStar.starName}");

                int lastPlayerStar = PlayerController.GetPlayerLastStar ();
                int actualStar = startingStar.starIndex;

                if (actualStar > lastPlayerStar) {

                    ShowLastStarResults ();
                } else {

                    textController.ShowText (ALREADY_WISHED, () => textController.HideText ());
                }
            }
            starStatusController.StartUpdatingTime (onCompleteTimeCycle: ShowLastStarResults);
        }

        public void StarPresentationCutscene (CurrentStar star) {

            ConfigService configService = new ConfigService ();
            configService.GetConfig (this, (config) => {

                string textToShow = string.Format (starPresentationCutsceneText, star.starName, star.starProperty, config.wishesNeeded - star.wishesReceived);
                textController.ShowText (textToShow, () => {
                    textController.HideText ();
                    wishController.ShowMakeWishField ();
                });
            });
        }

        public void ShowLastStarResults () {

            configService.GetConfig (this, (config) => {

                int lastSeenStar = PlayerController.GetPlayerLastStar ();
                starService.GetStarResult (lastSeenStar, this, (result) => {

                    textController.ShowText (result.starSurvived ? SURVIVED_TEXT : PERISHED_TEXT, () => {

                        StarService starService = new StarService ();
                        starService.GetCurrentStar (this, (currentStar) => {

                            InputManager.SetMouseClick (true, () => {

                                textController.HideText ();
                                StarPresentationCutscene (currentStar);
                            });
                        });

                    });
                });

            });
        }
    }
}