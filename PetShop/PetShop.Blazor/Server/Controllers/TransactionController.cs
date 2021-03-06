using Microsoft.AspNetCore.Mvc;
using PetShop.Blazor.Shared;
using PetShop.EF.Repos;
using PetShop.Model;

namespace PetShop.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController
    {
        private readonly IEntityRepo<Transaction> _transactionRepo;
        private readonly IEntityRepo<Pet> _petRepo;
        public TransactionController(IEntityRepo<Transaction> transactionRepo, IEntityRepo<Pet> petRepo)
        {
            _transactionRepo = transactionRepo;
            _petRepo = petRepo;
        }
        [HttpGet]
        public async Task<IEnumerable<TransactionListViewModel>> Get()
        {
            var result = await _transactionRepo.GetAllAsync();
            return result.Select(x => new TransactionListViewModel
            {
                ID = x.ID,
                Date = x.Date,
                CustomerName = $"{x.Customer.Name} {x.Customer.Surname}",
                EmployeeName = $"{x.Employee.Name} {x.Customer.Surname}",
                PetPrice = x.Pet.Price,
                PetFoodQty = x.PetFoodQty,
                PetFoodPrice = x.PetFood.Price,
                TotalPrice = x.TotalPrice,
                Breed = x.Pet.Breed
            });
        }
        [HttpGet("{id}")]
        public async Task<TransactionEditViewModel> Get(Guid id)
        {
            TransactionEditViewModel model = new();
            if (id != Guid.Empty)
            {
                var existing = await _transactionRepo.GetByIdAsync(id);
                model.ID = existing.ID;
                model.Date = existing.Date;
                model.PetPrice = existing.Pet.Price;
                model.PetFoodQty = existing.PetFoodQty;
                model.PetFoodPrice = existing.PetFood.Price;
                model.TotalPrice = existing.TotalPrice;
            }

            return model;
        }

        [HttpPost]
        public async Task Post(TransactionEditViewModel transaction)
        {
            var pet = await _petRepo.GetByIdAsync(transaction.PetID);
            pet.PetStatus = PetStatus.Sold;
            var newTransaction = new Transaction()
            {
                Date= transaction.Date,
                CustomerID = transaction.CustomerID,
                EmployeeID = transaction.EmployeeID,
                PetID = transaction.PetID,
                PetPrice = transaction.PetPrice,
                PetFoodID = transaction.PetFoodID,
                PetFoodQty = transaction.PetFoodQty,
                PetFoodPrice = transaction.PetFoodPrice,
                TotalPrice = transaction.TotalPrice,
            };

            await _transactionRepo.AddAsync(newTransaction);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            var transaction = await _transactionRepo.GetByIdAsync(id);
            transaction.Pet.PetStatus = PetStatus.OK;
            await _transactionRepo.DeleteAsync(id);
        }
    }
}
