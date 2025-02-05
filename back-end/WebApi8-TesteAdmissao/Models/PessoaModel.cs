using System.ComponentModel.DataAnnotations;

namespace WebApi8_TesteAdmissao.Models
{
    public class PessoaModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "O Nome Completo é obrigatório.")]
        [MaxLength(150, ErrorMessage = "O Nome Completo deve ter no máximo 150 caracteres.")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "A Data de Nascimento é obrigatória.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF inválido. Use o formato 000.000.000-00.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        public string? Telefone { get; set; }

        [Required(ErrorMessage = "O Endereço é obrigatório.")]
        public EnderecoModel Endereco { get; set; }
    }
}
