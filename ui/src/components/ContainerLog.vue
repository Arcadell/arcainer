<script setup lang="ts">
const prop = defineProps({
    idContainer: { type: String, required: true },
});
</script>

<template>
    <div class="container-log">
        <h1>{{ prop.idContainer }}</h1>
    </div>
</template>
