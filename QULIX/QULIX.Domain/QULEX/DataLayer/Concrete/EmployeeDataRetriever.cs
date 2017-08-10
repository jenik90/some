namespace QULIX.Domain.QULEX.DataLayer.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    using QULIX.Domain.QULEX.Core.Enums;
    using QULIX.Domain.QULEX.Core.Infrastructure;
    using QULIX.Domain.QULEX.DataLayer.Abstract;
    using QULIX.Domain.QULEX.DataLayer.DbEntities;

    /// <summary>
    /// The employee data retriever.
    /// </summary>
    public class EmployeeDataRetriever : SqlDbCrudWorker<DbEmployee, string>
    {
        /// <summary>
        /// The additional column name.
        /// </summary>
        private const string AdditionalColumnName = "CompanyName";

        /// <summary>
        /// The data table name.
        /// </summary>
        private const string DataTableName = "Employees";

        /// <summary>
        /// The queries dictionary.
        /// </summary>
        private readonly IReadOnlyDictionary<CrudQueriesEnum, string> queriesDict =
            CrudQueriesRetriever.GetQueriesDictionary<EmployeeDataRetriever>();

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeDataRetriever"/> class.
        /// </summary>
        public EmployeeDataRetriever()
            : base(ManufactureEmployeeDataTable, DataTableName)
        {
        }

        /// <summary>
        /// The select by id method.
        /// </summary>
        /// <param name="id"> The id. </param>
        /// <returns> The <see cref="Tuple"/>. </returns>
        public override Tuple<DbEmployee, string> SelectById(int id)
        {
            var query = this.queriesDict[CrudQueriesEnum.SelectByIdQueryTmpl];
            var row = this.DbConn.ExecuteSelectQuery(query, new SqlParameter { Value = id }).Rows.Cast<DataRow>().First();
            return new Tuple<DbEmployee, string>(GetDbEntityFromDataRow(row), row.Field<string>(AdditionalColumnName));
        }

        /// <summary>
        /// The select all method.
        /// </summary>
        /// <returns> The <see cref="IEnumerable"/>. </returns>
        public override IEnumerable<Tuple<DbEmployee, string>> SelectAll()
        {
            var table = this.DbConn.ExecuteSelectQuery(this.queriesDict[CrudQueriesEnum.SelectAllItemsQuery]);
            var resultList = new List<Tuple<DbEmployee, string>>();
            foreach (var row in table.Rows.Cast<DataRow>())
            {
                resultList.Add(new Tuple<DbEmployee, string>(GetDbEntityFromDataRow(row), row.Field<string>(AdditionalColumnName)));
            }

            return resultList.Count > 0 ? resultList : null;
        }

        /// <summary>
        /// The insert item.
        /// </summary>
        /// <param name="dbEmpl"> The database employee entity. </param>
        /// <returns> Returns true if success, false - otherwise </returns>
        public override bool InsertItem(DbEmployee dbEmpl)
        {
            return this.DbConn.ExecuteInsertQuery(
                this.queriesDict[CrudQueriesEnum.InsertItemQueryTmpl],
                new SqlParameter { Value = dbEmpl.FirstName },
                new SqlParameter { Value = dbEmpl.LastName },
                new SqlParameter { Value = dbEmpl.MiddleName },
                new SqlParameter { Value = dbEmpl.HiringDate },
                new SqlParameter { Value = dbEmpl.PositionId },
                new SqlParameter { Value = dbEmpl.CompanyId });
        }

        /// <summary>
        /// The update item.
        /// </summary>
        /// <param name="dbEmpl"> The database employee entity. </param>
        /// <returns> Returns true if success, false - otherwise. </returns>
        public override bool UpdateItem(DbEmployee dbEmpl)
        {
            return this.DbConn.ExecuteUpdateQuery(
                this.queriesDict[CrudQueriesEnum.UpdateItemQueryTmpl],
                new SqlParameter { Value = dbEmpl.FirstName },
                new SqlParameter { Value = dbEmpl.LastName },
                new SqlParameter { Value = dbEmpl.MiddleName },
                new SqlParameter { Value = dbEmpl.HiringDate },
                new SqlParameter { Value = dbEmpl.PositionId },
                new SqlParameter { Value = dbEmpl.CompanyId },
                new SqlParameter { Value = dbEmpl.EmployeeId });
        }

        /// <summary>
        /// The delete item method.
        /// </summary>
        /// <param name="id"> The database entity identifier. </param>
        /// <returns> Returns true if success, false - otherwise </returns>
        public override bool DeleteItem(int id)
        {
            return this.DbConn.ExecuteDeleteQuery(this.queriesDict[CrudQueriesEnum.DeleteItemQueryTmpl], new SqlParameter { Value = id });
        }

        /// <summary>
        /// The manufacture employee data table.
        /// </summary>
        /// <param name="ds"> The dataset. </param> 
        /// <param name="dataTableName"> The data table name. </param>
        private static void ManufactureEmployeeDataTable(DataSet ds, string dataTableName)
        {
            var employees = new DataTable(dataTableName);
            var dbEmpl = new DbEmployee();
            employees.Columns.Add(new DataColumn(dbEmpl[0], typeof(int)));
            employees.Columns.Add(new DataColumn(dbEmpl[1], typeof(string)));
            employees.Columns.Add(new DataColumn(dbEmpl[2], typeof(string)));
            employees.Columns.Add(new DataColumn(dbEmpl[3], typeof(string)));
            employees.Columns.Add(new DataColumn(dbEmpl[4], typeof(DateTime)));
            employees.Columns.Add(new DataColumn(dbEmpl[5], typeof(int)));
            employees.Columns.Add(new DataColumn(dbEmpl[6], typeof(int)));
            employees.Columns.Add(new DataColumn(AdditionalColumnName, typeof(string)));

            ds.Tables.Add(employees);
        }

        /// <summary>
        /// Returns database entity from data row.
        /// </summary>
        /// <param name="row"> The row. </param>
        /// <returns> The <see cref="DbEmployee"/>. </returns>
        private static DbEmployee GetDbEntityFromDataRow(DataRow row)
        {
            var dbEmpl = new DbEmployee();
            dbEmpl.EmployeeId = row.Field<int>(dbEmpl[0]);
            dbEmpl.FirstName = row.Field<string>(dbEmpl[1]);
            dbEmpl.LastName = row.Field<string>(dbEmpl[2]);
            dbEmpl.MiddleName = row.Field<string>(dbEmpl[3]);
            dbEmpl.HiringDate = row.Field<DateTime>(dbEmpl[4]);
            dbEmpl.PositionId = row.Field<int>(dbEmpl[5]);
            dbEmpl.CompanyId = row.Field<int>(dbEmpl[6]);
            return dbEmpl;
        }
    }
}
