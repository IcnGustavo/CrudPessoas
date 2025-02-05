using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApi8_TesteAdmissao.Data;
using WebApi8_TesteAdmissao.Dto.Pessoa;
using WebApi8_TesteAdmissao.Models;

namespace WebApi8_TesteAdmissao.Services.Pessoa
{
    public class PessoaService : IPessoaInterface
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public PessoaService(IMapper mapper, AppDbContext context)
        {
             _mapper = mapper;
            _context = context;
        }

        public async Task<ResponseModel<PessoaModel>> BuscarPessoaPorCpf(string cpf)
        {            
            try
            {
                var pessoa = await _context.Pessoas.Include(p => p.Endereco).FirstOrDefaultAsync(pessoaBanco => pessoaBanco.Cpf == cpf);
                return RetResponse(pessoa, null);
            }
            catch (Exception ex)
            {
                return RetResponse(null, ex);
            }

        }

        public async Task<ResponseModel<PessoaModel>> BuscarPessoaPorId(Guid idPessoa)
        {
            ResponseModel<PessoaModel> resposta = new ResponseModel<PessoaModel>();
            try
            {
                var pessoa = await _context.Pessoas.Include(p => p.Endereco).FirstOrDefaultAsync(pessoaBanco => pessoaBanco.Id == idPessoa);
                return RetResponse(pessoa, null);
            }
            catch (Exception ex)
            {
                return RetResponse(null, ex);
            }
        }

        public async Task<ResponseModel<PessoaModel>> BuscarPessoaPorNome(string nomeCompleto)
        {
            ResponseModel<PessoaModel> resposta = new ResponseModel<PessoaModel>();
            try
            {
                var pessoa = await _context.Pessoas.Include(p => p.Endereco)
                    .Where(p => EF.Functions.Like(p.NomeCompleto, $"%{nomeCompleto}%")).FirstOrDefaultAsync();

                return RetResponse(pessoa, null);
            }
            catch (Exception ex)
            {
                return RetResponse(null, ex);
            }
        }

        public ResponseModel<PessoaModel> RetResponse(PessoaModel pessoa, Exception ex)
        {
            ResponseModel<PessoaModel> resposta = new ResponseModel<PessoaModel>();
            if (ex != null)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
            }

            if (pessoa == null)
            {
                resposta.Mensagem = "Nenhum registro localizado!";
                return resposta;
            }
            resposta.Dados = pessoa;
            resposta.Mensagem = "Pessoa Localizada!";

            return resposta;
        }

        public async Task<ResponseModel<List<PessoaModel>>> ListarPessoas(string? nome = null,string? cpf = null,string? cidade = null,
            string? estado = null,int pagina = 1,int tamanhoPagina = 10)
        {
            ResponseModel<List<PessoaModel>> resposta = new ResponseModel<List<PessoaModel>>();
            try
            {
                var query = _context.Pessoas.Include(p => p.Endereco).AsQueryable();

                if (!string.IsNullOrWhiteSpace(nome))
                    query = query.Where(p => p.NomeCompleto.ToLower().Contains(nome.ToLower()));

                if (!string.IsNullOrWhiteSpace(cpf))
                    query = query.Where(p => p.Cpf == cpf);

                if (!string.IsNullOrWhiteSpace(cidade))
                    query = query.Where(p => p.Endereco.Cidade.ToLower().Contains(cidade.ToLower()));

                if (!string.IsNullOrWhiteSpace(estado))
                    query = query.Where(p => p.Endereco.Estado.ToLower().Contains(estado.ToLower()));

                int totalRegistros = await query.CountAsync();
                int totalPaginas = (int)Math.Ceiling(totalRegistros / (double)tamanhoPagina);

                var pessoas = await query
                    .Skip((pagina - 1) * tamanhoPagina)
                    .Take(tamanhoPagina)
                    .ToListAsync();

                resposta.Dados = pessoas;
                resposta.Mensagem = "Lista de pessoas retornada com sucesso!";
                resposta.Status = true;
                resposta.TotalPaginas = totalPaginas; // Adicionando total de páginas
                resposta.TotalRegistros = totalRegistros; // Adicionando total de registros

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<PessoaModel>>> CriarPessoa(PessoaCriacaoDto pessoaCriacaoDto)
        {
            ResponseModel<List<PessoaModel>> resposta = new ResponseModel<List<PessoaModel>> ();

            try
            {
                var pessoaModel = _mapper.Map<PessoaModel>(pessoaCriacaoDto);

                _context.Add(pessoaModel);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Pessoas.ToListAsync();
                resposta.Mensagem = "Pessoa criada com sucesso";

                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.InnerException?.Message ?? ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<PessoaModel>>> EditarPessoa(EditaPessoaDto putPessoa)
        {
            ResponseModel<List<PessoaModel>> resposta = new ResponseModel<List<PessoaModel>>();

            try
            {
                var pessoa = await _context.Pessoas.Include(p => p.Endereco).FirstOrDefaultAsync(pessoaBanco => pessoaBanco.Id == putPessoa.Id);

                if (pessoa == null)
                {
                    resposta.Mensagem = "Nenhuma pessoa localizada!";
                    return resposta;
                }

                _mapper.Map(putPessoa, pessoa);
                if (pessoa.Endereco != null && putPessoa.Endereco != null)
                {
                    _mapper.Map(putPessoa.Endereco, pessoa.Endereco);
                    _context.Entry(pessoa.Endereco).State = EntityState.Modified;
                }

                _context.Update(pessoa);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Pessoas.Include(p => p.Endereco).ToListAsync();
                resposta.Mensagem = "Pessoa editada com sucesso!"; 
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.InnerException?.Message ?? ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<PessoaModel>>> ExcluirPessoa(Guid idPessoa)
        {
            ResponseModel<List<PessoaModel>> resposta = new ResponseModel<List<PessoaModel>>();

            try
            {
                var pessoa = await _context.Pessoas.Include(p => p.Endereco).FirstOrDefaultAsync(pessoaBanco => pessoaBanco.Id == idPessoa);

                if (pessoa == null)
                {
                    resposta.Mensagem = "Nenhuma pessoa localizada!";
                    return resposta;
                }

                if (pessoa.Endereco != null)
                {
                    _context.Remove(pessoa.Endereco);
                }

                _context.Remove(pessoa);
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Pessoas.Include(p => p.Endereco).ToListAsync();
                resposta.Mensagem = "Pessoa excluida com sucesso!";
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.InnerException?.Message ?? ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
