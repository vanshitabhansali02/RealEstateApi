using AutoMapper;
using DataAcess.CustomModel;
using DataAcess.Data;
using DataAcess.Interfaces;
using DataAcess.Models;
using GoogleMaps.LocationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.CustomModel;
using Services.DTOs;
using Services.IServices;
using System.Net;
using System.Text.Json;
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
        private readonly HttpClient _client;

        public PropertyService(ApplicationDbContext appDbContext, IMapper mapper,IPropertyRepository propertyRepository,IAgentRepository agentRepository,IAgentPropertyRepository agentPropertyRepository) 
        {
            _context = appDbContext;
            _mapper = mapper;
            _propertyRepository = propertyRepository;
            _agentPropertyRepository = agentPropertyRepository;
            _agentRepository = agentRepository;
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("User-Agent", "RealEstateApp/1.0 (vanshitabhansali02@example.com)");



        }
        public async Task<PaginatedDto<PropertyDto>> GetAllPropertyAsync(Paginationparameters paginationParameters,string searchvalue)
        {
            var query = _context.Properties.AsQueryable();

            var totalCount = await query.CountAsync();

            var properties = await query
                .OrderBy(x => x.Id1).Where(x=>x.Location==searchvalue || searchvalue=="null")
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

            public async Task<PropertyDto> GetAllPropertyDetailsAsync(int propertyid)
            {
            try
            {
                var properties = await _context.Properties
                    .Where(x => x.Id == propertyid)
                    .Take(15)
                    .FirstOrDefaultAsync();
                var address = properties.Location;
                string url = $"https://nominatim.openstreetmap.org/search?q={address}&format=json&addressdetails=1";

                var response = await _client.GetStringAsync(url);
                var json = JsonDocument.Parse(response);
                double latitude = 0;
                double longitude = 0;
                if (json.RootElement.GetArrayLength() > 0)
                {
                    var location = json.RootElement[0];
                    string latString = location.GetProperty("lat").GetString();
                    string lonString = location.GetProperty("lon").GetString();
                    if (double.TryParse(latString, out double latResult))
                    {
                        latitude = latResult;
                    }

                    if (double.TryParse(lonString, out double lonResult))
                    {
                        longitude = lonResult;
                    }

                }
                var propertyDto = new PropertyDto
                {
                    ID = properties.Id,
                    LandMark = properties.Landmark,
                    Price = properties.Price,
                    Location = properties.Location,
                    Latutide = latitude,
                    Longitude = longitude
                };

                return propertyDto;

            }
            catch(Exception ex) {
                return null;
            }
            }
        public async Task AddPropertyAsync(Property property)
        {
            _propertyRepository.Add(property);
            var agentproperty=_mapper.Map<AgentProperty>(property);

            
            await _context.SaveChangesAsync();
        }
        public async Task<PaginatedDto<PropertyDto>> GetPropertyListByAgentId(int id,Paginationparameters paginationparameters)
        {
            var totalCountQuery = _context.Properties
        .Include(x => x.AgentProperties)
        .Where(x => x.AgentProperties.Any(ap => ap.AgentId == id));

            var totalCount = await totalCountQuery.CountAsync();

            // Query for retrieving the paginated items
            var propertiesQuery = totalCountQuery
                .OrderBy(x => x.Id)
                .Skip((paginationparameters.currentpage) * paginationparameters.PageSize)
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

            var enquiries = _context.Enquiries
                .Where(e => propertyIds.Contains(e.PorpertyId))
                .ToList();


            return enquiries;
        }
        public void CreateProperty(Property property)
        {
            _context.Properties.Add(property);
        }
        public List<ChartModel> CreateChart()
        {
            var prices = _context.Properties
                .Select(x => new ChartModel { Price = x.Price ?? 0, Property = x.Location }) 
                .Take(15)
                .ToList();

            return prices;
        }




    }
}
