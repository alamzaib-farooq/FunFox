using FunFox.Domain.Entities;

namespace FunFox.Application.Contracts.Repositories
{
    public interface IEnrollmentRepository
    {
        Task<List<Enrollment>> GetAllEnrollments(int id);
        Task<List<Enrollment>> GetAllEnrollments();
    }
}
