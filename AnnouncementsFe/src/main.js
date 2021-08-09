import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import axios from 'axios';
import VueAxios from 'vue-axios';
import ElementPlus from 'element-plus';
import 'element-plus/lib/theme-chalk/index.css';
import 'bootstrap/dist/css/bootstrap.min.css';

createApp(App).use(router).use(ElementPlus).use(VueAxios, axios).mount('#app');
