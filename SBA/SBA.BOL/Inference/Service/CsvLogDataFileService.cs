using SBA.BOL.Common.Factory;
using System;
using System.Collections.Generic;
using System.IO;

namespace SBA.BOL.Inference.Service
{
    public interface ICsvLogDataFileService
    {
        CsvDataFile GetCsv(string csvPath);
        void SaveCsv(CsvDataFile csvDataFile, string csvPath);
    }

    public class CsvLogDataFileService : ICsvLogDataFileService
    {
        private static readonly object _lockObject = new object();

        public CsvDataFile GetCsv(string csvPath)
        {
            var csvRows = SimpleFactory.Get<List<CsvDataFile.CsvDataFileRow>>();
            lock (_lockObject)
            {
                if (!File.Exists(csvPath))
                    return new CsvDataFile { Rows = csvRows };

                using (var streamReader = SimpleFactory.Get<StreamReader>(SimpleFactory.Get<FileStream>(csvPath, FileMode.Open, FileAccess.Read)))
                {
                    while (streamReader.Peek() >= 0)
                    {
                        string[] splited = streamReader.ReadLine().Split(new char[] { ',' });
                        csvRows.Add(new CsvDataFile.CsvDataFileRow
                        {
                            SessionId = Convert.ToInt32(splited[0]),
                            UrlId = Convert.ToInt32(splited[1]),
                            IsVisited = Convert.ToBoolean(Convert.ToInt32(splited[2]))
                        });
                    }
                }
            }

            return new CsvDataFile { Rows = csvRows };
        }

        public void SaveCsv(CsvDataFile csvDataFile, string csvPath)
        {
            lock (_lockObject)
            {
                using (var streamWriter = SimpleFactory.Get<StreamWriter>(SimpleFactory.Get<FileStream>(csvPath, FileMode.Create, FileAccess.Write)))
                    csvDataFile.Rows.ForEach(row => streamWriter.WriteLine($"{row.SessionId},{row.UrlId},{Convert.ToInt32(row.IsVisited)}"));
            }
        }
    }

    public class CsvDataFile
    {
        public List<CsvDataFileRow> Rows { get; set; }
        public bool IsEmpty => Rows.Count == 0;

        public class CsvDataFileRow
        {
            public int SessionId { get; set; }
            public int UrlId { get; set; }
            public bool IsVisited { get; set; }
        }
    }
}