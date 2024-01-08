using Exam1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Domain.Repositories
{
    public interface INIDRepository : IRepositoryBase<NID, Guid>
    {
        Task<(IList<NID> nIDs, int total, int totalDisplay)> GetTableDataAsync
            (int pageIndex, int pageSize, string name, uint ageFrom, uint ageTo, string sortBy);
    }
}
