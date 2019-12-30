using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assignment3;

namespace realwebthing.Model {
    public class jsonData {

        public static Dictionary<int,jsonDeserializer.locationPoints> loadJsonFile() {

            string json;
            var config = Model.config.configJason();
            using (StreamReader reader = new StreamReader(config["localData"])){
                json = reader.ReadToEnd();
            }

            int index = 0;
            return jsonDeserializer.deserializing(json).locations.ToDictionary(id => index++, dataPoint => dataPoint);
        }
        
    }
}