using Exam1.Application;
using Exam1.Domain.Repositories;
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
        public INIDRepository NIDRepository {  get; private set; }
        public ApplicationUnitOfWork(IApplicationDbContext dbContext, INIDRepository nIDRepository) : base((DbContext)dbContext)
        {
            NIDRepository = nIDRepository;
        }

    }
}
