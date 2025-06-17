namespace NZWalks.API.Models.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; } // Length in kilometers
        public string? WalkImageUrl { get; set; } // URL to an image of the walk
            // Navigation properties
            public RegionDto Region { get; set; }
            public DifficultyDto Difficulty { get; set; }
    }
}
