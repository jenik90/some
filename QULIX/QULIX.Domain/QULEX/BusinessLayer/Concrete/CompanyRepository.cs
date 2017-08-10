namespace QULIX.Domain.QULEX.BusinessLayer.Concrete
{
    using System;
    using System.Collections.Generic;

    using QULIX.Domain.QULEX.BusinessLayer.Abstract;
    using QULIX.Domain.QULEX.BusinessLayer.Entities;
    using QULIX.Domain.QULEX.DataLayer.Concrete;
    using System.Linq;

    /// <summary>
    /// The company repository.
    /// </summary>
    public class CompanyRepository : IRepository<Company>, IDisposable
    {
        /// <summary>
        /// The company data retriever.
        /// </summary>
        private readonly CompanyDataRetriever companyDataRetriever = new CompanyDataRetriever();

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns> The <see cref="IEnumerable"/>. </returns>
        public IEnumerable<Company> GetAll()
        {
            return this.companyDataRetriever.SelectAll().Select(tuple =>
            {
                var comp = tuple.Item1.ToCompany();
                comp.CompanyTypeName = tuple.Item2;
                return comp;
            });
        }

        /// <summary>
        /// Gets the company from repository.
        /// </summary>
        /// <param name="id"> The company identifier. </param>
        /// <returns> The <see cref="Company"/> instance. </returns>
        public Company Get(int id)
        {
            var tuple = this.companyDataRetriever.SelectById(id);
            var comp = tuple.Item1.ToCompany();
            comp.CompanyTypeName = tuple.Item2;
            return comp;
        }

        /// <summary>
        /// The find.
        /// </summary>
        /// <param name="predicate"> The predicate. </param>
        /// <returns> The <see cref="IEnumerable"/>. </returns>
        /// <exception cref="NotImplementedException"> </exception>
        public IEnumerable<Company> Find(Func<Company, bool> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="comp"> The company to create. </param>
        /// <returns> Returns true if success, false - otherwise. </returns>
        public bool Create(Company comp)
        {
            return this.companyDataRetriever.InsertItem(comp.ToDbCompany());
        }

        /// <summary>
        /// Updates item in repository.
        /// </summary>
        /// <param name="comp"> The company to update. </param>
        /// <returns> Returns true if success, false - otherwise. </returns>
        public bool Update(Company comp)
        {
            return this.companyDataRetriever.UpdateItem(comp.ToDbCompany());
        }

        /// <summary>
        /// Deletes item from repository.
        /// </summary>
        /// <param name="id"> The identified of deleted company. </param>
        /// <returns> Returns true if success, false - otherwise. </returns>
        public bool Delete(int id)
        {
            return this.companyDataRetriever.DeleteItem(id);
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.companyDataRetriever.Dispose();
        }
    }
}
