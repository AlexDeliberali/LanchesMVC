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
        [Column(TypeName = "Decimal(10,2)")]
        [Range(1,99.99,ErrorMessage = "O preço deve estar entre 1,00 real e 99,99 reais!")]
        public decimal Preco { get; set; }

        [Display(Name = "Caminho da imagem normal")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres!")]
        public string ImagemUrl { get; set; }

        [Display(Name = "Caminho da imagem em miniatura")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres!")]
        public string ImagemThumbnailUrl { get; set; }

        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name = "Estoque?")]
        public bool EmEstoque { get; set; }

        //Definindo a chave estrangeira
        public int CategoriaId { get; set; }

        //Definindo propriedade de navegação
        public virtual Categoria Categoria { get; set; }
    }
}
