using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities;

public class Swipe
{
    public int Id { get; set; }

    // Foreign Key for Recruit (Recruit swipes)
    public int RecruitId { get; set; }
    public required Recruit Recruit { get; set; }  // Recruit performs the swipe

    // Foreign Key for Intern (Intern being swiped)
    public int InternId { get; set; }
    public required Intern Intern { get; set; }  // Intern is being swiped

    public DateTime SwipeDate { get; set; } = DateTime.Now;

    [Column(TypeName = "bit")]
    public bool Status { get; set; }  // True (liked), False (disliked)
}
