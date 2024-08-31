import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/Home.vue'
import { useAuthStore } from '@/stores/auth'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      redirect: { name: 'containers' },
      children: [
        {
          path: 'containers',
          name: 'containers',
          component: () => import('../components/ContainersTable.vue')
        }
      ]
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('../views/Login.vue')
    },
  ]
})

router.beforeEach((to, from, next) => {
  const store = useAuthStore()

  if (!store.checkLogin() && to.name !== 'login') {
    next({ name: 'login' })
  } else {
    next()
  }
})

export default router