<template>
  <Header />
  <div class="login-card">
    <div class="logo">
      <img src="@/assets/logo.svg" alt="REVV Logo" />
    </div>
    <h2>Log in to your account</h2>
    <p class="subtitle">Welcome! Sign in with your organizational account.</p>

    <Form @submit="onSubmit" :validation-schema="schema">
      <div class="mb-3 text-start">
        <label for="email" class="form-label">Email Address</label>
        <Field
          id="email"
          name="email"
          type="email"
          class="form-control"
          placeholder="Email Address"
        />
        <ErrorMessage name="email" class="invalid-feedback" />
      </div>

      <div class="mb-3 text-start">
        <label for="password" class="form-label">Password</label>
        <div class="input-group input-group-password">
          <Field
            :type="showPassword ? 'text' : 'password'"
            name="password"
            id="password"
            class="form-control"
            placeholder="Password"
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

      <div class="form-check">
        <div class="d-flex align-items-center">
          <input v-model="keepSignedIn" type="checkbox" class="form-check-input" id="keepSignedIn" />
          <label class="form-check-label" for="keepSignedIn">Keep me signed in</label>
        </div>
        <a href="#" class="forgot-password-link">Forgot your password?</a>
      </div>

      <button type="submit" class="btn btn-primary">Sign in</button>
    </Form>

    <p class="copyright">&copy; {{ new Date().getFullYear() }} REVV. All rights reserved.</p>
  </div>

  <Footer />
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { Form, Field, ErrorMessage, useForm } from 'vee-validate'
import * as yup from 'yup'
import Header from './Header.vue'
import Footer from './Footer.vue'
import axios from '../api'

// Router
const router = useRouter()

// States
const showPassword = ref(false)
const keepSignedIn = ref(false)
const specialCharRegex = /[!@#$%^&*(),.?":{}|<>]/

// Validation schema
const schema = yup.object({
  email: yup.string().required('Email is required').email('Must be a valid email'),
  password: yup
    .string()
    .required('Password is required')
    .min(8, 'Minimum 8 characters')
    .matches(specialCharRegex, 'At least one special character')
})

// Form values
const { values } = useForm()

// Toggle password visibility
const togglePassword = () => {
  showPassword.value = !showPassword.value
}

// Submit handler
const onSubmit = async (values: any) => {
  try {
    const response = await axios.post('http://localhost:5063/api/Auth/login', {
      username: values.email,
      password: values.password,
    });
    const { token, username } = response.data;
    localStorage.setItem('token', token);
    localStorage.setItem('loggedInUser', JSON.stringify({ name: username }));
    router.push({ path: '/welcome', query: { email: values.email } });
  } catch (err: any) {
    alert(err.response?.data?.message || 'Invalid email or password');
  }
};



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
  width: 100%;
  margin-top: 20px;
}
.btn-primary:hover {
  background-color: #000066;
  border-color: #000066;
}
.form-check {
  text-align: left;
  margin-top: 15px;
  display: flex;
  align-items: center;
  justify-content: space-between;
}
.forgot-password-link {
  color: #0d6efd;
  text-decoration: none;
  font-size: 0.9rem;
}
.forgot-password-link:hover {
  text-decoration: underline;
}
.invalid-feedback {
  display: block;
  font-size: 0.875rem;
}
</style>
