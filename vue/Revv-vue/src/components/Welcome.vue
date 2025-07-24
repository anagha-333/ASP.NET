<template>
  <Header />

  <div class="welcome-container">
    <div class="text-center">
      <h1>Welcome, {{ email }}!</h1>
      <p class="lead">You have successfully logged in.</p>

       <div class="text-end mb-4">
      <button class="btn btn-success" @click="goToAddCar">
        + Add Car
      </button>
    </div>


      <div class="car-grid">
        <div class="car-card" v-for="(car, index) in cars" :key="index">
         
          <!-- <img :src="getImageUrl(car.image)" alt="Car Image" /> -->
           <img :src="`${baseUrl}/images/${car.image}`" class="card-img-top" alt="Car Image" />
          <div class="card-body">
            <h5>{{ car.brand }} {{ car.model }} ({{ car.year }})</h5>
            <p><strong>Created At:</strong> {{ car.createdAt || '-' }}</p>
            <p><strong>Updated At:</strong> {{ car.updatedAt || '-' }}</p>
            <p><strong>Location:</strong> {{ car.place }}</p>
            <p><strong>Reg No:</strong> {{ car.number }}</p>
            <p><strong>Date:</strong> {{ car.date }}</p>

            <!-- <button @click="editCar(car)" class="edit-button">Edit</button>
          <button @click="deleteCar(car.id)" class="delete-button">Delete</button> -->
<div class="button-row">
  <button @click="editCar(car)" class="action-button edit">Edit</button>
  <button @click="deleteCar(car.id)" class="action-button delete">Delete</button>
</div>

          </div>
        </div>
      </div>
    </div>
  </div>

  <Footer />
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import Header from './Header.vue'
import Footer from './Footer.vue'
import axios from 'axios'

const baseUrl = import.meta.env.VITE_BASE_API_URL.replace('/api', '');



function goToAddCar() {
  router.push({ name: 'AddCar' }); 
}

// Type definition
interface Car {
   id: string   
  image: string
  brand: string
  model: string
  year: number
  place: string
  number: string
  date: string
  createdAt?: string
  updatedAt?: string
}
async function deleteCar(id: string) {
  const confirmed = confirm("Are you sure you want to delete this car?");
  if (!confirmed) return;

  try {
    const token = localStorage.getItem('token');
    if (!token) {
      alert("Unauthorized. Please log in again.");
      router.push('/');
      return;
    }

    await axios.delete(`http://localhost:5063/api/Car/${id}`, {
      headers: {
        Authorization: `Bearer ${token}`
      }
    });

    // Remove deleted car from the list
    cars.value = cars.value.filter(car => car.id !== id);
    alert("Car deleted successfully.");
  } catch (error) {
    console.error("Failed to delete car:", error);
    alert("Failed to delete car.");
  }
}

// State
const cars = ref<Car[]>([])

const route = useRoute()
const router = useRouter()
const rawEmail = (route.query.email as string) || 'Guest'
const email = rawEmail.includes('@') ? rawEmail.split('@')[0] : rawEmail

// Fetch cars from backend
onMounted(async () => {
  try {
    const token = localStorage.getItem('token')
    if (!token) {
      alert('Unauthorized. Please log in again.')
      router.push('/')
      return
    }

    const response = await axios.get('http://localhost:5063/api/Car', {
      headers: {
        Authorization: `Bearer ${token}`
      }
    })

    cars.value = response.data
    console.log('Cars from API:', cars.value)
  } catch (error) {
    console.error('Error fetching cars:', error)
    alert('Failed to load cars.')
  }
})

function getImageUrl(name: string): string {
  return new URL(`../assets/${name}`, import.meta.url).href
}
// Go to Edit page
function editCar(car: Car) {
  router.push({
    name: 'EditCar',
    query: {
      number: String(car.number),
      email: rawEmail
    }
  })
}
</script>

<style scoped>
.welcome-container {
  padding: 2rem;
  background-color: #f8f9fa;
}

.text-center {
  text-align: center;
}

.car-grid {
  display: flex;
  flex-wrap: wrap;
  gap: 1.5rem;
  justify-content: center;
  margin-top: 2rem;
}

.car-card {
  background: white;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  width: 300px;
  overflow: hidden;
  text-align: left;
  transition: transform 0.2s ease-in-out;
}

.car-card:hover {
  transform: scale(1.03);
}

.car-card img {
  width: 100%;
  height: 180px;
  object-fit: cover;
}

.card-body {
  padding: 1rem;
  display: flex;
  flex-direction: column;
}
.card-img-top {
  height: 200px;
  object-fit: cover;
}

.card-body h5 {
  margin-bottom: 0.75rem;
  font-size: 1.1rem;
  color: #333;
}

.card-body p {
  margin-bottom: 0.4rem;
  font-size: 0.95rem;
}

.edit-button {
  align-self: flex-start;
  background-color: #007bff;
  color: white;
  border: none;
  padding: 6px 12px;
  font-size: 0.85rem;
  border-radius: 4px;
  cursor: pointer;
  margin-top: 0.5rem;
  transition: background-color 0.2s ease-in-out;
}

.edit-button:hover {
  background-color: #0056b3;
}
.delete-button {
  align-self: flex-start;
  background-color: #dc3545;
  color: white;
  border: none;
  padding: 6px 12px;
  font-size: 0.85rem;
  border-radius: 4px;
  cursor: pointer;
  margin-top: 0.3rem;
  margin-left: 0.3rem;
  transition: background-color 0.2s ease-in-out;
}

.delete-button:hover {
  background-color: #c82333;
}


.button-row {
  display: flex;
  justify-content: space-between; /* <-- this puts Edit on left, Delete on right */
  gap: 0.5rem;
  margin-top: 0.5rem;
}

.action-button {
  padding: 4px 10px;
  font-size: 0.75rem;
  border-radius: 4px;
  border: none;
  cursor: pointer;
  transition: background-color 0.2s ease-in-out;
}

/* Specific styles */
.action-button.edit {
  background-color: #007bff;
  color: white;
}

.action-button.edit:hover {
  background-color: #0056b3;
}

.action-button.delete {
  background-color: #dc3545;
  color: white;
}

.action-button.delete:hover {
  background-color: #c82333;
}




</style>
