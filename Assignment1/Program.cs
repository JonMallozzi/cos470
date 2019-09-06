/***************
* Jon Mallozzi *
* 9/05/19      *
* COS 470      *
* Assignment1  *
****************/

using System;
using System.IO;

namespace Assignment1{
    class Program{

        static void Main(string[] args){
            
            //using a StopWatch to see how long it takes for my algorithm to run
           var watch = System.Diagnostics.Stopwatch.StartNew();
            
            using(FileStream readStream = File.OpenRead(@"/Users/blaze/becoming the code god/cos470/Assignment1/words.txt")){
                using (StreamReader reader = new StreamReader(readStream)){
                    using (FileStream writeStream =
                        File.OpenWrite(@"/Users/blaze/becoming the code god/cos470/Assignment1/DollarWords.txt")){
                        using (StreamWriter writer = new StreamWriter(writeStream)){

                            while (!reader.EndOfStream){
                               
                                string line = reader.ReadLine();
                                int total = 0;
                               
                                if (line.Length >= 5){
                                  
                                  //making the string all lower case so I only have to worry about
                                  //lower case letters in my calculation
                                  string lowerLine = line.ToLower();

                                  foreach (var currentChar in lowerLine) {

                                    if (char.IsLetter(currentChar)){
                                        total += (int) currentChar - 96;
                                    } 

                                    if (total > 100){
                                         break;   
                                    }
                                }

                                if (total == 100){
                                    writer.WriteLine(line);
                                }
                            }
                          }
                       }
                   }
               }
           }

            watch.Stop();
            Console.WriteLine($"Total Execution Time: {watch.ElapsedMilliseconds} ms");
            
        }
    }
}