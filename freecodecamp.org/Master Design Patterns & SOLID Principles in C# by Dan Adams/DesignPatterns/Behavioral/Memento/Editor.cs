namespace DesignPatterns.Behavioral.Memento;

/// <summary>Memento Pattern - Originator component</summary>
public class Editor : IEditor
{
    public string Title { get; set; }
    public string Content { get; set; }

    public EditorState SaveState()
    {
        var editorState = new EditorState(Title, Content);
        return editorState;
    }

    public void RestoreState(EditorState editorState)
    {
        Title = editorState.Title;
        Content = editorState.Content;
    }
}