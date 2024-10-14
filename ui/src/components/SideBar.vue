<script setup lang="ts">
defineProps({
    title: { type: String, required: true },
    opened: { type: Boolean, required: true }
});
const emits = defineEmits(['close-sidebar']);

</script>

<template>
    <Transition name="sidebar">
        <div v-if="opened" class="main-sidebar">
            <div class="sidebar">
                <div class="header">
                    <h1>{{ title }}</h1>
                    <button class="btn btn-icon" v-on:click="emits('close-sidebar');">
                        <i class="ri-close-line"></i>
                    </button>
                </div>
                <div class="content">
                    <slot></slot>
                </div>
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
        height: 100%;

        .header {
            display: flex;
            justify-content: space-between;

            align-items: center;
            padding: 0.5em 1em;

            border-bottom: solid 1px var(--border-colour);
        }

        .content {
            display: flex;
            flex-direction: column;

            padding: 1em;
            height: 100%;
        }
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