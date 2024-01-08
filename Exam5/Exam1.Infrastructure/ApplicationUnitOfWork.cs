using Exam1.Domain.Repositories;
using FirstDemo.Application;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Infrastructure
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public IProductRepository ProductRepository {  get; set; }
        public ApplicationUnitOfWork(IApplicationDbContext dbContext, IProductRepository productRepository) : base((DbContext)dbContext)
        {
            ProductRepository = productRepository;
        }

    }
}
