using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Moq;
using Playstore.Common.MongoDB;
using Playstore.Common.Test.MongoDb;

namespace Playstore.Common.Test.MongoDb;

[TestClass]
public class MongoRepositoryTests
{
    private Mock<IMongoCollection<IEntity>> _mongoCollectionMock;

    private Mock<IMongoDatabase> _mongoDatabaseMock;

    private MongoRepository<IEntity> _repository;

    [TestInitialize]
    public void TestInitialize()
    {
        _mongoDatabaseMock = new Mock<IMongoDatabase>();
        _mongoCollectionMock = new Mock<IMongoCollection<IEntity>>();

        string expectedCollectionName = "expectedCollectionName";
        _mongoDatabaseMock.Setup(m => m.GetCollection<IEntity>(expectedCollectionName, It.IsAny<MongoCollectionSettings>()))
            .Returns(_mongoCollectionMock.Object);

        _repository = new MongoRepository<IEntity>(_mongoDatabaseMock.Object, expectedCollectionName);
    }

    [TestMethod]
    public async Task GetAllAsync_GetsFromDb()
    {
        // Arrange
        TestEntity expectedEntity = new();

        _mongoCollectionMock.Setup(m => m.FindAsync(Builders<IEntity>.Filter.Empty, It.IsAny<FindOptions<IEntity>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateMockAsyncCursor(new List<IEntity> { expectedEntity }));

        // Act
        IReadOnlyCollection<IEntity> actualResult = await _repository.GetAllAsync();

        // Assert
        Assert.AreSame(expectedEntity, actualResult.Single());
    }

    [TestMethod]
    public async Task GetAllAsync_AppliesFilter()
    {
        // Arrange
        Guid testId = Guid.NewGuid();
        TestEntity expectedEntity = new() { Id = testId };
        var testList = new List<IEntity> { expectedEntity };

        _mongoCollectionMock.Setup(m => m.FindAsync(It.IsAny<FilterDefinition<IEntity>>(), It.IsAny<FindOptions<IEntity>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                (FilterDefinition<IEntity> filter, FindOptions<IEntity> options, CancellationToken token) => 
                {
                    var expressionFilter = (ExpressionFilterDefinition<IEntity>)filter;
                    Func<IEntity, bool> filterFunc = expressionFilter.Expression.Compile();
                    IEnumerable<IEntity> filteredList = testList.Where(filterFunc);

                    return CreateMockAsyncCursor(filteredList);
                });

        // Act
        IReadOnlyCollection<IEntity> actualResult = await _repository.GetAllAsync(e => e.Id == testId);

        // Assert
        Assert.AreSame(expectedEntity, actualResult.Single());
    }

    [TestMethod]
    public async Task GetAsync_QueriesById()
    {
        // Arrange
        TestEntity expectedEntity = new();
        var testList = new List<IEntity> { expectedEntity };
        
        _mongoCollectionMock.Setup(m => m.FindAsync(
            // could not find a way to test specific filters created with FilterDefinitionBuilder, ex .Eq()
            It.IsAny<FilterDefinition<IEntity>>(), 
            It.IsAny<FindOptions<IEntity>>(), 
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(CreateMockAsyncCursor(testList));

        //Act
        IEntity actualEntity = await _repository.GetAsync(Guid.NewGuid());

        // Assert
        Assert.AreSame(expectedEntity, actualEntity);
    }

    [TestMethod]
    public async Task GetAsync_AppliesFilter()
    {
        // Arrange
        Guid testId = Guid.NewGuid();
        TestEntity expectedEntity = new() { Id = testId };
        var testList = new List<IEntity> { expectedEntity };

        _mongoCollectionMock.Setup(m => m.FindAsync(It.IsAny<FilterDefinition<IEntity>>(), It.IsAny<FindOptions<IEntity>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(
                (FilterDefinition<IEntity> filter, FindOptions<IEntity> options, CancellationToken token) =>
                { 
                    var expressionFilter = (ExpressionFilterDefinition<IEntity>)filter;
                    Func<IEntity, bool> filterFunc = expressionFilter.Expression.Compile();
                    IEnumerable<IEntity> filteredList = testList.Where(filterFunc);

                    return CreateMockAsyncCursor(filteredList);
                });

        //Act
        IEntity actualEntity = await _repository.GetAsync(e => e.Id == testId);

        // Assert
        Assert.AreSame(expectedEntity, actualEntity);
    }

    [TestMethod]
    public async Task CreateAsync_UpdatesDb()
    {
        // Arrange
        TestEntity expectedEntity = new();

        // Act
        await _repository.CreateAsync(expectedEntity);

        // Assert
        _mongoCollectionMock.Verify(m => m.InsertOneAsync(expectedEntity, It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()));
    }

    [TestMethod]
    public async Task CreateAsync_ErrorIfObjectNull()
    {
        bool isError = false;

        try
        {
            await _repository.CreateAsync(null);
        }
        catch(ArgumentNullException)
        {
            isError = true;
        }

        Assert.IsTrue(isError);
    }

    [TestMethod]
    public async Task UpdateAsync_UpdatesDb()
    {
        // Arrange
        TestEntity expectedEntity = new();

        // Act
        await _repository.UpdateAsync(expectedEntity);

        // Assert
        _mongoCollectionMock.Verify(m => m.ReplaceOneAsync(
            // could not find a way to test specific filters created with FilterDefinitionBuilder, ex .Eq()
            It.IsAny<FilterDefinition<IEntity>>(), 
            expectedEntity, 
            It.IsAny<ReplaceOptions>(), 
            It.IsAny<CancellationToken>()));
    }

    [TestMethod]
    public async Task UpdateAsync_ErrorIfObjectNull()
    {
        bool isError = false;

        try
        {
            await _repository.UpdateAsync(null);
        }
        catch(ArgumentNullException)
        {
            isError = true;
        }

        Assert.IsTrue(isError);
    }

    [TestMethod]
    public async Task RemoveAsync_UpdatesDb()
    {
        // Act
        await _repository.RemoveAsync(Guid.NewGuid());

        // Assert
        _mongoCollectionMock.Verify(m => m.DeleteOneAsync(
            // could not find a way to test specific filters created with FilterDefinitionBuilder, ex .Eq()
            It.IsAny<FilterDefinition<IEntity>>(), 
            It.IsAny<CancellationToken>()));
    }
    
    private IAsyncCursor<IEntity> CreateMockAsyncCursor(IEnumerable<IEntity> collection)
    {
        Mock<IAsyncCursor<IEntity>> asyncCursor = new ();
        asyncCursor.SetupSequence(m => m.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .ReturnsAsync(false);
        asyncCursor.SetupGet(m => m.Current).Returns(collection);

        return asyncCursor.Object;
    }
}