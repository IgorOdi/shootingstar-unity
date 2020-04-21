using TMPro;
using UnityEngine;

namespace PeixeAbissal.UI.Conteiner {

    public class StarStatusConteiner : MonoBehaviour {

        [SerializeField]
        private TextMeshProUGUI timeLeft;
        [SerializeField]
        private TextMeshProUGUI wishesMissing;

        public void SetTime (int minutes) {

            if (minutes >= 60) minutes /= 60;
            string timeScale = minutes >= 60 ? "hours" : "minutes";
            timeLeft.text = $"{minutes} {timeScale} left";
        }

        public void SetWishesMissing (int wishes, int wishesNeeded) {

            wishesMissing.text = $"{wishes}/{wishesNeeded} wishes";
        }
    }
}