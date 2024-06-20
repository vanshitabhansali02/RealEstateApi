using DataAcess.DTOs;
using DataAcess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IPropertyInterface
    {
        Task AddPropertyAsync(DataAcess.Models.Property property);
         Task<List<PropertyDto>> GetAllPropertyAsync();
         Task<DataAcess.Models.Property> GetAllPropertyDetailsAsync(int propertyid);
        Task<List<PropertyDto>> GetPropertyListByAgentId(int id);
          Task AddEnquiry(Enquiry enquiry);
        List<Enquiry> GetEnquiry(int id);
        void CreateProperty(Property property);
    }
}
