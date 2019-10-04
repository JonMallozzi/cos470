using System;
using System.Collections.Generic;

namespace Assignment3 {
    public class metCheckingLogic {
        
        //converts a timestamp long to a Datetime
        public static DateTime UnixTimeStampToDateTime( long unixTimeStamp ) {
            
            // Unix timestamp epoch
            DateTime dtDateTime = new DateTime(1970,1,1,0,0,0,0);
           
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp);
         
            return dtDateTime;
        }
        
        public static List<jsonDeserializer.locationPoints>  wherewasIOn(DateTime date, jsonDeserializer.locationData data) {

            List<jsonDeserializer.locationPoints> dateMatchingPoints = new List<jsonDeserializer.locationPoints>();
            
            //using a foreach instead of linq so the loop can break 
            foreach (var x in data.locations) {
                
                if (UnixTimeStampToDateTime(x.timestampMs).Date.ToShortDateString() == date.ToShortDateString()) {
                    dateMatchingPoints.Add(x);
                
                //breaking if data has gone past the time trying to be matched
                }else if (UnixTimeStampToDateTime(x.timestampMs).Date > date.Date) {
                    break;
                }
            }
            
            //returns a list of all the locationPoints that were taken that matched the specified date in the parameters
            return dateMatchingPoints;
        }

        public static List<jsonDeserializer.locationPoints> haveWeMet(jsonDeserializer.locationData person1,
            jsonDeserializer.locationData person2) {

            //the list that is being returned when all locationPoints are added to it
            List<jsonDeserializer.locationPoints> matchingPoints = new List<jsonDeserializer.locationPoints>();

            int i = 0; //person 1 iterator 
            int j = 0; //person 2 iterator 
            while (true) {

                //for some reason person1.location[point].equals(person2.locations[point] always returns false
                //Though it finds that they match if I test all three cases
                //also note in actual production there needs to be some offset for the data
                //to account for the two people meeting a location but google gets their location
                //data at different times or lat/long of the event 
                if (person1.locations[person1.locations.Length - 1 - i].latitudeE7
                    .Equals(person2.locations[person2.locations.Length - 1 - j].latitudeE7)
                    && person1.locations[person1.locations.Length - 1 - i].longitudeE7
                        .Equals(person2.locations[person2.locations.Length - 1 - j].longitudeE7) 
                    && person1.locations[person1.locations.Length - 1 - i].timestampMs
                        .Equals(person2.locations[person2.locations.Length - 1 - j].timestampMs)
                    ) {
                    
                    matchingPoints.Add(person1.locations[person1.locations.Length - 1 - i]);
                    i++;
                    j++;
                    
                }
                //since google orders all data points by the timestamp, the algorithm has to catch up
                //the points of data of each person to be close if they are falling behind 
                else if (person1.locations[person1.locations.Length - 1 - i].timestampMs >
                        person2.locations[person2.locations.Length - 1 - j].timestampMs) {
                    i++;
                }else {
                    j++;
                }
               
                //checking to see with increase of any of the incrementing variables hit past the end
                //of the list and if so causes the loop to break
                if (person1.locations.Length - 1 - i == -1 || person2.locations.Length - 1 - j == -1)
                    break;
            }
            return matchingPoints;
        }
    }
}