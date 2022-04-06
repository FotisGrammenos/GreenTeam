using Microsoft.AspNetCore.Mvc;
using PetShop.Blazor.Shared;
using PetShop.EF.Repos;
using PetShop.Model;

namespace PetShop.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetFoodController : ControllerBase
    {
        private readonly IEntityRepo<PetFood> _petFoodRepo;

        public PetFoodController(IEntityRepo<PetFood> petFoodRepo)
        {
            _petFoodRepo = petFoodRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<PetFoodListViewModel>> Get()
        {
            var result = await _petFoodRepo.GetAllAsync();
            return result.Select(x => new PetFoodListViewModel
            {
                Id = x.ID,
                AnimalType = x.AnimalType,
                Cost = x.Cost,
                Price = x.Price,
            });

        }

        [HttpGet("{id}")]
        public async Task<PetFoodEditViewModel> Get(Guid id)
        {
            PetFoodEditViewModel model = new();
            if (id != Guid.Empty)
            {
                var result = await _petFoodRepo.GetByIdAsync(id);
                model.Id = result.ID;
                model.AnimalType = result.AnimalType;
                model.Cost = result.Cost;
                model.Price = result.Price;
            }
            return model;
        }

        [HttpPost]
        public async Task Post(PetFoodEditViewModel petFood)
        {
            var newPetFood = new PetFood()
            {
                AnimalType = petFood.AnimalType,
                Price = petFood.Price,
                Cost = petFood.Cost,
            };
            await _petFoodRepo.AddAsync(newPetFood);

        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _petFoodRepo.DeleteAsync(id);
        }

        [HttpPut]
        public async Task<ActionResult> Put(PetFoodEditViewModel petFood)
        {
            var petFoodToUpdate = await _petFoodRepo.GetByIdAsync(petFood.Id);
            if (petFoodToUpdate == null) return NotFound();
            petFoodToUpdate.AnimalType = petFood.AnimalType;
            petFoodToUpdate.Cost = petFood.Cost;
            petFoodToUpdate.Price = petFood.Price;

            await _petFoodRepo.UpdateAsync(petFoodToUpdate.ID,petFoodToUpdate);

            return Ok();
        }
    }
}
