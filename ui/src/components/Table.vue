<script setup lang="ts">
import { TableField } from '@/models/table';

defineProps({
    fields: Array<TableField>,
    data: Array<any>,
    loading: Boolean
})

</script>

<template>
    <div class="table-main">
        <table>
            <tr class="tr-header">
                <th v-for="field in fields" :key="field.key">{{ field.label }}</th>
                <th></th>
            </tr>
            <tr v-for="item in data" :key="item.id">
                <td v-for="field in fields" :key="field.key">{{ item[field.key] }}</td>
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