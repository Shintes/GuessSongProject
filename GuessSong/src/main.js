import { createApp } from 'vue'
import router from './router'
import App from './App.vue'
import store from './store'
import axios from 'axios'

const axiosInstance = axios.create({
  withCredentials: true,
})

const app = createApp(App)
app.config.globalProperties.$axios = { ...axiosInstance }

createApp(App).use(store).use(router).mount('#app')