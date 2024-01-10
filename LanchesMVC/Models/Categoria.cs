namespace LanchesMVC.Models
{
    public class Categoria
    {
        public int CategoriaID { get; set; }
        public string CategoriaNome { get; set; }
        public string Descricao { get; set; }

        //Definindo o relacionamento 1 para muitos (Categoria x Lanche)
        public List<Lanche> Lanches { get; set; }
    }
}
