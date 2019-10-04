using  Newtonsoft.Json;

namespace Assignment3 {
    //json stuff that needs to be moved to another class
    public class jsonDeserializer {
        public static locationData deserializing(string jsonData) {

            string json = jsonData;

            var model = JsonConvert.DeserializeObject<locationData>(json);

            return model;
        }

        public class locationData {
            public locationPoints[] locations;
        }

        public class locationPoints {
            public long timestampMs;
            public double latitudeE7;
            public double longitudeE7;
        }

    } 
}