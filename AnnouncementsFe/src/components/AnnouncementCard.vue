<template>
  <el-card
    class="mb-3 bg-light"
    :class="hover && 'shadow bg-transparent'"
    role="button"
    @mouseover="hover = true"
    @mouseleave="hover = false"
    @click="handleClickItem"
  >
    <div
      class="content d-flex flex-column flex-lg-row justify-content-center align-items-start justify-content-lg-between align-items-lg-center"
    >
      <div class="flex-1">{{ item?.title }}</div>
      <announcement-date
        :createdDate="item?.createdDate"
        :expiredDate="item?.expiredDate"
      ></announcement-date>
    </div>
  </el-card>
</template>

<script>
import AnnouncementDate from '@/components/AnnouncementDate.vue';

export default {
  props: {
    item: {
      type: Object,
      required: true
    }
  },
  data() {
    return {
      hover: false
    };
  },
  methods: {
    handleClickItem() {
      let _detailBasePath = '/announcements/';
      let _itemId = this.item?.id;
      let _fullPath = `${_detailBasePath}${_itemId}`;
      this?.$router?.push(_fullPath);
    }
  },
  components: { AnnouncementDate }
};
</script>
