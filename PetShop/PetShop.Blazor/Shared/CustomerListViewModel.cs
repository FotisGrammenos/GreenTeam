using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Blazor.Shared
{
    public class CustomerListViewModel
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string TIN { get; set; }
    }
}
