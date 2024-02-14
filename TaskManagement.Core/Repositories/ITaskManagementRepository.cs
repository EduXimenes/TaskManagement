using TaskManagement.Core.Entities;

namespace TaskManagement.Infrastructure.Persistence.Repositories
{
    public interface ITaskManagementRepository
    {
        Task<Project> AddProject(Project project); 
        Task<TaskEntity> AddTask(Guid id, TaskEntity task); 
        Task DeleteProject(Guid idProject); 
        Task DeleteTask(Guid idTask);
        Task<List<Project>> GetAllProjects();   
        Task<Project?> GetProject(Guid id);  
        Task<TaskEntity?> GetTask(Guid id); 
        Task UpdateTask(Guid id, TaskEntity task); 
        Task<TaskEntity> AddComments(Guid id, string comments); 
        Task<TaskEntity> AddFollowUp(Guid id, TaskEntity task, Guid userId); 
    }
}