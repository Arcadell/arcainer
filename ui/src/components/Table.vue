<script setup lang="ts">
import { TableField, TableRow } from '@/models/table';
import { ref } from 'vue';

const props = defineProps({
    fields: { type: Array<TableField>, required: true },
    data: { type: Array<TableRow>, required: true },
    loading: { type: Boolean, default: false }
});
const emit = defineEmits(['row-selected']);

const selectAll = ref(false);

const onAllRowsSelected = () => {
    if (!props.data) return;

    selectAll.value = !selectAll.value;
    props.data.forEach((row: TableRow) => {
        row.selected = selectAll.value;
    })
}

const onRowSelected = () => {
    emit('row-selected');
}
</script>

<template>
    <div class="table-main">
        <table>
            <tbody>
                <tr class="tr-header">
                    <th>
                        <input type="checkbox" v-model="selectAll" @input="onAllRowsSelected"
                            v-on:change="onRowSelected">
                    </th>
                    <th v-for="field in fields" :key="field.key">{{ field.label }}</th>
                    <th></th>
                </tr>
                <tr v-for="row in data" :key="row.fields.id">
                    <td><input type="checkbox" v-model="row.selected" v-on:change="onRowSelected"></td>
                    <td v-for="field in fields" :key="field.key">{{ row.fields[field.key] }}</td>
                    <td><i class="ri-arrow-right-line"></i></td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<style lang="scss" scoped>
.table-main {
    display: flex;
}
</style>