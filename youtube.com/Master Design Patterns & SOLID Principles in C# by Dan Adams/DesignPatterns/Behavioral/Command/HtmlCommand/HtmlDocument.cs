namespace DesignPatterns.Behavioral.Command.HtmlCommand;

public class HtmlDocument
{
    public string Content { get; set; }

    public HtmlDocument(string content = "")
    {
        Content = content;
    }

    public void Emphasize()
    {
        Content = $"<em>{Content}</em>";
    }

    public void MakeStrong()
    {
        Content = $"<strong>{Content}</strong>";
    }
}