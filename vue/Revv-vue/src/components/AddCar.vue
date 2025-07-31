<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';


const router = useRouter();
const errorMessage = ref('');
const token = localStorage.getItem('token');

// File input
const imageFile = ref<File | null>(null);

// Form data
const newCar = ref({
  brand: '',
  model: '',
  year: new Date().getFullYear(),
  place: '',
  number: 0,
  date: '',
  createdAt: '',
  updatedAt: ''
});

// File change handler
function onFileChange(e: Event) {
  const target = e.target as HTMLInputElement;
  if (target.files && target.files.length > 0) {
    imageFile.value = target.files[0];
  }
}

async function saveNewCar() {
  errorMessage.value = '';

  if (!newCar.value.brand.trim() || !newCar.value.model.trim() || !imageFile.value) {
    errorMessage.value = 'Image, Brand, and Model are required.';
    return;
  }

  const now = new Date().toISOString();
  newCar.value.createdAt = now;
  newCar.value.updatedAt = now;

  // Format date to yyyy-MM-dd
  if (newCar.value.date) {
    const d = new Date(newCar.value.date);
    newCar.value.date = d.toISOString().split('T')[0];
  }

  const formData = new FormData();
  formData.append('ImageFile', imageFile.value);
  formData.append('Brand', newCar.value.brand);
  formData.append('Model', newCar.value.model);
  formData.append('Year', newCar.value.year.toString());
  formData.append('Place', newCar.value.place);
  formData.append('Number', newCar.value.number.toString());
  formData.append('Date', newCar.value.date);
  formData.append('CreatedAt', newCar.value.createdAt);
  formData.append('UpdatedAt', newCar.value.updatedAt);

  try {
    await axios.post('http://localhost:5063/api/Car', formData, {
      headers: {
        Authorization: `Bearer ${token}`
      }
    });

    router.push('/welcome');
  } catch (err: any) {
    console.error("Save failed:", err.response?.data);
    errorMessage.value = err.response?.data?.message || 'Failed to add car.';
  }
}
</script>

<template>
  <div>
    
    <div class="container my-5">
      <h2 class="mb-4">Add New Car</h2>
      <form @submit.prevent="saveNewCar" class="card p-4 shadow-sm">
        <div v-if="errorMessage" class="alert alert-danger">{{ errorMessage }}</div>

        <!-- Image -->
        <div class="mb-3">
          <label class="form-label">Image</label>
          <input class="form-control" type="file" accept="image/*" @change="onFileChange" required />
        </div>

        <!-- Brand -->
        <div class="mb-3">
          <label class="form-label">Brand</label>
          <input class="form-control" v-model="newCar.brand" required />
        </div>

        <!-- Model -->
        <div class="mb-3">
          <label class="form-label">Model</label>
          <input class="form-control" v-model="newCar.model" required />
        </div>

        <!-- Year -->
        <div class="mb-3">
          <label class="form-label">Year</label>
          <input class="form-control" type="number" v-model.number="newCar.year" required />
        </div>

        <!-- Place -->
        <div class="mb-3">
          <label class="form-label">Place</label>
          <input class="form-control" v-model="newCar.place" required />
        </div>

        <!-- Number -->
        <div class="mb-3">
          <label class="form-label">Registration Number</label>
          <input class="form-control" type="number" v-model.number="newCar.number" required />
        </div>

        <!-- Date -->
        <div class="mb-3">
          <label class="form-label">Date</label>
          <input class="form-control" type="date" v-model="newCar.date" required />
        </div>

        <button type="submit" class="btn btn-success">Save</button>
      </form>
    </div>
 
  </div>
</template>

<style scoped>
.container {
  max-width: 600px;
}
</style>
