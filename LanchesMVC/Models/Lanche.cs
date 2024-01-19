using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMVC.Models
{
    //Mapeando a tabela que será criada
    [Table("Lanches")]
    public class Lanche
    {
        //Definindo como coluna obrigatória (Será criada como NOT NULL)
        //Definindo a mensagem de erro que será exibida.
        [Required(ErrorMessage = "O código do lanche deve ser de preenchimento obrigatório!")] 
        [Display(Name = "Código do Lanche")] //Nome que será exibido
        [Key] //Reforçando que será criada como PK da tabela
        public int LancheId { get; set; }

        [Required] //Definindo como coluna obrigatória (Será criada como NOT NULL)
        [Display(Name = "Nome do Lanche")] //Nome que será exibido
        [MinLength(1)] //Minimo de caracteres
        [MaxLength(50)] // Maximo de caracteres
        public string Nome { get; set; }

        [Required] //Definindo como coluna obrigatória (Será criada como NOT NULL)
        [Display(Name = "Descrição curta do Lanche")] //Nome que será exibido
        [MinLength(1)] //Minimo de caracteres
        [MaxLength(100)] // Maximo de caracteres
        public string DescricaoCurta { get; set; }

        [Required] //Definindo como coluna obrigatória (Será criada como NOT NULL)
        [Display(Name = "Descrição completa do Lanche")] //Nome que será exibido
        [MinLength(1)] //Minimo de caracteres
        [MaxLength(200)] // Maximo de caracteres
        public string DescricaoDetalhada { get; set; }

        [Required] //Definindo como coluna obrigatória (Será criada como NOT NULL)
        [Display(Name = "Preço do Lanche")] //Nome que será exibido
        
        public decimal Preco { get; set; }

        public string ImagemUrl { get; set; }
        public string ImagemThumbnailUrl { get; set; }
        public bool IsLanchePreferido { get; set; }
        public bool EmEstoque { get; set; }

        //Definindo a chave estrangeira
        public int CategoriaId { get; set; }

        //Definindo propriedade de navegação
        public virtual Categoria Categoria { get; set; }
    }
}
