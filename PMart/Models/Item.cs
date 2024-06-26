﻿#region Header
// Author:      Shubham Gaikwad
// Date:        06/01/2024
#endregion

using System.ComponentModel.DataAnnotations;

namespace PMart.Models
{
    // Represents an item in the PMart inventory.
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "The name of the item is required")]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public double Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Item should have atleast 1 quantity")]
        public int Quantity { get; set; }

        public Item(string name, double price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }
}
