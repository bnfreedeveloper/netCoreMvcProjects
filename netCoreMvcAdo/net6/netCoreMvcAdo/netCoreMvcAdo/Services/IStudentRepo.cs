using netCoreMvcAdo.Models;

namespace netCoreMvcAdo.Services
{
    public interface IStudentRepo
    {
        Task<List<Student>> GetStudents();
    }
}
