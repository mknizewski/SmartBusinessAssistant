using SBA.BOL.Common.Factory;
using SBA.BOL.Inference.Service;
using System.Collections.Generic;

namespace SBA.Core.BOL.Common.Extensions
{
    public static class LemmatizerExtensions
    {
        public static string[] Lemmatize(this string[] words)
        {
            var lemmatizerService = SimpleFactory.Get<WordVarietyService, IWordVarietyService>();
            var wordsWithoutStopWords = SimpleFactory.Get<List<string>>();
            foreach (var word in words)
                wordsWithoutStopWords.Add(lemmatizerService.Lemmatize(word));

            return wordsWithoutStopWords
                    .ToArray();
        }
    }
}