/**********************
* Jon Mallozzi        *
* 10/30/19             *
* COS 470             *
* Assignment 3 Part 3 *
***********************/

using System;
using System.Collections.Generic;
using System.Linq;
using Assignment3;
using Microsoft.AspNetCore.Mvc;
using realwebthing.Model;

namespace realwebthing.Controllers {
    
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {
        
       static Dictionary<int,jsonDeserializer.locationPoints> DataLayer 
           = new Dictionary<int,jsonDeserializer.locationPoints>(); 

       static Dictionary<int, jsonDeserializer.locationPoints> localData = jsonData.loadJsonFile();

        // GET api/values
        [HttpGet]
        public ActionResult<Dictionary<int,jsonDeserializer.locationPoints>> Get() {
            return DataLayer;
        }
        
        // GET api/values/5
       [HttpGet("{id}")]
        public ActionResult<jsonDeserializer.locationPoints> Get(int id) {
            
            if (DataLayer.ContainsKey(id)) {
                return DataLayer[id];
            }
                return new NotFoundResult();
            
        }
        
        // GET api/values/whereWasI/{variable}/{time}
        //variable is weather local or userdata is being checked
        [HttpGet("whereWasI/{variable}/{time}")]
        public ActionResult<List<jsonDeserializer.locationPoints>> Get(string variable, DateTime time) {
            
            List<jsonDeserializer.locationPoints> dateMatchingPoints = new List<jsonDeserializer.locationPoints>();

            if (variable.Equals("localData")) {
                dateMatchingPoints = metCheckingLogic.wherewasIOn(time, localData);
            }else if(DataLayer.Count != 0){
                dateMatchingPoints = metCheckingLogic.wherewasIOn(time, DataLayer);
            } 
                
            if (dateMatchingPoints != null) {
                return dateMatchingPoints;
            }
               return new NotFoundResult();
        }
        
        // GET api/values/haveWeMet/{minute}/{meter}
        [HttpGet("haveWeMet/{minute}/{meter}")]
        public ActionResult<List<jsonDeserializer.locationPoints>> GetHaveWeMet(int minute, int meter) {

            if (DataLayer.Count == 0 || localData.Count == 0) 
                return new NotFoundResult();
              
          List<jsonDeserializer.locationPoints> matchingPoints =  metCheckingLogic.haveWeMet(
                        localData, DataLayer, minute, meter );
                
            if (matchingPoints != null) {
                return matchingPoints;
            }
            return new NotFoundResult();
        }
        
        // POST api/values
        [HttpPost]
        public ActionResult<string> Post([FromBody] jsonDeserializer.locationData value) {
           int index = 0;
           DataLayer = value.locations.ToDictionary(id => index++, dataPoint => dataPoint);
           return "data sent and received";            
        }
        
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] jsonDeserializer.locationData value) {
            DataLayer[id] = value.locations[0]; //the array index will always be 0 because its one point 

        }

        // DELETE api/values/all    
        [HttpDelete("all")]
        public void DeleteAll() {
             DataLayer.Clear();     
        }
    
        //DELETE api/values/id
        [HttpDelete("{id}")]
        public void DeleteLocationPoint(int id) {
            DataLayer.Remove(id);
        }
    }
}