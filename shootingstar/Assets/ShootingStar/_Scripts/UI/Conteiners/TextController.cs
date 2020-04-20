using System;
using System.Collections;
using DG.Tweening;
using PeixeAbissal.Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PeixeAbissal.UI.Conteiner {

    public class TextController : MonoBehaviour {

        [SerializeField]
        private TextMeshProUGUI textBox;
        [SerializeField]
        private Image background;

        private const float FILL_TIME = 0.05f;
        private const float FADE_DELAY = 2f;

        public void ShowText (string text, Action onComplete = null) {

            textBox.gameObject.SetActive (true);
            background.gameObject.SetActive (true);
            StartCoroutine (FillText (text, onComplete));
        }

        private IEnumerator FillText (string text, Action onComplete) {

            textBox.text = "";
            background.DOFade (0.5f, 0.5f)
                .From (0)
                .OnComplete (() => {

                    textBox.DOFade (1f, 0.5f)
                        .From (0);
                });

            yield return new WaitForSeconds (1f);
            textBox.maxVisibleCharacters = 0;
            textBox.text = text;
            while (textBox.maxVisibleCharacters < text.Length) {

                textBox.maxVisibleCharacters += 1;
                yield return new WaitForSeconds (FILL_TIME);
            }

            InputManager.SetMouseClick (true, () => onComplete?.Invoke ());
        }

        public void HideText () {

            textBox.DOFade(0, 1f);
            background.DOFade(0, 1f);
        }
    }
}