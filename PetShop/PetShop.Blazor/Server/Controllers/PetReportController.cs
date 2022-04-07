using Microsoft.AspNetCore.Mvc;
using PetShop.Blazor.Shared;
using PetShop.EF.Repos;
using PetShop.Model;

namespace PetShop.Blazor.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PetReportController : ControllerBase

    {

        private readonly IEntityRepo<Transaction> _transactionRepo;
        private readonly IEntityRepo<PetReport> _petReportRepo;

        public PetReportController( IEntityRepo<Transaction> transactionRepo, IEntityRepo<PetReport> petReportRepo)
        { 
           
            _transactionRepo = transactionRepo;
            _petReportRepo = petReportRepo;
 
        }

        [HttpGet]
        public async Task<IEnumerable<PetReportViewModel>> Get()
        {
            var result = await _petReportRepo.GetAllAsync();
            return result.Select(x => new PetReportViewModel
            {
                Id = x.ID,
                Month = x.Month,
                Year = x.Year,
                PetType  =x.AnimalType,
                TotalSold = x.TotalSold,   
             
            });
        }

        [HttpPost]
        public async Task Post(PetReportViewModel petReport)
        {
            var animalTypes = Enum.GetValues(typeof(AnimalType));
            foreach(var item in animalTypes)
            {
                var newPetReport = new PetReport()
                {

                    Month = DateTime.Now.Month.ToString(),
                    Year = DateTime.Now.Year.ToString(),
                    AnimalType = (AnimalType)item                   
                };
                newPetReport.TotalSold = await GetTotal(newPetReport.Year,newPetReport.Month, newPetReport.AnimalType);
                await _petReportRepo.AddAsync(newPetReport);

            }

        }


        [HttpPut]
        public async Task<ActionResult> Put(PetReportViewModel petReport)
        {
            var petReportLists = await _petReportRepo.GetAllAsync();
            var itemsToUpdate = petReportLists.Where(ml => ml.Month == petReport.Month &&
                                                            ml.Year == petReport.Year).ToList();
            if (itemsToUpdate == null) return NotFound();
            foreach(var item in itemsToUpdate)
            {
                item.TotalSold = await GetTotal(item.Year, item.Month, item.AnimalType);
                await _petReportRepo.UpdateAsync(item.ID, item);
            }
          
            return Ok();
        }

        //[HttpDelete("{id}")]
        //public async Task Delete(Guid id)
        //{
        //    await _monthlyLedgerRepo.DeleteAsync(id);
        //}

        private async Task<int> GetTotal(string year, string month,AnimalType animalType)
        {
            var counter = 0;
            var transactionList = await _transactionRepo.GetAllAsync();
            foreach (var trans in transactionList.Where(tr => tr.Date.Year.ToString() == year &&
                                                              tr.Date.Month.ToString() == month))
            {
                if( trans.Pet.AnimalType == animalType)
                    counter++;
            }
            return counter;
        }
    }

}


