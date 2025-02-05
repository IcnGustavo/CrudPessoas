using System.ComponentModel.DataAnnotations;
using WebApi8_TesteAdmissao.Dto.Endereco;

namespace WebApi8_TesteAdmissao.Dto.Pessoa
{
    public class PessoaCriacaoDto
    {
        [Required(ErrorMessage = "O Nome Completo é obrigatório.")]
        [StringLength(150, ErrorMessage = "O Nome Completo deve ter no máximo 150 caracteres.")]
        public string NomeCompleto { get; set; }
        [Required(ErrorMessage = "A Data de Nascimento é obrigatória.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter 11 dígitos.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O Email não é válido.")]
        public string Email { get; set; }

        public string? Telefone { get; set; }

        [Required(ErrorMessage = "O Endereço é obrigatório.")]
        public EnderecoDto Endereco { get; set; }
    }
}
