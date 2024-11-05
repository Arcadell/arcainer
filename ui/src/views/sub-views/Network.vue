<script setup lang="ts">
import Table from '@/components/Table.vue';

import type { Network } from '@/models/data';
import type { TableField, TableRow } from '@/models/table';

import { useNetworkStore } from '@/stores/data/network';

import { ref } from 'vue';
import { v7 as uuid } from 'uuid'

const networkStore = useNetworkStore();

const fields: TableField[] = [
    { key: 'id', label: 'Id' },
    { key: 'name', label: 'Name' },
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
