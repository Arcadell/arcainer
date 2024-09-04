<script setup lang="ts">
import { TableField, TableRow } from '@/models/table';
import { ref } from 'vue';

const props = defineProps({
    fields: Array<TableField>,
    data: Array<TableRow>,
    loading: Boolean
});

const selectAll = ref(false);

const onAllRowsSelected = () => {
    if (!props.data) { return; }
    !selectAll.value ? props.data.forEach((row: TableRow) => { row.selected = true; }) : props.data.forEach((row: TableRow) => { row.selected = false; })
}

</script>

<template>
    <div class="table-main">
        <table>
            <tr class="tr-header">
                <th>
                    <input type="checkbox" v-model="selectAll" @input="onAllRowsSelected">
                </th>
                <th v-for="field in fields" :key="field.key">{{ field.label }}</th>
                <th></th>
            </tr>
            <tr v-for="row in data" :key="row.fields.id">
                <td><input type="checkbox" v-model="row.selected"></td>
                <td v-for="field in fields" :key="field.key">{{ row.fields[field.key] }}</td>
                <td><i class="ri-arrow-right-line"></i></td>
            </tr>
        </table>
    </div>
</template>

<style lang="scss" scoped>
.table-main {
    display: flex;
}
</style>