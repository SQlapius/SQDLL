using System;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;

namespace medicijn.Utils
{
    public static class Dosering
    {

        public static void Test2(string dosering)
        {
            bool isDone = false;
            var eersteTeken = dosering[0];
            int? freq;
            string tijdseenheid;
            int aantal;
            string advieseenheid;
            string[] info;

            //for (int i = 0; ; i)
            while(!isDone)
            {

            }
        }

        public static void Test(string dosering)
        {
            var firstChar = dosering[0];
            string freq;
            string tijdseenheid;
            string aantal;
            string advieseenheid;
            string[] info;

            //if (firstChar != '-')
            //{
            // frequentie: Get all digets until 
            freq = firstChar != '-' ? GetCharactersUntilLetter(dosering) : "-";

                // remove frequentie from dosering
                dosering = RemoveSubstring(dosering, freq.ToString());

                //tijdenseenheid: volgende letters tot cijfer
                tijdseenheid = GetCharactersUntilDigit(dosering);

              
                dosering = RemoveSubstring(dosering, tijdseenheid);
                aantal = GetCharactersUntilLetter(dosering);
                dosering = dosering.TrimStart(aantal.ToString().ToCharArray());
                advieseenheid = GetCharactersUntilDigit(dosering);
                dosering = dosering.TrimStart(advieseenheid.ToString().ToCharArray());
                info = dosering.Trim().Split(' ');

                Debug.WriteLine(freq);
                Debug.WriteLine(tijdseenheid);
                Debug.WriteLine(aantal);
                Debug.WriteLine(advieseenheid);
                Debug.WriteLine(JsonConvert.SerializeObject(info));
            //}
        }


        private static string RemoveSubstring(string inputString, string removeString)
        {
            return inputString.TrimStart(removeString.ToCharArray());
        }

        private static string GetCharactersUntilDigit(string dosering)
        {
            return new string(dosering.TakeWhile(char.IsLetter).ToArray());
        }

        private static string GetCharactersUntilLetter(string dosering)
        {
            return new string(dosering.TakeWhile(c =>
                char.IsDigit(c) ||
                c == '-' ||
                c == '.'
            ).ToArray());
        }

        //private bool

        public static string ConvertCodeToString(
            int freq,
            string tijdseenheid,
            int aantal,
            string advieseenheid,
            params string[] info
        )
        {
            //var tijseenheidText = DBNull.getBla(tijdseenheid);

            //return freq + tijseenheidText + aantal + ;

            return "";
        }
    }
}
