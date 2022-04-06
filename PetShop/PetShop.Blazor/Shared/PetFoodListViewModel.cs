using PetShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Blazor.Shared
{
    public class PetFoodListViewModel
    {
        public Guid Id { get; set; }
        public AnimalType AnimalType { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
    }

    public class PetFoodEditViewModel
    {
        public Guid Id { get; set; }
        public AnimalType AnimalType { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
    }
}
