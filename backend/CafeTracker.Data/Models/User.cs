using Microsoft.AspNetCore.Identity;

namespace CafeTracker.Data.Models;

public class User : IdentityUser<Guid>
{
    public string FullName { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Contribution> Contributions { get; set; } = new List<Contribution>();
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}