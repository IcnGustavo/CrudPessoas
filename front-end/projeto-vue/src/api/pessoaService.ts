import axios from 'axios';

axios.defaults.baseURL = 'https://localhost:7105'; 

export const getPessoas = (nome: string, cpf: string, cidade: string, estado: string, pagina: number, tamanhoPagina: number) => {
    const params = new URLSearchParams();
    if (nome) params.append('nome', nome);
    if (cpf) params.append('cpf', cpf);
    if (cidade) params.append('cidade', cidade);
    if (estado) params.append('estado', estado);
    if (pagina) params.append('pagina', pagina.toString());
  if (tamanhoPagina) params.append('tamanhoPagina', tamanhoPagina.toString());
    return axios.get('/api/Pessoa/ListarPessoas', { params });
  };  
export const getPessoaById = (id: string) => axios.get(`/api/Pessoa/BuscarPessoaPorId/${id}`);
export const getPessoaByCpf = (cpf: string) => axios.get(`/api/Pessoa/BuscarPessoaPorCpf/${cpf}`);
export const getPessoaByNome = (nome: string) => axios.get(`/api/Pessoas/BuscarPessoaPorNome/${nome}`);
export const createPessoa = (data: any) => axios.post('/api/Pessoa/CriarPessoa', data);
export const updatePessoa = (data: any) => axios.put(`/api/Pessoa/EditarPessoa`, data);
export const deletePessoa = (id: string) => axios.delete(`/api/Pessoa/ExcluirPessoa`, {
  params: { id }
});

