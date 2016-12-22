using System.Data.Entity;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using SampleBookingSystem.Data;
using SampleBookingSystem.Data.Common;
using SampleBookingSystem.Services.Data;
using SampleBookingSystem.Services.Data.Contracts;

namespace SampleBookingSystem.Web.App_Start
{
    public static class AutofacCofig
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Register services
            RegisterServices(builder);

            // For any dependency container to work, it must be set as the dependency resolver.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            // Register the DbContext, needed by the repository (it takes a DbContext in the ctor, so the container substitutes it with SampleBookingSystemDbContext
            builder.Register(x => new SampleBookingSystemDbContext())
                .As<DbContext>()
                .InstancePerRequest();

            builder.RegisterGeneric(typeof(DbRepository<>))
                .As(typeof(IDbRepository<>))
                .InstancePerRequest();

            var servicesAssembly = Assembly.GetAssembly(typeof(IRoomService));
            builder.RegisterAssemblyTypes(servicesAssembly).AsImplementedInterfaces();

            //builder.Register(x => new IdentifierProvider())
            //    .As<IIdentifierProvider>()
            //    .InstancePerRequest();



            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            //    .AssignableTo<BaseController>().PropertiesAutowired();
        }
    }
}