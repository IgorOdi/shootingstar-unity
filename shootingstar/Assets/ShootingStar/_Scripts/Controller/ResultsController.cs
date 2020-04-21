using System.Collections.Generic;
using PeixeAbissal.Model;
using PeixeAbissal.Service;
using PeixeAbissal.UI;
using PeixeAbissal.UI.Conteiner;
using PeixeAbissal.Utils;
using TMPro;
using UnityEngine;

namespace PeixeAbissal.Controller {

    public class ResultsController : MonoBehaviour {

        [SerializeField]
        private TextController textController;

        [SerializeField]
        private UIElement scrollElement;

        [SerializeField]
        private TextMeshProUGUI wishesText;
        [SerializeField]
        private TextMeshProUGUI starName;
        [SerializeField]
        private TextMeshProUGUI lifetime;

        [SerializeField]
        private Transform contentParent;

        [SerializeField]
        private ButtonController continueButton;
        [SerializeField]
        private ButtonController creditsButton;

        [SerializeField]
        private GameObject wishResultPrefab;

        WishService wishService = new WishService ();
        ConfigService configService = new ConfigService ();

        [SerializeField]
        private Color imparColor;
        [SerializeField]
        private Color parColor;

        private List<GameObject> wishesAdded = new List<GameObject> ();

        private bool wishListOnScreen;

        private const string SURVIVED_TEXT = "THE LAST STAR SURVIVED!";
        private const string PERISHED_TEXT = "THE LAST STAR PERISHED :(";

        public void ShowResults (Results result) {

            wishesAdded.Clear ();
            textController.ShowText (result.starSurvived ? SURVIVED_TEXT : PERISHED_TEXT, onClick: () => {

                float delay = 0.1f;
                if (wishListOnScreen) {
                    HideResults ();
                    delay = 1f;
                }

                this.RunDelayed (delay, () => {

                    wishListOnScreen = true;
                    textController.HideText ();
                    int starIndex = PlayerController.GetPlayerLastStar ();
                    SetHeader (starIndex, result);
                    SetWishes (starIndex);
                    scrollElement.SetState (ActiveState.ENABLE, 1f, 2f);
                    PlayerController.SetPlayerStatus (PlayerStatus.NEVER_PLAYED);
                });
            });

            continueButton.SetState (ActiveState.ENABLE);
            creditsButton.SetState (ActiveState.ENABLE);

            continueButton.AddClickEvent (() => {

                HideResults ();
                FindObjectOfType<CutsceneController> ().InitializeGame ();

            });
            creditsButton.AddClickEvent (HideResults);
        }

        private void SetHeader (int starIndex, Results result) {

            configService.GetConfig (this, (config) => {

                int interval = config.starInterval;
                starName.text = result.starName;
                wishesText.text = $"{result.wishesReceived}/{config.wishesNeeded}";

                var sufix = interval < 86400 ? "h" : "d";
                interval = interval >= 86400 ? interval / 24 /60 /60 : interval / 60 / 60;
                lifetime.text = (interval + sufix).ToString ();
            });
        }

        private void SetWishes (int starIndex) {

            wishService.GetAllWishesWithIndex (PlayerController.GetPlayerLastStar (), this, (wishes) => {

                Debug.Log ($"Configurando {wishes.Count} desejos");
                for (int i = 0; i < wishes.Count; i++) {

                    wishesAdded.Add (Instantiate (wishResultPrefab, contentParent));
                    wishesAdded[i].GetComponent<WishResultController> ().SetWishResult (i, wishes[i].text);
                    wishesAdded[i].GetComponent<WishResultController> ().SetColor (i % 2 == 0 ? imparColor : parColor);
                }
            });
        }

        public void HideResults () {

            wishListOnScreen = false;
            scrollElement.SetState (ActiveState.DISABLE);
            continueButton.SetState (ActiveState.DISABLE);
            creditsButton.SetState (ActiveState.DISABLE);

            foreach (GameObject g in wishesAdded) {
                DestroyImmediate (g);
            }
            wishesAdded.Clear ();
        }
    }
}