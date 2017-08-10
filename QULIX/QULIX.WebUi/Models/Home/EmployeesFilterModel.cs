namespace QULIX.WebUi.Models.Home
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Domain.QULEX.BusinessLayer.Entities;

    using QULIX.Domain.QULEX.BusinessLayer.Enums;

    public class EmployeesFilterModel
    {
        [Display(Name = "Компания")]
        public int? CompanyId { get; set; }

        [Display(Name = "Должность")]
        public Position? EmployeePosition { get; set; }

        public IEnumerable<Employee> FilterEmloyees(IEnumerable<Employee> parentEmployees)
        {
            return 
                parentEmployees
                .Where(x => this.CompanyId == null 
                    || this.CompanyId == 0 || x.Company.CompanyId == this.CompanyId)
                    .Where(
                        x =>
                            this.EmployeePosition == null
                            || this.EmployeePosition == 0 || x.Position.Equals(this.EmployeePosition));
        }
    }
}