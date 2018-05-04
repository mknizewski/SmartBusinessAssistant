using SBA.BOL.Common.Factory;
using SBA.Core.BOL.Dictionaries;
using System.Collections.Generic;
using System.Linq;

namespace SBA.Core.BOL.Common.Extensions
{
    public static class StopWordExtensions
    {
        public static string[] ExcludeStopWords(this string[] words, StopWordLanguage language)
        {
            var wordsWithoutStopWords = SimpleFactory.Get<List<string>>();
            var stopWords =
                GetStopWordsByLanguague(language)
                    .Split(new char[] { ',' })
                    .ToList();

            foreach (var word in words)
            {
                if (!stopWords.Contains(word.ToLower()))
                    wordsWithoutStopWords.Add(word);
            }

            return wordsWithoutStopWords
                    .ToArray();
        }

        private static string GetStopWordsByLanguague(StopWordLanguage language)
        {
            switch (language)
            {
                case StopWordLanguage.Polish:
                    return StopWords.Polish;
                default:
                    return StopWords.Polish;
            }
        }
    }

    public enum StopWordLanguage
    {
        Polish
    }
}