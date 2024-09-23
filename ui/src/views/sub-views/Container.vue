<script setup lang="ts">
import Table from '@/components/Table.vue';
import SideBar from '@/components/SideBar.vue';

import { ContainerCommands, type Container } from '@/models/data';
import type { TableField, TableRow } from '@/models/table';

import { useContainerStore } from '@/stores/data/container';
import { onMounted, ref } from 'vue';

const containerStore = useContainerStore();

const fields: TableField[] = [
    { key: 'id', label: 'Id' },
    { key: 'name', label: 'Name' },
    { key: 'state', label: 'Status' },
]

let containerTable: TableRow[] = [];

let loadingContainer = ref(true);
let enableControlButtons = ref(false);
let openSideBar = ref(false);

onMounted(async () => {
    const res = await containerStore.getContainers();

    res.data.forEach((container: Container) => { containerTable.push({ selected: false, fields: container }); })
    loadingContainer.value = false;
})

const onRowSelected = () => {
    const rowSelected = containerTable.filter(row => row.selected);
    enableControlButtons.value = rowSelected.length > 0;
}

const handleContainers = async (command: ContainerCommands) => {
    const containerSelected = containerTable.filter(row => row.selected).map(row => row.fields) as Container[];
    await containerStore.handleContainers(containerSelected, command);
}
</script>

<template>
    <SideBar :opened="openSideBar" @close-sidebar="openSideBar = false"></SideBar>
    <div class="container-main" v-if="!loadingContainer">
        <div class="menu-header">
            <div class="left-header">
                <i class="ri-instance-line"></i>
                <h1>Containers</h1>
            </div>

            <div class="right-header">
                <div class="right-header-control" v-if="enableControlButtons">
                    <button class="btn btn-icon" v-on:click="handleContainers(ContainerCommands.Delete)"><i
                            class="ri-delete-bin-6-line"></i></button>
                    <button class="btn btn-icon" v-on:click="handleContainers(ContainerCommands.Stop)"><i
                            class="ri-stop-line"></i></button>
                    <button class="btn btn-icon" v-on:click="handleContainers(ContainerCommands.Start)">
                        <i class="ri-play-line"></i>
                    </button>
                </div>

                <button class="btn btn-icon"><i class="ri-refresh-line"></i></button>
                <button class="btn" v-on:click="openSideBar = !openSideBar"><i class="ri-add-line"></i>Create container</button>
            </div>
        </div>

        <div class="content">
            <Table :fields="fields" :data="containerTable" :loading="loadingContainer" @row-selected="onRowSelected" />
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
