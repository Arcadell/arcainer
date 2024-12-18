<script setup lang="ts">
import Table from '@/components/Table.vue';

import type { Network } from '@/models/data';
import type { TableField, TableRow } from '@/models/table';

import { useNetworkStore } from '@/stores/data/network';

import { ref } from 'vue';
import { v7 as uuid } from 'uuid'

const networkStore = useNetworkStore();

const fields: TableField[] = [
    { isId: true, key: 'id', label: 'Id' },
    { isId: false, key: 'name', label: 'Name' },
]

let networkTable = ref<TableRow[]>([]);

let loadingNetowrks = ref(true);
let enableControlButtons = ref(false);

const refreshNetworks = () => {
    networkStore.getNetworks().then(res => {
        networkTable.value = [];
        res.data.forEach((network: Network) => { networkTable.value.push({ id: uuid(), selected: false, fields: network }); })
        loadingNetowrks.value = false;
    });
}

refreshNetworks();

const onRowSelected = () => {
    const rowSelected = networkTable.value.filter(row => row.selected);
    enableControlButtons.value = rowSelected.length > 0;
}

const deleteNetworks = async () => {
    const networksSelected = networkTable.value.filter(row => row.selected).map(row => row.fields) as Network[];
    await networkStore.deleteNetworks(networksSelected)
    refreshNetworks();
}
</script>

<template>
    <div class="sub-view-main" v-if="true">
        <div class="menu-header">
            <div class="left-header">
                <i class="ri-global-line"></i>
                <h1>Networks</h1>
            </div>

            <div class="right-header">
                <div class="right-header-control" v-if="enableControlButtons">
                    <button class="btn btn-icon" v-on:click="deleteNetworks()"><i
                            class="ri-delete-bin-6-line"></i></button>
                </div>
                <button class="btn btn-icon" v-on:click="refreshNetworks"><i class="ri-refresh-line"></i></button>
            </div>
        </div>

        <div class="content">
            <Table :fields="fields" :data="networkTable" :loading="loadingNetowrks" @row-selected="onRowSelected" />
        </div>
    </div>
</template>

<style scoped lang="scss"></style>
