using Nest;
using System.Collections.Generic;
using System.Linq;
using System.Text;

internal static class Elastic
{
  public static ElasticClient client;
  public static string CloudID { get; set; }
  public static string UserName { get; set; }
  public static string Password { get; set; }
  public static string IndexName { get; set; }

  public static void Authorization()
  {
    client = new ElasticClient(CloudID, new Elasticsearch.Net.BasicAuthenticationCredentials(UserName, Password));
  }
  public static void CreateIndexResponse()//создать индекс
  {
    var createIndexResp = client.Indices.Create(IndexName, x => x.Map<TestText>(m => m.AutoMap()));
  }
  public static void CreateDocumentResponse(List<TestText> records)//проиндексировать файл csv
  {
    var createDocumentResponse = client.Bulk(b => b.CreateMany(records).Index(IndexName));
  }
  public static string SearchTextSnippet(string text)//полнотекстовый поиск
  {
    var searchResponse = client.Search<TestText>(s => s.Index(Elastic.IndexName).Size(20)
      .Query(q => q.Match(m => m.Field(f => f.text).Query(text))));
    var output = searchResponse.Hits.OrderBy(x => x.Source.created_date);
    StringBuilder sb = new();
    foreach (var record in output)
    {
      sb.Append($"Id: {record.Id}. Дата: {record.Source.created_date}\nРубрика:{record.Source.rubrics}\n{record.Source.text}\n\n\n============================================\n");
    }
    string result =  sb.Length == 0 ? "Совпадений не найдено" : sb.ToString();
    return result;
  }
  public static void DeleteDocumentWithID(string id)
  {
    var deleteDocument = client.Delete<TestText>(id, x => x.Index(IndexName));
  }
  public static void DeleteDocument()
  {
    var deleteDocument = client.Indices.Delete(IndexName);
  }
}
