using Exam1.Domain.Entities;
using Exam1.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Infrastructure.Repositories
{
    public class NIDRepository : Repository<NID, Guid>, INIDRepository
    {
        public NIDRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

        public async Task<(IList<NID> nIDs, int total, int totalDisplay)> GetTableDataAsync
            (int pageIndex, int pageSize, string name, uint ageFrom, uint ageTo, string sortBy)
        {
            //Expression<Func<NID, bool>> expression = null;

            //if(!string.IsNullOrEmpty(name)) 
            //{
            //    expression = (x => x.Name.Contains(name) && (x.Age >= ageFrom && x.Age <= ageTo));
            //}

            //var r = await GetDynamicAsync(expression!, sortBy, null, pageIndex, pageSize, false);

            Expression<Func<NID, bool>> expression = null;

            if (!string.IsNullOrWhiteSpace(name))
                expression = x => x.Name.Contains(name) &&
                (x.Age >= ageFrom && x.Age <= ageTo);

            var r = await GetDynamicAsync(expression,
                sortBy, null, pageIndex, pageSize, true);
            return r;
        }
    }
}
