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
    /// The company data retriever.
    /// </summary>
    public class CompanyDataRetriever : SqlDbCrudWorker<DbCompany, string>
    {
        /// <summary>
        /// The additional column name.
        /// </summary>
        private const string AdditionalColumnName = "CompanyTypeName";

        /// <summary>
        /// The data table name.
        /// </summary>
        private const string DataTableName = "Companies";

        /// <summary>
        /// The queries dictionary.
        /// </summary>
        private readonly IReadOnlyDictionary<CrudQueriesEnum, string> queriesDict =
            CrudQueriesRetriever.GetQueriesDictionary<CompanyDataRetriever>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyDataRetriever"/> class.
        /// </summary>
        public CompanyDataRetriever()
            : base(ManufactureCompanyDataTable, DataTableName)
        {
        }

        /// <summary>
        /// The select by id method.
        /// </summary>
        /// <param name="id"> The id. </param>
        /// <returns> The <see cref="Tuple"/>. </returns>
        public override Tuple<DbCompany, string> SelectById(int id)
        {
            var query = this.queriesDict[CrudQueriesEnum.SelectByIdQueryTmpl];
            var row = this.DbConn.ExecuteSelectQuery(query, new SqlParameter { Value = id }).Rows.Cast<DataRow>().First();
            return new Tuple<DbCompany, string>(GetDbEntityFromDataRow(row), row.Field<string>(AdditionalColumnName));
        }

        /// <summary>
        /// The select all method.
        /// </summary>
        /// <returns> The <see cref="IEnumerable"/>. </returns>
        public override IEnumerable<Tuple<DbCompany, string>> SelectAll()
        {
            var table = this.DbConn.ExecuteSelectQuery(this.queriesDict[CrudQueriesEnum.SelectAllItemsQuery]);
            var resultList = new List<Tuple<DbCompany, string>>();
            foreach (var row in table.Rows.Cast<DataRow>())
            {
                resultList.Add(new Tuple<DbCompany, string>(GetDbEntityFromDataRow(row), row.Field<string>(AdditionalColumnName)));
            }

            return resultList.Count > 0 ? resultList : null;
        }

        /// <summary>
        /// The insert item.
        /// </summary>
        /// <param name="dbComp"> The database employee entity. </param>
        /// <returns> Returns true if success, false - otherwise </returns>
        public override bool InsertItem(DbCompany dbComp)
        {
            return this.DbConn.ExecuteInsertQuery(
                this.queriesDict[CrudQueriesEnum.InsertItemQueryTmpl],
                new SqlParameter { Value = dbComp.CompanyName },
                new SqlParameter { Value = dbComp.CompanyTypeId });
        }

        /// <summary>
        /// The update item.
        /// </summary>
        /// <param name="dbComp"> The database employee entity. </param>
        /// <returns> Returns true if success, false - otherwise </returns>
        public override bool UpdateItem(DbCompany dbComp)
        {
            return this.DbConn.ExecuteUpdateQuery(
                this.queriesDict[CrudQueriesEnum.UpdateItemQueryTmpl],
                new SqlParameter { Value = dbComp.CompanyName },
                new SqlParameter { Value = dbComp.CompanyTypeId },
                new SqlParameter { Value = dbComp.CompanyId });
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
        private static void ManufactureCompanyDataTable(DataSet ds, string dataTableName)
        {
            var companies = new DataTable(dataTableName);
            var dbComp = new DbCompany();
            companies.Columns.Add(new DataColumn(dbComp[0], typeof(int)));
            companies.Columns.Add(new DataColumn(dbComp[1], typeof(string)));
            companies.Columns.Add(new DataColumn(dbComp[2], typeof(int)));
            companies.Columns.Add(new DataColumn(dbComp[3], typeof(int)));
            companies.Columns.Add(new DataColumn(AdditionalColumnName, typeof(string)));

            ds.Tables.Add(companies);
        }

        /// <summary>
        /// Returns database entity from data row.
        /// </summary>
        /// <param name="row"> The row. </param>
        /// <returns> The <see cref="DbCompany"/>. </returns>
        private static DbCompany GetDbEntityFromDataRow(DataRow row)
        {
            var dbComp = new DbCompany();
            dbComp.CompanyId = row.Field<int>(dbComp[0]);
            dbComp.CompanyName = row.Field<string>(dbComp[1]);
            dbComp.NumberOfStuff = row.Field<int>(dbComp[2]);
            dbComp.CompanyTypeId = row.Field<int>(dbComp[3]);
            return dbComp;
        }
    }
}
