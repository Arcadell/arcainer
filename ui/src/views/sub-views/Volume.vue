<script setup lang="ts">
import Table from '@/components/Table.vue';
import type { Volume } from '@/models/data';
import type { TableField, TableRow } from '@/models/table';

import { useVolumeStore } from '@/stores/data/volume';

import { ref } from 'vue';
import { v7 as uuid } from 'uuid'

const volumeStore = useVolumeStore();

const fields: TableField[] = [
    { key: 'name', label: 'Name' },
]

let volumeTable = ref<TableRow[]>([]);

let loadingVolumes = ref(true);
let enableControlButtons = ref(false);

const refreshVolumes = () => {
    volumeStore.getVolumes().then(res => {
        volumeTable.value = [];
        res.data.forEach((network: Volume) => { volumeTable.value.push({ id: uuid(), selected: false, fields: network }); })
        loadingVolumes.value = false;
    });
}

refreshVolumes();

const onRowSelected = () => {
    const rowSelected = volumeTable.value.filter(row => row.selected);
    enableControlButtons.value = rowSelected.length > 0;
}
</script>

<template>
    <div class="sub-view-main" v-if="true">
        <div class="menu-header">
            <div class="left-header">
                <i class="ri-archive-line"></i>
                <h1>Volume</h1>
            </div>

            <div class="right-header">
                <div class="right-header-control" v-if="enableControlButtons">

                </div>

                <button class="btn btn-icon" v-on:click="refreshVolumes"><i class="ri-refresh-line"></i></button>
            </div>
        </div>

        <div class="content">
            <Table :fields="fields" :data="volumeTable" :loading="loadingVolumes" @row-selected="onRowSelected" />
        </div>
    </div>
</template>

<style scoped lang="scss"></style>
