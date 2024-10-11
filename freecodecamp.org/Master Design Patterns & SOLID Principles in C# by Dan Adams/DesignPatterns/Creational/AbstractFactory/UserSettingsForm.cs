namespace DesignPatterns.Creational.AbstractFactory;

public class UserSettingsForm
{
    private IUIComponentFactory _uiComponentFactory;

    public UserSettingsForm(IUIComponentFactory uiComponentFactory)
    {
        _uiComponentFactory = uiComponentFactory;
    }

    public void Render()
    {
        var button = _uiComponentFactory.CreateButton();
        button.Render();

        var checkbox = _uiComponentFactory.CreateCheckbox();
        checkbox.Render();
    }
}