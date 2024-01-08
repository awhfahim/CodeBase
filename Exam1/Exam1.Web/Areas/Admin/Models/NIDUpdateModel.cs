using Autofac;
using Exam1.Application.DTOs;
using Exam1.Application.Features.NIDservices;

namespace Exam1.Web.Areas.Admin.Models
{
    public class NIDUpdateModel
    {
        private INIDManagementService _nIDManagementService;
        public NIDcreateUpdateDTO UpdateDTO { get; set; }
        public NIDUpdateModel()
        {
            
        }
        public NIDUpdateModel(INIDManagementService nIDManagementService)
        {
            _nIDManagementService = nIDManagementService;
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _nIDManagementService = scope.Resolve<INIDManagementService>();
        }

        internal async Task GetNIDByID(Guid id)
        {
            var res = await _nIDManagementService.GetNIDById(id);
            UpdateDTO = res;
        }

        internal async Task UpdateNIDAsync(Guid Id)
        {
           await _nIDManagementService.UpdateNidAsync(UpdateDTO, Id);
        }
    }
}
