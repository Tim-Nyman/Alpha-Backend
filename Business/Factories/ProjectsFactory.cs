using Business.Models;
using Infrastructure.Data.Entities;

namespace Business.Factories;

public class ProjectsFactory
{
    public static Project ToModel(ProjectEntity entity)
    {
        return entity == null
            ? null!
            : new Project
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
    }

    public static ProjectEntity ToEntity(UpdateProjectForm projectForm)
    {
        return projectForm == null
            ? null!
            : new ProjectEntity
            {
                Id = projectForm.Id,
                ImageUrl = projectForm.ImageUrl,
                ProjectName = projectForm.ProjectName,
                Description = projectForm.Description,
                StartDate = projectForm.StartDate,
                EndDate = projectForm.EndDate,
                Budget = projectForm.Budget,
                ClientId = projectForm.ClientId,
                UserId = projectForm.UserId,
                StatusId = projectForm.StatusId
            };
    }

    public static ProjectEntity ToEntity(AddProjectForm projectForm)
    {
        return projectForm == null
            ? null!
            : new ProjectEntity
            {
                ImageUrl = projectForm.ImageUrl,
                ProjectName = projectForm.ProjectName,
                Description = projectForm.Description,
                StartDate = projectForm.StartDate,
                EndDate = projectForm.EndDate,
                Budget = projectForm.Budget,
                ClientId = projectForm.ClientId,
                UserId = projectForm.UserId,
            };
    }
}
