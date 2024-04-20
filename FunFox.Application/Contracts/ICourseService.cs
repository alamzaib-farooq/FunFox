using FunFox.Application.Dtos;
using FunFox.Domain.Entities;

namespace FunFox.Application.Contracts
{
    public interface ICourseService
    {
        Task<List<CourseDto>> GetAllCoursesAsync();
        Task AddCourse(Course course, CancellationToken cancellationToken);
        Task<Course?> GetCourse(int id);
        Task DeleteCourse(Course course, CancellationToken cancellationToken);
        Task UpdateCourse(Course course, CourseDto courseToUpdate, CancellationToken cancellationToken);
        Task Enroll(int courseId, string userId, CancellationToken cancellationToken);
        Task<int> CheckEnrollCount(int courseId);
        Task<List<CourseDto>> GetAllCoursesAsync(string userId);
        Task<List<Enrollment>> GetAllEnrollmentsForCourseAsync(int courseId);
    }
}
