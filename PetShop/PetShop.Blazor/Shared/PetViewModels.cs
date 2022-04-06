using PetShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Blazor.Shared
{
    public class PetListViewModel
    {
        public Guid Id { get; set; } 
        public string Breed { get; set; }
        public AnimalType Type { get; set; }
        public PetStatus Status { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
    }

    public class PetEditViewModel
    {
        public Guid Id { get; set; }
        public string Breed { get; set; }
        public AnimalType Type { get; set; }
        public PetStatus Status { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
    }
}
