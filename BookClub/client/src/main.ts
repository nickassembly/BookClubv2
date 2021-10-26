import Vue from 'vue';
import router from './router';
import store from './store/store';
import BootstrapVue from "bootstrap-vue"
import App from './App.vue'
import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap-vue/dist/bootstrap-vue.css"

// import the main Sass manifest file
import '@/assets/sass/main.scss';
import axios from 'axios';

Vue.use(BootstrapVue)
Vue.config.productionTip = false;

new Vue({
  router,
  store,
  render: (h: any) => h(App),
}).$mount('#app');

axios.interceptors.request.use((config: any) => {

  const authToken = store.getters['auth/authToken'];
  if (authToken) {
    config.headers.Authorization = `Bearer ${authToken}`;
  }
  return config;
}, (err: any) => {
  return Promise.reject(err);
});

