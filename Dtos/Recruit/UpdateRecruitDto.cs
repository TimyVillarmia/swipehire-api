using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Recruit
{
    public class UpdateRecruitDto
    {
        [Required, MaxLength(100)]
        public required string Firstname { get; set; }

        [Required, MaxLength(100)]
        public required string Lastname { get; set; }

        [Required, MaxLength(100)]
        public required string Position { get; set; }

        [Required, MaxLength(150)]
        public required string Company { get; set; }

        [Required, MaxLength(100)]
        public required string Field { get; set; }

        [Required, MaxLength(255)]
        public required string Address { get; set; }

        [Required, Phone, MaxLength(15)]
        public required string PhoneNumber { get; set; }
    }
}
