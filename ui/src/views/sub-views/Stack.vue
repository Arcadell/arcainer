<script setup lang="ts">
import Table from '@/components/Table.vue';
import SideBar from '@/components/SideBar.vue';
import CreateContainer from '@/components/CreateContainer.vue';

import { Stack, } from '@/models/data';
import type { TableField, TableRow } from '@/models/table';

import { useContainerStore } from '@/stores/data/container';
import { ref } from 'vue';

const containerStore = useContainerStore();

const fields: TableField[] = [
    { key: 'name', label: 'Name' },
]

let stackTable = ref<TableRow[]>([]);

let loadingStacks = ref(true);
let enableControlButtons = ref(false);
let openSideBar = ref(false);

const refreshStacks = () => {
    containerStore.getStacks().then(res => {
        stackTable.value = [];
        res.data.forEach((stack: Stack) => { stackTable.value.push({ selected: false, fields: stack }); });
        loadingStacks.value = false;
    });
}

refreshStacks();

const onRowSelected = () => {
    const rowSelected = stackTable.value.filter(row => row.selected);
    enableControlButtons.value = rowSelected.length > 0;
}
</script>

<template>
    <SideBar :title="'Create container'" :opened="openSideBar" @close-sidebar="openSideBar = false">
        <CreateContainer v-on:created-compose="openSideBar = false" />
    </SideBar>
    <div class="sub-view-main" v-if="!loadingStacks">
        <div class="menu-header">
            <div class="left-header">
                <i class="ri-instance-line"></i>
                <h1>Stacks</h1>
            </div>

            <div class="right-header">
                <button class="btn btn-icon" v-on:click="refreshStacks"><i class="ri-refresh-line"></i></button>
                <button class="btn" v-on:click="openSideBar = !openSideBar"><i class="ri-add-line"></i>Create
                    stack</button>
            </div>
        </div>

        <div class="content">
            <Table :fields="fields" :data="stackTable" :loading="loadingStacks" @row-selected="onRowSelected" />
        </div>
    </div>
</template>

<style scoped lang="scss"></style>
