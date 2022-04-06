//using Microsoft.AspNetCore.Mvc;
//using PetShop.Blazor.Shared;
//using PetShop.EF.Repos;
//using PetShop.Model;

//namespace PetShop.Blazor.Server.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class TransactionController
//    {
//        private readonly IEntityRepo<Transaction> _transactionRepo;
//        public TransactionController(IEntityRepo<Transaction> transactionRepo)
//        {
//            _transactionRepo = transactionRepo;
//        }
//        [HttpGet]
//        public async Task<IEnumerable<TransactionListViewModel>> Get()
//        {
//            var result = await _transactionRepo.GetAllAsync();
//            return result.Select(x => new TransactionListViewModel
//            {
//                ID = x.ID,
//                Date = x.Date,
//                CustomerID = x.CustomerID,
//                CustomerName = x.Customer.Name,
//                EmployeeID = x.EmployeeID,
//                EmployeeName = x.Employee.Name,
//                PetId = x.PetID,
//                PetPrice = x.PetPrice,
//                PetFoodID = x.PetFoodID,
//                PetFoodQty = x.PetFoodQty,
//                PetFoodPrice=x.PetFoodPrice,
//                TotalPrice = x.TotalPrice,
//            });
//        }
//        public async Task<TransactionListViewModel> Get(Guid id)
//        {
//            TransactionListViewModel model = new();
//            if (id != Guid.Empty)
//            {
//                var existing = await _transactionRepo.GetByIdAsync(id);
//                model.ID = existing.ID;
//                model.Date = existing.Date;
//                model.CustomerID = existing.CustomerID;
//                model.CustomerName = existing.Customer.Name;
//                model.EmployeeID = existing.EmployeeID;
//                model.EmployeeName = existing.Employee.Name;
//                model.PetId = existing.PetID;
//                model.PetPrice = existing.PetPrice;
//                model.PetFoodID = existing.PetFoodID;
//                model.PetFoodQty = existing.PetFoodQty;
//                model.PetFoodPrice = existing.PetFoodPrice;
//                model.TotalPrice = existing.TotalPrice;
//            }

//            return model;
//        }
//        [HttpPost]
//        public async Task Post(TransactionListViewModel transaction)
//        {
//            var newTransaction = new Transaction(transaction.Name, customer.Surname, customer.Phone, customer.TIN);

//            await _customerRepo.AddAsync(newCustomer);
//        }
//    }
//}
