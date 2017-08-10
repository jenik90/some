namespace QULIX.Domain.QULEX.DataLayer.DbEntities
{
    using System.Collections.Generic;

    using QULIX.Domain.QULEX.BusinessLayer.Entities;

    /// <summary>
    /// The database company entity.
    /// </summary>
    public class DbCompany
    {
        /// <summary>
        /// The column names.
        /// </summary>
        private static readonly IList<string> ColumnNames =
           new[] { "CompanyId", "CompanyName", "NumberOfStuff", "CompanyTypeId" };

        /// <summary>
        /// Gets or sets the company id.
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the number of stuff.
        /// </summary>
        public int NumberOfStuff { get; set; }

        /// <summary>
        /// Gets or sets the company type id.
        /// </summary>
        public int CompanyTypeId { get; set; }

        /// <summary>
        /// The indexer that is always the same for all objects of type <see cref="DbCompany"/>
        /// </summary>
        /// <param name="index"> The index. </param>
        /// <returns> The <see cref="string"/>. </returns>
        public string this[int index] => ColumnNames[index];

        /// <summary>
        /// The to employee converting method.
        /// </summary>
        /// <returns> The <see cref="Employee"/>. </returns>
        public Company ToCompany()
        {
            return new Company { CompanyId = this.CompanyId, CompanyName = this.CompanyName, NumberOfStuff = this.NumberOfStuff, CompanyTypeId = this.CompanyTypeId};
        }
    }
}
