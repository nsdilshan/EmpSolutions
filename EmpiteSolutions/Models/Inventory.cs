using System.ComponentModel.DataAnnotations;

namespace EmpiteSolutions.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string ItemDescription { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
