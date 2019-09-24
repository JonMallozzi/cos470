using Newtonsoft.Json;

namespace Assignment2 {
    public class jsonDeserializer {
        public featureArray deserializing(string wedReqJason) {
            
        string json = wedReqJason;

        var model = JsonConvert.DeserializeObject<featureArray>(json);
       
        return model;
        }
    }
    public class featureArray{
        public attributeField[] features;
    }

    public class attributeField {
        public fields attributes; 
    }

    public class fields{
        public int ADDRESS_NUMBER;
        public string STREETNAME;
        public string SUFFIX;
    }
}