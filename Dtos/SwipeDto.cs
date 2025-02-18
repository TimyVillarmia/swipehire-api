using System;

namespace api.Dtos;

public class SwipeDto
{
    public int Id { get; set; }
    public int RecruitId { get; set; }
    public int InternId { get; set; }
    public DateTime SwipeDate { get; set; }
    public bool Status { get; set; }
}