using CsvHelper;
using FilePoller.Mappers;
using FilePoller.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FilePoller.Helpers
{
    public class FileHelper
    {
        public List<Order> ReadCSVFile(string location)
        {
            try
            {
                using (var reader = new StreamReader(location, Encoding.Default))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.RegisterClassMap<OrderMap>();
                    var records = csv.GetRecords<Order>().ToList();
                    return records;
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void WriteCSVFile(string path, List<Order> order)
        {
            using (StreamWriter streamWriter = new StreamWriter(path, false, new UTF8Encoding(true)))
            using (CsvWriter csvWriter = new CsvWriter(streamWriter))
            {
                csvWriter.WriteHeader<Order>();
                csvWriter.NextRecord();
                foreach (Order ord in order)
                {
                    csvWriter.WriteRecord<Order>(ord);
                    csvWriter.NextRecord();
                }
            }
        }
    }
}
