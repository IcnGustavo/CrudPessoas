<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { getPessoaById } from '@/api/pessoaService';
import type { Pessoa } from '@/utils/Pessoa';

const route = useRoute();
const pessoa = ref<Pessoa>({
  id: '',
  nomeCompleto: '',
  dataNascimento: '',
  cpf: '',
  email: '',
  telefone: '',
  endereco: {
    logradouro: '',
    numero: '',
    bairro: '',
    cidade: '',
    estado: '',
    cep: ''
  }
});

onMounted(async () => {
  const { id } = route.params;
  console.log('ID da pessoa:', id);
  if (id) {
    try {
      const response = await getPessoaById(id as string);
      console.log('Resposta da API:', response.data);
      pessoa.value = response.data.dados;

      if (pessoa.value.dataNascimento) {
        const date = new Date(pessoa.value.dataNascimento);
        pessoa.value.dataNascimento = date.toLocaleDateString('pt-BR');
      }

      console.log( "nome : " + pessoa.value.nomeCompleto);
    } catch (error) {
      console.error("Erro ao carregar os dados da pessoa:", error);
    }
  }
});
</script>

<template>
  <div v-if="pessoa" class="max-w-md mx-auto mt-6 p-4 rounded-md border-2 border-green-500">
    <h1 class="text-xl font-medium text-green-600">Detalhes da Pessoa</h1>
    <hr class="my-1 border-t-2 border-green-300" />
    <div class="mt-2">
      <div class="text-green-600 font-medium"><strong>Nome:</strong> <span class="text-black">{{ pessoa.nomeCompleto }}</span></div>
      <div class="text-green-600 font-medium"><strong>Data de Nascimento:</strong> <span class="text-black">{{ pessoa.dataNascimento}}</span></div>
      <div class="text-green-600 font-medium"><strong>CPF:</strong> <span class="text-black">{{ pessoa.cpf }}</span></div>
      <div class="text-green-600 font-medium"><strong>Email:</strong> <span class="text-black">{{ pessoa.email }}</span></div>
      <div class="text-green-600 font-medium"><strong>Telefone:</strong> <span class="text-black">{{ pessoa.telefone }}</span></div>
      
      <div class=" mt-2 text-green-600 font-medium"><strong>Endereço</strong>
        <hr class="my-1 border-t-2 border-green-300" />
        <div class="text-green-600 font-medium"><strong>logradouro:</strong> <span class="text-black">{{ pessoa.endereco.logradouro }}</span></div>
        <div class="text-green-600 font-medium"><strong>Número:</strong> <span class="text-black">{{ pessoa.endereco.numero}}</span></div>
        <div class="text-green-600 font-medium"><strong>Bairro:</strong> <span class="text-black">{{ pessoa.endereco.bairro }}</span></div>
        <div class="text-green-600 font-medium"><strong>Cidade:</strong> <span class="text-black">{{ pessoa.endereco.cidade }}</span></div>
        <div class="text-green-600 font-medium"><strong>Estado:</strong> <span class="text-black">{{ pessoa.endereco.estado }}</span></div>
        <div class="text-green-600 font-medium"><strong>CEP:</strong> <span class="text-black">{{ pessoa.endereco.cep }}</span></div>
      </div>
    </div>
    <router-link to="/">
        <button type="submit" class="mt-6 px-6 py-1 bg-green-500 text-white rounded-md hover:bg-green-600 disabled:bg-green-400 disabled:cursor-not-allowed transition-all duration-200">{{ "Voltar"}} </button>
    </router-link>
  </div>
  <div v-else class="text-center text-green-600">Carregando...</div>
</template>





<style scoped>
strong {
  font-weight: 600;
}
</style>
