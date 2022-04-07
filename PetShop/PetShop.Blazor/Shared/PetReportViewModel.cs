using PetShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Blazor.Shared
{
    public class PetReportViewModel
    {
        public Guid Id { get; set; }
        public AnimalType PetType { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public int TotalSold { get; set; }


    }
}
