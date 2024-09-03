<script setup lang="ts">
import Table from '@/components/Table.vue';
import type { Container } from '@/models/data';
import type { TableField } from '@/models/table';

import { useContainerStore } from '@/stores/data/container';
import { onMounted, ref } from 'vue';

const containerStore = useContainerStore();

const fields: TableField[] = [
    { key: 'id', label: 'Id' },
    { key: 'name', label: 'Name' },
    { key: 'state', label: 'Status' },
]

let container: Container[] = [];
let loadingContainer = ref(true);

onMounted(async () => {
    const res = await containerStore.getContainers();

    container = res.data;
    loadingContainer.value = false;
})
</script>

<template>
    <div class="container-main" v-if="!loadingContainer">
        <div class="menu-header">
            <div class="left-header">
                <i class="ri-instance-line"></i>
                <h1>Containers</h1>
            </div>

            <div class="right-header">
                <button class="btn btn-icon"><i class="ri-refresh-line"></i></button>
                <button class="btn"><i class="ri-add-line"></i> Create container</button>
            </div>
        </div>

        <div class="content">
            <Table :fields="fields" :data="container" :loading="loadingContainer" />
        </div>
    </div>
</template>

<style scoped lang="scss">
.container-main {
    display: flex;
    flex-direction: column;

    width: 100%;

    .content {
        display: flex;
        flex-direction: column;

        padding: 1em;
    }
}
</style>
