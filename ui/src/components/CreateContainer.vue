<script setup lang="ts"></script>

<template>
    <div class="main-container-create">
        <div class="generate-docker-compose">
            <input type="text" placeholder="Docker run command">
            <button class="btn btn-outline">Convert</button>
        </div>
    </div>
</template>

<style lang="scss">
.main-container-create {
    display: flex;
    flex-direction: column;

    gap: 0.5em;

    .generate-docker-compose {
        display: flex;

        align-items: center;
        gap: 5px;

        input {
            width: 100%;
        }
    }
}
</style>