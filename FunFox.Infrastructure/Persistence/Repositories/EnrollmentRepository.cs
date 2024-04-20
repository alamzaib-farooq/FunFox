using FunFox.Application.Contracts.Repositories;
using FunFox.Domain.Entities;

namespace FunFox.Infrastructure.Persistence.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly IGenericRepository<Enrollment> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public EnrollmentRepository(IGenericRepository<Enrollment> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Enrollment>> GetAllEnrollments(int id)
        {
            return await _repository.GetAllByWhereWithNoTrackingAsync(x => x.CourseId == id);
        }

        public async Task<List<Enrollment>> GetAllEnrollments()
        {
            return await _repository.GetAllAsync();
        }
    }
}
