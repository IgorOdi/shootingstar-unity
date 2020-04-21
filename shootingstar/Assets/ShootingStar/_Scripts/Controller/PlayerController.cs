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

            return !PlayerPrefs.HasKey (PlayerStatusKey) ? PlayerStatus.NEVER_PLAYED : (PlayerStatus) PlayerPrefs.GetInt (PlayerStatusKey);
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