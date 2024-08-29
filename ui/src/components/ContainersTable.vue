<script setup lang="ts">
import { useDataStore } from '@/stores/data';
import { onMounted } from 'vue';

const dataStore = useDataStore();

onMounted(() => {
    dataStore.getContainers();
})
</script>

<template>
    <div class="toasts-container">
        <table>
            <tr>
                <th>Id</th>
                <th>Name</th>
                <th>State</th>
            </tr>
            <tr v-for="container in dataStore.containers" :key="container.id">
                <td>{{ container.id }}</td>
                <td>{{ container.name }}</td>
                <td>{{ container.state }}</td>
            </tr>
        </table>
    </div>
</template>

<style scoped lang="scss"></style>
