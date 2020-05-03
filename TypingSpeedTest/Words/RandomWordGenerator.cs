using System;
using System.IO;
using static System.Environment;
namespace TypingSpeedTest.Words
{
    public static class RandomWordGenerator
    {
        private static readonly Random RandomNumbers;
        static RandomWordGenerator()
        {
            RandomNumbers = new Random();
        }

        public static string RandomWord()
        {
            string path = Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "EnglishWords.txt");
            string[] lines = File.ReadAllLines(path);
            return lines[RandomNumbers.Next(0, lines.Length - 1)].ToLower();
        }
    }
}
