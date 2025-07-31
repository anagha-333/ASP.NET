import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { Field, Form, ErrorMessage } from 'vee-validate'
import './css/style.css' // your global styles
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap'
import '@fortawesome/fontawesome-free/css/all.min.css';
import { createPinia } from 'pinia'


const app = createApp(App)
app.use(router)
app.use(createPinia())
app.mount('#app')


