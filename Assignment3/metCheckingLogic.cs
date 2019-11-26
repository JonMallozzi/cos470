/**********************
* Jon Mallozzi        *
* 10/3/19             *
* COS 470             *
* Assignment 3 Part 1 *
***********************/

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
        
        public static List<jsonDeserializer.locationPoints>  wherewasIOn(DateTime date, 
                    Dictionary<int,jsonDeserializer.locationPoints> data) {

            List<jsonDeserializer.locationPoints> dateMatchingPoints = new List<jsonDeserializer.locationPoints>();
            
            //using a foreach instead of linq so the loop can break 
            foreach (var x in data) {
                if (UnixTimeStampToDateTime(x.Value.timestampMs).Date.ToShortDateString() == date.ToShortDateString()) {
                    dateMatchingPoints.Add(x.Value);
                
                //breaking if data has gone past the time trying to be matched
                }else if (UnixTimeStampToDateTime(x.Value.timestampMs).Date > date.Date) {
                    break;
                }
            }
            
            return dateMatchingPoints;
        }

        public static List<jsonDeserializer.locationPoints> haveWeMet(
            Dictionary<int,jsonDeserializer.locationPoints> person1,
            Dictionary<int,jsonDeserializer.locationPoints> person2,
            int minuteOffset,
            int meterOffset) {

            //the list that is being returned when all locationPoints are added to it
            List<jsonDeserializer.locationPoints> matchingPoints = new List<jsonDeserializer.locationPoints>();
            
            //creating the offsets
            int timeOffset = minuteOffset * 40000;
            int distanceOffset = meterOffset * 8000;

            int i = 0; //person 1 iterator 
            int j = 0; //person 2 iterator 
            while (true) {

                if ( Math.Abs(person1[person1.Count - 1 - i].timestampMs 
                              - person2[person2.Count - 1 - j].timestampMs) <= timeOffset
                    && Math.Abs(person1[person1.Count - 1 - i].latitudeE7  
                                - person2[person2.Count - 1 - i].latitudeE7 ) <= distanceOffset 
                    && Math.Abs(person1[person1.Count - 1 - i].longitudeE7 
                                - person2[person2.Count - 1 - i].longitudeE7) <= distanceOffset
                ) {
                    
                    matchingPoints.Add(person1[person1.Count - 1 - i]);
                    i++;
                    j++;
                    
                }
                //since google orders all data points by the timestamp, the algorithm has to catch up
                //the points of data of each person to be close if they are falling behind 
                else if (person1[person1.Count - 1 - i].timestampMs - timeOffset >
                        person2[person2.Count - 1 - j].timestampMs) {
                    i++;
                }else {
                    j++;
                }
               
                //checking to see with increase of any of the incrementing variables hit past the end
                //of the list and if so causes the loop to break
                if (person1.Count - 1 - i <= 0 || person2.Count - 1 - j <= 0)
                    break;
            }
            return matchingPoints;
        }
    }
}