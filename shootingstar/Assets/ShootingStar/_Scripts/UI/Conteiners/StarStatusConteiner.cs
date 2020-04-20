﻿using TMPro;
using UnityEngine;

namespace PeixeAbissal.UI.Conteiner {

    public class StarStatusConteiner : MonoBehaviour {

        [SerializeField]
        private TextMeshProUGUI timeLeft;
        [SerializeField]
        private TextMeshProUGUI wishesMissing;

        public void SetTime (int minutes) {

            timeLeft.text = $"{minutes} minutes left";
        }

        public void SetWishesMissing (int wishes) {

            wishesMissing.text = $"{wishes} wishes missing";
        }
    }
}