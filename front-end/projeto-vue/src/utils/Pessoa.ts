import type { Endereco } from '@/utils/Endereco';

export interface Pessoa {
    id: string; 
    nomeCompleto: string;
    dataNascimento: string;
    cpf: string;
    email: string;
    telefone?: string; 
    endereco: Endereco;
  }