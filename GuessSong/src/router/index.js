import {createRouter, createWebHistory} from 'vue-router'

import Category from '@/components/CategoryPage.vue'
import Login from '@/components/LoginPage.vue'
import Register from '@/components/RegisterPage.vue'
import LeaderboardsPage from '@/components/LeaderboardsPage.vue'

const routes = [
    {path: '/login', component: Login},
    {path: '/', component: Category},
    {path: '/register',component: Register},
    {path: '/leaderboards', component: LeaderboardsPage}
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
})

export default router