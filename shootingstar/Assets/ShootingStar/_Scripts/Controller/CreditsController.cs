using PeixeAbissal.Input;
using PeixeAbissal.UI;
using UnityEngine;

namespace PeixeAbissal.Controller {

    public class CreditsController : MonoBehaviour {

        [SerializeField]
        private UIElement creditsImage;

        public void ShowCredits () {

            creditsImage.SetState (ActiveState.ENABLE);
            InputManager.SetMouseClick (true, HideCredits);
        }

        public void HideCredits () {

            creditsImage.SetState (ActiveState.DISABLE);
        }
    }
}