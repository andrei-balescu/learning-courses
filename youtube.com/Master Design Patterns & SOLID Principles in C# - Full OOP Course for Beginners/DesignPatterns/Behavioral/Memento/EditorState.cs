namespace DesignPatterns.Behavioral.Memento;

/**
 * Memento Pattern - Memento component
 */
public class EditorState
{
    public string Title { get; private set; }
    public string Content { get; private set; }

    public EditorState(string title, string content)
    {
        Title = title;
        Content = content;
    }
}