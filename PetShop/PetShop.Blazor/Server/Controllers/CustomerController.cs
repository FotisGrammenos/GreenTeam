//using Microsoft.AspNetCore.Mvc;
//using PetShop.Blazor.Shared;
//using PetShop.EF.Repos;
//using PetShop.Model;

//namespace PetShop.Blazor.Server.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class CustomerController: ControllerBase
//    {
//        private readonly IEntityRepo<Customer> _customerRepo;

//        public CustomerController(IEntityRepo<Customer> customerRepo)
//        {
//            _customerRepo = customerRepo;
//        }
//        [HttpGet]
//        public async Task<IEnumerable<CustomerListViewModel>> Get()
//        {

//        }
//        [HttpPost]
//        public async Task Post(CustomerListViewModel customer)
//        {

//        }
//        [HttpDelete("{id}")]
//        public async Task Delete(Guid id)
//        {

//        }
//        [HttpPut]
//        public async Task<ActionResult> Put(CustomerListViewModel customer)
//        {

//        }
//    }
//}
