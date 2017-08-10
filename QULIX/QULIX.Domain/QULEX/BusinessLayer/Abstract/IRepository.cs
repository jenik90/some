namespace QULIX.Domain.QULEX.BusinessLayer.Abstract
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The Repository interface. It is generic.
    /// </summary>
    /// <typeparam name="T"> </typeparam>
    public interface IRepository<T> : IDisposable
        where T : class
    {
        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns> The <see cref="IEnumerable"/>. </returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="id"> The id. </param>
        /// <returns> The <see cref="T"/>. </returns>
        T Get(int id);

        /// <summary>
        /// The find.
        /// </summary>
        /// <param name="predicate"> The predicate. </param>
        /// <returns> The <see cref="IEnumerable"/>. </returns>
        IEnumerable<T> Find(Func<T, bool> predicate);

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="item"> The item. </param>
        bool Create(T item);

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="item"> The item. </param>
        bool Update(T item);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id"> The id. </param>
        bool Delete(int id);
    }
}
