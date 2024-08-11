namespace LanchesMVC.Services
{
    public interface ISeedUserRoleInitial
    {
        //Chamada para criar os perfis 
        void SeedRoles();
        //Chamada para alocar os usuários nos perfis criados
        void SeedUsers();
    }
}
