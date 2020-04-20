using System.Collections;
using UnityEngine;

namespace PeixeAbissal.Controller {

    public class ShootingStarController : MonoBehaviour {

        [SerializeField] private AnimationCurve movementCurve;
        [SerializeField] private AnimationCurve rotationCurve;
        [SerializeField] private RectTransform startingPoint;
        [SerializeField] private RectTransform endPoint;

        [SerializeField] private RectTransform starRectTransform;

        private const float MAX_ROTATION_ANGLE = 15;

        private Coroutine predictMovementCoroutine;

        private const float CURVE_PIXEL_INTENSITY = 100;

        public void MoveStar (float timeRemaining, float interval) {

            float t = (interval - timeRemaining) / interval;
            starRectTransform.anchoredPosition = GetPosition (t);
            starRectTransform.eulerAngles = GetRotation (t);

            if (predictMovementCoroutine != null) StopCoroutine (predictMovementCoroutine);
            predictMovementCoroutine = StartCoroutine (PredictMovement (t, interval));
        }

        private IEnumerator PredictMovement (float t, float interval) {

            while (true) {

                starRectTransform.anchoredPosition = GetPosition (t);
                starRectTransform.eulerAngles = GetRotation (t);
                t += Time.deltaTime / interval;
                yield return null;
            }
        }

        private Vector2 GetPosition (float t) {

            return Vector3.Lerp (startingPoint.anchoredPosition + Vector2.up * movementCurve.Evaluate (t) * CURVE_PIXEL_INTENSITY,
                endPoint.anchoredPosition + Vector2.up * movementCurve.Evaluate (t) * CURVE_PIXEL_INTENSITY, t);
        }

        private Vector3 GetRotation (float t) {

            return Vector3.Lerp (Vector3.zero, Vector3.forward * MAX_ROTATION_ANGLE, rotationCurve.Evaluate (t));
        }
    }
}