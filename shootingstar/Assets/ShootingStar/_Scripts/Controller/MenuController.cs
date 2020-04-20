using DG.Tweening;
using PeixeAbissal.Input;
using UnityEngine;
using UnityEngine.UI;

namespace PeixeAbissal.Controller {

    public class MenuController : MonoBehaviour {

        [SerializeField]
        private Image menuImage;
        [SerializeField]
        private Image blackScreen;

        public void InitializeMenu () {

            menuImage.color = new Color (1, 1, 1, 0);
            blackScreen.DOFade (0, 3f)
                .From (1)
                .OnComplete (() => {

                    menuImage.DOFade (1, 3f)
                        .From (0)
                        .OnComplete (() => {

                            InputManager.SetMouseClick(true, FadeMenuAndStartGame);
                        });
                });
        }

        private void FadeMenuAndStartGame () {

            menuImage.DOFade (0, 3f)
                .OnComplete (() => {

                    FindObjectOfType<Bootstrapping.Bootstrapper> ().OnInitializeGame ();
                });
        }
    }
}