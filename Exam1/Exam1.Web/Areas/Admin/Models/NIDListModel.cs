using Autofac;
using AutoMapper;
using Exam1.Application.Features.NIDservices;
using Exam1.Infrastructure.Utilities;
using System.Web;

namespace Exam1.Web.Areas.Admin.Models
{
    public class NIDListModel
    {
        private INIDManagementService _nidService;
        public NidSearch SearchItem { get; set; }
        public NIDListModel()
        {
            
        }
        public NIDListModel(INIDManagementService nIDManagementService)
        {
            _nidService = nIDManagementService;
        }

        internal void Resolve(ILifetimeScope scope)
        {
            _nidService = scope.Resolve<INIDManagementService>();
        }

        internal async Task<object> GetTableDataAsync(DataTablesAjaxRequestUtility dataTable)
        {
            var data = await _nidService.GetTableDataAsync(dataTable.PageIndex, 
                dataTable.PageSize,
                SearchItem.Name,
                SearchItem.AgeFrom,
                SearchItem.AgeTo,
                dataTable.GetSortText(new string[] {"Name", "Age"}));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from nid in data.nIDs
                        select new string[]
                        {
                            HttpUtility.HtmlEncode(nid.Name),
                            nid.Age.ToString(),
                            HttpUtility.HtmlEncode(nid.Gender),
                            HttpUtility.HtmlEncode(nid.Address),
                            HttpUtility.HtmlEncode(nid.PermanentAddress),
                            nid.DateOfBirth.ToString(),
                            HttpUtility.HtmlEncode(nid.FatherName),
                            HttpUtility.HtmlEncode(nid.MotherName),
                            nid.Id.ToString()

                        })
            };
        }

        internal async Task DeleteNIDAsync(Guid id)
        {
            await _nidService.DeleteNIDAsync(id);
        }
    }
}
