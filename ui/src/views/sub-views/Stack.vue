<script setup lang="ts">
import Table from '@/components/Table.vue';
import SideBar from '@/components/SideBar.vue';
import CreateContainer from '@/components/CreateStack.vue';
import EditStack from '@/components/EditStack.vue';

import { Stack, } from '@/models/data';
import type { TableField, TableRow } from '@/models/table';

import { useContainerStore } from '@/stores/data/container';

import { ref } from 'vue';
import { v7 as uuid } from 'uuid'

const containerStore = useContainerStore();

const fields: TableField[] = [
    { key: 'name', label: 'Name' },
]

let stackTable = ref<TableRow[]>([]);
let selectedStack = ref<Stack>();

let loadingStacks = ref(true);
let enableControlButtons = ref(false);
let openSideBarCreate = ref(false);
let openSideBarEdit = ref(false);

const refreshStacks = () => {
    containerStore.getStacks().then(res => {
        stackTable.value = [];
        res.data.forEach((stack: Stack) => { stackTable.value.push({ id: uuid(), selected: false, fields: stack }); });
        loadingStacks.value = false;
    });
}

refreshStacks();

const onRowSelected = () => {
    const rowSelected = stackTable.value.filter(row => row.selected);
    enableControlButtons.value = rowSelected.length > 0;
}

const onRowPressed = (tableRow: TableRow) => {
    console.log(tableRow);
    selectedStack = tableRow.fields;
    openSideBarEdit.value = true;
}
</script>

<template>
    <SideBar :title="'Create stack'" :opened="openSideBarCreate" @close-sidebar="openSideBarCreate = false">
        <CreateContainer v-on:created-compose="openSideBarCreate = false" />
    </SideBar>

    <SideBar :title="'Edit stack'" :opened="openSideBarEdit" @close-sidebar="openSideBarEdit = false">
        <EditStack v-if="selectedStack" :stack="selectedStack" v-on:created-compose="openSideBarEdit = false" />
    </SideBar>
    <div class="sub-view-main" v-if="!loadingStacks">
        <div class="menu-header">
            <div class="left-header">
                <i class="ri-instance-line"></i>
                <h1>Stacks</h1>
            </div>

            <div class="right-header">
                <button class="btn btn-icon" v-on:click="refreshStacks"><i class="ri-refresh-line"></i></button>
                <button class="btn" v-on:click="openSideBarCreate = !openSideBarCreate"><i class="ri-add-line"></i>Create
                    stack</button>
            </div>
        </div>

        <div class="content">
            <Table :fields="fields" :data="stackTable" :loading="loadingStacks" @row-selected="onRowSelected" @row-pressed="onRowPressed" />
        </div>
    </div>
</template>

<style scoped lang="scss"></style>
