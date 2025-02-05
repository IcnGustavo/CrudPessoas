using WebApi8_TesteAdmissao.Dto.Pessoa;
using WebApi8_TesteAdmissao.Models;

namespace WebApi8_TesteAdmissao.Services.Pessoa
{
    public interface IPessoaInterface
    {
        Task<ResponseModel<List<PessoaModel>>> ListarPessoas(string? nome = null, string? cpf = null, string? cidade = null,
            string? estado = null, int pagina = 1, int tamanhoPagina = 10);
        Task<ResponseModel<PessoaModel>> BuscarPessoaPorId(Guid idPessoa);
        Task<ResponseModel<PessoaModel>> BuscarPessoaPorCpf(String cpf);
        Task<ResponseModel<PessoaModel>> BuscarPessoaPorNome(String nome);

        Task<ResponseModel<List<PessoaModel>>> CriarPessoa(PessoaCriacaoDto pessoaCriacaoDto);

        Task<ResponseModel<List<PessoaModel>>> EditarPessoa(EditaPessoaDto pessoa);

        Task<ResponseModel<List<PessoaModel>>> ExcluirPessoa(Guid idPessoa);

    }
}
