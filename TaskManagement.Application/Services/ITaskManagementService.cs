using TaskManagement.Application.InputModels;
using TaskManagement.Application.ViewModels;
using TaskManagement.Core.Entities;

namespace TaskManagement.Application.Services
{
    public interface ITaskManagementService
    {
        Task<TaskEntity> AddComment(Guid idTask, string input);
        Task<TaskEntity> AddFollowUp(Guid idTask, TaskUpdateInputModel input, Guid userId);
        Task<ProjectViewModel> AddProject(Project input);
        Task<TaskViewModel> AddTask(Guid idProject, TaskEntity input);
        Task DeleteProject(Guid idProject);
        Task DeleteTask(Guid idTask);
        Task<List<ProjectViewModel>> GetAllProjects();
        Task<ProjectViewModel?> GetProject(Guid idProject);
        Task<TaskEntityViewModel?> GetTask(Guid idTask);
        Task UpdateTask(Guid id, TaskUpdateInputModel task);
        Task<FollowUpViewModel?> GetFollowUp(Guid idFollowUp);
        Task<List<FollowUpViewModel>> GetAllFollowUp();
        Task<List<CommentsViewModel>> GetComments(Guid idTask);
    }
}