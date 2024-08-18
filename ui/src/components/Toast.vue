<script setup lang="ts">
import { useToastStore } from '@/stores/utils';
const toastStore = useToastStore();

</script>

<template>
    <div class="toasts-container">
        <div class="toast" v-for="toast in toastStore.toasts" :key="toast.id">
            {{ toast.message }}
        </div>
    </div>
</template>

<style scoped>
.toasts-container {
    position: fixed;

    display: flex;
    flex-direction: column;
    gap: 0.5rem;

    bottom: 10px;
    right: 10px;
}

.toast {
    display: flex;
    flex-direction: row;
    align-items: center;

    padding: 10px;
    width: 300px;
    min-height: 40px;
    font-size: var(--mini-font-size);

    border: solid 1px var(--border-colour);
    border-radius: var(--border-radius);
    box-shadow: var(--default-shadow);

    background-color: var(--color-background);
}
</style>
