using netCoreMvcAdo.Models;

namespace netCoreMvcAdo.Services
{
    public interface IStudentRepo
    {
        Task<List<Student>> GetStudents();
        Task<Student> GetStudentById(int id);
        Task<bool> UpdateStudent(Student student);
        Task<bool> DeleteStudent(int id);  
        Task<bool> AddStudent(Student student); 
    }
}
