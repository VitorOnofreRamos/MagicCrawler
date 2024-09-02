﻿using HtmlAgilityPack;
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

var cardLinks = collectionHtml.DocumentNode.SelectNodes("//a[contains(@class, 'card-grid-item-card')]");

var cards = new List<MagicCard>();

Parallel.ForEach(cardLinks, linkNode =>
{
    var cardUrl = linkNode.GetAttributeValue("href", "");
    var cardHtml = htmlWeb.LoadFromWebAsync(cardUrl).Result;

    var name = cardHtml.DocumentNode.SelectSingleNode("//*[@class='card-text-card-name']").InnerText.Trim();

    var descriptionNode = cardHtml.DocumentNode.SelectSingleNode("//*[@class='card-text-oracle']");
    var description = descriptionNode != null ? string.Join(" ", descriptionNode.SelectNodes(".//p").Select(p => p.InnerText.Trim())) : "Sem descrição";

    cards.Add(new MagicCard(
        HttpUtility.HtmlDecode(name), 
        HttpUtility.HtmlDecode(description)
        ));

    GC.Collect();
});

cards.ForEach(Console.WriteLine);

// Exibir os objetos no console
// Salvar os dados em um arquivo CSV
var csv = new StringBuilder();
csv.AppendLine("Name # Description");
csv.AppendLine(string.Join("\n", cards.Select(c => $"{c.Name} # {c.Description}")));
await File.WriteAllTextAsync("cards.csv", csv.ToString(), Encoding.UTF8);