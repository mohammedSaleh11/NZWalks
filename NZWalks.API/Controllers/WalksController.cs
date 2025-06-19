using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }


        //Create WAlk
        //POST: api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
                //map the DTO to the Domain model
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

                //save the Walk to the database
                walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);

                //map the Domain model to DTO
                var walkDto = mapper.Map<WalkDto>(walkDomainModel);

                // return the walk;
                return Ok(walkDto);
        }

        //Get All Walks
        //Get: api/walks?filterOn=Name%filterQuery=Track&sortBy=Name&isAscending=true$pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string ? filterOn, [FromQuery] string ? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber=1, [FromQuery] int pageSize=1000)
        {
            //Get all walks from the database / domain model
            var walksDomain = await walkRepository.GetAllAsync(filterOn,filterQuery,sortBy,isAscending ?? true,
                pageNumber,pageSize);
            //map domain model to DTOs 
            var walksDto = mapper.Map<List<WalkDto>>(walksDomain);
            //return the DTOs
            return Ok(walksDto);
        }

        //Get Walk by Id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get the walk from the database / domain model
            var walkDomain = await walkRepository.GetByIdAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }
            //map domain model to DTOs
            var walkDto = mapper.Map<WalkDto>(walkDomain);
            //return the DTO
            return Ok(walkDto);
        }

        //Update Walk
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
                //map the DTO to the Domain model
                var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
                //save the Walk to the database
                walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);
                if (walkDomainModel == null)
                {
                    return NotFound();
                }
                //map the Domain model to DTO
                var walkDto = mapper.Map<WalkDto>(walkDomainModel);
                // return the walk;
                return Ok(walkDto);
            }

        //Delete Walk
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //Delete the walk from the database
            var walkDomain = await walkRepository.DeleteAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }
            //map the Domain model to DTO
            var walkDto = mapper.Map<WalkDto>(walkDomain);
            //return NoContent as the response
            return Ok(walkDto);
        }
    }
} 
