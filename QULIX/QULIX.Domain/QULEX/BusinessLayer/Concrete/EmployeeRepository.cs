namespace QULIX.Domain.QULEX.BusinessLayer.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using QULIX.Domain.QULEX.BusinessLayer.Abstract;
    using QULIX.Domain.QULEX.BusinessLayer.Entities;
    using QULIX.Domain.QULEX.DataLayer.Concrete;

    public class EmployeeRepository : IRepository<Employee>
    {
        /// <summary>
        /// The employee data retriever.
        /// </summary>
        private readonly EmployeeDataRetriever employeeDataRetriever = new EmployeeDataRetriever();

        public IEnumerable<Employee> GetAll()
        {
            return this.employeeDataRetriever.SelectAll().Select(tuple =>
                {
                    var empl = tuple.Item1.ToEmployee();
                    empl.Company.CompanyName = tuple.Item2;
                    return empl;
                });
        }

        public Employee Get(int id)
        {
            var tuple = this.employeeDataRetriever.SelectById(id);
            var empl = tuple.Item1.ToEmployee();
            empl.Company.CompanyName = tuple.Item2;
            return empl;
        }

        public IEnumerable<Employee> Find(Func<Employee, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Create(Employee empl)
        {
            return this.employeeDataRetriever.InsertItem(empl.ToDbEmployee());
        }

        public bool Update(Employee empl)
        {
            return this.employeeDataRetriever.UpdateItem(empl.ToDbEmployee());
        }

        public bool Delete(int id)
        {
            return this.employeeDataRetriever.DeleteItem(id);
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.employeeDataRetriever.Dispose();
        }
    }
}
