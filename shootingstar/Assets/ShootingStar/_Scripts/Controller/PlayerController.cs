using PeixeAbissal.Service;
using UnityEngine;

namespace PeixeAbissal.Controller {

    public enum PlayerStatus {

        NEVER_PLAYED,
        HAS_WISHED
    }

    public static class PlayerController {

        private static string PlayerStatusKey = "PlayerStatus";
        private static string LastStar = "LastStar";

        public static PlayerStatus GetPlayerStatus () {

            if (!PlayerPrefs.HasKey (PlayerStatusKey)) {

                return PlayerStatus.NEVER_PLAYED;
            } else {

                return PlayerStatus.HAS_WISHED;
            }
        }

        public static void SetPlayerStatus (PlayerStatus playerStatus) {

            PlayerPrefs.SetInt (PlayerStatusKey, (int) playerStatus);
        }

        public static void SetPlayerLastStar (int lastStarIndex) {

            PlayerPrefs.SetInt (LastStar, lastStarIndex);
        }

        public static int GetPlayerLastStar () {

            return PlayerPrefs.GetInt (LastStar);
        }
    }
}