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

        public async Task<Student> GetStudentById(int id)
        {
            Student student = new Student();    
            using(con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("select * from Students where Id = @Id", con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                await con.OpenAsync();
                using(reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        student = new Student
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = Convert.ToString(reader["Name"]),
                            Email = Convert.ToString(reader["Email"])
                        };
                    }
                }
                return student;
            }
        }
        public async Task<bool> UpdateStudent(Student student)
        {
            try
            {
                using (con = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_UpdateStudent", con);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", student.Id);
                    cmd.Parameters.AddWithValue("@Name", student.Name);
                    cmd.Parameters.AddWithValue("@Email", student.Email);
                    SqlParameter output = new SqlParameter();
                    output.ParameterName = "@Result";
                    output.SqlDbType = System.Data.SqlDbType.Bit;
                    output.Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(output);
                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return Convert.ToInt32(output.Value) == 1;


                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteStudent(int id)
        {
            try
            {
                using(con = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("Delete from Students where Id = @Id",con);
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@Id",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Input,
                        Value = id
                    });
                    await con.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> AddStudent(Student student)
        {
            try
            {
                using (con = new SqlConnection(_connection))
                {
                    SqlCommand cmd = new SqlCommand("SP_AddStudent", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@Name",
                        Value = student.Name,
                        Direction = System.Data.ParameterDirection.Input,
                        SqlDbType = System.Data.SqlDbType.NVarChar
                    });
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@Email",
                        Value = student.Email,
                        Direction = System.Data.ParameterDirection.Input,
                        SqlDbType = System.Data.SqlDbType.NVarChar
                    });
                    await con.OpenAsync();
                    var result = await cmd.ExecuteNonQueryAsync();
                    return result == 1;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
    }
}
