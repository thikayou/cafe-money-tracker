using Microsoft.EntityFrameworkCore;

namespace CafeTracker.Data.Models;

public class Contribution
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid? GoalId { get; set; }
    public Goal? Goal { get; set; }

    [Precision(18, 2)]
    public decimal Amount { get; set; }

    public int Month { get; set; }
    public int Year { get; set; }
    public string? Note { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}