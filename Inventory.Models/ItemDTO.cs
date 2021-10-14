using Inventory.Models.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Models
{
    [Table(name: "Items", Schema = "dbo")]
    public class ItemDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [NonUpdatable]
        public Guid id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public double price { get; set; }

        public int count { get; set; }

        [NonUpdatable]
        public DateTime updatedOn { get; set; }

        [NonUpdatable]
        public DateTime createdOn { get; set; }
    }
}

