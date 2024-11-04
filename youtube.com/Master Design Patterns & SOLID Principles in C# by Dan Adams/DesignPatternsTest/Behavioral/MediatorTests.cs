using DesignPatterns.Behavioral.Mediator;
using DesignPatterns.Behavioral.Mediator.UserInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DesignPatternsTest.Behavioral;

/// <summary>
/// More info on unit testing events: https://github.com/devlooped/moq/wiki/Quickstart#events
/// </summary>
[TestClass]
public class MediatorTests
{
    [TestMethod]
    public void UIButton_StateChangedEvent_OnStateChange()
    {
        bool actualEventRaised = false;

        var uiButton = new UIButton();
        uiButton.StateChanged += () => actualEventRaised = true;

        uiButton.IsEnabled = true;

        Assert.AreEqual(true, actualEventRaised);
        Assert.AreEqual(true, uiButton.IsEnabled);
    }

    [TestMethod]
    public void UIListBox_StateChangedEvent_OnStateChange()
    {
        bool actualEventRaised = false;
        string expectedSelection = "Selected item";

        var uiListBox = new UIListBox();
        uiListBox.StateChanged += () => actualEventRaised = true;

        uiListBox.Selection = expectedSelection;

        Assert.AreEqual(true, actualEventRaised);
        Assert.AreEqual(expectedSelection, uiListBox.Selection);
    }

    [TestMethod]
    public void UITextBox_StateChangedEvent_OnStateChange()
    {
        bool actualEventRaised = false;
        string expectedText = "Item name";

        var uiTextBox = new UITextBox();
        uiTextBox.StateChanged += () => actualEventRaised = true;

        uiTextBox.Text = expectedText;

        Assert.AreEqual(true, actualEventRaised);
        Assert.AreEqual(expectedText, uiTextBox.Text);
    }

    [TestMethod]
    public void DialogBox_ListBoxSelectionChanged_UpdatesComponsntState()
    {
        // Arrange
        var expectedText = "Item selected";

        var uiButton = new UIButton();
        var uiListBox = new UIListBox();
        var uiTextBox = new UITextBox();

        var dialogBox = new PostsDialog(uiListBox, uiTextBox, uiButton);

        // Act
        uiListBox.Selection = expectedText;

        // Assert
        Assert.AreEqual(expectedText, uiTextBox.Text);
        Assert.AreEqual(true, uiButton.IsEnabled);
    }

    [TestMethod]
    public void DialogBox_TextBoxTextChanged_UpdatesComponsntState()
    {
        // Arrange
        var uiButton = new UIButton();
        var uiListBox = new UIListBox();
        var uiTextBox = new UITextBox();

        var dialogBox = new PostsDialog(uiListBox, uiTextBox, uiButton);

        // Act
        uiTextBox.Text = "Selection title";

        // Assert
        Assert.AreEqual(true, uiButton.IsEnabled);
    }
}