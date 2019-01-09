using MedicalCertificates.Service.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MedicalCertificates.Service.CommonServices
{
    public class StringConverterService : IStringConverterService
    {
        private readonly Dictionary<string, string> russianEnglishValuePairs;

        public StringConverterService()
        {
            russianEnglishValuePairs = new Dictionary<string, string>()
            {
                {"а", "a"},
                {"б", "b" },
                {"в", "v"},
                {"г", "g"},
                {"д", "d"},
                {"е", "e"},
                {"ё", "yo"},
                {"ж", "zh"},
                {"з", "z"},
                {"и", "i"},
                {"й", "j"},
                {"к", "k"},
                {"л", "l"},
                {"м", "m"},
                {"н", "n"},
                {"о", "o"},
                {"п", "p"},
                {"р", "r"},
                {"с", "s"},
                {"т", "t"},
                {"у", "u"},
                {"ф", "f"},
                {"х", "h"},
                {"ц", "c"},
                {"ч", "ch"},
                {"ш", "sh"},
                {"щ", "sch"},
                {"ъ", "j"},
                {"ы", "i"},
                {"ь", "j"},
                { "э", "e"},
                { "ю", "yu"},
                { "я", "ya"},
                { "А", "A"},
                { "Б", "B"},
                { "В", "V"},
                { "Г", "G"},
                { "Д", "D"},
                { "Е", "E"},
                { "Ё", "Yo"},
                { "Ж", "Zh"},
                { "З", "Z"},
                { "И", "I"},
                { "Й", "J"},
                { "К", "K"},
                { "Л", "L"},
                { "М", "M"},
                { "Н", "N"},
                { "О", "O"},
                { "П", "P"},
                { "Р", "R"},
                { "С", "S"},
                { "Т", "T"},
                { "У", "U"},
                { "Ф", "F"},
                { "Х", "H"},
                { "Ц", "C"},
                { "Ч", "Ch"},
                { "Ш", "Sh"},
                { "Щ", "Sch"},
                { "Ъ", "J"},
                { "Ы", "I"},
                { "Ь", "J"},
                { "Э", "E"},
                { "Ю", "Yu"},
                { "Я", "Ya"},
            };
 
        }

        public string ConvertFromEnglishToRussian(string english)
        {
            string russian = english;
            foreach (KeyValuePair<string, string> pair in russianEnglishValuePairs)
            {
                russian = russian.Replace(pair.Key, pair.Value);
            }
            return russian;
        }

        public string ConvertFromRussianToEnglish(string russian)
        {
            string english = russian;
            foreach (KeyValuePair<string, string> pair in russianEnglishValuePairs)
            {
                english = english.Replace(pair.Key, pair.Value);
            }
            return english;
        }

        public string ConvertToUsername(string invalidUsername)
        {
            string validUsername = Regex.Replace(invalidUsername, "[^A-Za-z0-9_-]", "");

            return validUsername;
        }
    }
}
