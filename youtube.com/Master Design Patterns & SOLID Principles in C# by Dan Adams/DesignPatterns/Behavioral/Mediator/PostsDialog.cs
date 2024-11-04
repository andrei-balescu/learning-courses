using DesignPatterns.Behavioral.Mediator.UserInterface;

namespace DesignPatterns.Behavioral.Mediator;

public class PostsDialog
{
    private UIListBox _PostsListBox;
    private UITextBox _titleTextBox;
    private UIButton _saveButton;

    public PostsDialog(
        UIListBox listBox, 
        UITextBox titleTextBox, 
        UIButton saveButton)
    {
        _PostsListBox = listBox;
        _PostsListBox.StateChanged += new UIControl.StateChangedHandler(ListBoxSelectionChanged);

        _titleTextBox = titleTextBox;
        _titleTextBox.StateChanged += new UIControl.StateChangedHandler(TextBoxTextChanged);

        _saveButton = saveButton;
        _saveButton.IsEnabled = false;
    }

    private void ListBoxSelectionChanged()
    {
        _titleTextBox.Text = _PostsListBox.Selection;
        _saveButton.IsEnabled = true;
    }

    private void TextBoxTextChanged()
    {
        bool isTitleEmpty = string.IsNullOrEmpty(_titleTextBox.Text);
        _saveButton.IsEnabled = !isTitleEmpty;
    }
}