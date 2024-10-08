namespace DesignPatterns.Behavioral.Mediator.BadExample;

public class BadPostsDialogBox : BadDialogBox
{
    private BadListBox _postsListBox;
    private BadTextBox _titleText;
    private BadButton _saveButton;

    public BadPostsDialogBox()
    {
        _postsListBox = new BadListBox(this);
        _titleText = new BadTextBox(this);
        _saveButton = new BadButton(this);

        _saveButton.IsEnabled = false;
    }

    public override void Changed(BadUIControl uIControl)
    {
        if (uIControl is BadListBox)
        {
            HandlePostChanged();
        }
        else if (uIControl is BadTextBox)
        {
            HandleTitleChanged();
        }
    }

    private void HandlePostChanged()
    {
        _titleText.Text = _postsListBox.Selection;
        _saveButton.IsEnabled = true;
    }

    private void HandleTitleChanged()
    {
        bool isTitleEmpty = string.IsNullOrEmpty(_titleText.Text);
        _saveButton.IsEnabled = !isTitleEmpty;
    }
}