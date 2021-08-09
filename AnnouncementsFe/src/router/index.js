// router/index.js
import { createRouter, createWebHistory } from 'vue-router';
import Home from '@/views/Home.vue';
import AnnouncementDetail from '@/views/AnnouncementDetail.vue';

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/announcements/:id',
    name: 'AnnouncementDetail',
    component: AnnouncementDetail
  }
];

const router = createRouter({ history: createWebHistory(), routes });
export default router;
