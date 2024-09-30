<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router';
</script>

<template>
  <div class="home-main">
    <div class="menu-bar">
      <div class="menu-header">
        <div class="left-header">
          <i class="ri-box-3-line"></i>
          <h1>Arcainer</h1>
        </div>
      </div>

      <div class="menu-list">
        <RouterLink to="/containers"><a class="lnk lnk-active"><i class="ri-instance-line"></i> Containers</a></RouterLink>
        <RouterLink to="/images"><a class="lnk"><i class="ri-cloud-line"></i> Images</a></RouterLink>
        <a class="lnk"><i class="ri-archive-line"></i> Volumes</a>
        <a class="lnk"><i class="ri-global-line"></i> Networks</a>
      </div>

      <div class="menu-list">
        <a class="lnk"><i class="ri-user-line"></i> Users</a>
        <a class="lnk"><i class="ri-settings-3-line"></i> Settings</a>
      </div>
    </div>
    <div class="content">
      <RouterView />
    </div>
  </div>
</template>

<style scoped lang="scss">
.home-main {
  display: flex;
  flex-direction: row;
  width: 100%;
  height: 100dvh;

  .menu-bar {
    display: flex;
    flex-direction: column;
    width: 20%;

    border-right: solid 1px var(--border-colour);
  }

  .menu-list {
    display: flex;
    flex-direction: column;
    padding: 0.4em;
    gap: 0.3em;

    border-top: solid 1px var(--border-colour);

    &:nth-of-type(2) {
      border-top: none;
    }
  }

  .content {
    display: flex;
    flex-direction: column;
    width: 100%;

    height: 100dvh;
  }
}
</style>