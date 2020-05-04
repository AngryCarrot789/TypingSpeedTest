using System;
using System.IO;
using static System.Environment;
namespace TypingSpeedTest.Words
{
    public static class RandomWordGenerator
    {
        private static string[] EnglishWords { get; set; }
        private static Random RandomNumbers { get; set; }
        static RandomWordGenerator()
        {
            RandomNumbers = new Random();
            EnglishWords = 
                File.ReadAllLines(
                    Path.Combine(
                        GetFolderPath(SpecialFolder.MyDocuments),
                        "PLACE_IN_YOUR_DOCUMENTS",
                        "EnglishWords.txt"));
        }

        public static string RandomWord()
        {
            return EnglishWords[RandomNumbers.Next(0, EnglishWords.Length - 1)].ToLower();
        }
    }
}
