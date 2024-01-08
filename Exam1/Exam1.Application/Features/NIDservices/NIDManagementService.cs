using AutoMapper;
using Exam1.Application.DTOs;
using Exam1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Application.Features.NIDservices
{
    public class NIDManagementService : INIDManagementService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;

        public NIDManagementService(IApplicationUnitOfWork applicationUnitOfWork, IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        public async Task CreateNidAsync(NIDcreateUpdateDTO nIDcreateUpdateDTO)
        {
            var nid = new NID();
            _mapper.Map(nIDcreateUpdateDTO, nid);
            await _applicationUnitOfWork.NIDRepository.AddAsync(nid);
            await _applicationUnitOfWork.SaveAsync();
        }

        public async Task DeleteNIDAsync(Guid id)
        {
            await _applicationUnitOfWork.NIDRepository.RemoveAsync(id);
            await _applicationUnitOfWork.SaveAsync();
        }

        public async Task<NIDcreateUpdateDTO> GetNIDById(Guid id)
        {
            NIDcreateUpdateDTO dTO = new();
            var nid = await _applicationUnitOfWork.NIDRepository.GetByIdAsync(id);
            _mapper.Map(nid, dTO);
            return dTO;
        }

        public async Task<(IList<NID> nIDs, int total, int totalDisplay)> GetTableDataAsync
            (int pageIndex, int pageSize, string name, uint ageFrom, uint ageTo, string sortBy)
        {
            var r = await _applicationUnitOfWork.NIDRepository.GetTableDataAsync(pageIndex, pageSize, name, ageFrom, ageTo, sortBy);
            return r;
        }

        public async Task UpdateNidAsync(NIDcreateUpdateDTO updateDTO, Guid Id)
        {
            NID nid = await _applicationUnitOfWork.NIDRepository.GetByIdAsync(Id);
            _mapper.Map(updateDTO, nid);
            await _applicationUnitOfWork.SaveAsync();
        }

    }
}
