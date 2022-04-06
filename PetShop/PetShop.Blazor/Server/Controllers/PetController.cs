using Microsoft.AspNetCore.Mvc;
using PetShop.Blazor.Shared;
using PetShop.EF.Repos;
using PetShop.Model;

namespace PetShop.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetController : ControllerBase
    {
        private readonly IEntityRepo<Pet> _petRepo;

        public PetController(IEntityRepo<Pet> petRepo)
        {
            _petRepo = petRepo;
        }
        [HttpGet]
        public async Task<IEnumerable<PetListViewModel>> Get()
        {
            var result = await _petRepo.GetAllAsync();
            return result.Select(x => new PetListViewModel
            {
                Id = x.ID,
                Breed = x.Breed,
                Type = x.AnimalType,
                Status = x.PetStatus,
                Cost = x.Cost,
                Price = x.Price
            });
        }

        [HttpGet("{id}")]
        public async Task<PetEditViewModel> Get(Guid id)
        {
            PetEditViewModel model = new();
            if(id != Guid.Empty)
            {
                var result = await _petRepo.GetByIdAsync(id);
                model.Id = result.ID;
                model.Breed = result.Breed;
                model.Type = result.AnimalType;
                model.Status = result.PetStatus;
                model.Cost = result.Cost;
                model.Price = result.Price;
            }

            return model;
        }

        [HttpPost]
        public async Task Post(PetEditViewModel pet)
        {
            var newPet = new Pet()
            {
                Breed = pet.Breed,
                AnimalType = pet.Type,
                PetStatus = pet.Status,
                Cost = pet.Cost,
                Price = pet.Price
            };
            
            await _petRepo.AddAsync(newPet);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _petRepo.DeleteAsync(id);
        }

        [HttpPut]
        public async Task<ActionResult> Put(PetEditViewModel pet)
        {
            var itemToUpdate = await _petRepo.GetByIdAsync(pet.Id);
            if (itemToUpdate == null) return NotFound();
            itemToUpdate.Breed = pet.Breed;
            itemToUpdate.AnimalType = pet.Type;
            itemToUpdate.PetStatus = pet.Status;
            itemToUpdate.Cost = pet.Cost;
            itemToUpdate.Price = pet.Price;

            await _petRepo.UpdateAsync(pet.Id, itemToUpdate);

            return Ok();
        }
    }
}
