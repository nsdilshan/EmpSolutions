using System.ComponentModel.DataAnnotations;

namespace EmpiteSolutions.Models
{
    public class InventoryViewModel
    {
        public int Id { get; set; }
        [Display(Name ="Item Description")]
        public string ItemDescription { get; set; }
        public int Quantity { get; set; }
    }
}
