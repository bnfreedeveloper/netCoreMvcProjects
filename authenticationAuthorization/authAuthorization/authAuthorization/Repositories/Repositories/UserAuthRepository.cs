using authAuthorization.Models.Dtos;
using authAuthorization.Models.Entities;
using authAuthorization.Repositories.IRepositories;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace authAuthorization.Repositories.Repositories
{
    public class UserAuthRepository : IUserAuthRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserAuthRepository(UserManager<ApplicationUser> usermanag,SignInManager<ApplicationUser> signinManag,
            RoleManager<IdentityRole> roleManag)
        {
            _userManager = usermanag;
            _signinManager = signinManag;
            _roleManager = roleManag;   
        }
        public async Task<Status> LoginAsync(Login loginDto)
        {
            var userFound = await _userManager.FindByNameAsync(loginDto.UserName);
            if (userFound == null) return new Status
            {
                StatusCode = 0,
                StatusMessage = "user not found"
            };
            // i check firt if the password match but i could skip this part
            // and just use directly passwordAsync and block attempt to login after 5 failed 
            
            if(!await _userManager.CheckPasswordAsync(userFound, loginDto.Password))
            {
                return new Status
                {
                    StatusCode = 0,
                    StatusMessage = "invalid password"
                };

            };
            var result = await _signinManager.PasswordSignInAsync(userFound, loginDto.Password, false, true);
            
            if (result.Succeeded)
            {
                //not needed coz the signinmananager will add all the claims related to this user

                //var userRoles = await _userManager.GetRolesAsync(userFound);
                //var userClaims = new List<Claim>()
                //{
                //    new Claim(ClaimTypes.Name,userFound.UserName)
                //};
                //foreach (var role in userRoles)
                //{
                //    userClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                //}
                return new Status
                {
                    StatusCode = 1,
                    StatusMessage = "logged in successfully"
                };

            }
            else if (result.IsLockedOut)
            {
                return new Status
                {
                    StatusCode = 0,
                    StatusMessage = "invalid credentials"
                };
            }
            else return new Status
            {
                StatusCode = 0,
                StatusMessage = "an error occured,try again"
            };
        }

        public async Task LogoutAync()
        {
            await _signinManager.SignOutAsync();
        }

        // i could have used transaction to manage exception and rollback 
        //but i wanted to show how usefull are the class provided by entity
        public async Task<Status> RegistrationAsync(Registration registrationDto)
        {
            var checkUserExists = await _userManager.FindByEmailAsync(registrationDto.Email);
            if (checkUserExists != null) return new Status()
            {
                StatusCode = 0,
                StatusMessage = "user already exists"
            };
            ApplicationUser user = new ApplicationUser
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Name = registrationDto.Name,    
                UserName = registrationDto.UserName,    
                Email = registrationDto.Email
            };
            try
            {
                var result = await _userManager.CreateAsync(user, registrationDto.Password);
                if (!result.Succeeded) return new Status()
                {
                    StatusCode = 0,
                    StatusMessage = "user registration failed"
                };
                // we add some role
                if (!await _roleManager.RoleExistsAsync(registrationDto.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(registrationDto.Role));
                }
                if (await _roleManager.RoleExistsAsync(registrationDto.Role))
                {
                    await _userManager.AddToRoleAsync(user, registrationDto.Role);
                }
                return new Status()
                {
                    StatusCode = 1,
                    StatusMessage = "user registration successfull"
                };
            }
            catch (Exception ex)
            {
                var role = await _roleManager.FindByNameAsync(registrationDto.Role);
                if(role != null)
                {
                    await _userManager.RemoveFromRoleAsync(user, registrationDto.Role);
                    await _roleManager.DeleteAsync(role);
                }
                await _userManager.DeleteAsync(user);
                return new Status
                {
                    StatusCode = 0,
                    StatusMessage = "something went wrong, try again to register"
                };
                
            }
            

        }
    }
}
