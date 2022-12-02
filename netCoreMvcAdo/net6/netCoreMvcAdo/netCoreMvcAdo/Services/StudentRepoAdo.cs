using netCoreMvcAdo.Models;
using System.Data.SqlClient;

namespace netCoreMvcAdo.Services
{
    public class StudentRepoAdo : IStudentRepo
    {
        private string _connection;
        private IConfiguration _configuration;
        private SqlConnection con;
        SqlDataReader reader;
        public StudentRepoAdo(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("Default");
        }

        public async Task<List<Student>> GetStudents()
        {
            List<Student> students = new List<Student>();
            try
            {
                using (con = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_GetAllStudents",con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    await con.OpenAsync();
                    //for connected scenario
                    using(reader = await cmd.ExecuteReaderAsync())
                    {
                        Student student;
                        while (reader.Read())
                        {
                            student = new Student()
                            {
                                Id = Convert.ToInt32(reader["ID"]),
                                Name = Convert.ToString(reader["Name"]),
                                Email = Convert.ToString(reader["Email"])
                            };
                            students.Add(student);
                        }
                    }
                }
                return students;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Student>().ToList();
            }
        }
    }
}
