using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddWalkRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be a maximum of 100 characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "Description has to be a maximum of 1000 characters")]
        public string Description { get; set; }

        [Required]
        [Range(0,50, ErrorMessage = "length must be between 0 and 50 Km")]
        public double LengthInKm { get; set; } // Length in kilometers

        public string? WalkImageUrl { get; set; }// URL to an image of the walk

        [Required]
        public Guid RegionId { get; set; } // Foreign key to Region

        [Required]
        public Guid DifficultyId { get; set; } // Foreign key to Difficulty
    }
}
