namespace api.Dtos.Recruit
{
    public class RecruitDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        // ✅ Updated Field Relationship
        public int? FieldId { get; set; }
        public string? FieldName { get; set; } // To return field name
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int AccountId { get; set; } // Foreign key reference
    }
}
