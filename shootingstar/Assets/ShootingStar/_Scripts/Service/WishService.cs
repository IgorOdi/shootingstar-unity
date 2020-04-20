using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PeixeAbissal.Model;
using UnityEngine;

namespace PeixeAbissal.Service {

    public class WishService : BaseService {

        private const string WISH = "/wish";

        public void SendWish (string text, MonoBehaviour mono) {

            WWWForm form = new WWWForm ();
            form.AddField ("text", text);
            Post (WISH, form, mono, (response) => {
                Debug.Log ($"Enviou desejo com o texto: {text}");
            });
        }

        public void GetAllWishes (MonoBehaviour mono, Action<List<Wish>> callback = null) {

            List<Wish> wishes = new List<Wish> ();
            Get (WISH, mono, (response) => {

                Debug.Log ("Recebendo todos os desejos");
                wishes = JsonConvert.DeserializeObject<List<Wish>> (response);
                callback (wishes);
            });
        }

        public void GetRandomWish (MonoBehaviour mono, Action<Wish> callback = null) {

            GetAllWishes (mono, (wishlist) => {

                int randomWishIndex = GetRandomValue (0, wishlist.Count);
                callback (wishlist[randomWishIndex]);
            });

        }

        public void GetRandomWishes (int amount, MonoBehaviour mono, Action<List<Wish>> callback = null) {

            List<Wish> newList = new List<Wish> ();
            GetAllWishes (mono, (wishlist) => {

                if (wishlist.Count < amount) {

                    amount = wishlist.Count;
                    Debug.LogWarning ($"Não há desejos suficiente, irá retornar apenas {wishlist.Count}");
                }

                List<int> addedValues = new List<int> ();

                for (int i = 0; i < amount; i++) {

                    int randomValue = GetRandomValue (0, wishlist.Count);
                    while (addedValues.Contains (randomValue)) {

                        randomValue = GetRandomValue (0, wishlist.Count);
                    }
                    addedValues.Add (randomValue);
                }

                for (int i = 0; i < addedValues.Count; i++) {

                    newList.Add (wishlist[addedValues[i]]);
                }
                Debug.Log ($"Adicionou {newList.Count} desejos aleatórios à lista");
            });

            callback?.Invoke (newList);
        }

        private int GetRandomValue (int min, int max) {

            return UnityEngine.Random.Range (min, max);
        }
    }
}