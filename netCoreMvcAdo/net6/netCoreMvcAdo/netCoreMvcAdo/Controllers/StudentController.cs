using Microsoft.AspNetCore.Mvc;
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
    }
}
