<script setup lang="ts">
defineProps({
    opened: { type: Boolean, required: true }
});
const emit = defineEmits(['close-sidebar']);

</script>

<template>
    <Transition name="sidebar">
        <div v-if="opened" class="main-sidebar">
            <div class="sidebar">
                <button v-on:click="emit('close-sidebar');">Close</button>
            </div>
        </div>
    </Transition>
</template>

<style lang="scss" scoped>
.main-sidebar {
    position: absolute;
    z-index: 1;

    display: flex;
    justify-content: flex-end;

    background-color: #0000006c;

    height: 100dvh;
    width: 100dvw;

    right: 0;

    .sidebar {
        display: flex;
        flex-direction: column;

        background-color: var(--color-background);
        width: 700px;
    }
}

.sidebar-enter-active,
.sidebar-leave-active {
    transition: all 0.2s ease-out;
}

.sidebar-enter-from,
.sidebar-leave-to {
    transform: translateX(20px);
    opacity: 0;
}
</style>