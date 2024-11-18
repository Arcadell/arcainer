<script setup lang="ts">
// @ts-ignore
import composerize from 'composerize';
import { ref } from 'vue';
import CodeEditor from './CodeEditor.vue';
import { useContainerStore } from '@/stores/data/container';
import { useToastStore } from '@/stores/utils';

const emits = defineEmits(['created-compose'])

const toastStore = useToastStore();
const containerStore = useContainerStore();

const stackName = ref('');

const dockerRunCommand = ref('');
const dockerComposeValue = ref('');
const _localDockerComposeValue = ref('');

const creatingCompose = ref(false);

const converContainer = () => {
    try {
        let _dockerComposeValue = composerize(dockerRunCommand.value, null, 'latest', 2) as string;
        _dockerComposeValue = _dockerComposeValue.substring(26);
        if (dockerComposeValue.value != _dockerComposeValue)
        dockerComposeValue.value = _dockerComposeValue
    else {
        dockerComposeValue.value += ' '; // Idk what the f*#@! im doing
    }
    } catch {
        toastStore.error({message: 'Invalid docker run command'});
    }
}

const createContainer = async (startOnCreate: boolean) => {
    if (!stackName.value || !dockerComposeValue.value) {
        toastStore.error({
            message: "Stack name and compose are required"
        });
        return;
    }

    creatingCompose.value = true;
    await containerStore.createContainer({ name: stackName.value, compose: _localDockerComposeValue.value, startOnCreate })
    creatingCompose.value = false;
    emits('created-compose');
}

const updateCompose = (value: string) => {
    _localDockerComposeValue.value = value;
}
</script>

<template>
    <div class="main-container-create">
        <div class="container-create-content">
            <input type="text" placeholder="Stack name" v-model="stackName" required>
            <div class="generate-docker-compose">
                <input type="text" placeholder="Docker run command" v-model="dockerRunCommand">
                <button class="btn btn-outline" v-on:click="converContainer">Convert</button>
            </div>

            <h2>Compose</h2>
            <CodeEditor ref="codeEditor" :value="dockerComposeValue" @updated-compose="updateCompose" />
        </div>

        <div class="container-create-actions">
            <button class="btn btn-outline" v-on:click="createContainer(false)"
                :disabled="creatingCompose">Create</button>
            <button v-on:click="createContainer(true)" :disabled="creatingCompose">Create & run</button>
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