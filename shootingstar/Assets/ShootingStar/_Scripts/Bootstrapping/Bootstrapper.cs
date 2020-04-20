using PeixeAbissal.Controller;
using PeixeAbissal.Model;
using PeixeAbissal.Service;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PeixeAbissal.Bootstrapping {

    public class Bootstrapper : MonoBehaviour {

        private static StarService starService;
        private static CurrentStar currentStar;

        private const string BOOTSTRAPPER_SCENE = "Bootstrapper";
        private const string MAIN_SCENE = "Main";

        [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void OnRunApplication () {

            var activeScene = SceneManager.GetActiveScene ();
            if (activeScene.name == BOOTSTRAPPER_SCENE) {

                SceneManager.LoadSceneAsync (MAIN_SCENE, LoadSceneMode.Additive)
                    .completed += (operation) => {

                        var mainScene = SceneManager.GetSceneByName (MAIN_SCENE);
                        SceneManager.SetActiveScene (mainScene);
                        OnRunMainScene ();
                    };

            } else {

                SceneManager.LoadScene (BOOTSTRAPPER_SCENE, LoadSceneMode.Additive);
                OnRunMainScene ();
            }
        }

        private static void OnRunMainScene () {

            var menuController = FindObjectOfType<MenuController> ();
            menuController.InitializeMenu ();

            starService = new StarService ();
            starService.GetCurrentStar (menuController, (loadedStar) => {

                currentStar = loadedStar;
            });
        }

        public void OnInitializeGame () {

            FindObjectOfType<CutsceneController> ().InitializeGame (currentStar);
        }
    }
}