using AutoMapper;
using DataAcess.CustomModel;
using DataAcess.Data;
using DataAcess.Interfaces;
using DataAcess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.DTOs;
using Services.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Services.Services
{
 
    public class PropertyService :IPropertyService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IAgentPropertyRepository _agentPropertyRepository;
        private readonly IAgentRepository _agentRepository;
        public PropertyService(ApplicationDbContext appDbContext, IMapper mapper,IPropertyRepository propertyRepository,IAgentRepository agentRepository,IAgentPropertyRepository agentPropertyRepository) 
        {
            _context = appDbContext;
            _mapper = mapper;
            _propertyRepository = propertyRepository;
            _agentPropertyRepository = agentPropertyRepository;
            _agentRepository = agentRepository;

        }
        public async Task<PaginatedDto<PropertyDto>> GetAllPropertyAsync(Paginationparameters paginationParameters)
        {
            var query = _context.Properties.AsQueryable();

            var totalCount = await query.CountAsync();

            var properties = await query
                .OrderBy(x => x.Id)
                .Skip((paginationParameters.currentpage) * paginationParameters.PageSize)
                .Take(paginationParameters.PageSize)
                .ToListAsync();

            var propertyDtos = _mapper.Map<List<PropertyDto>>(properties);

            return new PaginatedDto<PropertyDto>
            {
                Items = propertyDtos,
                TotalCount = totalCount,
                currentpage=paginationParameters.currentpage,
                pageSize=paginationParameters.PageSize
            };
        }

        public async Task<Property> GetAllPropertyDetailsAsync(int propertyid)
        {
            var properties = await _context.Properties
                .Where(x => x.Id == propertyid)
                .Take(15)
                .FirstOrDefaultAsync();

            var propertyDtos = _mapper.Map<Property>(properties);
            return propertyDtos;
        }
        public async Task AddPropertyAsync(Property property)
        {
            _propertyRepository.Add(property);
            var agentproperty=_mapper.Map<AgentProperty>(property); 
            _agentPropertyRepository.Add(agentproperty);
            
            await _context.SaveChangesAsync();
        }
        public async Task<PaginatedDto<PropertyDto>> GetPropertyListByAgentId(int id,Paginationparameters paginationparameters)
        {
            var totalCountQuery = _context.Properties
        .Include(x => x.AgentProperties)
        .Where(x => x.AgentProperties.Any(ap => ap.AgentId == id));

            var totalCount = await totalCountQuery.CountAsync();

            var propertiesQuery = totalCountQuery
                .OrderBy(x => x.Id)
                .Skip((paginationparameters.currentpage - 1) * paginationparameters.PageSize)
                .Take(paginationparameters.PageSize);

            var properties = await propertiesQuery.ToListAsync();


            var propertydtos = _mapper.Map<List<PropertyDto>>(properties);
            return new PaginatedDto<PropertyDto>
            {
                Items = propertydtos,
                TotalCount =totalCount,
                currentpage = paginationparameters.currentpage,
                pageSize = paginationparameters.PageSize
            };
        }
        public async Task AddEnquiry(Enquiry enquiry)
        {
            await _context.AddAsync(enquiry);
            _context.SaveChanges();
        }
        public List<Enquiry> GetEnquiry(int id)
        {
            var propertyIds = _context.AgentProperties
                    .Where(ap => ap.AgentId == id)
                    .Select(ap => ap.PropertyId)
                    .ToList();

            // Step 2: Fetch all enquiries where PropertyId is in the list of propertyIds
            var enquiries = _context.Enquiries
                .Where(e => propertyIds.Contains(e.PorpertyId))
                .ToList();


            return enquiries;
        }
       



    }
}
