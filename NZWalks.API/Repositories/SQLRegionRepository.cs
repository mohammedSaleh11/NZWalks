using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }
        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> CreateAsync(Region region)
        {
            // Add the region to the database   
            await dbContext.Regions.AddAsync(region);
            // Save changes to the database
            await dbContext.SaveChangesAsync();
            // Return the created region
            return region;
        }
        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            // Find the existing region by id
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null; // Return null if the region does not exist
            }
            // Update the properties of the existing region
            existingRegion.Name = region.Name;
            existingRegion.Code = region.Code;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            // Save changes to the database
            await dbContext.SaveChangesAsync();
            // Return the updated region
            return existingRegion;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            // Find the existing region by id
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null; // Return null if the region does not exist
            }
            // Remove the region from the database
            dbContext.Regions.Remove(existingRegion);
            // Save changes to the database
            await dbContext.SaveChangesAsync();
            // Return the deleted region
            return existingRegion;
        }
}
