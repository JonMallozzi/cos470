using NUnit.Framework;
using Assignment2; 


namespace Test {
    
    public static class Test {

        [Test]
        public static void correctWordCost() {
            Assert.AreEqual(71,Program.wordCost("High","St"));
        } 
        
        [Test]
        public static void correctCharCost() {
           Assert.AreEqual(2,Program.wordCost("a","a"));
        }

        

        
    }
}