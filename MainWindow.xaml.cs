using System.Windows;

namespace Noobcoders_ElasticFromCSV
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private void Autorization_Click(object sender, RoutedEventArgs e)
    {
      Elastic.CloudID = CloudID.Text;
      Elastic.UserName = UserName.Text;
      Elastic.Password = Password.Text;
      //это я оставил и закомментировал для быстрой и частой авторизации чтобы не искать и копировать постоянно данные для нее)
      //Elastic.CloudID = "NoobCoders_NikAvramov:ZXVyb3BlLXdlc3QzLmdjcC5jbG91ZC5lcy5pbyQ4ODg2NWZiODRkNDU0MDQxOTI1MWIwNDg0NDk3NDI3NiQxNTMzYWE2NTkzZGI0M2RmOGM1ZjMwYTg5NTE1MDI1Yg==";
      //Elastic.UserName = "elastic";
      //Elastic.Password = "OBq6Glj0pKt8BL2kQF0kVpR5";
      Elastic.Authorization();
    }

    private void Index_Click(object sender, RoutedEventArgs e)
    {
      Elastic.IndexName = "tex_index_1";
      Elastic.CreateIndexResponse();
      var records = DataFile.ReadFile(Path.Text);
      Elastic.CreateDocumentResponse(records);
    }

    private void Search_Click(object sender, RoutedEventArgs e)
    {
      Output.Clear();
      string textRequest = SearchInput.Text;
      Output.Text = Elastic.SearchTextSnippet(textRequest);
    }

    private void Remove_Click(object sender, RoutedEventArgs e)
    {
      Elastic.IndexName = "tex_index_1";
      Elastic.DeleteDocumentWithID(ID.Text);
    }

    private void RemoveAll_Click(object sender, RoutedEventArgs e)
    {
      Elastic.IndexName = "tex_index_1";
      Elastic.DeleteDocument();
    }
  }
}
