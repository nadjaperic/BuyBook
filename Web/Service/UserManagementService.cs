using Application.Models;
using Microsoft.AspNetCore.Identity;
using Persistence;
using Persistence.Migrations;

namespace Web.Service
{
    public class UserManagementService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;

        public UserManagementService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
        }

        public List<UserModel> GetAllAdmins()
        {
            var users = _signInManager.UserManager.GetUsersInRoleAsync("Administrator").Result;

            return users.Select(x => new UserModel
            {
                Email = x.UserName,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Id = x.Id,
            }).ToList();
        }

        public bool AddNewAdmin(UserModel admin)
        {
            var user = CreateUser();
            user.FirstName = admin.FirstName;
            user.LastName = admin.LastName;

            string defaultPassword = "Password2023#";

            _userStore.SetUserNameAsync(user, admin.Email, CancellationToken.None);

            var result = _userManager.CreateAsync(user, defaultPassword).Result;

            if (result.Succeeded)
            {
                var roleReslt = _userManager.AddToRoleAsync(user, "Administrator").Result;
                return true;
            }

            return false;
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. ");
            }
        }

        public bool DeleteAdmin(string id)
        {
            var user = _signInManager.UserManager.FindByIdAsync(id).Result;

            if (user == null) return false;

            var result = _userManager.DeleteAsync(user).Result;

            return result.Succeeded;
        }
    }
}
