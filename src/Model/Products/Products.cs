using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Model.Products
{
    public class Products
    {
        public int Id { get; set; }

        [Required] //campo obrigatório.
        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(500)]
        public string Descricao { get; set; }
     
        [Required] //campo obrigatório
        [StringLength(100)]
        public string Categoria { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Preco { get; set; }

        [Range(0, int.MaxValue)]
        public int Estoque { get; set; }
    }
}

//Essas validações funcionam automaticamente com o [ApiController], retornando 400 Bad Request se os dados estiverem inválidos.

