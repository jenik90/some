namespace QULIX.Domain.QULEX.DataLayer.DbEntities
{
    using System;
    using System.Collections.Generic;

    using QULIX.Domain.QULEX.BusinessLayer.Entities;
    using QULIX.Domain.QULEX.BusinessLayer.Enums;

    /// <summary>
    /// The database employee.
    /// </summary>
    public class DbEmployee
    {
        /// <summary>
        /// The column names.
        /// </summary>
        private static readonly IList<string> ColumnNames =
            new[] { "EmployeeId", "FirstName", "LastName", "MiddleName", "HiringDate", "PositionId", "CompanyId" };

        /// <summary>
        /// Gets or sets the employee id.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the middle name.
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the hiring date.
        /// </summary>
        public DateTime HiringDate { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// Gets or sets the company.
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// The indexer that is always the same for all objects of type <see cref="DbEmployee"/>
        /// </summary>
        /// <param name="index"> The index. </param>
        /// <returns> The <see cref="string"/>. </returns>
        public string this[int index] => ColumnNames[index];

        /// <summary>
        /// The to employee converting method.
        /// </summary>
        /// <returns> The <see cref="Employee"/>. </returns>
        public Employee ToEmployee()
        {
            return new Employee { FirstName = this.FirstName, LastName = this.LastName, MiddleName = this.MiddleName, EmployeeId = this.EmployeeId, Position = (Position)this.PositionId, HiringDate = this.HiringDate, Company = new Company { CompanyId = this.CompanyId } };
        }
    }
}
