using Microsoft.AspNetCore.Mvc;
using PetShop.Blazor.Shared;
using PetShop.EF.Repos;
using PetShop.Model;

namespace PetShop.Blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonthlyLedgerController : ControllerBase
    {
        private readonly IEntityRepo<MonthlyLedger> _monthlyLedgerRepo;
        private readonly IEntityRepo<Employee> _employeeRepo;
        private readonly IEntityRepo<Pet> _petRepo;
        private readonly IEntityRepo<Transaction> _transactionRepo;
        private readonly IEntityRepo<PetFood> _petFood;

        private const int _RENT_COST = 2000;

        public MonthlyLedgerController(IEntityRepo<MonthlyLedger> monthRepo, IEntityRepo<Employee> employeeRepo, IEntityRepo<Transaction> transactionRepo, IEntityRepo<Pet> petRepo, IEntityRepo<PetFood> petFoodRepo)
        {
            _monthlyLedgerRepo = monthRepo;
            _petRepo=petRepo;
            _transactionRepo=transactionRepo;
            _employeeRepo=employeeRepo;
            _petFood=petFoodRepo;
        }

        [HttpGet]
        public async Task<IEnumerable<MonthlyLedgerListViewMondel>> Get()
        {
            var result = await _monthlyLedgerRepo.GetAllAsync();
            return result.Select(x => new MonthlyLedgerListViewMondel
            {
                Id = x.ID,
                Month = x.Month,
                Year = x.Year,
                Expenses = x.Expenses,
                Income = x.Income,
                Total = x.Total,
            });
        }

        [HttpPost]
        public async Task Post(MonthlyLedgerListViewMondel monthlyLedger)
        {
            var newMonthlyLedger = new MonthlyLedger()
            {
                ID = monthlyLedger.Id,
                Month = monthlyLedger.Month,
                Year = monthlyLedger.Year,
                Expenses =await GetMonthlyExpenses(monthlyLedger),
                Income =await GetIncome(monthlyLedger),
                Total = await GetTotal(monthlyLedger)
            };
            await _monthlyLedgerRepo.AddAsync(newMonthlyLedger);

        }


        [HttpPut]
        public async Task<ActionResult> Put(MonthlyLedgerListViewMondel monthlyLedger)
        {
            var itemToUpdate = await _monthlyLedgerRepo.GetByIdAsync(monthlyLedger.Id);
            if (itemToUpdate == null) return NotFound();
            itemToUpdate.Expenses = await GetMonthlyExpenses(monthlyLedger);
            itemToUpdate.Income = await GetIncome(monthlyLedger);
            itemToUpdate.Total = await GetTotal(monthlyLedger);

            await _monthlyLedgerRepo.UpdateAsync(monthlyLedger.Id, itemToUpdate);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _monthlyLedgerRepo.DeleteAsync(id);
        }

        private async Task<decimal> GetPetAndPetFoodExpences(MonthlyLedgerListViewMondel monthlyLedger)
        {
            var tmpTransactionList=await _transactionRepo.GetAllAsync();
            var monthlyTransactions = tmpTransactionList.Where(t => t.Date.Year.ToString() == monthlyLedger.Year && t.Date.Month.ToString() == monthlyLedger.Month).ToList();
            decimal expences = 0;
            foreach (var t in monthlyTransactions)
            {
                var pet = await _petRepo.GetByIdAsync(t.PetID);
                var petFood =await _petFood.GetByIdAsync(t.PetFoodID);
                expences += pet.Cost + (petFood.Cost * t.PetFoodQty);
            }
            return expences;
        }

        public async Task<decimal> GetIncome(MonthlyLedgerListViewMondel monthlyLedger)
        {
            var tmpTransactionList = await _transactionRepo.GetAllAsync();
            var monthlyTransactions = tmpTransactionList.Where(t => t.Date.Year.ToString() == monthlyLedger.Year && t.Date.Month.ToString() == monthlyLedger.Month).ToList();
            var Income = monthlyTransactions.Sum(t => t.TotalPrice);
            return Income;
        }

        private async Task<decimal> GetStuffExpences()
        {
            var employees = await _employeeRepo.GetAllAsync();
            return employees.Sum(t => t.SallaryPerMonth);
        }

        private async Task<decimal> GetMonthlyExpenses(MonthlyLedgerListViewMondel monthlyLedger)
        {
            return await GetPetAndPetFoodExpences(monthlyLedger) + await GetStuffExpences() + _RENT_COST;
        }

        private async Task<decimal> GetTotal(MonthlyLedgerListViewMondel monthlyLedger)
        {
            return await GetIncome(monthlyLedger) - await GetMonthlyExpenses(monthlyLedger);
        }

    }
}
