<script setup lang="ts">
import { RouterLink, RouterView, useRoute } from 'vue-router';
const route = useRoute();
</script>

<template>
  <div class="main">
    <div class="menu-bar">
      <div class="menu-header">
        <i class="ri-box-3-line"></i>
        <h1>Arcainer</h1>
      </div>

      <div class="menu-list">
        <RouterLink to="/containers"><a class="lnk lnk-active"><i class="ri-instance-line"></i> Containers</a>
        </RouterLink>
        <a class="lnk"><i class="ri-cloud-line"></i> Images</a>
        <a class="lnk"><i class="ri-archive-line"></i> Volumes</a>
        <a class="lnk"><i class="ri-global-line"></i> Networks</a>
      </div>

      <div class="menu-list">
        <a class="lnk"><i class="ri-user-line"></i> Users</a>
        <a class="lnk"><i class="ri-settings-3-line"></i> Settings</a>
      </div>
    </div>
    <div class="content">
      <div class="menu-header">
        <template v-if="route.name === 'containers'">
          <i class="ri-instance-line"></i>
          <h1>Containers</h1>
        </template>
      </div>
      <div class="content-box">
        <RouterView />
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
.main {
  display: flex;
  flex-direction: row;
  width: 100%;
  height: 100dvh;
}

.menu-bar {
  display: flex;
  flex-direction: column;
  width: 20%;

  border-right: solid 1px var(--border-colour);
}

.menu-header {
  display: flex;
  gap: 0.2rem;
  align-items: center;

  padding: 0.5rem 1rem;
  min-height: 29px;

  border-bottom: solid 1px var(--border-colour);

  i {
    font-size: 1.5rem;
  }

  h1 {
    font-size: 1.3rem;
  }
}

.menu-list {
  display: flex;
  flex-direction: column;
  padding: 0.4rem;
  gap: 0.3rem;

  border-top: solid 1px var(--border-colour);

  &:nth-of-type(2) {
    border-top: none;
  }
}

.content {
  display: flex;
  flex-direction: column;
  width: 100%;

  .content-box {
    display: flex;
    padding: 1rem;
    height: 100dvh;
  }
}
</style>