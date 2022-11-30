using authAuthorization.Models.Dtos;

namespace authAuthorization.Repositories.IRepositories
{
    public interface IUserAuthRepo
    {
        Task<Status> LoginAsync(Login loginDto);
        Task<Status> RegistrationAsync(Registration registrationDto);
        Task LogoutAync();
    }
}
