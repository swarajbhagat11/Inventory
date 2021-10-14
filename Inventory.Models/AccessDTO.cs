using Inventory.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Models
{
    [Table(name: "Access", Schema = "dbo")]
    public class AccessDTO
    {
        [Key]
        public Guid id { get; set; }

        public string accessId { get; set; }

        public string secretKey { get; set; }

        public Roles role { get; set; }

        public DateTime updatedOn { get; set; }
        
        public DateTime createdOn { get; set; }
    }
}
