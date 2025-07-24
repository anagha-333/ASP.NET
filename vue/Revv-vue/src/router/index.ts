// src/router/index.ts
import { createRouter, createWebHistory } from 'vue-router'
import Login from '@/components/Login.vue'
import Home from '@/components/Home.vue'
import Welcome from '@/components/Welcome.vue'
import Editcar from '@/components/Editcar.vue'
import AddCar from '@/components/AddCar.vue';
import Register from '@/components/Register.vue'
const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/welcome',
    name: 'Welcome',
    component: Welcome
  },
  {
    path: '/editcar',
    name: 'EditCar',
    component: Editcar
  },
     { path: '/add', 
      name: 'AddCar', 
      component: AddCar
     },
     
     { path: '/register', 
      name: 'Register', 
      component: Register
     }


]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
