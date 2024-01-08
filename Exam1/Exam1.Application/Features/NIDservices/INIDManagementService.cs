using Exam1.Application.DTOs;
using Exam1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Application.Features.NIDservices
{
    public interface INIDManagementService
    {
        Task CreateNidAsync(NIDcreateUpdateDTO nIDcreateUpdateDTO);
        Task DeleteNIDAsync(Guid id);
        Task<NIDcreateUpdateDTO> GetNIDById(Guid id);
        Task<(IList<NID> nIDs, int total, int totalDisplay)> GetTableDataAsync
            (int pageIndex, int pageSize, string name, uint ageFrom, uint ageTo, string sortBy);
        Task UpdateNidAsync(NIDcreateUpdateDTO updateDTO, Guid Id);
    }
}
