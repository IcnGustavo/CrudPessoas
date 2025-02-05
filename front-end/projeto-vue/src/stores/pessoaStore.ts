import { defineStore } from 'pinia';
import axios from 'axios';
import type { Endereco } from '@/utils/Endereco';
import { getPessoas } from '@/api/pessoaService';
import { deletePessoa } from '@/api/pessoaService';

interface Pessoa {
  id: string;
  nomeCompleto: string;
  dataNascimento: Date;
  cpf: string;
  email: string;
  telefone: string;
  endereco: Endereco;
}

export const usePessoaStore = defineStore('pessoa', {
  state: () => ({
    pessoas: [] as Pessoa[],
    totalPages: 1,
    totalItems: 0,
    currentPage: 1,
  }),
  actions: {
    async fetchPessoas( pagina: number, tamanhoPagina: number) {
      try{
        const response = await getPessoas("", "", "", "", pagina, tamanhoPagina);  
        this.pessoas = response.data.dados;
        this.totalPages = response.data.totalPaginas;
        this.totalItems = response.data.totalRegistros;
        this.currentPage = pagina;
        console.log("Dados carregados na store:", this.pessoas);
      }
      catch(error){
        console.error('Erro ao buscar pessoas:', error);
      }
    },
    async excluirPessoa(id: string) {
      try {
        console.log("id da pessoa que sera excluida :" + id)
        const response = await deletePessoa(id);
        
        this.pessoas = this.pessoas.filter(pessoa => Number(pessoa.id) !== Number(id));
        console.log("Pessoa excluída com sucesso:", response.data);
      } catch (error) {
        console.error('Erro ao excluir a pessoa:', error);
      }
    }
  }
});
