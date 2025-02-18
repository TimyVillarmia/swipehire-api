using System;

namespace api.Dtos;

public class InternWorkExperienceDto
{
    public int Id { get; set; }
    public string Company { get; set; } = string.Empty;
    public string? CompanyLocation { get; set; }
    public string Position { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int InternId { get; set; }
}
