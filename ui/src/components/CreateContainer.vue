<script setup lang="ts">
import { ref } from 'vue';
import CodeEditor from './CodeEditor.vue';

const composeValue = ref('');

const converContainer = () => {
    composeValue.value = 'Hello'
}

const createContainer = () => {
    console.log(composeValue.value)
}
</script>

<template>
    <div class="main-container-create">
        <div class="generate-docker-compose">
            <input type="text" placeholder="Docker run command">
            <button class="btn btn-outline" v-on:click="converContainer">Convert</button>
        </div>

        <h2>Compose</h2>
        <CodeEditor :value="composeValue" />
        <button v-on:click="createContainer">Create</button>
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