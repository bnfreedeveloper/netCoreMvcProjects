using authAuthorization.Models.Dtos;
using authAuthorization.Repositories.IRepositories;

namespace authAuthorization.Repositories.Repositories
{
    public class UserAuthRepository : IUserAuthRepo
    {
        public Task<Status> LoginAsync(Login loginDto)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAync()
        {
            throw new NotImplementedException();
        }

        public Task<Status> RegistrationAsync(Registration registrationDto)
        {
            throw new NotImplementedException();
        }
    }
}
