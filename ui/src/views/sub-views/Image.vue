<script setup lang="ts">
import Table from '@/components/Table.vue';

import type { Image } from '@/models/data';
import type { TableField, TableRow } from '@/models/table';

import { useImageStore } from '@/stores/data/image';

import { ref } from 'vue';
import { v7 as uuid } from 'uuid'

const imageStore = useImageStore();

const fields: TableField[] = [
    { isId: true, key: 'id', label: 'Id' },
]

let imageTable = ref<TableRow[]>([]);

let loadingImages = ref(true);
let enableControlButtons = ref(false);

const refreshImages = () => {
    imageStore.getImages().then(res => {
        imageTable.value = [];
        res.data.forEach((image: Image) => { imageTable.value.push({ id: uuid(), selected: false, fields: image }); })
        loadingImages.value = false;
    });
}

refreshImages();

const onRowSelected = () => {
    const rowSelected = imageTable.value.filter(row => row.selected);
    enableControlButtons.value = rowSelected.length > 0;
}

</script>

<template>
    <div class="sub-view-main" v-if="!loadingImages">
        <div class="menu-header">
            <div class="left-header">
                <i class="ri-cloud-line"></i>
                <h1>Images</h1>
            </div>

            <div class="right-header">
                <div class="right-header-control" v-if="enableControlButtons">

                </div>

                <button class="btn btn-icon" v-on:click="refreshImages"><i class="ri-refresh-line"></i></button>
            </div>
        </div>

        <div class="content">
            <Table :fields="fields" :data="imageTable" :loading="loadingImages" @row-selected="onRowSelected" />
        </div>
    </div>
</template>

<style scoped lang="scss"></style>
