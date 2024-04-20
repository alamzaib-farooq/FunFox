using FunFox.Domain.Common;

namespace FunFox.Domain.Entities
{
    public class Course : BaseAuditableEntity
    {
        public string ClassName { get; set; }
        public string GradeLevel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int MaxClassSize { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }

        public Course()
        {

        }

        public Course(string className, string gradeLevel, DateTime startDate, DateTime endDate, TimeSpan startTime, TimeSpan endTime, int maxClassSize)
        {
            ClassName = className;
            GradeLevel = gradeLevel;
            StartDate = startDate;
            EndDate = endDate;
            StartTime = startTime;
            EndTime = endTime;
            MaxClassSize = maxClassSize;
        }

    }
}
