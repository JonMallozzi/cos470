/***************
* Jon Mallozzi *
* 9/05/19      *
* COS 470      *
* Assignment1  *
****************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment1{
    class Program{
        
        //A function to open the file and give a list of words from the file
        public static List<string> createWordList(string filePath) {
          using (FileStream readStream = File.OpenRead(filePath)) {
              using (StreamReader reader = new StreamReader(readStream)) {
                  return reader.ReadToEnd().Split().ToList();
              }
          }
        }

        public static int wordCost(string word) {
            return word.ToLower().Where(char.IsLetter).Sum(n => n - 96);
        }

        public static List<string> dollarWords(List<string> words) {
          return words.Where((a => wordCost(a) == 100 )).ToList();
        }

        //writes to DollarWords.txt in same directory as the program
        public static void writeDollarWordsToFile(List<string> dollarWord) {
            using (FileStream writeStream =
                File.OpenWrite(@"DollarWords.txt")) {
                using (StreamWriter writer = new StreamWriter(writeStream)) {
                    writer.WriteLine(string.Join(Environment.NewLine , dollarWord));
                }
            }
        }

        public static string highestCost(List<string> words) {
            return words.Aggregate("", (maxWord, currWord) => wordCost(maxWord) > wordCost(currWord) ? maxWord : currWord);
        }

        //one function to find the longest word so that code can be reused
        public static string longestWord(List<string> words) {
            return words.OrderBy(n => n.Length).LastOrDefault();
        }

        public static string shortestWord(List<string> words) {         
           return words.OrderBy(n => n.Length).FirstOrDefault();
        }

        public static string allWordsOfShortestLen(List<string> words) {
            return string.Join(", ", words.Where(w => w.Length == shortestWord(words).Length));
        }
        
        public static string allWordsOfLongesttLen(List<string> words) {
            return string.Join(", ", words.Where(w => w.Length == longestWord(words).Length));
        }
        

        static void Main(string[] args){
            
         //using a StopWatch to see how long it takes for my algorithm to run
         var watch = System.Diagnostics.Stopwatch.StartNew();
           
         List<string> words = createWordList(@"words.txt");
         List<string> dollarWord = dollarWords(words);
         writeDollarWordsToFile(dollarWord);
         Console.WriteLine($"Number of dollar words: {dollarWord.Count}");
         Console.WriteLine($"Longest  word from input file is {longestWord(words)}");
         
         //Storing the most expensive word to cut out a pass on the words file
         string maxCost = highestCost(words); 
         
         Console.WriteLine($"The most expensive word {maxCost} which cost {wordCost(maxCost)}");
         Console.WriteLine($"Longest dollar word is {longestWord(dollarWord)}");
         Console.WriteLine($"Shortest dollar word is {shortestWord(dollarWord)}");
         Console.WriteLine($"All dollar words of shortest length {allWordsOfShortestLen(dollarWord)}");
         Console.WriteLine($"All dollar words of longest length {allWordsOfLongesttLen(dollarWord)}"); 
         
         watch.Stop();
         Console.WriteLine($"Total Execution Time: {watch.ElapsedMilliseconds} ms");

        }
    }
}

