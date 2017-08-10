namespace QULIX.WebUi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using Domain.QULEX.BusinessLayer.Abstract;
    using Domain.QULEX.BusinessLayer.Entities;

    public class CompanyController : ApiController
    {
        private readonly IRepository<Company> companyRepo;

        public CompanyController(IRepository<Company> companyRepo)
        {
            this.companyRepo = companyRepo;
        }

        public IEnumerable<Company> GetAllCompanies()
        {
            return companyRepo.GetAll();
        }

        public Company GetCompany(int id)
        {
            return companyRepo.Get(id);
        }

        [HttpPost]
        public bool CreateCompany(Company item)
        {
            return companyRepo.Create(item);
        }

        [HttpPut]
        public bool UpdateCompany(Company item)
        {
            return companyRepo.Update(item);
        }

        public bool DeleteCompany(int id)
        {
            return this.companyRepo.Delete(id);
        }

        protected override void Dispose(bool disposing)
        {
            this.companyRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}
