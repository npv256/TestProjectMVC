using Domain;
using Repository.Contexts;
using Repository.IRepostories;
using Repository.Repositories;
using Service.Interfaces;
using Service.Services;
using System;

using Unity;
using Unity.Lifetime;

namespace UniversityControl
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IHash, Hash>();
            container.RegisterType<IRepository<Student>, StudentRepository>();
            container.RegisterType<IRepository<Teacher>, TeacherRepository>();
            container.RegisterType<IRepository<Science>, ScienceRepository>();
            container.RegisterType<IService<Student>, StudentService>();
            container.RegisterType<IService<Teacher>, TeacherService>();
            container.RegisterType<IService<Science>, ScienceService>();
            container.RegisterType<dbcontext>(new HierarchicalLifetimeManager());
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
        }
    }
}