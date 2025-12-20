namespace ConstructionFinance.API.DTOs.Project
{
    public class ProjectDto : CreateProjectDto
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
    }

}
