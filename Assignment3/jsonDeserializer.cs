using  Newtonsoft.Json;

namespace Assignment3 {
    
    //the class the deserializes the json data into a json object Array with the timestamp, lat, and long
    //of each data point
    public class jsonDeserializer{
        public static locationData deserializing(string jsonData) {

            string json = jsonData;

            var model = JsonConvert.DeserializeObject<locationData>(json);
            
            return model;
        }

        public class locationData  {  
            public locationPoints[] locations;
        }

        public class locationPoints {
            public long timestampMs;
            public double latitudeE7;
            public double longitudeE7;
        }
    } 
}