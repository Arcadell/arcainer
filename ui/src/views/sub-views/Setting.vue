<script setup lang="ts">

</script>

<template>
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
                    <input type="checkbox">
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
