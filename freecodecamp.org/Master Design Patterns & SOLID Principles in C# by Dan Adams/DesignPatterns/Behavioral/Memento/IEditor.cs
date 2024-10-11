namespace DesignPatterns.Behavioral.Memento;

public interface IEditor
{
    EditorState SaveState();

    void RestoreState(EditorState editorState);
}