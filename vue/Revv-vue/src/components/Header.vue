<script setup lang="ts">

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
        <ul class="navbar-nav">
          <li class="nav-item">
            <a
              class="nav-link"
              href="https://app-revvapp-qa-eastus.azurewebsites.net/about-us"
              target="_blank"
              >About Us</a
            >
          </li>
          <li class="nav-item">
            <RouterLink class="btn btn-primary text-white" to="/login">
              Login
            </RouterLink>
          </li>
 <li class="nav-item">
                       <RouterLink to="/register" class="btn btn-primary text-white">User Register</RouterLink>

          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>


