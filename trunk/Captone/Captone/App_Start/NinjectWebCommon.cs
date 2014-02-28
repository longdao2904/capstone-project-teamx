using System.Linq;
using System.Reflection;
using Captone.Models;
using Captone.Repositories;
using Captone.Repositories.Interfaces;
using Captone.Services;
using Captone.Services.Interfaces;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Captone.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Captone.App_Start.NinjectWebCommon), "Stop")]

namespace Captone.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //uncomment following lines to register all services and repositories
            //var assembly = Assembly.GetExecutingAssembly();
            //var internalInterfaces = assembly.GetTypes().Where(t => t.IsInterface);
            //foreach (var internalInterface in internalInterfaces)
            //{
            //    var impl = assembly.GetTypes().SingleOrDefault(t => internalInterface.IsAssignableFrom(t)
            //    && t.IsClass && !t.IsAbstract);
            //    if (impl != null) kernel.Bind(internalInterface).To(impl);
            //}

            //Binding IUnit Of Work
            kernel.Bind<IUnitOfWork>().To<iDeliverEntities>().InRequestScope();

            //Binding Repositories
            kernel.Bind<IAccountRepository>().To<AccountRepository>();
            kernel.Bind<ICoachRepository>().To<CoachRepository>();
            kernel.Bind<ITripRepository>().To<TripRepository>();

            //Binding Services
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<ICoachService>().To<CoachService>();

            //Binding Coach
        }        
    }
}
