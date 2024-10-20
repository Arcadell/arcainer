<script setup lang="ts">
import { useToastStore } from '@/stores/utils';
const toastStore = useToastStore();

</script>

<template>
    <div class="toasts-container">
        <div class="toast"
            :class="{ 'toast-success': toast.status === 'success', 'toast-error': toast.status === 'error', 'toast-warning': toast.status === 'warning', }"
            v-for="toast in toastStore.toasts" :key="toast.id">
            {{ toast.message }}
        </div>
    </div>
</template>

<style scoped lang="scss">
.toasts-container {
    position: fixed;

    display: flex;
    flex-direction: column;
    gap: 0.5em;

    bottom: 10px;
    right: 10px;

    z-index: 1000;
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

    &.toast-success {
        border: solid 2px var(--success-colour);
    }

    &.toast-error {
        border: solid 2px var(--error-colour);
    }

    &.toast-warning {
        border: solid 2px var(--warning-colour);
    }
}
</style>
