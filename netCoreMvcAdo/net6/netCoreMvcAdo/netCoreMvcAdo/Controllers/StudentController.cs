using Microsoft.AspNetCore.Mvc;
using netCoreMvcAdo.Models;
using netCoreMvcAdo.Services;

namespace netCoreMvcAdo.Controllers
{
    [Route("student/")]
    public class StudentController : Controller
    {
        private readonly IStudentRepo _studentRepo;
        public StudentController(IStudentRepo studentRepository)
        {
            _studentRepo = studentRepository;   
        }
        [HttpGet("students")]
        public async Task<IActionResult> GetAllStudent()
        {
            return View("students",await _studentRepo.GetStudents()); 
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStudentById(int id)
        { 
            //i return a list with one entree, only because i can reuse a view 
            return View("students", new List<Student>() { await _studentRepo.GetStudentById(id) });
        }

        [HttpGet("update/{id:int}")]
        public async Task<IActionResult> Update(int id)
        {
            return View("UpdateStudent", await _studentRepo.GetStudentById(id));
        }
        [HttpPost("update/{id:int}")]  
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult>updateStudent(Student std)
        {
            var result = await _studentRepo.UpdateStudent(std);
            if (result)
            {
                return RedirectToAction(nameof(GetAllStudent), "Student");
            }
            else {
                TempData["message"] = "something went wrong, try again";
                return RedirectToAction(nameof(Update), "Student", new {id=std.Id});
            }
        }
        [HttpPost("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _studentRepo.DeleteStudent(id);
            if (result)
            {

                return RedirectToAction(nameof(GetAllStudent), "Student");
            }
            else
            {
                TempData["problem"] = "something went wrong, try again";
                return RedirectToAction(nameof(GetAllStudent), "Student");
            }
        }
    }
}
