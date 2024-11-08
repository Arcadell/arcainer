<script setup lang="ts">
import Table from '@/components/Table.vue';
import SideBar from '@/components/SideBar.vue';
import CreateContainer from '@/components/CreateStack.vue';

import { ContainerCommands, type Container } from '@/models/data';
import type { TableField, TableRow } from '@/models/table';

import { useContainerStore } from '@/stores/data/container';

import { ref } from 'vue';
import { v7 as uuid } from 'uuid'

const containerStore = useContainerStore();

const fields: TableField[] = [
    { key: 'id', label: 'Id' },
    { key: 'name', label: 'Name' },
    { key: 'state', label: 'Status' },
]

let containerTable = ref<TableRow[]>([]);

let loadingContainers = ref(true);
let enableControlButtons = ref(false);
let openSideBar = ref(false);

const refreshContainers = () => {
    containerStore.getContainers().then(res => {
        containerTable.value = [];
        res.data.forEach((container: Container) => { containerTable.value.push({ id: uuid(), selected: false, fields: container }); });
        loadingContainers.value = false;
    });
}

refreshContainers();

const onRowSelected = () => {
    const rowSelected = containerTable.value.filter(row => row.selected);
    enableControlButtons.value = rowSelected.length > 0;
}

const handleContainers = async (command: ContainerCommands) => {
    const containerSelected = containerTable.value.filter(row => row.selected).map(row => row.fields) as Container[];
    await containerStore.handleContainers(containerSelected, command);
    refreshContainers();
}
</script>

<template>
    <SideBar :title="'Create container'" :opened="openSideBar" @close-sidebar="openSideBar = false">
        <CreateContainer v-on:created-compose="openSideBar = false" />
    </SideBar>
    <div class="sub-view-main" v-if="!loadingContainers">
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

                <button class="btn btn-icon" v-on:click="refreshContainers"><i class="ri-refresh-line"></i></button>
            </div>
        </div>

        <div class="content">
            <Table :fields="fields" :data="containerTable" :loading="loadingContainers" @row-selected="onRowSelected" />
        </div>
    </div>
</template>

<style scoped lang="scss"></style>
