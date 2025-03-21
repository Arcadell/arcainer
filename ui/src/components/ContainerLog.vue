<script setup lang="ts">
import { Terminal } from '@xterm/xterm';
import { onMounted } from 'vue';

const prop = defineProps({
    idContainer: { type: String, required: true },
});

const logTerminal = new Terminal({
    rows: 30,
    cols: 100,
    fontSize: 15,
    disableStdin: true,
    fontFamily: 'monospace',
    scrollback: 0,
});

onMounted(() => {
    logTerminal.open(document.getElementById('terminal')!);
    let testString = 'test line 1\r\n'
    testString += 'test line 2\r\n'
    testString += 'test line 3\r\n'
    logTerminal.write(testString)
});

</script>

<template>
    <div class="container-log">
        <h1>{{ prop.idContainer }}</h1>
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
