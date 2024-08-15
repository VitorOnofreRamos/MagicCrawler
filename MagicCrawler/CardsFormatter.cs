namespace MagicCrawler;

public static class MagicCardFormatter
{
    public static string FormatForConsole(MagicCard card)
    {
        return $"Name: {card.Name}, Description: {card.Description}, CardId: {card.CardId}";
    }
    public static string ToCsvLine(MagicCard card)
    {
        var escapedDescription = card.Description?.Replace("\"", "\"\"").Replace("\n", " ").Replace("\r", "") ?? "";
        return $"\"{card.Name}\",\"{escapedDescription}\",\"{card.CardId}\"";
    }
}
