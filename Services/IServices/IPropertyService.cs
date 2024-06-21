using DataAcess.CustomModel;
using DataAcess.DTOs;
using DataAcess.Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IPropertyService
    {
        Task AddPropertyAsync(DataAcess.Models.Property property);
        Task<PaginatedDto<PropertyDto>> GetAllPropertyAsync(Paginationparameters paginationParameters);
         Task<DataAcess.Models.Property> GetAllPropertyDetailsAsync(int propertyid);
        Task<PaginatedDto<PropertyDto>> GetPropertyListByAgentId(int id,Paginationparameters paginationparameters);
          Task AddEnquiry(Enquiry enquiry);
        List<Enquiry> GetEnquiry(int id);
        void CreateProperty(Property property);
    }
}
