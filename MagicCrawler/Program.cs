using HtmlAgilityPack;
using MagicCrawler;
using System.Drawing.Printing;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

// Solicita ao usuário para inserir o ID da coleção
Console.WriteLine("Escreva o ID da Coleção: ");
var collectionID = Console.ReadLine();
var collectionUrl = $"https://scryfall.com/sets/{collectionID}";

// Carrega o conteúdo HTML da página da coleção
var htmlWeb = new HtmlWeb();
var collectionHtml = await htmlWeb.LoadFromWebAsync(collectionUrl);

// Encontra todos os links das cartas na página
var cardLinks = collectionHtml.DocumentNode.SelectNodes("//a[contains(@class, 'card-grid-item-card')]");

// Cria uma lista vazia para armazenar as cartas
var cards = new List<MagicCard>();

// Processa as cartas em paralelo
Parallel.ForEach(cardLinks, linkNode =>
{
    // Obtém a URL da carta individual
    var cardUrl = linkNode.GetAttributeValue("href", "");

    // Carrega o conteúdo HTML da página da carta
    var cardHtml = htmlWeb.LoadFromWebAsync(cardUrl).Result;

    // Extrai o nome da carta
    var name = cardHtml.DocumentNode.SelectSingleNode("//*[@class='card-text-card-name']").InnerText.Trim();

    // Extrai a descrição da carta
    // Verifica se o nó de descrição existe
    var descriptionNode = cardHtml.DocumentNode.SelectSingleNode("//*[@class='card-text-oracle']");
    var description = descriptionNode != null ? string.Join(" ", descriptionNode.SelectNodes(".//p").Select(p => p.InnerText.Trim())) : "Sem descrição";

    // Cria uma nova instância de MagicCard e adiciona à lista
    cards.Add(new MagicCard(
        HttpUtility.HtmlDecode(name), 
        HttpUtility.HtmlDecode(description)
        ));

    // Limpa memória
    GC.Collect();
});

// Imprime as cartas encontradas
cards.ForEach(Console.WriteLine);

// Prepara os dados para salvar em um arquivo CSV
var csv = new StringBuilder();
csv.AppendLine("Name # Description");
csv.AppendLine(string.Join("\n", cards.Select(c => $"{c.Name} # {c.Description}")));

// Salva os dados em um arquivo CSV
await File.WriteAllTextAsync("cards.csv", csv.ToString(), Encoding.UTF8);
