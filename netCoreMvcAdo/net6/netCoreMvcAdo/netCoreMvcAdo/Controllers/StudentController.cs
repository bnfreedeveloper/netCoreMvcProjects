using Microsoft.AspNetCore.Mvc;
using netCoreMvcAdo.Services;

namespace netCoreMvcAdo.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepo _studentRepo;
        public StudentController(IStudentRepo studentRepository)
        {
            _studentRepo = studentRepository;   
        }
        [HttpGet("students")]
        public IActionResult GetAllStudent()
        {
            return View();
        }
    }
}
