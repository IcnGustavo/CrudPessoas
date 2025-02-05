import './assets/main.css'

import { createPinia } from 'pinia'
import { createApp } from 'vue'
import App from './App.vue'
import router from './router'   
import Toast from 'vue-toastification';
import 'vue-toastification/dist/index.css';

const app = createApp(App);

app.use(Toast, {
  position: "top-right",
  timeout: 5000,
})

app.use(createPinia())
app.use(router)
app.mount('#app')
