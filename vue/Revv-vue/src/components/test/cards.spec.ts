import { describe, it, expect, beforeEach } from 'vitest'
import { mount, VueWrapper } from '@vue/test-utils'
import Cards from '../cards.vue'

let wrapper: VueWrapper<any>

describe('cards.vue', () => {
  beforeEach(() => {
    wrapper = mount(Cards)
  })

  it('renders 3 feature cards with correct titles and text', () => {
    const titles = ['AI-Driven', 'Data-Driven', 'Customer Focused']
    const descriptions = [
      'Embracing cutting-edge technology, we streamline and simplify your processes decision-making.',
      'Leveraging data, we ensure you have all the facts for transparent and informed decision-making.',
      'With powerful data insights, we help guide your remarketing needs for optimal performance.',
    ]

    const cards = wrapper.findAll('.card')

    // Check number of cards
    expect(cards.length).toBe(3)

    // Check each title and description
    cards.forEach((cardWrapper, index) => {
      const title = cardWrapper.find('h5').text()
      const text = cardWrapper.find('p').text()

      expect(title).toBe(titles[index])
      expect(text).toBe(descriptions[index])
    })
  })

  it('displays final heading and paragraph below cards', () => {
    const heading = wrapper.find('h4').text()
    const paragraph = wrapper.find('p.mt-3').text()

    expect(heading).toBe(
      "Maximize net returns at auction using REVV’s comprehensive solution"
    )
    expect(paragraph).toBe(
      "Our fully compliant offering is a modern, easy-to-use AI interface providing:"
    )
  })
})
