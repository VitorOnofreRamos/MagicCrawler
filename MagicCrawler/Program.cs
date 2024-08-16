using HtmlAgilityPack;
using MagicCrawler;
using System.Drawing.Printing;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

Console.WriteLine("Escreva o ID da Coleção: ");
var collectionID = Console.ReadLine();
var collectionUrl = $"https://scryfall.com/sets/{collectionID}";

var htmlWeb = new HtmlWeb();
var collectionHtml = await htmlWeb.LoadFromWebAsync(collectionUrl);

// XPath revisado para encontrar os links das cartas na página da coleção
// Certifique-se de que este XPath corresponda a todos os links das cartas desejados

var cardLinks = collectionHtml.DocumentNode.SelectNodes("//a[contains(@class, 'card-grid-item-card')]");

var cards = new List<MagicCard>();


foreach (var linkNode in cardLinks)
{
    var cardUrl = linkNode.GetAttributeValue("href", "");
    var cardHtml = await htmlWeb.LoadFromWebAsync(cardUrl);

    // Verificação adicional para evitar URLs inválidos ou repetidos
    var name = cardHtml.DocumentNode.SelectSingleNode("//h1/span[@class='card-text-card-name']").InnerText.Trim();

    // Verifica se o elemento com a classe 'card-text-oracle' existe
    var descriptionNode = cardHtml.DocumentNode.SelectSingleNode("//div[@class='card-text-oracle']").SelectNodes(".//p");
    var description = string.Join(" ", descriptionNode.Select(p => InnerText));


    cards.Add(new MagicCard(HttpUtility.HtmlDecode(name), description));

    GC.Collect();
}

// Exibir os objetos no console
// Salvar os dados em um arquivo CSV
var csv = new StringBuilder();
csv.AppendLine("Name # Description");
csv.AppendLine(string.Join("\n", cards.Select(c => $"{c.Name} # {c.Description}")));
await File.WriteAllTextAsync("cards.csv", csv.ToString());

//var formattedDescription = Regex.Replace(card.Description.Trim(), "^\\s+", "", RegexOptions.Multiline);

//descriptionNode != null ? descriptionNode.InnerText.Trim() : "Sem Descrição"; // Usa uma string vazia se não encontrar