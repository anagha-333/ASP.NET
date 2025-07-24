<template>
  <section class="py-5">
    <div class="container">
      <div class="row">
        <div class="col-md-4 mb-4" v-for="(card, index) in longCards" :key="index">
          <div class="card shadow-sm h-100 card-hover" style="border-radius: 20px;">
            <div class="card-body d-flex flex-column align-items-center text-left">
              <div class="mb-3">
                <i :class="card.icon"></i>
              </div>
              <h5 class="card-title fw-bold">{{ card.title }}</h5>
              <p class="card-text">{{ card.text }}</p>
              <div class="mt-auto">
                <img :src="getImage(card.image)" :alt="card.title" class="img-fluid rounded" style="width: 100%; height: auto;" />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">
interface LongCard {
  icon: string
  title: string
  text: string
  image: string
}

const longCards: LongCard[] = [
  {
    icon: 'fa-solid fa-network-wired',
    title: 'API Options',
    text: 'Empower your application with our robust API options...',
    image: 'api-img.png',
  },
  {
    icon: 'fa-solid fa-chalkboard-user',
    title: 'Tech-Driven Work Flow',
    text: 'Our advanced technology seamlessly integrates...',
    image: 'tech-driven.png',
  },
  {
    icon: 'fa-solid fa-chart-bar',
    title: 'Early-Stage Condition Reporting',
    text: 'Our cutting-edge technology delivers condition information...',
    image: 'early-stage.png',
  },
]

const getImage = (fileName: string): string => {
  return new URL(`../assets/${fileName}`, import.meta.url).href
}
</script>
