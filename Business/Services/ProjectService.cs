using Business.Factories;
using Business.Models;
using Data.Repositories;
using Infrastructure.Data.Entities;

namespace Business.Services;

public interface IProjectService
{
    Task<bool> CreateProjectAsync(AddProjectForm projectForm, int defaultStatus = 1);
    Task<bool> DeleteProjectAsync(string id);
    Task<Project?> GetProjectByIdAsync(string id);
    Task<IEnumerable<Project>?> GetProjectsAsync();
    Task<bool> UpdateProjectAsync(UpdateProjectForm projectForm);
}

public class ProjectService(ProjectRepository projectRepository) : IProjectService
{
    private readonly ProjectRepository _projectRepository = projectRepository;

    public async Task<bool> CreateProjectAsync(AddProjectForm projectForm, int defaultStatus = 1)
    {
        if (projectForm == null)
            return false;

        var project = ProjectsFactory.ToEntity(projectForm);
        project.StatusId = defaultStatus;

        //Try Catch i och med att man skickar in något i databasen? eller räcker det med try catch i BaseRepositoryn?
        var result = await _projectRepository.AddAsync(project);
        return result;
    }

    public async Task<IEnumerable<Project>?> GetProjectsAsync()
    {
        var entities = await _projectRepository.GetAllAsync();
        if (entities == null)
            return null;

        var projects = entities.Select(ProjectsFactory.ToModel);

        projects = projects.OrderByDescending(x => x.Created);
        return projects;
    }

    public async Task<Project?> GetProjectByIdAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return null;

        var entity = await _projectRepository.GetAsync(x => x.Id == id);
        if (entity == null)
            return null;

        //Vill testa mappa utan factory också - därav att jag inte använder ProjectFactory här.
        var project = new Project
        {
            Id = entity.Id,
            ImageUrl = entity.ImageUrl,
            ProjectName = entity.ProjectName,
            Description = entity.Description,
            Budget = entity.Budget,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Created = entity.Created,
            User = new User
            {
                Id = entity.User.Id,
                FirstName = entity.User.FirstName,
                LastName = entity.User.LastName
            },
            Client = new Client
            {
                Id = entity.Client.Id,
                ClientName = entity.Client.ClientName
            },
            Status = new Status
            {
                Id = entity.Status.Id,
                StatusName = entity.Status.StatusName
            }
        };

        return project;
    }

    public async Task<bool> UpdateProjectAsync(UpdateProjectForm projectForm)
    {
        if (projectForm == null)
            return false;

        if (!await _projectRepository.ExistsAsync(x => x.Id == projectForm.Id))
            return false;

        var project = ProjectsFactory.ToEntity(projectForm);
        var result = await _projectRepository.UpdateAsync(project);
        return result;
    }

    public async Task<bool> DeleteProjectAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return false;

        if(!await _projectRepository.ExistsAsync(x => x.Id == id))
            return false;

        var result = await _projectRepository.DeleteAsync(x => x.Id == id);
        return result;
    }
}
