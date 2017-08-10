namespace QULIX.WebUi.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Ninject;
    using QULIX.Domain.QULEX.BusinessLayer.Abstract;
    using QULIX.Domain.QULEX.BusinessLayer.Concrete;
    using QULIX.Domain.QULEX.BusinessLayer.Entities;

    /// <summary>
    /// The Ninject dependency resolver.
    /// </summary>
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectDependencyResolver"/> class.
        /// </summary>
        /// <param name="kernelParam"> The kernel param. </param>
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            this.kernel = kernelParam;
            this.AddBindings();
        }

        /// <summary>
        /// The get service.
        /// </summary>
        /// <param name="serviceType"> The service type. </param>
        /// <returns> The <see cref="object"/>. </returns>
        public object GetService(Type serviceType)
        {
            return this.kernel.TryGet(serviceType);
        }

        /// <summary>
        /// The get services.
        /// </summary>
        /// <param name="serviceType"> The service type. </param>
        /// <returns> The <see cref="IEnumerable"/>. </returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.kernel.GetAll(serviceType);
        }

        /// <summary>
        /// The add bindings.
        /// </summary>
        private void AddBindings()
        {
            // put bindings here
            this.kernel.Bind<IRepository<Employee>>().To<EmployeeRepository>();
            this.kernel.Bind<IRepository<Company>>().To<CompanyRepository>();
            this.kernel.Bind<IRepository<CompanyType>>().To<CompanyTypeRepository>();
        }
    }
}