using System;
using System.Collections;
using DG.Tweening;
using PeixeAbissal.Audio;
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

        private bool textOnScreen;

        private const float FILL_TIME = 0.05f;
        private const float FADE_DELAY = 0.5f;

        private const float BACKGROUND_ALPHA = 0.75f;
        private const float TEXT_ALPHA = 1f;

        public void ShowText (string text, Action onClick = null, Action onComplete = null) {

            StopAllCoroutines ();
            textBox.gameObject.SetActive (true);
            background.gameObject.SetActive (true);
            AudioManager.Instance.PlaySFX (SFX.POPUP);
            StartCoroutine (FillText (text, onClick, onComplete));
        }

        private IEnumerator FillText (string text, Action onClick = null, Action onComplete = null) {

            if (!textOnScreen) {
                textBox.text = "";
                background.DOFade (BACKGROUND_ALPHA, FADE_DELAY)
                    .From (0)
                    .OnComplete (() => {
                        textBox.DOFade (TEXT_ALPHA, FADE_DELAY)
                            .From (0);
                    });
            } else {

                textBox.DOFade (0f, FADE_DELAY)
                    .OnComplete (() => {
                        textBox.text = "";
                        textBox.DOFade (TEXT_ALPHA, FADE_DELAY);
                    });
            }

            textOnScreen = true;
            yield return new WaitForSeconds (1f);
            textBox.maxVisibleCharacters = 0;
            textBox.text = text;
            while (textBox.maxVisibleCharacters < text.Length) {

                textBox.maxVisibleCharacters += 1;
                yield return new WaitForSeconds (FILL_TIME);
            }

            InputManager.SetMouseClick (true, () => onClick?.Invoke ());
            onComplete?.Invoke ();
        }

        public void HideText (Action onComplete = null) {

            StopAllCoroutines ();
            float duration = textOnScreen ? 1f : 0f;
            textOnScreen = false;
            textBox.DOFade (0, duration);
            background.DOFade (0, duration)
                .OnComplete (() => {

                    onComplete?.Invoke ();
                });
        }
    }
}