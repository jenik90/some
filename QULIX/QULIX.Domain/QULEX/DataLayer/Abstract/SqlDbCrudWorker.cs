namespace QULIX.Domain.QULEX.DataLayer.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    using QULIX.Domain.QULEX.DataLayer.Concrete;

    /// <summary>
    /// The SQL database CRUD worker.
    /// </summary>
    /// <typeparam name="TEntiry"> Entity type </typeparam>
    /// <typeparam name="TAdditionalInfo"> Type of additional info. </typeparam>
    public abstract class SqlDbCrudWorker<TEntiry, TAdditionalInfo> : IDisposable
    {
        /// <summary>
        /// The connection to database.
        /// </summary>
        protected readonly DbConnection DbConn;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDbCrudWorker{TEntiry,TAdditionalInfo}"/> class.
        /// </summary>
        /// <param name="manufactureDataTable"> The manufacture data table. </param>
        /// <param name="dataTableName"> The data table name. </param>
        protected SqlDbCrudWorker(Action<DataSet, string> manufactureDataTable, string dataTableName = "DefaultTableName")
        {
            this.DbConn = new DbConnection(manufactureDataTable, dataTableName);
        }
        
        /// <summary>
        ///  The dispose method.
        /// </summary>
        public virtual void Dispose()
        {
            this.DbConn.Dispose();
        }

        /// <summary>
        /// The select entity by id method.
        /// </summary> <param name="id"> The entity identifier. </param>
        /// <returns> The <see cref="Tuple"/>. </returns>
        public abstract Tuple<TEntiry, TAdditionalInfo> SelectById(int id);

        /// <summary>
        /// The select all entities method.
        /// </summary>
        /// <returns> The <see cref="IEnumerable"/>. </returns>
        public abstract IEnumerable<Tuple<TEntiry, TAdditionalInfo>> SelectAll();

        /// <summary>
        /// The insert method.
        /// </summary>
        /// <param name="dbEntity"> The database entity. </param>
        /// <returns> Returns true if insert if successful, false - otherwise. </returns>
        public abstract bool InsertItem(TEntiry dbEntity);

        /// <summary>
        /// The update item method.
        /// </summary>
        /// <param name="dbEntity"> The database entity. </param>
        /// <returns> Returns true if update if successful, false - otherwise. </returns>
        public abstract bool UpdateItem(TEntiry dbEntity);

        /// <summary>
        /// The delete item method.
        /// </summary>
        /// <param name="id"> The id. </param>
        /// <returns> Returns true if delete if successful, false - otherwise. </returns>
        public abstract bool DeleteItem(int id);
    }
}
