using System;
using System.ComponentModel.DataAnnotations;

namespace Inventory.ViewModels
{
    public class Item
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Item name is required.")]
        [MaxLength(200, ErrorMessage = "Item name should be upto 200 characters.")]
        public string name { get; set; }

        [Required(ErrorMessage = "Item description is required.")]
        [MaxLength(500, ErrorMessage = "Item description should be upto 500 characters.")]
        public string description { get; set; }

        [Required(ErrorMessage = "Item price is required.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Please enter a price greater than 0.1.")]
        public double price { get; set; }

        [Required(ErrorMessage = "Item count is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a item count greater than 0.")]
        public int count { get; set; }
    }
}
