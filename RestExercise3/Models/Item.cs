using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestExercise3.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ItemQuality { get; set; }
        public int Quantity { get; set; }

        //Needs to have a no-argument constructor for serialization/deserialization
        public Item()
        {
        }

        //A constructor taking all the properties as parameters
        public Item(int id, string name, int itemQuality, int quantity)
        {
            Id = id;
            Name = name;
            ItemQuality = itemQuality;
            Quantity = quantity;
        }

        //Overrides the default ToString method
        public override string ToString()
        {
            //Simple string containing the property names and thier respective values
            return $"Id: {Id} - Name: {Name} - ItemQuality: {ItemQuality} - Quantity: {Quantity}";
        }
    }
}
