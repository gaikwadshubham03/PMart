#region Header
// Author:      Shubham Gaikwad
// Date:        06/01/2024
#endregion

using System.ComponentModel.DataAnnotations;

namespace PMart.Models
{
    public class ItemDTO
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public ItemDTO(string name, double price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
    }
}
