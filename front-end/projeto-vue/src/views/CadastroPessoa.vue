<script setup lang="ts">
import { onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { usePessoaStore } from '@/stores/pessoaStore';
import { useField, useForm } from 'vee-validate';
import * as yup from 'yup';
import { createPessoa, updatePessoa, getPessoaById } from '@/api/pessoaService';
import { useToast } from 'vue-toastification';


const route = useRoute();
const router = useRouter();
const pessoaStore = usePessoaStore();
const toast = useToast();

const { id } = route.params;
const isEdit = Boolean(id);

const schema = yup.object({
  nomeCompleto: yup.string().required('Nome é obrigatório'),
  dataNascimento: yup.date().nullable().required('Data de Nascimento é obrigatória'),
  cpf: yup.string().required('CPF é obrigatório').length(11, 'CPF inválido'),
  email: yup.string().email('Email inválido').required('Email é obrigatório'),
  telefone: yup.string().nullable(),
  endereco: yup.object({
    logradouro: yup.string().required('Endereço é obrigatório'),
    numero: yup.string().required('Número é obrigatório'),
    bairro: yup.string().required('Bairro é obrigatório'),
    cidade: yup.string().required('Cidade é obrigatória'),
    estado: yup.string().required('Estado é obrigatório'),
    cep: yup.string().required('CEP é obrigatório')
  })
});


const form = useForm({
  validationSchema: schema, 
  initialValues: {
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
  },
  validateOnMount: false
});
console.log(form);

const { handleSubmit, errors } = form;

const { value: nomeCompleto } = useField('nomeCompleto');
const { value: dataNascimento } = useField('dataNascimento');
const { value: cpf } = useField('cpf');
const { value: email } = useField('email');
const { value: telefone } = useField('telefone');

const { value: logradouro } = useField('endereco.logradouro');
const { value: numero } = useField('endereco.numero');
const { value: bairro } = useField('endereco.bairro');
const { value: cidade } = useField('endereco.cidade');
const { value: estado } = useField('endereco.estado');
const { value: cep } = useField('endereco.cep');



onMounted(async () => {
  if (isEdit) {
    try {
      const response = await getPessoaById(id as string);
      console.log("Pessoa carregada:", response);

      const pessoa = response.data.dados;

      const pessoaValores = {
        id: pessoa.id || "",
        nomeCompleto: pessoa.nomeCompleto || '',
        dataNascimento: pessoa.dataNascimento ? pessoa.dataNascimento.slice(0, 10) : '',
        cpf: pessoa.cpf || '',
        email: pessoa.email || '',
        telefone: pessoa.telefone || '',
        endereco: {
          logradouro: pessoa.endereco?.logradouro || '',
          numero: String(pessoa.endereco?.numero || ''),
          bairro: pessoa.endereco?.bairro || '',
          cidade: pessoa.endereco?.cidade || '',
          estado: pessoa.endereco?.estado || '',
          cep: pessoa.endereco?.cep || ''
        }
      };

      console.log("Valores para setValues:", pessoaValores);
      form.setValues(pessoaValores);

      console.log("Valores após setValues:", form.values);
    } catch (error) {
      console.error("Erro ao buscar pessoa:", error);
    }
  }
});

const submitForm = handleSubmit(async (values) => {
  try {
    if (isEdit) {
      console.log("Dados enviados para atualização:", values);
      await updatePessoa(values);
      pessoaStore.fetchPessoas(1, 5);
      router.push(`/pessoas/${id}`);
      toast.success("Pessoa atualizada com sucesso!"); 
    } else {
      await createPessoa(values);
      router.push('/');
      toast.success("Pessoa cadastrada com sucesso!"); 
    }
  } catch (error) {
    console.error(error);
    toast.error("Erro ao salvar pessoa. Tente novamente.");
  }
});

</script>

<template>
  <div class="max-w-md mx-auto mt-6">
    <h1 class="text-xl font-semibold">{{ isEdit ? 'Editar Pessoa' : 'Cadastrar Nova Pessoa' }}</h1>
    <form @submit="submitForm">
      <div>
        <label for="nomeCompleto">Nome Completo :</label>
        <input v-model="nomeCompleto" type="text" id="nomeCompleto" class="input" />
        <span v-if="errors.nomeCompleto">{{ errors.nomeCompleto }}</span>
      </div>
      <div>
        <label for="dataNascimento">Data de Nascimento :</label>
        <input v-model="dataNascimento" type="date" id="dataNascimento" class="input" />
        <span v-if="errors.dataNascimento">{{ errors.dataNascimento }}</span>
      </div>
      <div>
        <label for="cpf">CPF :</label>
        <input v-model="cpf" type="text" id="cpf" class="input" />
        <span v-if="errors.cpf">{{ errors.cpf }}</span>
      </div>
      <div>
        <label for="email">Email :</label>
        <input v-model="email" type="email" id="email" class="input" />
        <span v-if="errors.email">{{ errors.email }}</span>
      </div>
      <div>
        <label for="telefone">Telefone :</label>
        <input v-model="telefone" type="telefone" id="telefone" class="input" />
        <span v-if="errors.telefone">{{ errors.telefone }}</span>
      </div>

      <div>
        <h2 class="text-lg font-semibold">Endereço</h2>
        <div>
          <label for="logradouro">Logradouro :</label>
          <input v-model="logradouro" type="text" id="logradouro" class="input" />
          <span v-if="errors['endereco.logradouro']">{{ errors['endereco.logradouro'] }}</span>
        </div>
        <div>
          <label for="numero">Número :</label>
          <input v-model="numero" type="text" id="numero" class="input" />
          <span v-if="errors['endereco.numero']">{{ errors['endereco.numero'] }}</span>
        </div>
        <div>
          <div>
          <label for="bairro">Bairro :</label>
          <input v-model="bairro" type="text" id="bairro" class="input" />
          <span v-if="errors['endereco.bairro']">{{ errors['endereco.bairro'] }}</span>
        </div>
        <div></div>
        </div>
        <div>
          <label for="cidade">Cidade :</label>
          <input v-model="cidade" type="text" id="cidade" class="input" />
          <span v-if="errors['endereco.cidade']">{{ errors['endereco.cidade'] }}</span>
        </div>
        <div>
          <label for="estado">Estado :</label>
          <input v-model="estado" type="text" id="estado" class="input" />
          <span v-if="errors['endereco.estado']">{{ errors['endereco.estado'] }}</span>
        </div>
        <div>
          <label for="cep">CEP :</label>
          <input v-model="cep" type="text" id="cep" class="input" />
          <span v-if="errors['endereco.cep']">{{ errors['endereco.cep'] }}</span>
        </div>
      </div>
      <button type="submit" class="mt-1 px-4 py-2 bg-green-500 text-white rounded-md hover:bg-green-600 disabled:bg-green-400 disabled:cursor-not-allowed transition-all duration-200">{{ isEdit ? 'Salvar' : 'Cadastrar' }}</button>
      <router-link to="/">
        <button type="submit" class="mt-1 ml-21 px-5 py-2 bg-green-500 text-white rounded-md hover:bg-green-600 disabled:bg-green-400 disabled:cursor-not-allowed transition-all duration-200">{{ "Voltar"}} </button>
      </router-link>
    </form>
  </div>
</template>

<style scoped>
.input {
  width: 100%;
  padding: 0.8rem;
  margin: 0.5rem 0;
  border-radius: 4px;
  border: 1px solid #ccc;
}

.btn {
  padding: 1rem;
  background-color: #4CAF50;
  color: white;
  border-radius: 4px;
  cursor: pointer;
}

span {
  color: red;
  font-size: 0.8rem;
}
</style>
