using System;

namespace PeixeAbissal.Model {

    [Serializable]
    public class CurrentStar {

        public string starName { get; set; }
        public string starProperty { get; set; }
        public string starMessage { get; set; }
        public int wishesReceived { get; set; }
        public double endTime { get; set; }
        public int starIndex { get; set; }
    }
}