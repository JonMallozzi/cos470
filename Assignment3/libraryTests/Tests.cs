using System;
using NUnit.Framework;
using Assignment3;
 
namespace libraryTests {
    
    //test jason data 
    public class testJson {

        public static string data1 = @"{
    ""locations"" : [ {
    ""timestampMs"" : ""1387305535688"",
    ""latitudeE7"" : 435911812,
    ""longitudeE7"" : -703347666,
    ""accuracy"" : 70
  }, {
    ""timestampMs"" : ""1387305539616"",
    ""latitudeE7"" : 435911812,
    ""longitudeE7"" : -703347666,
    ""accuracy"" : 70,
    ""activity"" : [ {
      ""timestampMs"" : ""1387305539751"",
      ""activity"" : [ {
        ""type"" : ""STILL"",
        ""confidence"" : 92
      }, {
        ""type"" : ""UNKNOWN"",
        ""confidence"" : 7
      } ]
    } ]
  }, {
   ""timestampMs"" : ""1387305599650"",
    ""latitudeE7"" : 435912349,
    ""longitudeE7"" : -703348623,
    ""accuracy"" : 62
  }, {
    ""timestampMs"" : ""1387305660738"",
    ""latitudeE7"" : 435916775,
    ""longitudeE7"" : -703355085,
    ""accuracy"" : 33,
    ""activity"" : [ {
      ""timestampMs"" : ""1387305664595"",
      ""activity"" : [ {
        ""type"" : ""STILL"",
        ""confidence"" : 61
      }, {
        ""type"" : ""IN_VEHICLE"",
        ""confidence"" : 38
      } ]
    } ]
  }
]}";
        
   public static string data2 = @"{
    ""locations"" : [ {
    ""timestampMs"" : ""1387305535688"",
    ""latitudeE7"" : 435911812,
    ""longitudeE7"" : -703347666,
    ""accuracy"" : 70
  }, {
    ""timestampMs"" : ""1387305539616"",
    ""latitudeE7"" : 435911812,
    ""longitudeE7"" : -703347666,
    ""accuracy"" : 70,
    ""activity"" : [ {
      ""timestampMs"" : ""1387305539751"",
      ""activity"" : [ {
        ""type"" : ""STILL"",
        ""confidence"" : 92
      }, {
        ""type"" : ""UNKNOWN"",
        ""confidence"" : 7
      } ]
    } ]
  }, {
    ""timestampMs"" : ""1570112485493"",
    ""latitudeE7"" : 436624068,
    ""longitudeE7"" : -702774227,
    ""accuracy"" : 14,
    ""altitude"" : -5,
    ""verticalAccuracy"" : 6
  }
]}";    
        
  }

    [TestFixture]
    public class Tests {
       
        [Test]
        public void dateConvertionTest() {      
            Assert.AreEqual(metCheckingLogic.UnixTimeStampToDateTime(1387305535688), 
                new DateTime(2013,12,17,18,38,55,688));
        }

        [Test]
        public void jasonDeserailizeTest() {
            Assert.AreEqual(jsonDeserializer.deserializing(testJson.data1).locations[0].latitudeE7,435911812);
        }

        [Test]
        public void dateMatchingtTest() {
          Assert.AreEqual(metCheckingLogic.wherewasIOn(new DateTime(2013,12,17,18,38,55,688), 
              jsonDeserializer.deserializing(testJson.data1))[3].latitudeE7,
            435916775 );
        }

        [Test]
        public void haveWeMetTest() {
          Assert.AreEqual(metCheckingLogic.haveWeMet(jsonDeserializer.deserializing(testJson.data1),
            jsonDeserializer.deserializing(testJson.data2))[1].timestampMs,1387305535688);
        }
        
        
    }
}