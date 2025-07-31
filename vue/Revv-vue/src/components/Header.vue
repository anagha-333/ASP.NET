<script setup lang="ts">
import { RouterLink, useRouter } from 'vue-router'
import { ref, onMounted } from 'vue'
import { useAuthStore } from '../stores/auth'; // <-- Pinia store


const router = useRouter()
const auth = useAuthStore();


function handleLogout() {
  auth.logout();
  router.push('/login');
}
</script>

<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-light shadow">
    <div class="container">
      <RouterLink class="navbar-brand" to="/">
        <img src="@/assets/logo.svg" alt="REVV Logo" />
      </RouterLink>

      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarNav"
      >
        <span class="navbar-toggler-icon"></span>
      </button>

      <div class="collapse navbar-collapse justify-content-end" id="navbarNav">
        <ul class="navbar-nav align-items-center gap-2">
          <li class="nav-item">
            <a
              class="nav-link"
              href="https://app-revvapp-qa-eastus.azurewebsites.net/about-us"
              target="_blank"
              >About Us</a
            >
          </li>

          <li class="nav-item" v-if="!auth.isLoggedIn">
            <RouterLink class="btn btn-primary text-white" to="/login">
              Login
            </RouterLink>
          </li>

          <li class="nav-item" v-if="!auth.isLoggedIn">
            <RouterLink class="btn btn-primary text-white" to="/register">
              User Register
            </RouterLink>
          </li>

          <li class="nav-item" v-if="auth.isLoggedIn">
           <button @click="handleLogout" class="login-btn">Logout</button>
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>
