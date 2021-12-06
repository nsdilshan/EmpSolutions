using System;
using System.Collections.Generic;
using System.Text;

namespace EmpiteDataAccess.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public string ItemDescription { get; set; }
        public int Quantity { get; set; }
    }
}
