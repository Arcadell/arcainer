<script setup lang="ts">
// @ts-ignore
import composerize from 'composerize';
import { ref } from 'vue';
import CodeEditor from './CodeEditor.vue';
import { useContainerStore } from '@/stores/data/container';

const containerStore = useContainerStore();

const dockerRunCommand = ref('');
const dockerComposeValue = ref('');
const localDockerComposeValue = ref('');

const converContainer = () => {
    dockerComposeValue.value = composerize(dockerRunCommand.value)
}

const createContainer = (startOnCreate: boolean) => {
    containerStore.createContainer({ name: 'test', compose: localDockerComposeValue.value, startOnCreate })
}

const updateCompose = (value: string) => {
    localDockerComposeValue.value = value;
}
</script>

<template>
    <div class="main-container-create">
        <div class="container-create-content">
            <div class="generate-docker-compose">
                <input type="text" placeholder="Docker run command" v-model="dockerRunCommand">
                <button class="btn btn-outline" v-on:click="converContainer">Convert</button>
            </div>

            <h2>Compose</h2>
            <CodeEditor :value="dockerComposeValue" @updated-compose="updateCompose" />
        </div>

        <div class="container-create-actions">
            <button class="btn btn-outline" v-on:click="createContainer(false)">Create</button>
            <button v-on:click="createContainer(true)">Create & run</button>
        </div>
    </div>
</template>

<style scoped lang="scss">
.main-container-create {
    display: flex;
    flex-direction: column;

    gap: 1em;
    justify-content: space-between;
    height: 100%;

    .container-create-content {
        display: flex;
        flex-direction: column;

        gap: 1em;
        height: 100%;

        .generate-docker-compose {
            display: flex;

            align-items: center;
            gap: 5px;

            input {
                width: 100%;
            }
        }
    }

    .container-create-actions {
        display: flex;
        gap: 0.5em;

        button {
            width: 100%;
        }
    }
}
</style>