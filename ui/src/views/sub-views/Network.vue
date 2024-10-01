<script setup lang="ts">
import Table from '@/components/Table.vue';

import type { Network } from '@/models/data';
import type { TableField, TableRow } from '@/models/table';
import { useNetworkStore } from '@/stores/data/network';
import { ref } from 'vue';

const networkStore = useNetworkStore();

const fields: TableField[] = [
    { key: 'id', label: 'Id' },
    { key: 'name', label: 'Name' },
]

let networkTable: TableRow[] = [];

let loadingNetowrks = ref(true);
let enableControlButtons = ref(false);

networkStore.getNetworks().then(res => {
    res.data.forEach((network: Network) => { networkTable.push({ selected: false, fields: network }); })
    loadingNetowrks.value = false;
});

const onRowSelected = () => {
    const rowSelected = networkTable.filter(row => row.selected);
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

                <button class="btn btn-icon"><i class="ri-refresh-line"></i></button>
            </div>
        </div>

        <div class="content">
            <Table :fields="fields" :data="networkTable" :loading="loadingNetowrks" @row-selected="onRowSelected" />
        </div>
    </div>
</template>

<style scoped lang="scss"></style>
