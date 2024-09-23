<script setup lang="ts">
defineProps({
    opened: { type: Boolean, required: true }
});
const emit = defineEmits(['close-sidebar']);

</script>

<template>
    <Transition name="sidebar">
        <div v-if="opened" class="main-sidebar">
            <button v-on:click="emit('close-sidebar');">Close</button>
        </div>
    </Transition>
</template>

<style lang="scss" scoped>
.main-sidebar{
    position: absolute;

    display: flex;
    flex-direction: column;

    background-color: var(--color-background);
    height: 100dvh;
    width: 40%;

    right: 0;
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