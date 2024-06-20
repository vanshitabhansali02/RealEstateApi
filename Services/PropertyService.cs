using AutoMapper;
using DataAcess.Data;
using DataAcess.DTOs;
using DataAcess.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PropertyService : GnericRepository<Property>,IPropertyInterface
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public PropertyService(ApplicationDbContext appDbContext,IMapper mapper):base(appDbContext)
        {
            _context = appDbContext;
            _mapper = mapper;

        }
        public async Task<List<PropertyDto>> GetAllPropertyAsync()
        {
            var properties = await _context.Properties
                .OrderBy(x => x.Id)
                .Take(15)
                .ToListAsync();

            var propertyDtos = _mapper.Map<List<PropertyDto>>(properties);
            return propertyDtos;
        }
        public async Task<DataAcess.Models.Property> GetAllPropertyDetailsAsync(int propertyid)
        {
            var properties = await _context.Properties
                .Where(x => x.Id==propertyid)
                .Take(15)
                .FirstOrDefaultAsync();

            var propertyDtos = _mapper.Map<DataAcess.Models.Property>(properties);
            return propertyDtos;
        }
        public async Task AddPropertyAsync(DataAcess.Models.Property property)
        {
            _context.Properties.Add(property);
            await _context.SaveChangesAsync();
        }
        public async Task<List<PropertyDto>> GetPropertyListByAgentId(int id)
        {
            var res=  _context.Properties.Include(x=>x.AgentProperties).Where(x=>x.AgentProperties.FirstOrDefault().AgentId==id).ToList();
            var propertydtos=_mapper.Map<List<PropertyDto>>(res); 
             return propertydtos;
        }
        public async Task AddEnquiry(Enquiry enquiry)
        {
          await _context.AddAsync(enquiry);
            _context.SaveChanges();
        }
        public  List<Enquiry> GetEnquiry(int id)
        {
            var propertyIds =  _context.AgentProperties
                    .Where(ap => ap.AgentId == id)
                    .Select(ap => ap.PropertyId)
                    .ToList();

            // Step 2: Fetch all enquiries where PropertyId is in the list of propertyIds
            var enquiries =  _context.Enquiries
                .Where(e => propertyIds.Contains(e.PorpertyId))
                .ToList();
            

            return enquiries;
        }
        public void CreateProperty(Property property)
        {
            CreateProperty(property);
        }




    }
}
