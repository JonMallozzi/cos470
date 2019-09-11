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

           //variables to hold the statistic printed out after the computation 
           int dollarWordCount = 0;
           string highestValueWord = "";
           
           string shortestDollarWord = "tempString";
           string longestDollarWord = "";
           
           //hold the value of the largest word
           int maxCost = 0;
           
            
           //files must be in the same directory as Program.cs
           //and set up to be Idisposeable 
            using(FileStream readStream = File.OpenRead(@"words.txt")){
                using (StreamReader reader = new StreamReader(readStream)){
                    using (FileStream writeStream =
                        File.OpenWrite(@"DollarWords.txt")){
                        using (StreamWriter writer = new StreamWriter(writeStream)){

                            while (!reader.EndOfStream){
                               
                                string line = reader.ReadLine();
                                int total = 0;
                               
                                //if (line.Length >= 5){
                                  
                                  //making the string all lower case so I only have to worry about
                                  //lower case letters in my calculation
                                  string lowerLine = line.ToLower();

                                  foreach (var currentChar in lowerLine) {

                                    if (char.IsLetter(currentChar)){
                                        total +=  currentChar - 96;
                                    } 
                                }

                                  if (total > maxCost){
                                      maxCost = total;
                                      highestValueWord = line;
                                  }

                                if (total == 100){
                                    
                                    dollarWordCount++;
                                    writer.WriteLine(line);

                                    if (line.Length > longestDollarWord.Length){
                                        longestDollarWord = line;
                                    }else if (line.Length < shortestDollarWord.Length){
                                        shortestDollarWord = line;
                                    }       
                                }
                            //}
                          }
                       }
                   }
               }
           }

            watch.Stop();
            Console.WriteLine($"Total Execution Time: {watch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Number of dollar words: {dollarWordCount}");
            Console.WriteLine($"The most expensive word is {highestValueWord} which cost {maxCost}");
            Console.WriteLine($"Longest dollar word is {longestDollarWord}");
            Console.WriteLine($"Shortest dollar word is {shortestDollarWord}");
            
        }
    }
}

