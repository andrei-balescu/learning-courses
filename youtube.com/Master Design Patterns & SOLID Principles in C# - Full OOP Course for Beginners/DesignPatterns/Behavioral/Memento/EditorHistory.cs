namespace DesignPatterns.Behavioral.Memento;

/**
 * Memento pattern - Caretaker component
 */
public class EditorHistory
{
    private readonly IList<EditorState> _states;
    private readonly IEditor _editor;

    public EditorHistory(IEditor editor)
    {
        _editor = editor;
        _states = new List<EditorState>();
    }

    public void Backup()
    {
        var state = _editor.SaveState();
        _states.Add(state);
    }

    public void Undo()
    {
        if (_states.Count != 0){
            var prevState = _states.Last();
            _editor.RestoreState(prevState);

            _states.Remove(prevState);
        }
    }
}