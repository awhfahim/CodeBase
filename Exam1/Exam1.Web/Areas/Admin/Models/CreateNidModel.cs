
using Autofac;
using AutoMapper;
using Exam1.Application.DTOs;
using Exam1.Application.Features.NIDservices;

namespace Exam1.Web.Areas.Admin.Models
{
    public class CreateNidModel
    {
        private INIDManagementService _nIDManagementService;
        private IMapper _mapper;

        public string Name { get; set; }
        public uint Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PermanentAddress { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }

        public CreateNidModel()
        {
            
        }
        public CreateNidModel(INIDManagementService nIDManagementService, IMapper mapper)
        {
            _nIDManagementService = nIDManagementService;
            _mapper = mapper;
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _nIDManagementService = scope.Resolve<INIDManagementService>();
            _mapper = scope.Resolve<IMapper>();
        }
        internal async Task CreateNidAsync()
        {
            NIDcreateUpdateDTO nIDcreateUpdateDTO = new();
            _mapper.Map(this, nIDcreateUpdateDTO);
            await _nIDManagementService.CreateNidAsync(nIDcreateUpdateDTO);
        }
    }
}
