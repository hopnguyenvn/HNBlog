namespace HNBlog.Web.Mvc.CastleWindsor
{
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    using SharpArch.Domain.Commands;
    using SharpArch.Domain.Events;
    using SharpArch.Domain.PersistenceSupport;
    using SharpArch.NHibernate;
    using SharpArch.NHibernate.Contracts.Repositories;
    using SharpArch.Web.Mvc.Castle;
    using HNBlog.Tasks;

    public class ComponentRegistrar
    {
        public static void AddComponentsTo(IWindsorContainer container) 
        {
            container.Register(Component.For<IBlogTasks>().ImplementedBy<BlogTasks>().Named("blogrepository"));
            //container.AddComponent("blogrepository",
            //    typeof(IBlogTasks), typeof(BlogTasks)); 
            AddGenericRepositoriesTo(container);
            AddCustomRepositoriesTo(container);
            AddQueryObjectsTo(container);
            AddTasksTo(container);
            AddHandlersTo(container);
        }

        private static void AddTasksTo(IWindsorContainer container)
        {
            container.Register(
                AllTypes
                    .FromAssemblyNamed("HNBlog.Tasks")
                    .Pick()
                    .WithService.FirstNonGenericCoreInterface("HNBlog.Domain"));
                
        }

        private static void AddCustomRepositoriesTo(IWindsorContainer container) 
        {
            container.Register(
                AllTypes
                    .FromAssemblyNamed("HNBlog.Infrastructure")
                    .BasedOn(typeof(IRepositoryWithTypedId<,>))
                    .WithService.FirstNonGenericCoreInterface("HNBlog.Domain"));
        }

        private static void AddGenericRepositoriesTo(IWindsorContainer container)
        {
            container.Register(
                Component.For(typeof(IEntityDuplicateChecker))
                    .ImplementedBy(typeof(EntityDuplicateChecker))
                    .Named("entityDuplicateChecker"));

            container.Register(
                Component.For(typeof(INHibernateRepository<>))
                    .ImplementedBy(typeof(NHibernateRepository<>))
                    .Named("nhibernateRepositoryType")
                    .Forward(typeof(IRepository<>)));

            container.Register(
                Component.For(typeof(INHibernateRepositoryWithTypedId<,>))
                    .ImplementedBy(typeof(NHibernateRepositoryWithTypedId<,>))
                    .Named("nhibernateRepositoryWithTypedId")
                    .Forward(typeof(IRepositoryWithTypedId<,>)));

            container.Register(
                    Component.For(typeof(ISessionFactoryKeyProvider))
                        .ImplementedBy(typeof(DefaultSessionFactoryKeyProvider))
                        .Named("sessionFactoryKeyProvider"));

            container.Register(
                    Component.For(typeof(ICommandProcessor))
                        .ImplementedBy(typeof(CommandProcessor))
                        .Named("commandProcessor"));
        }

        private static void AddQueryObjectsTo(IWindsorContainer container) 
        {
            container.Register(
                AllTypes.FromAssemblyNamed("HNBlog.Web.Mvc")
                    .BasedOn<NHibernateQuery>()
                    .WithService.DefaultInterfaces());

            container.Register(
                AllTypes.FromAssemblyNamed("HNBlog.Infrastructure")
                    .BasedOn(typeof(NHibernateQuery))
                    .WithService.DefaultInterfaces());
        }

        private static void AddHandlersTo(IWindsorContainer container)
        {
            container.Register(
                AllTypes.FromAssemblyNamed("HNBlog.Tasks")
                    .BasedOn(typeof(ICommandHandler<>))
                    .WithService.FirstInterface());

            container.Register(
                AllTypes.FromAssemblyNamed("HNBlog.Tasks")
                    .BasedOn(typeof(IHandles<>))
                    .WithService.FirstInterface());
        }
    }
}