<template>
  <section class="py-5 bg-light">
    <div class="container">
      <div class="row text-center">
        <div class="col-md-4 mb-4" v-for="(card, index) in cards" :key="index">
          <div
            class="card p-4 border-0 shadow-sm rounded-3"
            :style="{ backgroundColor: '#c9eec9' }"
          >
            <div class="card-body">
              <div class="mb-3">
                <img :src="getImage(card.icon)" :alt="card.title" style="width: 40px" />
              </div>
              <h5 class="card-title fw-bold">{{ card.title }}</h5>
              <p class="card-text">{{ card.text }}</p>
            </div>
          </div>
        </div>
      </div>
      <div class="text-center mt-5">
        <h4>
          Maximize net returns at auction using REVV’s comprehensive solution
        </h4>
        <p class="mt-3">
          Our fully compliant offering is a modern, easy-to-use AI interface providing:
        </p>
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">
interface Card {
  icon: string
  title: string
  text: string
}

const cards: Card[] = [
  {
    icon: 'AI-driven.svg',
    title: 'AI-Driven',
    text: 'Embracing cutting-edge technology, we streamline and simplify your processes decision-making.',
  },
  {
    icon: 'data_driven.svg',
    title: 'Data-Driven',
    text: 'Leveraging data, we ensure you have all the facts for transparent and informed decision-making.',
  },
  {
    icon: 'customer_focused.svg',
    title: 'Customer Focused',
    text: 'With powerful data insights, we help guide your remarketing needs for optimal performance.',
  },
]

const getImage = (fileName: string): string => {
  return new URL(`../assets/${fileName}`, import.meta.url).href
}
</script>
