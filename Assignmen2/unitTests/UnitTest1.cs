using NUnit.Framework;
using Assignment2; 


namespace Test {
    
    public static class Test {

        [Test]
        public static void correctCharCost() {
            Assert.AreEqual(2,Program.wordCost("a","a"));
        }
        
        [Test]
        public static void correctWordCost() {
            Assert.AreEqual(71,Program.wordCost("High","St"));
        } 
        
        //setup to fail to make sure 
        [Test]
        public static void correctWordCostFail() {
            Assert.AreEqual(71,Program.wordCost("Hig","St"));
        } 
        
        //testing it does not count non letter characters
        [Test]
        public static void specialCharWordCost() {
            Assert.AreEqual(71,Program.wordCost("High85832901&*@3","St"));
        } 
    }
}