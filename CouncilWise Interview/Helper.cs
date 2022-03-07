using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouncilWise
{
    public static class Helper
    {
        public const decimal TaxRate = 0.1m;

        public static decimal CurrencyRound(this decimal value)
        {
            return Math.Round(value, 2);
        }

        public static bool IsPalindrome(string word)
        {
            string reversedString = new string(word.Reverse().ToArray());
            return string.Compare(word, reversedString) == 0 ? true : false;
        }
    }
}
