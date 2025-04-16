namespace Business.Models;

public class UpdateProjectForm
{
    public string Id { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string ProjectName { get; set; } = null!;
    public string? Description { get; set; }
    public decimal? Budget { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public string UserId { get; set; } = null!;
    public int ClientId { get; set; }
    public int StatusId { get; set; }
}
