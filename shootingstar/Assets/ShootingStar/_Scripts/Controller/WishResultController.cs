using TMPro;
using UnityEngine;

namespace PeixeAbissal.Controller {

    public class WishResultController : MonoBehaviour {

        [SerializeField]
        private TextMeshProUGUI index;
        [SerializeField]
        private TextMeshProUGUI wishText;

        public void SetWishResult (int index, string wishText) {

            this.index.text = index.ToString ();
            this.wishText.text = wishText;
        }

        public void SetColor(Color color) {

            index.color = color;
            wishText.color = color;
        }
    }
}