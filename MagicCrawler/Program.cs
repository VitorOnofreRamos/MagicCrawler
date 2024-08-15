using HtmlAgilityPack;
using MagicCrawler;
using System.Text;

var collectionUrl = "https://scryfall.com/sets/pcbb";
var htmlWeb = new HtmlWeb();
var collectionHtml = await htmlWeb.LoadFromWebAsync(collectionUrl);

// XPath revisado para encontrar os links das cartas na página da coleção
// Certifique-se de que este XPath corresponda a todos os links das cartas desejados
var cardLinksXPath = "//a[contains(@class, 'card-grid-item-card')]";
var cardLinks = collectionHtml.DocumentNode.SelectNodes(cardLinksXPath);

var cards = new List<MagicCard>();

foreach (var linkNode in cardLinks)
{
    var cardUrl = linkNode.GetAttributeValue("href", "");
    var cardHtml = await htmlWeb.LoadFromWebAsync(cardUrl);

    // Verificação adicional para evitar URLs inválidos ou repetidos
    if (!string.IsNullOrEmpty(cardUrl) && !cards.Any(c => c.CardId == cardUrl.Split('/').Last()))
    {
        var name = cardHtml.DocumentNode.SelectSingleNode("//h1/span[@class='card-text-card-name']").InnerText;
        var description = cardHtml.DocumentNode.SelectSingleNode("//div[@class='card-text-oracle']").InnerText;
        var buttonNode = cardHtml.DocumentNode.SelectSingleNode("//button[@data-card-id]");
        var cardId = buttonNode?.GetAttributeValue("data-card-id", "");

        cards.Add(new MagicCard(name, description, cardId));

        GC.Collect();
    }
}

// Exibir os objetos no console
foreach (var card in cards)
{
    Console.WriteLine(card);
}

// Salvar os dados em um arquivo CSV
var csv = new StringBuilder();
csv.AppendLine("Name,Description,CardId");
foreach (var card in cards)
{
    csv.AppendLine($"{card.Name},{card.Description},{card.CardId}");
}
await File.WriteAllTextAsync("cards.csv", csv.ToString());
