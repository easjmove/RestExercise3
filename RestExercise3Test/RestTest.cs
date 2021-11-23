using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestExercise3.Managers;
using RestExercise3.Models;

namespace RestExercise3Test
{
    [TestClass]
    public class RestTest
    {
        //Having a object variable manager, so each method doesn't have to initialize one
        public ItemsManager _manager = new ItemsManager();
        [TestMethod]
        public void TestGetAll()
        {
            //Test without any parameters
            //Here we assume no changes has been made to the list since the server started
            Assert.IsTrue(_manager.GetAll().Count == 3);
            //Testing the substring filter works
            Assert.IsTrue(_manager.GetAll("Book").Count == 2);
            //testing the substring filter is case-insensitive
            Assert.IsTrue(_manager.GetAll("book").Count == 2);
            //Testing with a filter that doens't return anything, expecting an empty list, not null
            Assert.IsTrue(_manager.GetAll("something").Count == 0);

            //Testing the ItemQuality filter
            Assert.IsTrue(_manager.GetAll(null, 25).Count == 2);
            Assert.IsTrue(_manager.GetAll(null, 0).Count == 3);
            Assert.IsTrue(_manager.GetAll(null, 5000).Count == 0);

            //Testing the Quantity filter
            Assert.IsTrue(_manager.GetAll(null, 0, 2).Count == 2);
            Assert.IsTrue(_manager.GetAll(null, 0, 0).Count == 3);
            Assert.IsTrue(_manager.GetAll(null, 0, 20).Count == 0);

            //Testing combinations
            Assert.IsTrue(_manager.GetAll("book", 100, 5).Count == 1);
            Assert.IsTrue(_manager.GetAll("book", 1, 1).Count == 2);
            Assert.IsTrue(_manager.GetAll("fruit", 1, 6).Count == 0);
        }

        [TestMethod]
        public void TestGetAllBetweenQuality()
        {
            //Expecting the single result (Fruit basket)
            Assert.IsTrue(_manager.GetAllBetweenQuality(40, 60).Count == 1);
            //Checking that the single result's name should be Fruit basket
            Assert.IsTrue(_manager.GetAllBetweenQuality(40, 60)[0].Name == "Fruit basket");

            //Testing a range that shouldn't have any Items
            Assert.IsTrue(_manager.GetAllBetweenQuality(20, 30).Count == 0);
        }

        [TestMethod]
        public void TestGetByID()
        {
            //checking the first Item
            Assert.IsTrue(_manager.GetById(1).Name.Equals("Book about C#"));
            //checking an ID that shouldn't exist
            Assert.IsNull(_manager.GetById(25));
        }

        //Here we test add and delete together
        //We do this to clean up after, so the other test methods aren't affected by the changes
        [TestMethod]
        public void TestAddAndDelete()
        {
            //Reads the count before adding so we can compare it to the number after adding
            int beforeAddCount = _manager.GetAll().Count;
            //creates a variable with the Id we assign, should be overridden when the manager add the Item
            int defaultId = 0;
            //Creates a testitem to be added
            Item newItem = new Item(defaultId, "TestItem", 3, 3);
            //Adding the Item, and storing the result in a variable
            Item addResult = _manager.Add(newItem);
            //stores the newly assigned Id
            int newID = addResult.Id;
            //Checking that the manager assigns a new ID
            Assert.AreNotEqual(defaultId, newID);
            //Checking that the count of the list is now 1 more than before
            Assert.AreEqual(beforeAddCount + 1, _manager.GetAll().Count);

            //checks that the ID of the deleted Item is the same that we asked to be deleted
            Item deletedItem = _manager.Delete(newID);
            //checks that the count is now the same as when we began before adding and deleting
            Assert.AreEqual(beforeAddCount, _manager.GetAll().Count);

            //checks that if we ask to delete an Item with an Id that doesn't exist, that it returns null
            Assert.IsNull(_manager.Delete(30));
        }

        [TestMethod]
        public void TestUpdate()
        {
            //Creates a new Item which holds data to update another Item
            Item newItem = new Item(14, "TestItem", 3, 4);
            //Updates the Item
            _manager.Update(1, newItem);
            //Checks that Item in the manager has the name from the newItem
            Assert.AreEqual(newItem.Name, _manager.GetById(1).Name);

            //Checks that we receive a null when trying to update something not existing in the manager
            Assert.IsNull(_manager.Update(4, newItem));

            //Cleans up
            Item cleanUpItem = new Item() { Name = "Book about C#", ItemQuality = 300, Quantity = 10 };
            _manager.Update(1, cleanUpItem);
        }
    }
}
