using Autofac;
using Library.Application;
using Library.Domain.Repositories;
using Library.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure
{
    public class InfrastructureModule : Module
    {
        private readonly string connectionString;
        private readonly string migrationAssembly;

        public InfrastructureModule(string connectionString, string migrationAssembly)
        {
            this.connectionString = connectionString;
            this.migrationAssembly = migrationAssembly;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                .WithParameter("connectionString", connectionString)
                .WithParameter("migrationAssembly", migrationAssembly)
                .InstancePerLifetimeScope(); 
            
            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", connectionString)
                .WithParameter("migrationAssembly", migrationAssembly)
                .InstancePerLifetimeScope();
            
            builder.RegisterType<BookRepository>().As<IBookRepository>()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
                .InstancePerLifetimeScope();
        }
    }
}
