using System;
using System.Collections.Generic;

namespace GuildComm.Common.Extensions
{
    public static class StringExtensions
    {
        public static string CapitalizeFirstLetter(this string text)
        {
            var separateWords = new List<string>();

            if (text != null)
            {
                var words = text.Split();

                foreach (var word in words)
                {
                    if (word.Length > 0)
                    {
                        var newWord = string.Concat(word[0].ToString().ToUpper(), word.AsSpan(1));
                        separateWords.Add(newWord);
                    }
                }
            }

            return string.Join(" ", separateWords);
        }
    }
}
