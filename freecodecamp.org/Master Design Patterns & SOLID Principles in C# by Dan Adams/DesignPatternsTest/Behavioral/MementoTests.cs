using DesignPatterns.Behavioral.Memento;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DesignPatternsTest.Behavioral;

[TestClass]
public class MementoTests
{
    [TestMethod]
    public void Editor_SaveState_ReturnsCurrentState()
    {
        // Arrange
        var expecteTitle = "Expected Title";
        var expectedContent = "Expected Content";

        var editor = new Editor { Title = expecteTitle, Content = expectedContent };

        // Act
        var actualState = editor.SaveState();

        // Assert
        Assert.AreEqual(expecteTitle, actualState.Title);
        Assert.AreEqual(expectedContent, actualState.Content);
    }

    [TestMethod]
    public void Editor_RestoreState_UpdatesState()
    {
        // Arrange
        var expecteTitle = "Expected Title";
        var expectedContent = "Expected Content";
        var state = new EditorState(expecteTitle, expectedContent);
        var editor = new Editor { Title = "Title", Content = "Content" };

        // Act
        editor.RestoreState(state);

        // Assert
        Assert.AreEqual(expecteTitle, editor.Title);
        Assert.AreEqual(expectedContent, editor.Content);
    }

    [TestMethod]
    public void EditorHistory_Backup_RetrievesState()
    {
        // Arrange
        var editorMock = new Mock<IEditor>();

        var expectedState = new EditorState("Title", "Content");
        editorMock.Setup(m => m.SaveState()).Returns(expectedState);
        
        var editorHistory = new EditorHistory(editorMock.Object);
        
        // Act
        editorHistory.Backup();
        editorHistory.Undo();
        editorHistory.Undo();

        // Assert
        editorMock.Verify(m => m.RestoreState(expectedState), Times.Once);
    }
}