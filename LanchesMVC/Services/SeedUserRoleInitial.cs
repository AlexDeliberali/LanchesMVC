using Microsoft.AspNetCore.Identity;

namespace LanchesMVC.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedRoles()
        {
            //Verifica se o perfil não existe e cria o mesmo.
            if(!_roleManager.RoleExistsAsync("Member").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Member";
                role.NormalizedName = "MEMBER";
                IdentityResult roleResult  = _roleManager.CreateAsync(role).Result;
            }

            //Verifica se o perfil não existe e cria o mesmo.
            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }

        public void SeedUsers()
        {
            //Tenta localizar o usuário e se não encontrar, será criado
            if(_userManager.FindByEmailAsync("usuario@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "usuario@localhost";
                user.Email = "usuario@localhost";
                user.NormalizedUserName = "USUARIO@LOCALHOST";
                user.NormalizedEmail = "USUARIO@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                //Criando o usuário e passando uma senha padrão
                IdentityResult userResult = _userManager.CreateAsync(user,"Mudar#2024").Result;

                //Se a criação do usuário foi bem sucedida, já adicionamos o mesmo num grupo
                if(userResult.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Member").Wait();
                }
            }

            //Tenta localizar o usuário e se não encontrar, será criado
            if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.NormalizedEmail = "ADMIN@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                //Criando o usuário e passando uma senha padrão
                IdentityResult userResult = _userManager.CreateAsync(user, "Mudar#2024").Result;

                //Se a criação do usuário foi bem sucedida, já adicionamos o mesmo num grupo
                if (userResult.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "admin").Wait();
                }
            }
        }
    }
}
