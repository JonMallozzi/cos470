/***************
* Jon Mallozzi *
* 9/23/19      *
* COS 470      *
* Assignment2  *
****************/

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Linq;

namespace Assignment2 {
    
   public class Program {

        public static string webJsonReq(config configData) {
            
            // Using HttpUtility to generate the parameters for the query string handles HTTP escaping the values.
            var query = HttpUtility.ParseQueryString(string.Empty);

            //query ids
            //added resultRecordCount and orderByFields to sort the json but they are not needed
            query["where"] = configData.configJason()["municipality"];
            query["outFields"] = configData.configJason()["outFields"];
            query["orderByFields"] = configData.configJason()["orderByFields"];
            query["resultRecordCount"] = configData.configJason()["numberOfRecords"]; 
            query["f"] = configData.configJason()["format"];

            var address = configData.configJason()["getReqURL"] + query;

            using (var client = new HttpClient()) {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, address)) {
                    
                    var response = client.SendAsync(request).Result;
                    var content = response.Content.ReadAsStringAsync().Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new Exception($"{content}: {response.StatusCode}");
                    }
                    
                    return content;
                }
            }
        }
        
       //gets the cost of the ADDRESS 
       public static int wordCost(string address, string suffix) {
           
           //concats the address and suffix so it get cost of both variables combined
           //to get the true cost
            string word = address + suffix; 
               
            return word.ToLower().Where(char.IsLetter).Sum(n => n - '`');
        }
       
       //gets the list matching costs
       public static List<attributeField> costsThatMatch(featureArray jsonModel) {
           return jsonModel.features.Where((a => wordCost(a.attributes.STREETNAME, a.attributes.SUFFIX) 
                                             == a.attributes.ADDRESS_NUMBER)).ToList();
       }
       
       //prints out the all of the matching addresses
       public static void results(List<attributeField> resultList , config configData) {
           
           Console.WriteLine("All matching cost address for " 
                                    + configData.configJason()["municipality"] 
                                    + " or the first set of matching addresses if the municipality has over 5000 addresses");
           
           //loops through the list because its type name and not string
           foreach (var x in resultList) {
               Console.WriteLine("The address " 
                                 + x.attributes.STREETNAME 
                                 + " " + x.attributes.SUFFIX 
                                 + " cost matched it's address of " 
                                 + x.attributes.ADDRESS_NUMBER);
           }
       }
        
        static void Main(string[] args) {
            
           //load config json data
           config configData = new config();

           //loading the jsonDeserialzer in 
           jsonDeserializer deserializer = new jsonDeserializer();

          //calling the webrequest to get json and then deserializing it 
          featureArray jsonData = deserializer.deserializing(webJsonReq(configData));
          
          //getting the matching addresses
          List<attributeField> matchingAdresses = costsThatMatch(jsonData);
          
          //printing the results
          results(matchingAdresses, configData);

        }
    }
   // 9/24 notes 
   // use interface instead of class so you only have to change the interface instead of the code base
   // dependancy injection
   // 
   
   
}