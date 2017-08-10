namespace QULIX.Domain.QULEX.BusinessLayer.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Attributes.ValidationAttributes;

    using Core.Attributes.ValidationAttributes;

    using QULIX.Domain.QULEX.BusinessLayer.Enums;
    using QULIX.Domain.QULEX.DataLayer.DbEntities;

    /// <summary>
    /// The employee.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets the employee id.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Поле 'Имя' является обязательным для заполнения")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Поле 'Фамилия' является обязательным для заполнения")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the middle name.
        /// </summary>
        [Display(Name = "Отчество")]
        [Required(ErrorMessage = "Поле 'Отчество' является обязательным для заполнения")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the hiring date.
        /// </summary>
        [Display(Name = "Дата приема на работу")]
        [DataType(DataType.Date, ErrorMessage = "Введите дату в формате MM/dd/yyyy, например 12/22/2010")] // this common attribute I need for client side validation
        [TodayOrDateInPast(ErrorMessage = "Введите дату в прошлом или сегодняшнюю в формате MM/dd/yyyy")] // this custom attribute I need for server side validation, it is derived from 'RequiredAttribute' but adds date specific validation
        public DateTime HiringDate { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        [Display(Name = "Должность")]
        [Required(ErrorMessage = "Поле 'Должность' является обязательным для заполнения")]  // this common attribute I need for client side validation  
        [EnumValidate(typeof(Position), ErrorMessage = "Выберите значение для должности из списка")]  // this custom attribute I need for server side validation, it is derived from 'RequiredAttribute' but adds enum specific validation
        // this custom validation attribute is necessary for server side validation, as DefaultModelBinder sets 0 for this field 
        public Position Position { get; set; }

        /// <summary>
        /// Gets or sets the company.
        /// </summary>
        [EmployeeCompanyIdMustNotBeZero(ErrorMessage = "Выберите компанию из списка")]
        // this custom validation attribute is necessary for server side validation, as DefaultModelBinder sets 0 for Company.CompanyId 
        public Company Company { get; set; }

        /// <summary>
        /// Convert to <see cref="DbEmployee"/> object.
        /// </summary>
        /// <returns> The <see cref="DbEmployee"/>. </returns>
        public DbEmployee ToDbEmployee()
        {
            return new DbEmployee { FirstName = this.FirstName, LastName = this.LastName, MiddleName = this.MiddleName, EmployeeId = this.EmployeeId, PositionId = (int)this.Position, HiringDate = this.HiringDate, CompanyId = this.Company.CompanyId };
        }
    }
}
