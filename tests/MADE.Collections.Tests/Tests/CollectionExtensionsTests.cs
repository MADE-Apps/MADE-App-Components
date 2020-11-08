namespace MADE.Collections.Tests.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;

    using MADE.Collections.ObjectModel;
    using MADE.Collections.Tests.Fakes;
    using MADE.Testing;

    using NUnit.Framework;

    using Shouldly;

    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class CollectionExtensionsTests
    {
        public class WhenUpdatingACollectionItem
        {
            [Test]
            public void ShouldThrowArgumentNullExceptionIfNullCollection()
            {
                // Arrange
                List<string> list = null;
                string item = "Hello";

                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => list.Update(item, (s, i) => s == i));
            }

            [Test]
            public void ShouldThrowArgumentNullExceptionIfNullItem()
            {
                // Arrange
                var list = new List<string> { "Hello" };
                string item = null;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => list.Update(item, (s, i) => s == i));
            }

            [Test]
            public void ShouldReturnTrueIfItemUpdated()
            {
                // Arrange
                TestObject objectToAdd = TestObjectFaker.Create().Generate();
                TestObject objectToUpdateWith = TestObjectFaker.Create().Generate();

                var list = new List<TestObject> { objectToAdd };

                // Act
                bool updated = list.Update(objectToUpdateWith, (s, i) => s.Name == objectToAdd.Name);

                // Assert
                updated.ShouldBeTrue();
            }

            [Test]
            public void ShouldReturnFalseIfItemToUpdateDoesNotExist()
            {
                // Arrange
                TestObject objectToAdd = TestObjectFaker.Create().Generate();
                TestObject objectToUpdateWith = TestObjectFaker.Create().Generate();

                var list = new List<TestObject> { objectToAdd };

                // Act
                bool updated = list.Update(objectToUpdateWith, (s, i) => s.Name == objectToUpdateWith.Name);

                // Assert
                updated.ShouldBeFalse();
            }
        }

        public class WhenUpdatingCollectionEqualToAnother
        {
            [Test]
            public void ShouldThrowArgumentNullExceptionIfNullCollection()
            {
                // Arrange
                List<string> list = null;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => list.MakeEqualTo(null));
            }

            [Test]
            public void ShouldThrowArgumentNullExceptionIfNullSource()
            {
                // Arrange
                var list = new List<string> { "Hello" };

                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => list.MakeEqualTo(null));
            }

            [Test]
            public void ShouldUpdateCollectionToBeEqualOther()
            {
                // Arrange
                var list = new List<string> { "Hello" };
                var update = new List<string> { "New", "List" };

                // Act
                list.MakeEqualTo(update);

                // Assert
                list.ShouldBeEquivalentTo(update);
            }
        }

        public class WhenAddingRangeOfItems
        {
            [Test]
            public void ShouldAddRangeOfItems()
            {
                // Arrange
                List<TestObject> objectsToAdd = TestObjectFaker.Create().Generate(10);

                var collection = new ObservableCollection<TestObject>();

                // Act
                collection.AddRange(objectsToAdd);

                // Assert
                foreach (TestObject item in objectsToAdd)
                {
                    collection.ShouldContain(item);
                }
            }
        }

        public class WhenRemovingRangeOfItems
        {
            [Test]
            public void ShouldRemoveRangeOfItems()
            {
                // Arrange
                List<TestObject> items = TestObjectFaker.Create().Generate(10);
                var itemsToRemove = items.Take(5).ToList();

                var collection = new ObservableCollection<TestObject>(items);

                // Act
                collection.RemoveRange(itemsToRemove);

                // Assert
                foreach (TestObject item in itemsToRemove)
                {
                    collection.ShouldNotContain(item);
                }
            }
        }
    }
}