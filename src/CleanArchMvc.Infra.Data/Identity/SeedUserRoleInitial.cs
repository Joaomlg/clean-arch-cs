using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Identity
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedUsers()
        {
            CreateUserInRoleIfNotExists("usuario@localhost", "passwd@123", "User");
            CreateUserInRoleIfNotExists("admin@localhost", "passwd@123", "Admin");
        }

        private void CreateUserInRoleIfNotExists(string email, string password, string role)
        {
            if (_userManager.FindByEmailAsync(email).Result == null)
            {
                var user = new ApplicationUser()
                {
                    UserName = email,
                    Email = email,
                    NormalizedUserName = email.ToUpper(),
                    NormalizedEmail = email.ToUpper(),
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var result = _userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, role).Wait();
                }
            }
        }

        public void SeedRoles()
        {
            CreateRoleIfNotExists("User");
            CreateRoleIfNotExists("Admin");
        }

        private void CreateRoleIfNotExists(string roleName)
        {
            if (!_roleManager.RoleExistsAsync(roleName).Result)
            {
                var role = new IdentityRole()
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpper()
                };

                _roleManager.CreateAsync(role);
            }
        }
    }
}
