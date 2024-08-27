<script setup lang="ts">
import { useDataStore } from '@/stores/data';
import { onMounted } from 'vue';

const dataStore = useDataStore();

onMounted(() => {
    dataStore.getContainers();
})
</script>

<template>
    <div class="toasts-container">

    </div>
</template>

<style scoped lang="scss"></style>
