<template>
  <div class="infinite-list-wrapper">
    <div class="list" v-infinite-scroll="loadMore" infinite-scroll-disabled="disabled">
      <announcement-card v-for="item in items" :key="item?.id" :item="item"></announcement-card>
      <p v-if="loading">Loading announcements...</p>
      <p v-if="noMore">No more announcement.</p>
    </div>
  </div>
</template>

<script>
import AnnouncementCard from '@/components/AnnouncementCard.vue';
import { request, urls } from '@/utils/request.js';

export default {
  data() {
    return {
      loading: false,
      items: [],
      page: 0,
      totalPage: 0,
      pageUrl: '',
      nextPageUrl: '',
      previousPageUrl: ''
    };
  },
  created() {
    this.load();
  },
  computed: {
    noMore() {
      return !!!this.nextPageUrl;
    },
    disabled() {
      return this.loading || this.noMore;
    }
  },
  methods: {
    async load() {
      try {
        this.loading = true;
        const res = await request({ method: 'get', url: urls?.ANNOUNCEMENTS_PATH });
        const fetchedItem = res?.data;
        this.items = fetchedItem?.data;
        this.page = fetchedItem?.page;
        this.totalPage = fetchedItem?.totalPage;
        this.pageUrl = fetchedItem?.pageUrl;
        this.nextPageUrl = fetchedItem?.nextPageUrl;
        this.previousPageUrl = fetchedItem?.previousPageUrl;

        console.log('fetched: ', fetchedItem);
      } catch (error) {
        this.pageUrl = '';
        this.nextPageUrl = '';
        this.previousPageUrl = '';

        console.log('fetched error: ', error);
      } finally {
        this.loading = false;
      }
    },
    async loadMore() {
      try {
        this.loading = true;
        const res = await request({ method: 'get', url: this.nextPageUrl });
        const fetchedItem = res?.data;
        this.items = [...this.items, ...fetchedItem?.data];
        this.page = fetchedItem?.page;
        this.totalPage = fetchedItem?.totalPage;
        this.pageUrl = fetchedItem?.pageUrl;
        this.nextPageUrl = fetchedItem?.nextPageUrl;
        this.previousPageUrl = fetchedItem?.previousPageUrl;

        console.log('fetchedMore: ', fetchedItem);
      } catch (error) {
        this.pageUrl = '';
        this.nextPageUrl = '';
        this.previousPageUrl = '';

        console.log('fetchedMore error: ', error);
      } finally {
        this.loading = false;
      }
    }
  },
  components: { AnnouncementCard }
};
</script>
