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
    theme: {
        background: '#1a1b26',
        black: '#32344a',
        blue: '#7aa2f7',
        brightBlack: '#444b6a',
        brightBlue: '#7da6ff',
        brightCyan: '#7dcfff',
        brightGreen: '#b9f27c',
        brightMagenta: '#bb9af7',
        brightRed: '#ff7a93',
        brightWhite: '#c0caf5',
        brightYellow: '#ff9e64',
        cursor: '#c0caf5',
        cursorAccent: '#1a1b26',
        cyan: '#7dcfff',
        foreground: '#c0caf5',
        green: '#9ece6a',
        magenta: '#bb9af7',
        red: '#f7768e',
        white: '#a9b1d6',
        yellow: '#e0af68',
    }
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
    background-color: #1a1b26;
    border-radius: var(--border-radius);
    padding: 10px;
}
</style>
