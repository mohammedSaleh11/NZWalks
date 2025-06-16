using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        //Get All Regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get all regions from the database / domain model
            var regionsDomain =await regionRepository.GetAllAsync();

            //map domain model to DTOs 
            var regionsDto=mapper.Map<List<RegionDto>>(regionsDomain);
            //return the DTOs
            return Ok(regionsDto);
        }

        // Get Region by Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get the region from the database / domain model
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            //map domain model to DTOs
            
            var regionDto = mapper.Map<RegionDto>(regionDomain);
            //return the DTO
            return Ok(regionDto);
        }

        // Add Region
        [HttpPost]
        public async  Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequest)
        {
            //map DTO to domain model
            var regionDomainModel = mapper.Map<Region>(addRegionRequest);
            //save the region to the database
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);


            //map domain model back to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            //return the created region
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // Update Region
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {


            //map DTO to domain model
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
            //change if region exists
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
        
            //map domain model back to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            //return the updated region
            return Ok(regionDto);
        }

        //Delete Region
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //Get the region from the database
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //map domain model to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);   

            //return no content
            return Ok(regionDto);

        }
    }
}
