<script setup lang="ts">
import { ref, onMounted } from 'vue';
import axios from 'axios';


import HeroSection from './HeroSection.vue';
import cards from './cards.vue';
import Longcards from './Longcards.vue';


// Define a reactive variable for cars
const cars = ref<string[]>([]);

// Fetch data from ASP.NET Core API on component mount

</script>

<template>
  <div>
    
    <HeroSection />
    <cards />
    <Longcards />

    <!-- 🚗 Displaying data from backend -->
    <div style="padding: 20px">
     
      <ul>
        <li v-for="car in cars" :key="car">{{ car }}</li>
      </ul>
    </div>

    
  </div>
</template>

<style scoped>
h2 {
  margin-bottom: 10px;
}
</style>
