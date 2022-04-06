using PetShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Blazor.Shared
{
    public class EmployeeListViewModel{
        public Guid Id;

        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal SallaryPerMonth { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }

    public class EmployeeEditViewModel
    {
        public Guid Id;
        public string Name { get; set; }
        public string Surname { get; set; }
        public decimal SallaryPerMonth { get; set; }
        public EmployeeType EmployeeType { get; set; }

    }
}
