using FunFox.Application.Contracts;
using FunFox.Application.Contracts.Repositories;
using FunFox.Application.Dtos;
using FunFox.Domain.Entities;
using Mapster;
using Microsoft.Extensions.Logging;

namespace FunFox.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseRepository _courseRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ILogger<CourseService> _logger;

        public CourseService(IUnitOfWork unitOfWork,
            ICourseRepository courseRepository,
            IEnrollmentRepository enrollmentRepository,
            ILogger<CourseService> logger)
        {
            _unitOfWork = unitOfWork;
            _courseRepository = courseRepository;
            _enrollmentRepository = enrollmentRepository;
            _logger = logger;
        }

        public async Task<List<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllCourses();

            return courses.Adapt<List<CourseDto>>();
        }

        public async Task AddCourse(Course course, CancellationToken cancellationToken)
        {

            var result = await _unitOfWork.Repository<Course>().AddAsync(course);
            _logger.LogInformation("New course added.");
            await _unitOfWork.Save(cancellationToken);
        }

        public async Task<Course?> GetCourse(int id)
        {
            return await _unitOfWork.Repository<Course>().GetByIdAsync(id);
        }

        public async Task DeleteCourse(Course course, CancellationToken cancellationToken)
        {
            await _unitOfWork.Repository<Course>().DeleteAsync(course);
            _logger.LogInformation(course.ClassName + " deleted.");
            await _unitOfWork.Save(cancellationToken);
        }

        public async Task UpdateCourse(Course course, CourseDto courseToUpdate, CancellationToken cancellationToken)
        {
            await _unitOfWork.Repository<Course>().UpdateAsync(courseToUpdate.Adapt<Course>());
            _logger.LogInformation(course.ClassName + " updated.");
            await _unitOfWork.Save(cancellationToken);
        }

        public async Task Enroll(int courseId, string userId, CancellationToken cancellationToken)
        {
            var enroll = new Enrollment() { CourseId = courseId, UserId = userId };
            await _unitOfWork.Repository<Enrollment>().AddAsync(enroll);
            _logger.LogInformation("User: " + userId + " enrolled course: " + courseId);
            await _unitOfWork.Save(cancellationToken);
        }

        public async Task<int> CheckEnrollCount(int courseId)
        {
            //There can be better approach rather than fetching all records. Due to shortage of time I am doing this.
            var enrollments = await _enrollmentRepository.GetAllEnrollments(courseId);
            return enrollments.Count;
        }

        public async Task<List<CourseDto>> GetAllCoursesAsync(string userId)
        {
            //To optimized it we can use includes here so that for each course we can add all enrollment in navigation collection.
            //Because of tight schedule did not do that. 
            var courses = await _courseRepository.GetAllCourses();
            var enrollments = await _enrollmentRepository.GetAllEnrollments();
            var courseList = courses.Adapt<List<CourseDto>>();
            foreach (var course in courseList)
            {
                if (enrollments.Any(x => x.CourseId == course.Id && x.UserId == userId))
                {
                    course.IsEnrolled = true;
                }
            }

            return courseList;
        }

        public async Task<List<Enrollment>> GetAllEnrollmentsForCourseAsync(int courseId)
        {
            var enrollments = await _enrollmentRepository.GetAllEnrollments(courseId);
            return enrollments;
        }

    }
}
