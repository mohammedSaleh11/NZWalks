namespace NZWalks.API.Models.DTO
{
    public class AddWalkRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; } // Length in kilometers
        public string? WalkImageUrl { get; set; } // URL to an image of the walk
        public Guid RegionId { get; set; } // Foreign key to Region
        public Guid DifficultyId { get; set; } // Foreign key to Difficulty
    }
}
