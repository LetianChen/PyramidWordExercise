using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace PyramidWord
{
    /// <summary>
    /// Summary description for PyramidWordWebService
    /// </summary>
    [WebService(Namespace = "LetianChen/FetchRewards/PyramidWord")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class PyramidWordWebService : System.Web.Services.WebService
    {
        [WebMethod(Description = "Checks whether a word is a pyramid word. " +
            "A word is a ‘pyramid’ word if you can arrange the letters in increasing frequency, " +
            "starting with 1 and continuing without gaps and without duplicates. Only words consisting of " +
            "all english letters can be pyramid words. " +
            "The method takes one string as input and returns a boolean.")]
        public bool IsPyramidWord(string input)
        {
            // input validation - only process input if its purely letter
            if (!input.All(char.IsLetter))
            {
                return false;
            }
            // input validation - letter needs to be at least 3 characters long
            if (input.Length < 3)
            {
                return false;
            }
            // count the occurrence of each letter in the string
            Dictionary<char, int> letterCount = new Dictionary<char, int>();
            foreach (char letter in input)
            {
                if (letterCount.ContainsKey(letter))
                {
                    letterCount[letter]++;
                }
                else
                {
                    letterCount[letter] = 1;
                }
            }
            // set up hasCount array
            bool[] hasCount = new bool[52]; //array to check if each count exists. maximum supported count is 52 (52 letters a-zA-Z).
            foreach (char letter in letterCount.Keys)
            {
                int charCount = letterCount[letter] - 1;
                if (charCount >= 52)   //too many to fit in the pyramid
                {
                    return false;
                }
                else if (hasCount[charCount])  //duplicate count - no two levels can have the same number
                {
                    return false;
                }
                else
                {
                    hasCount[charCount] = true;
                }
            }
            // check hasCount array to see if a pyramid is formed
            bool hasEmptySpot = false;
            for (int i = 0; i < 52; i++)
            {
                if (hasCount[i])
                {
                    if (hasEmptySpot)
                    {
                        return false;
                    }
                }
                else
                {
                    hasEmptySpot = true;
                }
            }

            return true;
        }
    }
}
