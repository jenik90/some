namespace QULIX.Domain.QULEX.DataLayer.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    using Core.Enums;
    using Core.Infrastructure;

    using DbEntities;

    using QULIX.Domain.QULEX.DataLayer.Abstract;

    public class CompanyTypeDataRetriever : SqlDbCrudWorker<DbCompanyType, IEnumerable<string>>
    {
        /// <summary>
        /// The additional column name.
        /// </summary>
        private const string AdditionalColumnName = "RelatedCompanies";

        /// <summary>
        /// The data table name.
        /// </summary>
        private const string DataTableName = "CompanyTypes";

        /// <summary>
        /// The queries dictionary.
        /// </summary>
        private readonly IReadOnlyDictionary<CrudQueriesEnum, string> queriesDict =
            CrudQueriesRetriever.GetQueriesDictionary<CompanyTypeDataRetriever>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyDataRetriever"/> class.
        /// </summary>
        public CompanyTypeDataRetriever()
            : base(ManufactureCompanyTypeDataTable, DataTableName)
        {
        }

        /// <summary>
        /// The select by id method.
        /// </summary>
        /// <param name="id"> The id. </param>
        /// <returns> The <see cref="Tuple"/>. </returns>
        public override Tuple<DbCompanyType, IEnumerable<string>> SelectById(int id)
        {
            string query = this.queriesDict[CrudQueriesEnum.SelectByIdQueryTmpl];
            var row = this.DbConn.ExecuteSelectQuery(query, new SqlParameter { Value = id }).Rows.Cast<DataRow>().First();
            return new Tuple<DbCompanyType, IEnumerable<string>>(GetDbEntityFromDataRow(row), row.Field<string>(AdditionalColumnName).Split(',', ';'));
        }

        /// <summary>
        /// The select all method.
        /// </summary>
        /// <returns> The <see cref="IEnumerable"/>. </returns>
        public override IEnumerable<Tuple<DbCompanyType, IEnumerable<string>>> SelectAll()
        {
            var table = this.DbConn.ExecuteSelectQuery(this.queriesDict[CrudQueriesEnum.SelectAllItemsQuery]);
            var resultList = new List<Tuple<DbCompanyType, IEnumerable<string>>>();
            foreach (var row in table.Rows.Cast<DataRow>())
            {
                resultList.Add(new Tuple<DbCompanyType, IEnumerable<string>>(GetDbEntityFromDataRow(row), row.Field<string>(AdditionalColumnName).Split(',', ';')));
            }

            return resultList.Count > 0 ? resultList : null;
        }

        /// <summary>
        /// The insert item.
        /// </summary>
        /// <param name="dbCompType"> The database employee entity. </param>
        /// <returns> Returns true if success, false - otherwise </returns>
        public override bool InsertItem(DbCompanyType dbCompType)
        {
            return this.DbConn.ExecuteInsertQuery(
                this.queriesDict[CrudQueriesEnum.InsertItemQueryTmpl],
                new SqlParameter { Value = dbCompType.CompanyTypeName });
        }

        /// <summary>
        /// The update item.
        /// </summary>
        /// <param name="dbCompType"> The database employee entity. </param>
        /// <returns> Returns true if success, false - otherwise </returns>
        public override bool UpdateItem(DbCompanyType dbCompType)
        {
            return this.DbConn.ExecuteUpdateQuery(
               this.queriesDict[CrudQueriesEnum.UpdateItemQueryTmpl],
               new SqlParameter { Value = dbCompType.CompanyTypeName },
               new SqlParameter { Value = dbCompType.CompanyTypeId });
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
        private static void ManufactureCompanyTypeDataTable(DataSet ds, string dataTableName)
        {
            var compTypes = new DataTable(dataTableName);
            var dbCompType = new DbCompany();
            compTypes.Columns.Add(new DataColumn(dbCompType[0], typeof(int)));
            compTypes.Columns.Add(new DataColumn(dbCompType[1], typeof(string)));
            compTypes.Columns.Add(new DataColumn(AdditionalColumnName, typeof(string)));

            ds.Tables.Add(compTypes);
        }

        /// <summary>
        /// Returns database entity from data row.
        /// </summary>
        /// <param name="row"> The row. </param>
        /// <returns> The <see cref="DbCompany"/>. </returns>
        private static DbCompanyType GetDbEntityFromDataRow(DataRow row)
        {
            var dbCompType = new DbCompanyType();
            dbCompType.CompanyTypeId = row.Field<int>(dbCompType[0]);
            dbCompType.CompanyTypeName = row.Field<string>(dbCompType[1]);
            return dbCompType;
        }

    }
}
