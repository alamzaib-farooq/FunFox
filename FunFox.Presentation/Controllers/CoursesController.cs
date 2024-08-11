using FunFox.Application.Contracts;
using FunFox.Application.Dtos;
using FunFox.Domain.Entities;
using FunFox.Infrastructure.Identity;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FunFox.Presentation.Controllers
{

    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly UserManager<ApplicationUser> _userManager;

        //User service can be injected in services if it's setup properly
        public CoursesController(ICourseService courseService, UserManager<ApplicationUser> userManager)
        {
            _courseService = courseService;
            _userManager = userManager;
        }
        [Authorize(Roles = "Administrator")]
        // GET: CoursesController
        public async Task<ActionResult> Index()
        {
            return View(await _courseService.GetAllCoursesAsync());
        }
        //
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseDto dto, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                await _courseService.AddCourse(dto.Adapt<Course>(), cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseService.GetCourse((int)id);

            if (course == null)
            {
                return NotFound();
            }

            await _courseService.DeleteCourse(course, cancellationToken);

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseService.GetCourse((int)id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course.Adapt<CourseDto>());
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseDto courseToUpdate, CancellationToken cancellationToken)
        {
            if (id != courseToUpdate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var course = await _courseService.GetCourse(id);
                    await _courseService.UpdateCourse(course, courseToUpdate, cancellationToken);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(courseToUpdate);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> ExploreCourses()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(await _courseService.GetAllCoursesAsync(userId));
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Enroll(int? id, CancellationToken cancellationToken)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var enrollmentCount = await _courseService.CheckEnrollCount((int)id);


                var course = await _courseService.GetCourse((int)id);

                if (course == null)
                {
                    return NotFound();
                }

                if (enrollmentCount > course?.MaxClassSize)
                {
                    RedirectToAction("CustomMessage", new { error = "Limit exceed." });
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _courseService.Enroll((int)id, userId, cancellationToken);
            }
            catch (Exception e)
            {
                return RedirectToAction("CustomMessage", new { error = "You are already enrolled" });
            }

            return RedirectToAction("CustomMessage", new { error = "You are enrolled!" });


        }

        public ActionResult CustomMessage(string error)
        {
            ViewBag.Error = error;
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EnrolledUsers(int? courseId)
        {
            if (courseId == null)
            {
                return NotFound();
            }
            var enrollments = await _courseService.GetAllEnrollmentsForCourseAsync((int)courseId);
            var users = _userManager.Users.Where(x => enrollments.Select(x => x.UserId).Contains(x.Id)).ToList();
            return View(users);
        }



    }
}
