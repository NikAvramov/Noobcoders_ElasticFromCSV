using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

internal class DataFile
{
  public static List<TestText> ReadFile(string path)
  {
    using var reader = new StreamReader(path);
    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
    var records = csv.GetRecords<TestText>();
    return records.ToList();
  }
}

