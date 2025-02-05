<script setup lang="ts">
import { onMounted, ref, computed } from 'vue';
import { usePessoaStore } from '@/stores/pessoaStore';
import type { Pessoa } from '@/utils/Pessoa';

const pessoaStore = usePessoaStore();
const currentPage = ref<number>(1);  
const itemsPerPage = 5;
const searchNome = ref('');
const searchCPF = ref('');
const searchCidade = ref('');
const searchEstado = ref('');
const showModal = ref(false); 
const pessoaToDelete = ref<Pessoa | null>(null);

onMounted(() => {
  pessoaStore.fetchPessoas(1, itemsPerPage);
});

const filteredPessoas = computed(() => {
  return pessoaStore.pessoas.filter(pessoa => {
    return (
      (searchNome.value ? pessoa.nomeCompleto.toLowerCase().includes(searchNome.value.toLowerCase()) : true) &&
      (searchCPF.value ? pessoa.cpf.includes(searchCPF.value) : true) &&
      (searchCidade.value ? pessoa.endereco.cidade.includes(searchCidade.value) : true) &&
      (searchEstado.value ? pessoa.endereco.estado.includes(searchEstado.value) : true)
    );
  });
});

const totalPages = computed(() => Math.ceil(filteredPessoas.value.length / itemsPerPage));

function changePage(page: number) {
  if (page < 1) return;
  currentPage.value = page;
  pessoaStore.fetchPessoas(page, itemsPerPage);
}

function openDeleteModal(pessoa: any) {
  pessoaToDelete.value = pessoa;
  showModal.value = true;
}

function closeDeleteModal() {
  showModal.value = false;
  pessoaToDelete.value = null;
}

async function deletePessoa() {
  if (pessoaToDelete.value) {
    await pessoaStore.excluirPessoa(pessoaToDelete.value.id);
    showModal.value = false;
    pessoaToDelete.value = null;
    pessoaStore.fetchPessoas(1, itemsPerPage);
  }
}
</script>

<template> 
  <div class="max-w-4xl mx-auto p-1">
    <h1 class="text-4xl font-extrabold text-left text-green-600 mt-5 mb-4 font-medium">Home</h1>
    <hr class="my-4 border-t-2 border-green-500" />
    <div class="flex justify-center items-center">
      <router-link to="/pessoas/novo">
        <button class="px-4 py-2 bg-green-500 text-white rounded-md hover:bg-green-600 disabled:bg-green-400 disabled:cursor-not-allowed transition-all duration-200">
            Cadastrar nova pessoa
        </button>
      </router-link>
    </div>
    <hr class="my-4 border-t-2 border-green-500" />
    <h1 class="text-1xl font-extrabold text-left text-green-600 mt-5 mb-4 font-medium">Filtros</h1>
    <div class="border-2 border-green-500 p-4 rounded-md">
      <input v-model="searchNome" placeholder="Filtrar por Nome" class="mb-2 p-2 border border-gray-300 rounded-md w-full bg-green-100" />
      <input v-model="searchCPF" placeholder="Filtrar por CPF" class="mb-2 p-2 border border-gray-300 rounded-md w-full bg-green-100" />
      <input v-model="searchCidade" placeholder="Filtrar por Cidade" class="mb-2 p-2 border border-gray-300 rounded-md w-full bg-green-100" />
      <input v-model="searchEstado" placeholder="Filtrar por Estado" class="mb-2 p-2 border border-gray-300 rounded-md w-full bg-green-100" />
    </div>
    <hr class="my-4 border-t-2 border-green-500" />
    <h1 class="text-1xl font-extrabold text-left text-green-600 mt-5 mb-4 font-medium flex items-center">
        Lista de Pessoas
      <span class="ml-4 text-sm font-normal text-geen-600">Qtd:</span>
      <input 
        v-model="itemsPerPage" 
        type="number" 
        min="1" 
        max="100" 
        class="ml-2 p-1 border border-gray-300 rounded-md w-16 bg-green-100"
        @change="pessoaStore.fetchPessoas(1, itemsPerPage)"
      />
    </h1>
    <div class="border-2 border-green-500 p-4 rounded-md">
      <ul>
        <li v-for="pessoa in pessoaStore.pessoas" :key="pessoa.id" class="font-light mb-2 p-2 border border-green-300 rounded-md w-full">
          {{ pessoa.nomeCompleto || 'Nome não encontrado' }} - 
          <router-link :to="`/pessoas/${pessoa.id}`">Detalhes</router-link>
          <router-link :to="`/pessoas/${pessoa.id}/editar`">- Editar</router-link>
          <button @click="openDeleteModal(pessoa)" class="text-red-500">- Excluir</button>
        </li>
      </ul>
    </div>
    
    <hr class="my-4 border-t-2 border-green-500" />

    <div class="border-2 border-green-500 p-4 rounded-md flex items-center space-x-4">
      <button 
        :disabled="pessoaStore.currentPage === 1" 
        @click="pessoaStore.fetchPessoas(pessoaStore.currentPage - 1, itemsPerPage)" 
        class="px-4 py-2 bg-green-500 text-white rounded-md hover:bg-green-600 disabled:bg-green-400 disabled:cursor-not-allowed transition-all duration-200">
        Anterior
      </button>

      <span> Página {{ pessoaStore.currentPage }} de {{ pessoaStore.totalPages }} </span>

      <button 
        :disabled="pessoaStore.currentPage >= pessoaStore.totalPages" 
        @click="pessoaStore.fetchPessoas(pessoaStore.currentPage + 1, itemsPerPage)" 
        class="px-4 py-2 bg-green-500 text-white rounded-md hover:bg-green-600 disabled:bg-green-400 disabled:cursor-not-allowed transition-all duration-200">
        Próximo
      </button>
    </div>

    <div v-if="showModal" class="fixed inset-0 bg-gray bg-opacity-40 flex justify-center items-center z-50">
      <div class="bg-white p-6 rounded-md border-2 border-green-500 shadow-lg">
        <h2 class="text-xl font-semibold mb-4 text-center text-green-700">Tem certeza que deseja excluir esta pessoa?</h2>
        <div class="flex justify-center space-x-4">
          <button @click="deletePessoa" class="px-4 py-2 bg-red-500 text-white rounded-md hover:bg-red-600">Sim, excluir</button>
          <button @click="closeDeleteModal" class="px-4 py-2 bg-gray-300 text-black rounded-md hover:bg-gray-400">Cancelar</button>
        </div>
      </div>
    </div>
  </div>
</template>

