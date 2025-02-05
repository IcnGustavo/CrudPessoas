using System.ComponentModel.DataAnnotations;

namespace WebApi8_TesteAdmissao.Models
{
    public class EnderecoModel
    {
        [Key]  
        public int Id { get; set; }

        [Required(ErrorMessage = "O Logradouro é obrigatório.")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O Número é obrigatório.")]
        public int Numero { get; set; } 

        [Required(ErrorMessage = "O Bairro é obrigatório.")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "A Cidade é obrigatória.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O Estado é obrigatório.")]
        [StringLength(2, ErrorMessage = "O Estado deve ser a sigla de 2 caracteres (ex: SP, RJ).")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "CEP inválido. Use o formato 00000-000.")]
        public string Cep { get; set; }
}