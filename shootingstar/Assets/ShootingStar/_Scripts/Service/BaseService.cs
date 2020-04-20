using System;
using System.Collections;
using PeixeAbissal.Model;
using UnityEngine;
using UnityEngine.Networking;

namespace PeixeAbissal.Service {

    public abstract class BaseService {

        protected const string BASE_URL = "https://shootingstar-backend.herokuapp.com";
        protected const string DEV_URL = "localhost:3000";

        protected static string APP_URL {
            get {
#if DEV
                return DEV_URL;
#else
                return BASE_URL;
#endif
            }
        }

        protected void Get (string url, MonoBehaviour mono, Action<string> callback) {

            UnityWebRequest www = UnityWebRequest.Get ($"{APP_URL}{url}");
            mono.StartCoroutine (GetCoroutine (www, callback));
        }

        protected IEnumerator GetCoroutine (UnityWebRequest www, Action<string> callback) {

            yield return www.SendWebRequest ();
            if (!www.isNetworkError && !www.isHttpError) {

                callback (www.downloadHandler.text);
            } else {

                Debug.Log (www.error);
            }
        }

        protected void Post (string url, WWWForm body, MonoBehaviour mono, Action<string> callback) {

            UnityWebRequest www = UnityWebRequest.Post ($"{APP_URL}{url}", body);
            mono.StartCoroutine (PostCoroutine (www, callback));
        }

        protected IEnumerator PostCoroutine (UnityWebRequest www, Action<string> callback) {

            yield return www.SendWebRequest ();
            if (!www.isNetworkError && !www.isHttpError) {

                callback (www.downloadHandler.text);
            } else {

                Debug.Log (www.error);
            }
        }
    }
}