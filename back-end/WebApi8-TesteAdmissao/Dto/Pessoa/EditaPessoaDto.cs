using System.ComponentModel.DataAnnotations;
using WebApi8_TesteAdmissao.Dto.Endereco;

namespace WebApi8_TesteAdmissao.Dto.Pessoa
{
    public class EditaPessoaDto
    {
        [Required(ErrorMessage = "O ID é obrigatório.")]
        public Guid Id { get; set; }

        [StringLength(150, ErrorMessage = "O Nome Completo deve ter no máximo 150 caracteres.")]
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }

        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter 11 dígitos.")]
        public string CPF { get; set; }

        [EmailAddress(ErrorMessage = "O Email não é válido.")]
        public string Email { get; set; }

        public string Telefone { get; set; }

        public EnderecoDto Endereco { get; set; }
    }
}
