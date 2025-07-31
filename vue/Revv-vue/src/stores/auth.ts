import { defineStore } from 'pinia'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    token: localStorage.getItem('token') || '',
    username: localStorage.getItem('loggedInUser')
      ? JSON.parse(localStorage.getItem('loggedInUser')!).name
      : '',
  }),
  getters: {
    isLoggedIn: (state) => !!state.token,
  },
  actions: {
    login(token: string, username: string) {
      this.token = token
      this.username = username
      localStorage.setItem('token', token)
      localStorage.setItem('loggedInUser', JSON.stringify({ name: username }))
    },
    logout() {
      this.token = ''
      this.username = ''
      localStorage.removeItem('token')
      localStorage.removeItem('loggedInUser')
    },
  },
})
