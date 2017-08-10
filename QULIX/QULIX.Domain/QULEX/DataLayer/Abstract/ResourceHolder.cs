namespace QULIX.Domain.QULEX.DataLayer.Abstract
{
    using System;

    public abstract class ResourceHolder: IDisposable
    {
        /// <summary>
        /// Gets or sets a value indicating whether is disposed.
        /// </summary>
        protected bool IsDisposed = false;

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.DisposeInternal(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The internal dispose method.
        /// </summary>
        protected abstract void DisposeInternal(bool disposing);

        /// <summary>
        /// Finalizes an instance of the <see cref="ResourceHolder"/> class. 
        /// </summary>
        ~ResourceHolder()
        {
            this.DisposeInternal(false);
        }
    }
}
