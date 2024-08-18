<script setup lang="ts">
import { useToastStore } from '@/stores/utils';
const toastStore = useToastStore();

</script>

<template>
    <div v-for="toast in toastStore.toasts" :key="toast.id" class="toast">
        {{ toast.message }}
    </div>
</template>

<style scoped></style>
