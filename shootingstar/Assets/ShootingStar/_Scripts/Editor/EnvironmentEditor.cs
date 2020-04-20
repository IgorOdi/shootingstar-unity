using UnityEditor;

namespace PeixeAbissal.Editor {

    public class EnvironmentEditor {

        [MenuItem ("Peixes/Set Environment/DEV")]
        public static void SetDev () {

            PlayerSettings.SetScriptingDefineSymbolsForGroup (BuildTargetGroup.WebGL, "DEV");
            PlayerSettings.SetScriptingDefineSymbolsForGroup (BuildTargetGroup.Standalone, "DEV");
        }

        [MenuItem ("Peixes/Set Environment/PROD")]
        public static void SetProd () {

            PlayerSettings.SetScriptingDefineSymbolsForGroup (BuildTargetGroup.WebGL, "PROD");
            PlayerSettings.SetScriptingDefineSymbolsForGroup (BuildTargetGroup.Standalone, "PROD");
        }

        [MenuItem ("Peixes/ClearPlayerPrefs", false, 51)]
        public static void ClearPlayerPrefs () {

            UnityEngine.PlayerPrefs.DeleteAll ();
        }
    }
}