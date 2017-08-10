namespace QULIX.Domain.QULEX.BusinessLayer.Entities
{
    using System.ComponentModel.DataAnnotations;

    using QULIX.Domain.QULEX.DataLayer.DbEntities;

    /// <summary>
    /// The company.
    /// </summary>
    public class Company
    {
        /// <summary>
        /// Gets or sets the company id.
        /// </summary>
        [Display(Name = "Компания")]
        //[Required(ErrorMessage = "Поле 'Компания' является обязательным для заполнения")]
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        [Display(Name = "Наименование компании")]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the number of stuff.
        /// </summary>
        [Display(Name = "Количество сотрудников")]
        public int NumberOfStuff { get; set; }

        /// <summary>
        /// Gets or sets the company type id.
        /// </summary>
        [Display(Name = "Тип компании")]
        public int CompanyTypeId { get; set; }

        /// <summary>
        /// Gets or sets the company type.
        /// </summary>
        public string CompanyTypeName { get; set; }

        /// <summary>
        /// Converts to <see cref="DbCompany"/> object.
        /// </summary>
        /// <returns> The <see cref="DbCompany"/>. </returns>
        public DbCompany ToDbCompany()
        {
            return new DbCompany { CompanyId = this.CompanyId, CompanyName = this.CompanyName, NumberOfStuff = this.NumberOfStuff, CompanyTypeId = this.CompanyTypeId };
        }
    }
}
