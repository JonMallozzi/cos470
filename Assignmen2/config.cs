using Microsoft.Extensions.Configuration;

namespace Assignment2 {
    
    public class config {
        
        public  IConfigurationRoot configJason(){
            
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build(); 
            
            return config; 
        }
    }
}