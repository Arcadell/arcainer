<script setup lang="ts">
import { useContainerStore } from '@/stores/data/container';
import { Terminal } from '@xterm/xterm';
import { onMounted, ref } from 'vue';

const prop = defineProps({
    idContainer: { type: String, required: true },
});

let loadingContainerLogs = ref(true);
let containerLogs = ref('');

const logTerminal = new Terminal({
    rows: 30,
    cols: 100,
    fontSize: 15,
    disableStdin: true,
    fontFamily: 'monospace',
    scrollback: 0,
});

const containerStore = useContainerStore();

const refreshContainers = () => {
    containerStore.getContainerLogs(prop.idContainer).then(res => {
        containerLogs.value = res.data.replace(/\n/g, '\r\n');
        loadingContainerLogs.value = false;
        logTerminal.write(containerLogs.value);
    });
}

refreshContainers();

onMounted(() => {
    logTerminal.open(document.getElementById('terminal')!);
});
</script>

<template>
    <div class="container-log">
        <div class="terminal-container">
            <div id="terminal" ref="terminal"></div>
        </div>
    </div>
</template>

<style scoped>
.container-log {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
}

.terminal-container {
    width: 100%;
    height: 100%;
    background-color: #000000;
    border-radius: var(--border-radius);
    padding: 10px;
}
</style>
