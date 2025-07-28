namespace Tms.Application.DTOs.Tender;

public class CreateTenderRequestDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }
    public decimal? EstimatedBudget { get; set; }
    public string Requirements { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public int StatusId { get; set; }
} 