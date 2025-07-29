using Tms.Application.DTOs.Lookup;

namespace Tms.Application.DTOs.Tender;

public class TenderDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public CategoryDto Category { get; set; } = null!;
    public StatusDto Status { get; set; } = null!;
} 