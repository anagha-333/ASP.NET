<template>


  <div class="edit-container">
    <h2>Edit Car Details</h2>

    <form @submit.prevent="updateCar">
      <label>Brand:</label>
      <input v-model="form.brand" required />

      <label>Model:</label>
      <input v-model="form.model" required />

      <label>Year:</label>
      <input type="number" v-model.number="form.year" required />

      <label>Location:</label>
      <input v-model="form.place" required />

      <label>Reg No:</label>
      <input v-model.number="form.number" required readonly />

      <label>Date:</label>
      <input type="date" v-model="form.date" required />

      <div class="button-group">
        <button type="submit" class="save-button">Save</button>
        <button type="button" class="cancel-button" @click="cancel">Cancel</button>
      </div>
    </form>
  </div>

</template>

<script setup lang="ts">
import { useRoute, useRouter } from 'vue-router'
import { reactive, onMounted } from 'vue'
import axios from 'axios'


const route = useRoute()
const router = useRouter()

const number = Number(route.query.number)
const email = route.query.email as string

const form = reactive({
   id: '',
  brand: '',
  model: '',
  year: 0,
  place: '',
  number: 0,
  date: '',
   image: ''
})

onMounted(async () => {
  const token = localStorage.getItem('token')
  if (!token) {
    alert('Unauthorized')
    router.push('/')
    return
  }

  try {
    const res = await axios.get(`http://localhost:5063/api/Car/bynumber/${number}`, {
      headers: { Authorization: `Bearer ${token}` }
    })

    const car = res.data

    form.brand = car.brand
    form.model = car.model
    form.year = car.year
    form.place = car.place
    form.number = car.number
    form.date = formatDate(car.date)
    form.id = car.id
form.image = car.image
  } catch (err) {
    console.error('Failed to load car:', err)
    alert('Car not found')
    router.push({ name: 'Welcome', query: { email } })
  }
})

function formatDate(dateStr: string): string {
  const date = new Date(dateStr)
  return isNaN(date.getTime()) ? '' : date.toISOString().substring(0, 10)
}

async function updateCar() {
  const token = localStorage.getItem('token')
  if (!token) {
    alert('Unauthorized')
    router.push('/')
    return
  }

  try {
    await axios.put(`http://localhost:5063/api/Car/${form.id}`, {
        id: form.id,
        image: form.image,
      brand: form.brand,
      model: form.model,
      year: form.year,
      place: form.place,
      number: form.number,
      date: new Date(form.date).toISOString() 
      // ✅ Fix: ISO format
    }, {
      headers: { Authorization: `Bearer ${token}` }
    })

    alert('Car updated successfully!')
    router.push({ name: 'Welcome', query: { email } })
  } catch (err) {
    if (axios.isAxiosError(err)) {
      console.error('Backend error:', err.response?.data)
      alert(`Failed to update car: ${err.response?.data?.message || err.message}`)
    } else {
      console.error('Unexpected error:', err)
      alert('Unexpected error occurred while updating car.')
    }
  }
}


function cancel() {
  router.push({ name: 'Welcome', query: { email } })
}
</script>

<style scoped>
.edit-container {
  max-width: 600px;
  margin: 2rem auto;
  padding: 2rem;
  background-color: #fff;
  border-radius: 10px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

form label {
  display: block;
  margin-top: 1rem;
  font-weight: bold;
}

form input {
  width: 100%;
  padding: 0.5rem;
  margin-top: 0.25rem;
  border: 1px solid #ccc;
  border-radius: 5px;
}

.button-group {
  margin-top: 1.5rem;
  display: flex;
  gap: 1rem;
}

.save-button,
.cancel-button {
  background-color: #007bff;
  color: white;
  border: none;
  padding: 0.6rem 1.2rem;
  font-weight: 600;
  border-radius: 5px;
  cursor: pointer;
}

.cancel-button {
  background-color: #dc3545;
}

.save-button:hover {
  background-color: #0056b3;
}

.cancel-button:hover {
  background-color: #c82333;
}
</style>
