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
        string GetTempCsvWithUserLogs(List<CsvDataFile.CsvDataFileRow> csvDataFileRows, string csvPath);
        void DeleteTempCsv(string tempCsvFile);
    }

    public class CsvLogDataFileService : ICsvLogDataFileService
    {
        private static readonly object _lockObject = new object();

        public void DeleteTempCsv(string tempCsvFile)
        {
            if (File.Exists(tempCsvFile))
                File.Delete(tempCsvFile);
        }

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
                            VisitCount = Convert.ToDouble(splited[2])
                        });
                    }
                }
            }

            return new CsvDataFile { Rows = csvRows };
        }

        public string GetTempCsvWithUserLogs(List<CsvDataFile.CsvDataFileRow> csvDataFileRows, string csvPath)
        {
            string tempCsvFile = Path.GetTempPath() + Guid.NewGuid().ToString() + ".csv";
            var csvFile = GetCsv(csvPath);

            csvFile.Rows.AddRange(csvDataFileRows);
            SaveCsv(csvFile, tempCsvFile);

            return tempCsvFile;
        }

        public void SaveCsv(CsvDataFile csvDataFile, string csvPath)
        {
            lock (_lockObject)
            {
                using (var streamWriter = SimpleFactory.Get<StreamWriter>(SimpleFactory.Get<FileStream>(csvPath, FileMode.Create, FileAccess.Write)))
                    csvDataFile.Rows.ForEach(row => streamWriter.WriteLine($"{row.SessionId},{row.UrlId},{row.VisitCount}"));
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
            public double VisitCount { get; set; }
        }
    }
}