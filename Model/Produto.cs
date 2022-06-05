using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Model
{
    public class Produto
    {
        [Key]
        public Guid id { get; set; }
        public string Nome { get; set; }
        public int TipoProduto { get; set; }
        public bool Ativo { get; set; }
    }
}
