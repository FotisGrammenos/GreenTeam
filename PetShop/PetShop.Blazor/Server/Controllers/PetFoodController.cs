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

        [HttpPost]
        public async Task Post(PetFoodListViewModel petFood)
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
        public async Task<ActionResult> Put(PetFoodListViewModel petFood)
        {
            var petFoodToUpdate = await _petFoodRepo.GetByIdAsync(petFood.Id);
            if (petFoodToUpdate == null) return NotFound();
            petFoodToUpdate.AnimalType = petFoodToUpdate.AnimalType;
            petFoodToUpdate.Cost = petFoodToUpdate.Cost;
            petFoodToUpdate.Price = petFoodToUpdate.Price;

            await _petFoodRepo.AddAsync(petFoodToUpdate);

            return Ok();
        }
    }
}
