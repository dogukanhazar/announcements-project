<template>
  <div class="rounded">
    <div
      class="bg-light p-3 mb-3 rounded d-flex flex-column align-items-start justify-content-center"
    >
      <h3>{{ item?.title }}</h3>
      <announcement-date
        class="align-self-end"
        :createdDate="item?.createdDate"
        :expiredDate="item?.expiredDate"
      ></announcement-date>
    </div>
    <div v-if="!!item?.description" class="p-3">
      <p class="fw-light lh-lg">{{ item?.description }}</p>
    </div>
  </div>
</template>

<script>
import { request, urls } from '@/utils/request.js';
import AnnouncementDate from '@/components/AnnouncementDate.vue';

export default {
  data() {
    return {
      loading: false,
      item: {}
    };
  },
  created() {
    this.fetchItemById();
  },
  methods: {
    async fetchItemById() {
      console.log(this.$route);
      try {
        this.loading = true;
        const res = await request({
          method: 'get',
          url: `${urls?.ANNOUNCEMENTS_PATH}/${this.$route.params?.id}`
        });
        const fetchedItem = res?.data;
        this.item = fetchedItem;

        console.log('fetched: ', fetchedItem);
      } catch (error) {
        console.log('fetched error: ', error);
      } finally {
        this.loading = false;
      }
    }
  },
  components: { AnnouncementDate }
};
</script>
