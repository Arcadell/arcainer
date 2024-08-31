<script setup lang="ts">
import Table from '@/components/Table.vue';
import type { Container } from '@/models/data';
import type { TableField } from '@/models/table';

import { useDataStore } from '@/stores/data';
import { onMounted, ref } from 'vue';

const dataStore = useDataStore();

const fields: TableField[] = [
    { key: 'id', label: 'Name' },
    { key: 'name', label: 'Image' },
    { key: 'state', label: 'Status' },
]

let container: Container[] = [];
let loadingContainer = ref(true);

onMounted(async () => {
    container = await dataStore.getContainers();
    loadingContainer.value = false;
})
</script>

<template>
    <div class="container-main" v-if="!loadingContainer">
        <Table :fields="fields" :data="container" :loading="loadingContainer" />
    </div>
</template>

<style scoped lang="scss">
.container-main {
    width: 100%;
}
</style>
