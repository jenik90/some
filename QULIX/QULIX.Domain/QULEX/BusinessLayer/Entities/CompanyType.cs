namespace QULIX.Domain.QULEX.BusinessLayer.Entities
{
    using QULIX.Domain.QULEX.DataLayer.DbEntities;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The company type.
    /// </summary>
    public class CompanyType
    {
        /// <summary>
        /// The company type id.
        /// </summary>
        public int CompanyTypeId { get; set; }

        /// <summary>
        /// The company type name.
        /// </summary>
        [Required(ErrorMessage = "Поле 'Наименование типа' является обязательным для заполнения")]
        public string CompanyTypeName { get; set; }

        /// <summary>
        /// The related companies names.
        /// </summary>
        public IEnumerable<string> RelatedCompanies { get; set; }

        /// <summary>
        /// Converts to database company type.
        /// </summary>
        /// <returns> The <see cref="DbCompany"/>. </returns>
        public DbCompanyType ToDbCompanyType()
        {
            return new DbCompanyType { CompanyTypeId = this.CompanyTypeId, CompanyTypeName = this.CompanyTypeName };
        }
    }
}
