using System;

namespace PeixeAbissal.Model {

    [Serializable]
    public class Results {

        public string starName { get; set; }
        public int starIndex { get; set; }
        public int wishesReceived { get; set; }
        public bool starSurvived { get; set; }
    }
}