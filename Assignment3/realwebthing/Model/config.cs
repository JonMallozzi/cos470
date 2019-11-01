using Microsoft.Extensions.Configuration;

namespace realwebthing.Model {
    
    public class config {
        
        public  static IConfigurationRoot configJason(){
            
            var config = new ConfigurationBuilder()
                .AddJsonFile("Model/appsettings.json", false, true)
                .Build(); 
            
            return config; 
        }
    }
}