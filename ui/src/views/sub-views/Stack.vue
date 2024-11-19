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
import { useToastStore } from '@/stores/utils';

const containerStore = useContainerStore();
const toastStore = useToastStore();

const fields: TableField[] = [
    { key: 'name', label: 'Name' },
]

let stackTable = ref<TableRow[]>([]);
let selectedStack = ref<Stack>();

let loadingStacks = ref(true);
let loadingSingleStacks = ref(false);
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
    const stack = tableRow.fields as Stack;
    loadingSingleStacks.value = true;
    containerStore.getStacks(stack.name).then(res => {
        if (res.data.length <= 0) {
            toastStore.error({ message: 'No stack found' });
            return;
        }

        selectedStack.value = new Stack();
        selectedStack.value.name = res.data[0].name
        selectedStack.value.dockerCompose = res.data[0].dockerCompose

        loadingSingleStacks.value = false;
    });
    openSideBarEdit.value = true;
}

const onCloseSidebar = (edit: boolean = false) => {
    edit ? openSideBarEdit.value = false: openSideBarCreate.value = false;
    refreshStacks();
}

const deleteStacks = async () => {
    const stacksSelected = stackTable.value.filter(row => row.selected).map(row => row.fields) as Stack[];
    await containerStore.deleteStacks(stacksSelected)
    refreshStacks();
}
</script>

<template>
    <SideBar :title="'Create stack'" :opened="openSideBarCreate" @close-sidebar="onCloseSidebar">
        <CreateContainer v-on:created-compose="onCloseSidebar" />
    </SideBar>

    <SideBar :title="'Edit stack'" :opened="openSideBarEdit" @close-sidebar="onCloseSidebar(true)">
        <template v-if="!loadingSingleStacks">
            <EditStack v-if="selectedStack" :stack="selectedStack" v-on:created-compose="onCloseSidebar(true)" />
        </template>
        <template v-else>
            <h1>Loading</h1>
        </template>
    </SideBar>
    <div class="sub-view-main" v-if="!loadingStacks">
        <div class="menu-header">
            <div class="left-header">
                <i class="ri-instance-line"></i>
                <h1>Stacks</h1>
            </div>

            <div class="right-header">
                <div class="right-header-control" v-if="enableControlButtons">
                    <button class="btn btn-icon" v-on:click="deleteStacks()"><i
                            class="ri-delete-bin-6-line"></i></button>
                </div>

                <button class="btn btn-icon" v-on:click="refreshStacks"><i class="ri-refresh-line"></i></button>
                <button class="btn" v-on:click="openSideBarCreate = !openSideBarCreate"><i
                        class="ri-add-line"></i>Create
                    stack</button>
            </div>
        </div>

        <div class="content">
            <Table :fields="fields" :data="stackTable" :loading="loadingStacks" @row-selected="onRowSelected"
                @row-pressed="onRowPressed" />
        </div>
    </div>
</template>

<style scoped lang="scss"></style>
