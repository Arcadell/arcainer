<script setup lang="ts">
import ActionToast from '@/components/ActionToast.vue';
import type { Settings } from '@/models/data';
import { useSettingStore } from '@/stores/data/settings';
import { ref } from 'vue';

const settingsStore = useSettingStore()

let somethingChanged = ref(false);
let settingsObj = ref<Settings>({
    id: '',
    disableRegistration: false
});

const getSetting = () => {
    settingsStore.getSetting().then(res => {
        if (res.data) { settingsObj.value = res.data; }
    });
}

const updateSetting = () => {
    settingsStore.updateSetting(settingsObj.value)
}

const resetSetting = () => {
    somethingChanged.value = false;
    getSetting();
}

getSetting();
</script>

<template>
    <ActionToast v-model:enable="somethingChanged" :message="'Save settings'" :button-text-cancel="'Cancel'"
        :button-text-ok="'Save'" v-on:button-cancel="resetSetting" v-on:button-ok="updateSetting" />
    <div class="sub-view-main" v-if="true">
        <div class="menu-header">
            <div class="left-header">
                <i class="ri-settings-3-line"></i>
                <h1>Settings</h1>
            </div>
        </div>

        <div class="content settings-content-custom">
            <div class="settings-container">
                <div class="settings-field">
                    <p>Disable signup</p>
                    <input type="checkbox" v-model="settingsObj.disableRegistration" @input="somethingChanged = true">
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss">
.settings-content-custom {
    display: flex !important;
    flex-direction: column !important;
    align-items: center !important;

    .settings-container {
        display: flex;
        flex-direction: column;

        max-width: 600px;
        width: 100%;

        border: 1px solid var(--border-colour);
        border-radius: var(--border-radius);

        padding: 1em;
    }

    .settings-field {
        display: flex;
        justify-content: space-between;
        align-items: center;
    }
}
</style>
