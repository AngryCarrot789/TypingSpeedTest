using System;
using System.IO;
using static System.Environment;
namespace TypingSpeedTest.Words
{
    public static class RandomWordGenerator
    {
        private static string[] EnglishWords { get; set; }
        private static Random RandomNumbers { get; set; }
        private static bool EnglishWordsAvaliable { get; set; }
        static RandomWordGenerator()
        {
            RandomNumbers = new Random();
            EnglishWordsAvaliable = true;
            string docsWordListPath = Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "Words", "EnglishWords.txt");
            string localWordListPath = "../../Words/EnglishWords.txt";
            if (!File.Exists(docsWordListPath))
            {
                if (File.Exists(localWordListPath))
                {
                    string newWordListPath = Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "Words", "EnglishWords.txt");
                    Directory.CreateDirectory(Path.GetDirectoryName(newWordListPath));
                    File.Copy(localWordListPath, newWordListPath);
                    LoadWords(newWordListPath);
                }
                else EnglishWordsAvaliable = false;
            }
            else LoadWords(docsWordListPath);
        }

        private static void LoadWords(string pathOfWords)
        {
            EnglishWords = File.ReadAllLines(pathOfWords);
        }

        public static string RandomWord()
        {
            if (EnglishWordsAvaliable)
                return EnglishWords[RandomNumbers.Next(0, EnglishWords.Length - 1)].ToLower();
            else
                return NumberToWords(RandomNumbers.Next(0, 300000)).ToLower();
        }

        // Just in case
        private static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                string[] unitsMap = new string[]
                {
                    "zero", "one", "two", "three", 
                    "four", "five", "six", "seven",
                    "eight", "nine", "ten", "eleven", 
                    "twelve", "thirteen", "fourteen", 
                    "fifteen", "sixteen", "seventeen", 
                    "eighteen", "nineteen"
                };
                string[] tensMap = new string[] 
                { 
                    "zero", "ten", "twenty", 
                    "thirty", "forty", "fifty", 
                    "sixty", "seventy", "eighty", "ninety" 
                };

                if (number < 20) 
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
    }
}
