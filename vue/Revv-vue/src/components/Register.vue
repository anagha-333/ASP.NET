<template>
  
  <div class="login-card">
    <div class="logo">
      <img src="@/assets/logo.svg" alt="REVV Logo" />
    </div>
    <h2>Create an Account</h2>
    <p class="subtitle">Register with a role to get started.</p>

    <Form @submit="onSubmit" :validation-schema="schema">
      <!-- Username -->
      <div class="mb-3 text-start">
        <label for="username" class="form-label">Username</label>
        <Field
          id="username"
          name="username"
          type="text"
          class="form-control"
          placeholder="Enter username"
        />
        <ErrorMessage name="username" class="invalid-feedback" />
      </div>

      <!-- Password -->
      <div class="mb-3 text-start">
        <label for="password" class="form-label">Password</label>
        <div class="input-group input-group-password">
          <Field
            :type="showPassword ? 'text' : 'password'"
            name="password"
            id="password"
            class="form-control"
            placeholder="Enter password"
          />
          <span class="input-group-text" @click="togglePassword" style="cursor: pointer">
            <i :class="showPassword ? 'bi bi-eye-slash' : 'bi bi-eye'"></i>
          </span>
        </div>
        <ErrorMessage name="password" class="invalid-feedback" />
        <div class="password-feedback" v-if="values.password">
          <ul class="mb-0">
            <li :class="{ valid: values.password.length >= 8 }">Min 8 characters</li>
            <li :class="{ valid: specialCharRegex.test(values.password) }">
              At least one special character
            </li>
          </ul>
        </div>
      </div>

  
<div class="mb-3 text-start">
  <label for="role" class="form-label">Role</label>
  <Field
    id="role"
    name="role"
    type="text"
    class="form-control"
    placeholder="Enter role (e.g., Admin or User)"
  />
  <ErrorMessage name="role" class="invalid-feedback" />
</div>


      <!-- Submit -->
      <button type="submit" class="btn btn-primary w-100">Register</button>
    </Form>

    <p class="copyright mt-4">&copy; {{ new Date().getFullYear() }} REVV. All rights reserved.</p>
  </div>
  
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import axios from '../api' // your axios instance or replace with full URL
import { Form, Field, ErrorMessage, useForm } from 'vee-validate'
import * as yup from 'yup'


const router = useRouter()
const showPassword = ref(false)
const specialCharRegex = /[!@#$%^&*(),.?":{}|<>]/

const schema = yup.object({
  username: yup.string().required('Username is required'),
  password: yup
    .string()
    .required('Password is required')
    .min(8, 'Minimum 8 characters')
    .matches(specialCharRegex, 'Must include a special character'),
  role: yup.string().required('Role is required')
})

const { values } = useForm()

const togglePassword = () => {
  showPassword.value = !showPassword.value
}

const onSubmit = async (values: any) => {
  const payload = {
    Username: values.username,
    Password: values.password,
    Role: values.role
  }
  console.log("Register payload:", payload)
  try {
    const response = await axios.post('http://localhost:5063/api/Auth/register', payload)
    alert('Registration successful!')
    router.push('/login')
  } catch (err: any) {
    console.error("Registration failed:", err.response?.data)
    const errs = err.response?.data?.errors
    const msg = errs ? JSON.stringify(errs) : err.response?.data || err.message
    alert(`Registration failed: ${msg}`)
  }
}

</script>

<style scoped>
@import 'bootstrap/dist/css/bootstrap.min.css';
@import 'bootstrap-icons/font/bootstrap-icons.css';

.login-card {
  background-color: #ffffff;
  border-radius: 8px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  padding: 40px;
  width: 100%;
  max-width: 450px;
  text-align: center;
  margin: auto;
}
.logo {
  margin-bottom: 30px;
}
.logo img {
  max-width: 150px;
  height: auto;
}
h2 {
  font-size: 1.8rem;
  font-weight: 600;
  margin-bottom: 10px;
  color: #333;
}
.subtitle {
  color: #666;
  margin-bottom: 30px;
  font-size: 0.95rem;
}
.password-feedback {
  font-size: 0.85rem;
  color: #dc3545;
  text-align: left;
  margin-top: 5px;
}
.password-feedback ul {
  list-style-type: none;
  padding-left: 0;
  margin-bottom: 0;
}
.password-feedback li {
  margin-bottom: 3px;
}
.password-feedback li.valid {
  color: #28a745;
}
.btn-primary {
  background-color: #000080;
  border-color: #000080;
  padding: 12px 0;
  font-size: 1.1rem;
  font-weight: 600;
  border-radius: 6px;
}
.btn-primary:hover {
  background-color: #000066;
  border-color: #000066;
}
.invalid-feedback {
  display: block;
  font-size: 0.875rem;
}
</style>
