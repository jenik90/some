namespace QULIX.Domain.QULEX.BusinessLayer.Concrete
{
    using DataLayer.Concrete;
    using Entities;
    using QULIX.Domain.QULEX.BusinessLayer.Abstract;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The company type repository.
    /// </summary>
    public class CompanyTypeRepository : IRepository<CompanyType>
    {
        /// <summary>
        /// The company data retriever.
        /// </summary>
        private readonly CompanyTypeDataRetriever companyTypeDataRetriever = new CompanyTypeDataRetriever();

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns> The <see cref="IEnumerable"/>. </returns>
        public IEnumerable<CompanyType> GetAll()
        {
            return this.companyTypeDataRetriever.SelectAll().Select(tuple =>
            {
                var compType = tuple.Item1.ToCompanyType();
                compType.RelatedCompanies = tuple.Item2;
                return compType;
            });
        }

        /// <summary>
        /// Gets the company from repository.
        /// </summary>
        /// <param name="id"> The company identifier. </param>
        /// <returns> The <see cref="CompanyType"/> instance. </returns>
        public CompanyType Get(int id)
        {
            var tuple = this.companyTypeDataRetriever.SelectById(id);
            var compType = tuple.Item1.ToCompanyType();
            compType.RelatedCompanies = tuple.Item2;
            return compType;
        }

        /// <summary>
        /// The find.
        /// </summary>
        /// <param name="predicate"> The predicate. </param>
        /// <returns> The <see cref="IEnumerable"/>. </returns>
        /// <exception cref="NotImplementedException"> </exception>
        public IEnumerable<CompanyType> Find(Func<CompanyType, bool> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="compType"> The company to create. </param>
        /// <returns> Returns true if success, false - otherwise. </returns>
        public bool Create(CompanyType compType)
        {
            return this.companyTypeDataRetriever.InsertItem(compType.ToDbCompanyType());
        }

        /// <summary>
        /// Updates item in repository.
        /// </summary>
        /// <param name="comp"> The company to update. </param>
        /// <returns> Returns true if success, false - otherwise. </returns>
        public bool Update(CompanyType comp)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes item from repository.
        /// </summary>
        /// <param name="id"> The identified of deleted company. </param>
        /// <returns> Returns true if success, false - otherwise. </returns>
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.companyTypeDataRetriever.Dispose();
        }
    }
}
