using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PeixeAbissal.Model;
using UnityEngine;

namespace PeixeAbissal.Service {

    public class StarService : BaseService {

        private const string STAR_URL = "/star";
        private const string STAR_LIST = STAR_URL + "/list";
        private const string STAR_CURRENT = STAR_URL + "/current";
        private const string STAR_RESULTS = STAR_URL + "/survive";

        public void GetCurrentStar (MonoBehaviour mono, Action<CurrentStar> callback) {

            Get (STAR_CURRENT, mono, (response) => {

                var currentStar = JsonConvert.DeserializeObject<CurrentStar> (response);
                callback?.Invoke (currentStar);
            });
        }

        public void GetStarResult (int index, MonoBehaviour mono, Action<Results> callback) {

            Get (STAR_RESULTS + $"/{index}", mono, (response) => {

                var starResult = JsonConvert.DeserializeObject<Results> (response);
                callback?.Invoke (starResult);
            });
        }
    }
}