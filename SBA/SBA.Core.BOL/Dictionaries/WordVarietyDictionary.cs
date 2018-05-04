using SBA.Core.BOL.Infrastructure;
using SBA.DAL.Context.InferenceDb.Entity;
using SBA.DAL.Context.InferenceDb.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SBA.Core.BOL.Dictionaries
{
    public class WordVarietyDictionary
    {
        public void SeedData(SbaInferenceContext context)
        {
            if (context.WordVarieties.Any())
                return;

            var variety = SimpleFactory.Get<List<Words>>();
            string fileName = Path.Combine(Environment.CurrentDirectory, Settings.Core.VarietyFileName);
            using (var streamReader = SimpleFactory.Get<StreamReader>(new FileStream(fileName, FileMode.Open, FileAccess.Read)))
            {
                while (streamReader.Peek() >= 0)
                {
                    string[] splited = streamReader.ReadLine().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    if (splited.Length == 1)
                        variety.Add(new Words
                        {
                            OrginalName = splited[0].ToLower().Replace(" ", string.Empty),
                            Varieties = new string[0]
                        });
                    else
                    {
                        string[] varieties = splited
                            .Skip(1)
                            .Select(x => x.ToLower())
                            .ToArray();

                        variety.Add(new Words
                        {
                            OrginalName = splited[0].ToLower().Replace(" ", string.Empty),
                            Varieties = varieties
                        });
                    }
                }
            }

            int saveChangesIterator = 0;
            int rebuildContextIterator = 0;
            context.Configuration.AutoDetectChangesEnabled = false;
            context.Configuration.ValidateOnSaveEnabled = false;

            foreach (var word in variety)
            {
                if (rebuildContextIterator == 1000)
                {
                    context = new SbaInferenceContext();
                    rebuildContextIterator = 0;
                    context.Configuration.AutoDetectChangesEnabled = false;
                    context.Configuration.ValidateOnSaveEnabled = false;
                }

                var orginalWord = context.WordVarieties
                    .FirstOrDefault(x => x.Word == word.OrginalName);

                if (orginalWord == null)
                {
                    orginalWord = new WordVariety
                    {
                        Word = word.OrginalName
                    };

                    context.WordVarieties.Add(orginalWord);
                    context.SaveChanges();
                    rebuildContextIterator++;
                }

                foreach (var varieties in word.Varieties)
                {
                    if (saveChangesIterator == 100)
                    {
                        context.SaveChanges();
                        saveChangesIterator = 0;
                    }

                    var dbVariety = context.WordVarieties
                        .FirstOrDefault(x => x.Word == varieties);

                    if (dbVariety != null)
                        continue;

                    context.WordVarieties.Add(new WordVariety
                    {
                        Word = varieties,
                        OrginalWordId = orginalWord.Id
                    });
                    saveChangesIterator++;
                    rebuildContextIterator++;
                }
            }

            context.SaveChanges();
        }

        public class Words
        {
            public string OrginalName { get; set; }
            public string[] Varieties { get; set; }
        }
    }
}