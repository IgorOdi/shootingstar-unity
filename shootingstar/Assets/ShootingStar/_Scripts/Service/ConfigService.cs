using System;
using Newtonsoft.Json;
using PeixeAbissal.Model;
using UnityEngine;

namespace PeixeAbissal.Service {

    public class ConfigService : BaseService {

        private const string CONFIG_URL = "/config";

        private Config config;

        public void GetConfig (MonoBehaviour mono, Action<Config> callback) {

            Get (CONFIG_URL, mono, (response) => {

                var config = JsonConvert.DeserializeObject<Config> (response);
                callback?.Invoke (config);
            });
        }
    }
}