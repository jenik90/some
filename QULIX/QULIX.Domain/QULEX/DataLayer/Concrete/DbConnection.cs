namespace QULIX.Domain.QULEX.DataLayer.Concrete
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    using QULIX.Domain.QULEX.Core.Enums;
    using QULIX.Domain.QULEX.Core.Utils;
    using QULIX.Domain.QULEX.DataLayer.Abstract;

    /// <summary>
    /// The db connection.
    /// </summary>
    public class DbConnection : ResourceHolder
    {
        /// <summary>
        /// The connection string settings.
        /// </summary>
        private static readonly string ConnectionStringSettings = ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString;

        /// <summary>
        /// The SQL Data adapter.
        /// </summary>
        private readonly SqlDataAdapter sqlAdapter;

        public Action<DataSet, string> ManufactureDataTable;

        /// <summary>
        /// The data table name.
        /// </summary>
        private readonly string dataTableName;

        /// <summary>
        /// The SQL connection.
        /// </summary>
        private readonly SqlConnection conn;

        /// <summary>
        /// Gets the sql connection.
        /// </summary>
        private SqlConnection SqlConnection
        {
            get
            {
                if (this.conn.State == ConnectionState.Closed || this.conn.State == ConnectionState.Broken)
                {
                    this.conn.Open();
                }

                return this.conn;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbConnection"/> class. 
        /// The database connection.
        /// </summary>
        public DbConnection()
        {
            this.sqlAdapter = new SqlDataAdapter();
            this.conn = new SqlConnection(ConnectionStringSettings);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbConnection"/> class. 
        /// The database connection.
        /// </summary>
        public DbConnection(Action<DataSet, string> ManufactureDataTable, string dataTableName = "DefaultTableName")
            : this()
        {
            this.ManufactureDataTable = ManufactureDataTable;
            this.dataTableName = dataTableName;
        }

        /// <summary>
        /// Executes specified select query.
        /// </summary>
        /// <param name="query"> The specified select query text. </param>
        /// <param name="sqlParams"> The SQL Parameters. </param>
        /// <returns> The <see cref="DataTable"/>. </returns>
        public DataTable ExecuteSelectQuery(string query, params SqlParameter[] sqlParams)
        {
            if (!this.ExecuteQuery(query, QueryType.Select, sqlParams))
            {
                return null;
            }

            return this.GetSelectQueryResult();
        }

        /// <summary>
        /// Executes specified select query.
        /// </summary>
        /// <param name="query"> The specified select query text. </param>
        /// <returns> The <see cref="DataTable"/>. </returns>
        public DataTable ExecuteSelectQuery(string query)
        {
            if (!this.ExecuteQuery(query, QueryType.Select))
            {
                return null;
            }

            return this.GetSelectQueryResult();
        }

        /// <summary>
        /// Executes specified insert query.
        /// </summary>
        /// <param name="query"> The specified insert query text. </param>
        /// <param name="sqlParams"> The SQL parameters. </param>
        /// <returns> True if query was successful, false otherwise. </returns>
        public bool ExecuteInsertQuery(string query, params SqlParameter[] sqlParams) =>
            this.ExecuteQuery(query, QueryType.Insert, sqlParams);

        /// <summary>
        /// Executes specified insert query.
        /// </summary>
        /// <param name="query"> The specified insert query text. </param>
        /// <returns> True if query was successful, false otherwise. </returns>
        public bool ExecuteInsertQuery(string query) =>
            this.ExecuteQuery(query, QueryType.Insert);

        /// <summary>
        /// Executes specified update query.
        /// </summary>
        /// <param name="query"> The specified update query text. </param>
        /// <param name="sqlParams"> The SQL parameters. </param>
        /// <returns> True if query was successful, false otherwise. </returns>
        public bool ExecuteUpdateQuery(string query, params SqlParameter[] sqlParams) =>
            this.ExecuteQuery(query, QueryType.Update, sqlParams);

        /// <summary>
        /// Executes specified update query.
        /// </summary>
        /// <param name="query"> The specified update query text. </param>
        /// <returns> True if query was successful, false otherwise. </returns>
        public bool ExecuteUpdateQuery(string query) =>
            this.ExecuteQuery(query, QueryType.Update);

        /// <summary>
        /// Executes specified delete query.
        /// </summary>
        /// <param name="query"> The specified update query text. </param>
        /// <param name="sqlParams"> The SQL parameters. </param>
        /// <returns> True if query was successful, false otherwise. </returns>
        public bool ExecuteDeleteQuery(string query, params SqlParameter[] sqlParams) =>
            this.ExecuteQuery(query, QueryType.Delete, sqlParams);

        /// <summary>
        /// Executes specified delete query.
        /// </summary>
        /// <param name="query"> The specified delete query text. </param>
        /// <returns> True if query was successful, false otherwise. </returns>
        public bool ExecuteDeleteQuery(string query) =>
            this.ExecuteQuery(query, QueryType.Delete);

        /// <summary>
        /// Executes specified query to database.
        /// </summary>
        private bool ExecuteQuery(string query, QueryType queryType, params SqlParameter[] sqlParams)
        {
            var myCommand = new SqlCommand { CommandText = query };
            myCommand.Parameters.AddRange(sqlParams);

            return this.ExecuteSqlCommand(myCommand, queryType);
        }

        /// <summary>
        /// The execute query.
        /// </summary>
        /// <param name="query"> The query. </param>
        /// <param name="queryType"> The query type. </param>
        /// <returns> The <see cref="bool"/>. </returns>
        private bool ExecuteQuery(string query, QueryType queryType)
        {
            var myCommand = new SqlCommand { CommandText = query };

            return this.ExecuteSqlCommand(myCommand, queryType);
        }

        /// <summary>
        /// The execute sql command.
        /// </summary>
        /// <param name="myCommand"> The my command. </param>
        /// <param name="queryType"> The query type. </param>
        /// <returns> The <see cref="bool"/>. </returns>
        /// <exception cref="NotImplementedException"> </exception>
        private bool ExecuteSqlCommand(SqlCommand myCommand, QueryType queryType)
        {
            try
            {
                myCommand.Connection = this.SqlConnection;
                switch (queryType)
                {
                    case QueryType.Select:
                        this.sqlAdapter.SelectCommand = myCommand;
                        return true;  // no command execution for select commands
                    case QueryType.Insert:
                        //this.sqlAdapter.InsertCommand = myCommand;
                        break;
                    case QueryType.Update:
                        //this.sqlAdapter.UpdateCommand = myCommand;
                        break;
                    case QueryType.Delete:
                        //this.sqlAdapter.DeleteCommand = myCommand;
                        break;
                    case QueryType.StoredProcedure:
                        myCommand.CommandType = CommandType.StoredProcedure;
                        break;
                    default:
                        throw new NotImplementedException();
                }

                myCommand.ExecuteNonQuery();
            }
            catch (SqlException exception)
            {
                Logger.LogError($"Can't execute query to database - Query Type: {queryType}; Query: {myCommand.CommandText} \nException:\n{exception.StackTrace}");
                return false;
            }

            Logger.LogInfo($"Successful query to Database - Query Type: {queryType}; Query: {myCommand.CommandText}");
            return true;
        }

        /// <summary>
        /// The get query result.
        /// </summary>
        /// <returns> The <see cref="DataTable"/>. </returns>
        private DataTable GetSelectQueryResult()
        {
            var ds = new DataSet();
            this.ManufactureDataTable?.Invoke(ds, this.dataTableName);

            this.sqlAdapter.Fill(ds, this.dataTableName);
            var dataTable = ds.Tables[0];
            Logger.LogInfo($"Successful query to Database - Query Type: {QueryType.Select}; Query: {this.sqlAdapter.SelectCommand.CommandText}");

            return dataTable;
        }

        /// <summary>
        /// The internal dispose method.
        /// </summary>
        protected override void DisposeInternal(bool disposing)
        {
            if (!this.IsDisposed)
            {
                if (disposing)
                {
                    // here should be placed clearing of all managed resources by calling its .Dispose() methods
                    this.conn.Close();
                    this.conn.Dispose();
                }

                // clearing all unmanaged resources
            }

            this.IsDisposed = true;
        }
    }
}
