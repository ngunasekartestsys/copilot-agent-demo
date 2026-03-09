namespace TicketTrackingApi.Models;

public class CreateTicketRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TicketPriority Priority { get; set; } = TicketPriority.Medium;
    public string Category { get; set; } = string.Empty;
    public string CreatedBy { get; set; } = string.Empty;
    public string? AssignedTo { get; set; }
}
