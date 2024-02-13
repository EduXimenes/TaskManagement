using TaskManagement.Core.Entities;

namespace TaskManagement.Infrastructure.Persistence.Repositories
{
    public interface ITaskManagementRepository
    {
        Project AddProject(Project project); 
        TaskEntity AddTask(Guid id, TaskEntity task);
        void DeleteProject(Guid idProject);
        void DeleteTask(Guid idTask);
        List<Project> GetAllProjects();
        Project? GetProject(Guid id);
        TaskEntity? GetTask(Guid id);
        void UpdateTask(Guid id, TaskEntity task);
        TaskEntity AddComments(Guid id, string comments);
        void AddFollowUp(Guid id, TaskEntity task, Guid userId);
    }
}