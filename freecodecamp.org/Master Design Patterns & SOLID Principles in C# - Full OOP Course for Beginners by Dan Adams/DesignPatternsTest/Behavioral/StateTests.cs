using DesignPatterns.Behavioral.State;
using DesignPatterns.Behavioral.State.DocumentState;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DesignPatternsTest.Behavioral;

[TestClass]
public class StateTests
{
    [TestMethod]
    public void DraftState_Publish_NothingIfUserIsReader()
    {
        // Arrange
        var documentContextMock = new Mock<IDocumentContext>();
        documentContextMock.SetupGet(m => m.CurrentUserRole).Returns(UserRoles.Reader);

        var draftState = new DraftState(documentContextMock.Object);

        // Act
        draftState.Publish();

        // Assert
        documentContextMock.VerifySet(m=> m.State = It.IsAny<DraftState>());
    }

    [TestMethod]
    public void DraftState_Publish_ModerationStateIfUserIsEditor()
    {
        // Arrange
        var documentContextMock = new Mock<IDocumentContext>();
        documentContextMock.SetupGet(m => m.CurrentUserRole).Returns(UserRoles.Editor);

        var draftState = new DraftState(documentContextMock.Object);

        // Act
        draftState.Publish();

        // Assert
        documentContextMock.VerifySet(m=> m.State = It.IsAny<ModerationState>());
    }

    [TestMethod]
    public void DraftState_Publish_ModerationStateIfUserIsAdmin()
    {
        // Arrange
        var documentContextMock = new Mock<IDocumentContext>();
        documentContextMock.SetupGet(m => m.CurrentUserRole).Returns(UserRoles.Admin);

        var draftState = new DraftState(documentContextMock.Object);

        // Act
        draftState.Publish();

        // Assert
        documentContextMock.VerifySet(m=> m.State = It.IsAny<ModerationState>());
    }

    [TestMethod]
    public void ModerationState_Publish_NothingIfUserIsNotAdmin()
    {
        // Arrange
        var documentContextMock = new Mock<IDocumentContext>();
        documentContextMock.SetupGet(m => m.CurrentUserRole).Returns(UserRoles.Editor);

        var moderationState = new ModerationState(documentContextMock.Object);

        // Act
        moderationState.Publish();

        // Assert
        documentContextMock.VerifySet(m=> m.State = It.IsAny<ModerationState>());
    }

    [TestMethod]
    public void ModerationState_Publish_PublishedStateIfUserIsAdmin()
    {
        // Arrange
        var documentContextMock = new Mock<IDocumentContext>();
        documentContextMock.SetupGet(m => m.CurrentUserRole).Returns(UserRoles.Admin);

        var moderationState = new ModerationState(documentContextMock.Object);

        // Act
        moderationState.Publish();

        // Assert
        documentContextMock.VerifySet(m=> m.State = It.IsAny<PublishedState>());
    }

    [TestMethod]
    public void Document_Publish_ExecutesStateLogic()
    {
        // Arrange
        var document = new Document(UserRoles.Editor);

        // Act
        document.Publish();

        // Assert
        Assert.IsInstanceOfType(document.State, typeof(ModerationState));
    }
}