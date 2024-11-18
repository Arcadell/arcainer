<script setup lang="ts">
// @ts-ignore
import composerize from 'composerize';
import { onMounted, ref } from 'vue';
import CodeEditor from './CodeEditor.vue';
import { useContainerStore } from '@/stores/data/container';
import { useToastStore } from '@/stores/utils';
import { Stack } from '@/models/data';

const prop = defineProps({
    stack: { type: Stack, required: true },
});

const emits = defineEmits(['created-compose'])

const toastStore = useToastStore();
const containerStore = useContainerStore();

const dockerRunCommand = ref('');
const dockerComposeValue = ref('');
const _localDockerComposeValue = ref('');

const creatingCompose = ref(false);

onMounted(() => {
    dockerComposeValue.value = prop.stack.dockerCompose;
})

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
    if (!dockerComposeValue.value) {
        toastStore.error({
            message: "Stack name and compose are required"
        });
        return;
    }

    creatingCompose.value = true;
    await containerStore.createContainer({ name: prop.stack.name, compose: _localDockerComposeValue.value, startOnCreate })
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
            <input type="text" placeholder="Stack name" v-model="stack.name!" disabled>
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