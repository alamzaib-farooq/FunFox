using FunFox.Application.Contracts.Repositories;
using FunFox.Domain.Entities;

namespace FunFox.Infrastructure.Persistence.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly IGenericRepository<Course> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CourseRepository(IGenericRepository<Course> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Course>> GetAllCourses()
        {

            return await _repository.GetAllAsync();
        }




    }
}
