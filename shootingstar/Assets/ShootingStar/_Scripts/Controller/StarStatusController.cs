using System;
using System.Collections;
using PeixeAbissal.Service;
using PeixeAbissal.UI.Conteiner;
using UnityEngine;

namespace PeixeAbissal.Controller {

    public class StarStatusController : MonoBehaviour {

        [SerializeField]
        private StarStatusConteiner starStatusConteiner;
        private WaitForSeconds updateTime = new WaitForSeconds (10);

        private StarService starService = new StarService ();
        private DateTime unixStartTime = new DateTime (1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        private ShootingStarController shootingStarController;

        private int lastMinutes;
        private bool firstCheck = true;

        public void StartUpdatingTime (Action onCompleteTimeCycle) {

            shootingStarController = FindObjectOfType<ShootingStarController> ();
            StartCoroutine (UpdateTimeCoroutine (onCompleteTimeCycle));
        }

        private IEnumerator UpdateTimeCoroutine (Action onCompleteTimeCycle) {

            while (true) {

                SetTimeAndWishes (onCompleteTimeCycle);
                yield return updateTime;
            }
        }

        private void SetTimeAndWishes (Action onCompleteTimeCycle) {

            starService.GetCurrentStar (this, (currentStar) => {

                ConfigService configService = new ConfigService ();
                configService.GetConfig (this, (config) => {

                    int lastCheck = lastMinutes;
                    lastMinutes = UnixToMinutes (currentStar.endTime, Mathf.CeilToInt (config.starInterval / 60f));
                    double lastSeconds = UnixToSeconds (currentStar.endTime, Mathf.CeilToInt (config.starInterval / 60f));

                    if (lastMinutes > lastCheck) {
                        if (!firstCheck)
                            onCompleteTimeCycle?.Invoke ();
                        firstCheck = false;
                    }

                    shootingStarController.MoveStar ((float) lastSeconds, config.starInterval);
                    starStatusConteiner.SetTime (lastMinutes);
                    starStatusConteiner.SetWishesMissing (config.wishesNeeded - currentStar.wishesReceived);
                });
            });
        }

        private int UnixToMinutes (double currentStarEndTime, int interval) {

            DateTime endDateTime = unixStartTime.AddMilliseconds (currentStarEndTime);
            DateTime nowDateTime = DateTime.UtcNow;
            return (endDateTime - nowDateTime).Minutes + 1;
        }

        private double UnixToSeconds (double currentStarEndTime, int interval) {

            DateTime endDateTime = unixStartTime.AddMilliseconds (currentStarEndTime);
            DateTime nowDateTime = DateTime.UtcNow;
            return (endDateTime - nowDateTime).TotalSeconds;
        }
    }
}