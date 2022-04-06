using Microsoft.AspNetCore.Mvc;
using PetShop.Blazor.Shared;
using PetShop.EF.Repos;
using PetShop.Model;

namespace PetShop.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IEntityRepo<Customer> _customerRepo;

        public CustomerController(IEntityRepo<Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }
        [HttpGet]
        public async Task<IEnumerable<CustomerListViewModel>> Get()
        {
            var result = await _customerRepo.GetAllAsync();
            return result.Select(x => new CustomerListViewModel
            {
                Id = x.ID,
                Name = x.Name,
                Surname = x.Surname,
                Phone = x.Phone,
                TIN = x.TIN,
            });
        }
        [HttpGet("{id}")]
        public async Task<CustomerEditViewModel> Get(Guid id)
        {
            CustomerEditViewModel model = new();
            if (id != Guid.Empty)
            {
                var existing = await _customerRepo.GetByIdAsync(id);
                model.Id = existing.ID;
                model.Name = existing.Name;
                model.Surname = existing.Surname;
                model.Phone = existing.Phone;
                model.TIN = existing.TIN;
               
            }

            return model;
        }
        [HttpPost]
        public async Task Post(CustomerEditViewModel customer)
        {
            var newCustomer = new Customer(customer.Name,customer.Surname,customer.Phone,customer.TIN);
            
            await _customerRepo.AddAsync(newCustomer);
        }
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _customerRepo.DeleteAsync(id);
        }
        [HttpPut]
        public async Task<ActionResult> Put(CustomerEditViewModel customer)
        {
            var itemToUpdate = await _customerRepo.GetByIdAsync(customer.Id);
            if (itemToUpdate == null) return NotFound();
            itemToUpdate.Name = customer.Name;
            itemToUpdate.Surname = customer.Surname;
            itemToUpdate.Phone = customer.Phone;
            itemToUpdate.TIN = customer.TIN;

            await _customerRepo.UpdateAsync(customer.Id, itemToUpdate);

            return Ok();
        }
    }
}
