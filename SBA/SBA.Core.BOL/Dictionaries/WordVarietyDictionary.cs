using SBA.Core.BOL.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SBA.Core.BOL.Dictionaries
{
    public static class WordVarietyDictionary
    {
        private static List<Words> _variety;

        static WordVarietyDictionary() => 
            _variety = SimpleFactory.Get<List<Words>>();

        public static void LoadData()
        {
            var logManager = SimpleFactory.GetLogger();
            string fileName = Path.Combine(Environment.CurrentDirectory, Settings.Core.VarietyFileName);

            logManager.RegisterLogToConsole($"Loading words variety from {Settings.Core.VarietyFileName}");
            logManager.RegisterLogToFile($"Loading words variety from {Settings.Core.VarietyFileName}");

            var task = Task.Run(() => 
            {
                using (var streamReader = SimpleFactory.Get<StreamReader>(new FileStream(fileName, FileMode.Open, FileAccess.Read)))
                {
                    while (streamReader.Peek() >= 0)
                    {
                        string[] splited = streamReader.ReadLine().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        if (splited.Length == 1)
                            _variety.Add(new Words
                            {
                                OrginalName = splited[0].ToLower(),
                                Varieties = new string[0]
                            });
                        else
                        {
                            string[] varieties = splited
                                .Skip(1)
                                .Select(x => x.ToLower())
                                .ToArray();

                            _variety.Add(new Words
                            {
                                OrginalName = splited[0].ToLower(),
                                Varieties = varieties
                            });
                        }
                    }
                }
            });
            while (!task.IsCompleted) { Settings.ProcessingScreen(); }

            logManager.RegisterLogToConsole("Loading words variety completed.");
            logManager.RegisterLogToFile("Loading words variety completed.");
        }

        public static string GetOriginalName(string token)
        {
            var wordFoundInVariety = _variety
                .AsParallel()
                .Where(x => x.Varieties.Contains(token))
                .FirstOrDefault();

            if (wordFoundInVariety != default(Words))
                return wordFoundInVariety.OrginalName;

            return token;
        }

        public class Words
        {
            public string OrginalName { get; set; }
            public string[] Varieties { get; set; }
        }
    }
}