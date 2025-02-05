import { createRouter, createWebHistory } from 'vue-router';
import Home from '@/views/Home.vue';
import PessoaForm from '@/views/CadastroPessoa.vue';
import PessoaDetalhes from '@/views/PessoaDetalhes.vue';
import NotFound from '../views/NotFound.vue' 


const routes = [
  { path: '/', component: Home },
  { path: '/pessoas/novo', component: PessoaForm },
  { path: '/pessoas/:id', component: PessoaDetalhes },
  { path: '/pessoas/:id/editar', component: PessoaForm },
  { path: '/:pathMatch(.*)*', component: NotFound }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
