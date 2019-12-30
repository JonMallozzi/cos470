using Microsoft.AspNetCore.Mvc; 
using System.Collections.Generic; 
using System.Linq; 
namespace backendWebAPI {
    
    [Route("api/[controller]")]
    [ApiController]
    public class apiController : ControllerBase {
        
        
        [HttpGet("/get")]
        public ActionResult<List<string>> GetTest(){
    
            List<string> results = new List<string>();   
            results.Add("hello jon");

            return results;
        }
        
    }
}