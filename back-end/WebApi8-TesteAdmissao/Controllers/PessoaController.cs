using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi8_TesteAdmissao.Dto.Pessoa;
using WebApi8_TesteAdmissao.Models;
using WebApi8_TesteAdmissao.Services.Pessoa;

namespace WebApi8_TesteAdmissao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaInterface _pessoaInterface;
        public PessoaController(IMapper mapper,IPessoaInterface pessoaInterface) 
        {
            _pessoaInterface = pessoaInterface;
        }

        [HttpGet("ListarPessoas")]
        public async Task<ActionResult<ResponseModel<List<PessoaModel>>>> ListarPessoas(string? nome, string? cpf,
        string? cidade, string? estado, int pagina = 1, int tamanhoPagina = 1)
        {
            var pessoas = await _pessoaInterface.ListarPessoas(nome,cpf,cidade,estado,pagina,tamanhoPagina);

            return Ok(pessoas);
        }

        [HttpGet("BuscarPessoaPorId/{idPessoa}")]
        public async Task<ActionResult<ResponseModel<PessoaModel>>> BuscarPessoaPorId(Guid idPessoa)
        {
            var pessoa = await _pessoaInterface.BuscarPessoaPorId(idPessoa);
            return Ok(pessoa);
        }

        [HttpGet("BuscarPessoaPorCpf/{cpf}")]
        public async Task<ActionResult<ResponseModel<PessoaModel>>> BuscarPessoaPorCpf(string cpf)
        {
            var pessoa = await _pessoaInterface.BuscarPessoaPorCpf(cpf);
            return Ok(pessoa);
        }

        [HttpGet("BuscarPessoaPorNome/{nomeCompleto}")]
        public async Task<ActionResult<ResponseModel<PessoaModel>>> BuscarPessoaPorNome(string nomeCompleto)
        {
            var pessoa = await _pessoaInterface.BuscarPessoaPorNome(nomeCompleto);
            return Ok(pessoa);
        }

        [HttpPost("CriarPessoa")]
        public async Task<ActionResult<ResponseModel<List<PessoaModel>>>> CriarPessoa(PessoaCriacaoDto pessoaCriacaoDto)
        {
            var pessoas = await _pessoaInterface.CriarPessoa(pessoaCriacaoDto);
            return Ok(pessoas);
        }

        [HttpPut("EditarPessoa")]
        public async Task<ActionResult<ResponseModel<List<PessoaModel>>>> EditarPessoa(EditaPessoaDto editaPessoaDto)
        {
            var pessoas = await _pessoaInterface.EditarPessoa(editaPessoaDto);
            return Ok(pessoas);
        }

        [HttpDelete("ExcluirPessoa")]
        public async Task<ActionResult<ResponseModel<List<PessoaModel>>>> ExcluirPessoa(Guid id)
        {
            var pessoas = await _pessoaInterface.ExcluirPessoa(id);
            return Ok(pessoas);
        }
    }
}
