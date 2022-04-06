using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Blazor.Shared
{
    public class TransactionListViewModel
    {
        public Guid ID { get; set; }
        public DateTime Date { get; set; }= DateTime.Now;
        public string Breed { get; set; }
        public string CustomerName { get; set; }
        public string EmployeeName { get; set; }
        public decimal PetPrice { get; set; }
        public int PetFoodQty { get; set; }
        public decimal PetFoodPrice { get; set; }
        public decimal TotalPrice { get; set; }

    }
    public class TransactionEditViewModel
    {
        public Guid ID { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public Guid CustomerID { get; set; }
        public Guid EmployeeID { get; set; }
        public Guid PetID { get; set; }
        public decimal PetPrice { get; set; }
        public Guid PetFoodID { get; set; }
        public int PetFoodQty { get; set; }
        public decimal PetFoodPrice { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
// Date, CustomerID, EmployeeID, PetID, PetPrice,
//PetFoodID, PetFoodQty, PetFoodPrice, Total Price)
