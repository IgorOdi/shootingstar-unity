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

        [TextArea (3, 10)]
        public string starPresentationCutsceneText;

        private StarService starService = new StarService ();
        private ConfigService configService = new ConfigService ();

        private const string SURVIVED_TEXT = "THE LAST STAR SURVIVED!";
        private const string PERISHED_TEXT = "THE LAST STAR PERISHED :(";

        private const string ALREADY_WISHED = "Você já desejou para essa estrela, volte mais tarde saber o seu destino";

        void Start () {

            var playerStatus = PlayerController.GetPlayerStatus ();

            starService.GetCurrentStar (this, (currentStar) => {
                if (playerStatus.Equals (PlayerStatus.NEVER_PLAYED)) {

                    StarPresentationCutscene (currentStar);
                } else {

                    Debug.Log ($"The current star is {currentStar.starName}");

                    int lastPlayerStar = PlayerController.GetPlayerLastStar ();
                    int actualStar = currentStar.starIndex;

                    if (actualStar > lastPlayerStar) {

                        ShowLastStarResults ();
                    } else {
                        //Ainda está na mesma estrela
                        FindObjectOfType<TextController> ().ShowText (ALREADY_WISHED, true);
                    }
                }
            });
            FindObjectOfType<StarStatusController> ().StartUpdatingTime (onCompleteTimeCycle: ShowLastStarResults);
        }

        public void StarPresentationCutscene (CurrentStar star) {

            ConfigService configService = new ConfigService ();
            configService.GetConfig (this, (config) => {

                string textToShow = string.Format (starPresentationCutsceneText, star.starName, star.starProperty, config.wishesNeeded - star.wishesReceived);
                textController.ShowText (textToShow, true, () => {
                    wishController.ShowMakeWishField ();
                });
            });
        }

        public void ShowLastStarResults () {

            configService.GetConfig (this, (config) => {

                int lastSeenStar = PlayerController.GetPlayerLastStar ();
                starService.GetStarResult (lastSeenStar, this, (result) => {

                    FindObjectOfType<TextController> ().ShowText (result.starSurvived ? SURVIVED_TEXT : PERISHED_TEXT, true, () => {

                        StarService starService = new StarService ();
                        starService.GetCurrentStar (this, (currentStar) => {
                            StarPresentationCutscene (currentStar);
                        });

                    });
                });

            });
        }
    }
}