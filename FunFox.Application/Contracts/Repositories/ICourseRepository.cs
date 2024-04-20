using FunFox.Domain.Entities;

namespace FunFox.Application.Contracts.Repositories
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllCourses();
    }
}
