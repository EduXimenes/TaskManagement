using TaskManagement.Application.ViewModels;

namespace TaskManagement.Application.Services
{
    public interface IPerformanceService
    {
        Task<List<ReportViewModel>> GetReport(Guid idUser);
    }
}