using RestExercise3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestExercise3.Managers
{
    public class ItemsManager
    {
        //Keeps track of ids, in order for all items to have a unique ID
        private static int _nextId = 1;
        //Creates the list of Items and fills it with 3 items to begin with
        //The 3 items is only for testing purposes
        private static readonly List<Item> Data = new List<Item>
        {
            new Item {Id = _nextId++, Name = "Book about C#", ItemQuality = 300, Quantity = 10},
            new Item {Id = _nextId++, Name = "Not a Book about C#", ItemQuality = 1, Quantity =1},
            new Item {Id = _nextId++, Name = "Fruit basket", ItemQuality = 50, Quantity =5}
            // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/object-and-collection-initializers
        };

        //Returns all items in the List, in a new List, if the all the parameters is null (or 0 for int values, default value)
        //if the substring is not null, the it returns all items that has a name containing the substring
        //the filter is case-insensitive
        //Specified default values, so one using the method doesn't have to specify the values if they don't have them
        public List<Item> GetAll(string substring = null, int minimumQuality = 0, int minimumQuantity = 0)
        {
            List<Item> result = new List<Item>(Data);
            if (substring != null)
            {
                result = result.FindAll(item => item.Name.Contains(substring, StringComparison.OrdinalIgnoreCase));
            }
            if (minimumQuality != 0)
            {
                result = result.FindAll(item => item.ItemQuality >= minimumQuality);
            }
            if (minimumQuantity != 0)
            {
                result = result.FindAll(item => item.Quantity >= minimumQuantity);
            }

            return result;
        }

        //Filter function to return all items having a quality between minQuality and maxQuality
        public List<Item> GetAllBetweenQuality(int minQuality, int maxQuality)
        {
            List<Item> result = new List<Item>(Data);
            result = result.FindAll(item => item.ItemQuality >= minQuality && item.ItemQuality <= maxQuality);
            return result;
        }

        //Returns a specific Item from the list
        //return null if the id is not found
        public Item GetById(int id)
        {
            return Data.Find(item => item.Id == id);
        }

        //Adds a new Item to the list, and assigns it the next id
        //returns the newly added Item
        public Item Add(Item newItem)
        {
            newItem.Id = _nextId++;
            Data.Add(newItem);
            return newItem;
        }

        //Deletes the Item from the list with the specific Id
        //Returns null of no Item has the Id
        public Item Delete(int id)
        {
            Item item = Data.Find(item1 => item1.Id == id);
            if (item == null) return null;
            Data.Remove(item);
            return item;
        }

        //Updates the values of the Item with the specific Id
        //Returns null of no Item has the Id
        //Notice Id is not changed in the Item from the List
        public Item Update(int id, Item updates)
        {
            Item item = Data.Find(item1 => item1.Id == id);
            if (item == null) return null;
            item.Name = updates.Name;
            item.ItemQuality = updates.ItemQuality;
            item.Quantity = updates.Quantity;
            return item;
        }
    }
}
