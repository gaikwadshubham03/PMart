#region Header
// Author:      Shubham Gaikwad
// Date:        06/01/2024
#endregion

using System.ComponentModel.DataAnnotations;

namespace PMart.Models
{
    // Represents a Data Transfer Object (DTO) for an item in the PMart inventory.
    public class ItemDTO
    {
        [Required(ErrorMessage = "The name of the item is required")]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public double Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Item should have atleast 1 quantity")]
        public int Quantity { get; set; }

        public ItemDTO(string name, double price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }
}
