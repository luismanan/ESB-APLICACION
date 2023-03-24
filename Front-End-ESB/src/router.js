import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'
import Bomberos from './components/Bomberos.vue'
import RegistroIncendio from './components/RegistroIncendio.vue'
import Rol from './components/Rol.vue'
import Usuario from './components/Usuario.vue'
import BomberoDestacado from './components/BomberoDestacado.vue'

Vue.use(Router)

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/Bomberos',
      name: 'Bomberos',
      component: Bomberos
    },
    {
      path: '/RegistroIncendio',
      name: 'RegistroIncendio',
      component: RegistroIncendio
    },
    {
      path: '/roles',
      name: 'roles',
      component: Rol
    },
    {
      path: '/usuarios',
      name: 'usuarios',
      component: Usuario
    },
    {
      path: '/BomberoDestacado',
      name: 'BomberoDestacado',
      component: BomberoDestacado
    }
  ]
})
