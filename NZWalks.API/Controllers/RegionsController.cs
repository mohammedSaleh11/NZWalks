using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        //Get All Regions
        [HttpGet]
        public IActionResult GetAll()
        {
            //Get all regions from the database / domain model
            var regionsDomain = dbContext.Regions.ToList();

            //map domain model to DTOs
            var regionsDto = new List<Models.DTO.RegionDto>();
            foreach (var region in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                });
            }
            //return the DTOs
            return Ok(regionsDto);
        }

        // Get Region by Id
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //Get the region from the database / domain model
            var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (region == null)
            {
                return NotFound();
            }

            //map domain model to DTOs
            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            };
            //return the DTO
            return Ok(regionDto);
        }

        // Add Region
        [HttpPost]
        public IActionResult Create ([FromBody] AddRegionRequestDto addRegionRequest)
        {
            //map DTO to domain model
            var regionDomainModel = new Region()
            {
                Name = addRegionRequest.Name,
                Code = addRegionRequest.Code,
                RegionImageUrl = addRegionRequest.RegionImageUrl
            };
            //save the region to the database
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();


            //map domain model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            //return the created region
            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // Update Region
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Get the region from the database
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //map DTO to domain model
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
            //save the changes to the database
            dbContext.SaveChanges();
            //map domain model back to DTO
            var regionDto = new RegionDto()
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            //return the updated region
            return Ok(regionDto);
        }
    }
}
