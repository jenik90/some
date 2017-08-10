namespace QULIX.Domain.QULEX.DataLayer.DbEntities
{
    using BusinessLayer.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// The database company type entity.
    /// </summary>
    public class DbCompanyType
    {
        /// <summary>
        /// The column names.
        /// </summary>
        private static readonly IList<string> ColumnNames =
            new[] { "CompanyTypeId", "CompanyTypeName" };

        /// <summary>
        /// Gets or sets the company type id.
        /// </summary>
        public int CompanyTypeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyTypeName { get; set; }

        /// <summary>
        /// The indexer that is always the same for all objects of type <see cref="DbCompanyType"/>
        /// </summary>
        /// <param name="index"> The index. </param>
        /// <returns> The <see cref="string"/>. </returns>
        public string this[int index] => ColumnNames[index];

        /// <summary>
        /// The to employee converting method.
        /// </summary>
        /// <returns> The <see cref="Employee"/>. </returns>
        public CompanyType ToCompanyType()
        {
            return new CompanyType { CompanyTypeId = this.CompanyTypeId, CompanyTypeName = this.CompanyTypeName };
        }
    }
}
