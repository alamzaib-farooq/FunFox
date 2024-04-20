using FluentValidation;
using FunFox.Application.Contracts;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FunFox.Application.Dtos
{
    public class CourseDto : IDto
    {
        public int Id { get; set; }
        [DisplayName("Course Name")]
        [MaxLength(100)]
        public string ClassName { get; set; }
        [DisplayName("Grade Level")]
        [Required]
        public string GradeLevel { get; set; }
        [DisplayName("Start Date")]
        [Required]
        public DateTime StartDate { get; set; }
        [DisplayName("End Date")]
        [Required]
        public DateTime EndDate { get; set; }
        [DisplayName("Start Time")]
        [Required]
        public TimeSpan StartTime { get; set; }
        [DisplayName("End Time")]
        [Required]
        public TimeSpan EndTime { get; set; }
        [DisplayName("Max. Class Size")]
        [Range(1, 300)]
        public int MaxClassSize { get; set; }

        public bool IsEnrolled { get; set; }


    }

    public class CourseDtoValidator : AbstractValidator<CourseDto>
    {
        public CourseDtoValidator()
        {
            RuleFor(x => x.ClassName).NotEmpty().NotNull().MaximumLength(100).MinimumLength(1);
            RuleFor(x => x.StartTime).NotEmpty().NotNull();
            RuleFor(x => x.EndDate).NotEmpty().NotNull();
            RuleFor(x => x.StartTime).NotEmpty().NotNull();
            RuleFor(x => x.EndTime).NotEmpty().NotNull();
            RuleFor(x => x.MaxClassSize).NotEmpty().NotNull();
            RuleFor(x => x.GradeLevel).NotEmpty().NotNull();
        }
    }
}
