using Microsoft.AspNetCore.Mvc;
using PetShop.Blazor.Shared;
using PetShop.EF.Repos;
using PetShop.Model;

namespace PetShop.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEntityRepo<Employee> _employeeRepo;

        public EmployeeController(IEntityRepo<Employee> employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }
        [HttpGet]
        public async Task<IEnumerable<EmployeeListViewModel>> Get()
        {
            var result = await _employeeRepo.GetAllAsync();
            return result.Select(x => new EmployeeListViewModel
            {
                Id = x.ID,
                Name = x.Name,
                Surname = x.Surname,
                SallaryPerMonth = x.SallaryPerMonth,
                EmployeeType = x.EmployeeType
            });

        }
        [HttpPost]
        public async Task Post(EmployeeListViewModel employee)
        {
            var newEmployee = new Employee(employee.Name, employee.Surname, employee.SallaryPerMonth, employee.EmployeeType);
            await _employeeRepo.AddAsync(newEmployee);
        }
        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _employeeRepo.DeleteAsync(id);
        }
        [HttpPut]
        public async Task<ActionResult> Put(EmployeeListViewModel employee)
        {
            var itemToUpdate = await _employeeRepo.GetByIdAsync(employee.Id);
            if (itemToUpdate == null) return NotFound();
            itemToUpdate.Name = employee.Name;
            itemToUpdate.Surname = employee.Surname;
            itemToUpdate.SallaryPerMonth = employee.SallaryPerMonth;
            itemToUpdate.EmployeeType = employee.EmployeeType;

            await _employeeRepo.UpdateAsync(employee.Id, itemToUpdate);

            return Ok();
        }
    }
}
