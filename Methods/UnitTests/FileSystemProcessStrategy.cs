using Methods.Enums;
using Methods.FileSystem;
using Moq;
using NUnit.Framework;
using System;
using System.IO;

namespace UnitTests
{
    [TestFixture]
    public class FileSystemProcessStrategy
    {
        private IFileSystemProcessingStrategy _strategy;
        private FileSystemInfo _fileSystemInfoMock;

        private void OnEvent<TArgs>(EventHandler<TArgs> someEvent, TArgs args)
        {
            someEvent?.Invoke(this, args);
        }

        [SetUp]
        public void SetUp()
        {
            _strategy = new FileSystemProcessingStrategy();
            _fileSystemInfoMock = new Mock<FileSystemInfo>().Object;
        }

        [Test]
        public void ProcessItemFinded_WhenItemFindedCall_Should()
        {
            // Arrange
            int delegatesCallCount = 0;

            // Act
            _strategy.ProcessItemFinded(_fileSystemInfoMock, null, (s, e) => delegatesCallCount++, null, OnEvent);

            // Assert
            Assert.AreEqual(1, delegatesCallCount);
        }

        [Test]
        public void ProcessItemFinded_WhenFilteredItemFindedCall_Should()
        {
            // Arrange
            int delegatesCallCount = 0;

            // Act
            _strategy.ProcessItemFinded(
                _fileSystemInfoMock, info => true, (s, e) => delegatesCallCount++, (s, e) => delegatesCallCount++, OnEvent);

            // Assert
            Assert.AreEqual(2, delegatesCallCount);
        }

        [Test]
        public void ProcessItemFinded_WhenItemNotPassFilter_Should()
        {
            // Arrange
            int delegatesCallCount = 0;

            // Act
            _strategy.ProcessItemFinded(
                _fileSystemInfoMock, info => false, (s, e) => delegatesCallCount++, (s, e) => delegatesCallCount++, OnEvent);
            // Assert
            Assert.AreEqual(1, delegatesCallCount);
        }

        [Test]
        public void ProcessItemFinded_WhenItemFinded_ShouldReturnContinueSearchAction()
        {
            // Act
            var action = _strategy.ProcessItemFinded(_fileSystemInfoMock, null, (s, e) => { }, null, OnEvent);
            
            // Assert
            Assert.AreEqual(ActionType.ContinueSearch, action);
        }

        [Test]
        public void ProcessItemFinded_WhenFilteredItemFinded_ShouldReturnContinueSearchAction()
        {
            // Act
            var action = _strategy.ProcessItemFinded(
                _fileSystemInfoMock, info => true, (s, e) => { }, (s, e) => { }, OnEvent);

            // Assert
            Assert.AreEqual(ActionType.ContinueSearch, action);
        }

        [Test]
        public void ProcessItemFinded_WhenFindedItemSkipped_ShouldReturnSkipElementAction()
        {
            // Arrange
            int delegatesCallCount = 0;

            // Act
            var action = _strategy.ProcessItemFinded(
                _fileSystemInfoMock, info => true, (s, e) =>
                {
                    delegatesCallCount++;
                    e.ActionType = ActionType.SkipElement;
                }, (s, e) => delegatesCallCount++, OnEvent);

            // Assert
            Assert.AreEqual(ActionType.SkipElement, action);
            Assert.AreEqual(1, delegatesCallCount);
        }

        [Test]
        public void ProcessItemFinded_WhenFilteredFindedItemSkipped_ShouldReturnSkipElementAction()
        {
            // Arrange
            int delegatesCallCount = 0;

            // Act
            var action = _strategy.ProcessItemFinded(
                _fileSystemInfoMock, info => true,
                (s, e) => delegatesCallCount++,
                (s, e) =>
                {
                    delegatesCallCount++;
                    e.ActionType = ActionType.SkipElement;
                }, OnEvent);

            // Assert
            Assert.AreEqual(ActionType.SkipElement, action);
            Assert.AreEqual(2, delegatesCallCount);
        }

        [Test]
        public void ProcessItemFinded_WhenFindedItemStopped_ShouldReturnStopSearchAction()
        {
            // Arrange
            int delegatesCallCount = 0;

            // Act
            var action = _strategy.ProcessItemFinded(
                _fileSystemInfoMock, info => true, (s, e) =>
                {
                    delegatesCallCount++;
                    e.ActionType = ActionType.StopSearch;
                }, (s, e) => delegatesCallCount++, OnEvent);

            // Assert
            Assert.AreEqual(ActionType.StopSearch, action);
            Assert.AreEqual(1, delegatesCallCount);
        }

        [Test]
        public void ProcessItemFinded_WhenFilteredFindedItemStopped_ShouldReturnStopSearchAction()
        {
            // Arrange
            int delegatesCallCount = 0;

            // Act
            var action = _strategy.ProcessItemFinded(
                _fileSystemInfoMock, info => true,
                (s, e) => delegatesCallCount++,
                (s, e) =>
                {
                    delegatesCallCount++;
                    e.ActionType = ActionType.StopSearch;
                }, OnEvent);

            // Assert
            Assert.AreEqual(ActionType.StopSearch, action);
            Assert.AreEqual(2, delegatesCallCount);
        }
    }
}
