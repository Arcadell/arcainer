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
            <tr>
                <th v-for="field in fields" :key="field.key">{{ field.label }}</th>
            </tr>
            <tr v-for="item in data" :key="item.id">
                <td v-for="field in fields" :key="field.key">{{ item[field.key] }}</td>
            </tr>
        </table>
    </div>
</template>

<style lang="scss" scoped>
.table-main {
    display: flex;
}
</style>