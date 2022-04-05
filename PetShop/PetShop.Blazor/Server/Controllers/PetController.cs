using Microsoft.AspNetCore.Mvc;
using PetShop.Blazor.Shared;
using PetShop.EF.Repos;
using PetShop.Model;

namespace PetShop.Blazor.Server.Controllers
{
    public class PetController
    {
        [ApiController]
        [Route("[controller]")]
        public class PetContoller : ControllerBase
        {
            private readonly IEntityRepo<Pet> _petRepo;

            public PetContoller(IEntityRepo<Pet> petRepo)
            {
                _petRepo = petRepo;
            }

            [HttpGet]
            public async Task<IEnumerable<PetViewModel>> Get()
            {
                var result = await _petRepo.GetAllAsync();
                return result.Select(pet => new PetViewModel
                {
                    Id = pet.ID,
                    Breed = pet.Breed,
                    AnimalType = pet.AnimalType,
                    PetStatus = pet.PetStatus,
                    Price = pet.Price,
                    Cost = pet.Cost,
                });
            }

            [HttpPost]
            public async Task Post(PetViewModel pet)
            {
                var newPet = new Pet()
                {
                    Breed = pet.Breed,
                    AnimalType = pet.AnimalType,
                    PetStatus = pet.PetStatus,
                    Price = pet.Price,
                    Cost = pet.Cost
                };

                await _petRepo.AddAsync(newPet);
            }

            [HttpDelete("{id}")]
            public async Task Delete(Guid id)
            {
                await _petRepo.DeleteAsync(id);
            }

            [HttpPut]
            public async Task<ActionResult> Put(PetViewModel pet)
            {
                var petToUpdate = await _petRepo.GetByIdAsync(pet.Id);
                if (petToUpdate == null) return NotFound();
                
                petToUpdate.Breed = pet.Breed;
                petToUpdate.AnimalType = pet.AnimalType;
                petToUpdate.PetStatus = pet.PetStatus;
                petToUpdate.Price = pet.Price;
                petToUpdate.Cost = pet.Cost;

                await _petRepo.UpdateAsync(pet.Id, petToUpdate);
                return Ok();
            }
        }
    }
}
