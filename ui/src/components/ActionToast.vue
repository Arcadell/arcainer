<script setup lang="ts">
const enable = defineModel('enable', { required: true })
enable.value = false;

const props = defineProps({
    message: { type: String, required: true },
    buttonTextOk: { type: String, required: true },
    buttonTextCancel: { type: String },
});

const emits = defineEmits(['button-cancel', 'button-ok']);

const buttonPressed = (ok: boolean) => {
    enable.value = false;
    ok ? emits('button-ok') : emits('button-cancel')
}
</script>

<template>
    <div class="action-toasts-container">
        <div class="action-toast" v-if="enable">
            <p> {{ message }} </p>
            <div class="action-toast-button">
                <button v-on:click="buttonPressed(false)" v-if="buttonTextCancel" class="btn-outline">{{
                    buttonTextCancel
                    }}</button>
                <button v-on:click="buttonPressed(true)">{{ buttonTextOk }}</button>
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss">
.action-toasts-container {
    position: fixed;

    display: flex;
    flex-direction: column;
    gap: 0.5em;

    bottom: 20px;
    left: 50%;
    transform: translate(-50%, -0%);

    z-index: 1000;
}

.action-toast {
    display: flex;
    flex-direction: row;
    align-items: center;
    justify-content: space-between;

    padding: 10px;
    width: 500px;
    min-height: 40px;
    font-size: var(--mini-font-size);

    border: solid 1px var(--border-colour);
    border-radius: var(--border-radius);
    box-shadow: var(--default-shadow);

    background-color: var(--color-background);
    border: solid 2px var(--primary-colour);

    .action-toast-button {
        display: flex;
        gap: 5px;
    }
}
</style>
