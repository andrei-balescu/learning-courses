using DesignPatterns.Behavioral.State;
using DesignPatterns.Behavioral.State.DocumentState;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesignPatternsTest.Behavioral;

[TestClass]
public class StateTests
{
    [TestMethod]
    public void DraftState_Publish_NothingIfUserIsReader()
    {
        // Arrange
        var document = new Document(UserRoles.Reader);

        // Act
        document.Publish();

        // Assert
        Assert.IsInstanceOfType(document.State, typeof(DraftState));
    }

    [TestMethod]
    public void DraftState_Publish_ModerationStateIfUserIsEditor()
    {
        // Arrange
        var document = new Document(UserRoles.Editor);

        // Act
        document.Publish();

        // Assert
        Assert.IsInstanceOfType(document.State, typeof(ModerationState));
    }

    [TestMethod]
    public void DraftState_Publish_ModerationStateIfUserIsAdmin()
    {
        // Arrange
        var document = new Document(UserRoles.Admin);

        // Act
        document.Publish();

        // Assert
        Assert.IsInstanceOfType(document.State, typeof(ModerationState));
    }

    [TestMethod]
    public void ModerationState_Publish_NothingIfUserIsEditor()
    {
        // Arrange
        var document = new Document(UserRoles.Editor);

        // Act
        document.Publish();
        document.Publish();

        // Assert
        Assert.IsInstanceOfType(document.State, typeof(ModerationState));
    }

    [TestMethod]
    public void ModerationState_Publish_PublishedStateIfUserIsAdmin()
    {
        // Arrange
        var document = new Document(UserRoles.Admin);

        // Act
        document.Publish();
        document.Publish();

        // Assert
        Assert.IsInstanceOfType(document.State, typeof(PublishedState));
    }

    [TestMethod]
    public void PublishedState_Publish_DoesNothing()
    {
        // Arrange
        var document = new Document(UserRoles.Admin);

        // Act
        document.Publish();
        document.Publish();
        document.Publish();

        // Assert
        Assert.IsInstanceOfType(document.State, typeof(PublishedState));
    }
}