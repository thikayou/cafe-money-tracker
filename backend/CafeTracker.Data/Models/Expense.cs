using Microsoft.EntityFrameworkCore;

namespace CafeTracker.Data.Models;

public class Expense
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    [Precision(18, 2)]
    public decimal Amount { get; set; }

    public string Category { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}