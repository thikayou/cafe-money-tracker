using Microsoft.EntityFrameworkCore;

namespace CafeTracker.Data.Models;

public class Goal
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    
    [Precision(18, 2)]
    public decimal TargetAmount { get; set; }
    
    [Precision(18, 2)]
    public decimal CurrentAmount { get; set; }

    public DateTime? Deadline { get; set; }
    public string Status { get; set; } = "Active";

    public ICollection<Contribution> Contributions { get; set; } = new List<Contribution>();
}