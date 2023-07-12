using HtmlAgilityPack;
using System.Net;

public class ScrappingService
{
  private readonly string _baseUrl;

  public ScrappingService(string baseUrl){
    this._baseUrl = baseUrl;
  }

  private async Task<string> callUrl(string url)
  {
    HttpClient client = new HttpClient();
    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
    client.DefaultRequestHeaders.Accept.Clear();

    // TODO: use Uri API
    var response = client.GetStringAsync(this._baseUrl + url);
    return await response;
  }

  private string getInnerText(HtmlNode node, string xpath){
    return node.SelectSingleNode(xpath).InnerText.ToString();
  }

  private int parseJobsCount(string rawJobCounts){
    // TODO: check btter way to do this
    return int.Parse(
      rawJobCounts
        .Replace("resultados", "")
        .Replace(",", "")
        .Trim()
    );

  }

  async public Task<int> GetResults(string companyName)
  {
    HtmlDocument htmlDoc = new HtmlDocument();

    string html = await this.callUrl("/empleos" + "/de-" + companyName);
   
    htmlDoc.LoadHtml(html);

    return this.parseJobsCount(
        this.getInnerText(htmlDoc.DocumentNode, "//div[contains(@class,\"leftSide\")]/p")
      );

  }
}
