<script setup lang="ts">
import Table from '@/components/Table.vue';
import type { Volume } from '@/models/data';
import type { TableField, TableRow } from '@/models/table';
import { useVolumeStore } from '@/stores/data/volume';
import { ref } from 'vue';

const volumeStore = useVolumeStore();

const fields: TableField[] =[
    { key: 'name', label: 'Name' },
]

let volumeTable: TableRow[] = [];

let loadingVolumes = ref(true);
let enableControlButtons = ref(false);

volumeStore.getVolumes().then(res => {
    res.data.forEach((network: Volume) => { volumeTable.push({ selected: false, fields: network }); })
    loadingVolumes.value = false;
});

const onRowSelected = () => {
    const rowSelected = volumeTable.filter(row => row.selected);
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

                <button class="btn btn-icon"><i class="ri-refresh-line"></i></button>
            </div>
        </div>

        <div class="content">
            <Table :fields="fields" :data="volumeTable" :loading="loadingVolumes" @row-selected="onRowSelected" />
        </div>
    </div>
</template>

<style scoped lang="scss"></style>
