<script setup lang="ts">
import CodeEditor from './CodeEditor.vue';
</script>

<template>
    <div class="main-container-create">
        <div class="generate-docker-compose">
            <input type="text" placeholder="Docker run command">
            <button class="btn btn-outline">Convert</button>
        </div>

        <h2>Compose</h2>
        <CodeEditor />
    </div>
</template>

<style scoped lang="scss">
.main-container-create {
    display: flex;
    flex-direction: column;

    gap: 1em;

    .generate-docker-compose {
        display: flex;

        align-items: center;
        gap: 5px;

        input {
            width: 100%;
        }
    }
}
</style>