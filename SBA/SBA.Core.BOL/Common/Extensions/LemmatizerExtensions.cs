using SBA.BOL.Common.Factory;
using SBA.Core.BOL.Dictionaries;
using System.Collections.Generic;
using System.Linq;

namespace SBA.Core.BOL.Common.Extensions
{
    public static class LemmatizerExtensions
    {
        public static string[][] Lemmatize(this string[][] array)
        {
            var wordsLemmed = SimpleFactory.Get<List<List<string>>>();
            foreach (var firstDimension in array)
            {
                var list = SimpleFactory.Get<List<string>>();
                foreach (var secondDimension in firstDimension)
                    list.Add(WordVarietyDictionary.GetOriginalName(secondDimension));

                wordsLemmed.Add(list);
            }

            return wordsLemmed
                    .Select(x => x.ToArray())
                    .ToArray();
        }

        public static string[] Lemmatize(this string[] words)
        {
            var wordsWithoutStopWords = SimpleFactory.Get<List<string>>();
            foreach (var word in words)
                wordsWithoutStopWords.Add(WordVarietyDictionary.GetOriginalName(word));

            return wordsWithoutStopWords
                    .ToArray();
        }
    }
}