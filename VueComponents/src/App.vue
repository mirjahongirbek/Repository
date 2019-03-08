<template>
    <div id="app">
        <server-table 
                      url="/home"
                      serviceName="data"
                      ></server-table>
    </div>
</template>

<script>
    import ServerTable from "./components/table/serverTable.vue"

    export default {
        name: 'app',
        components: {
            Home
        },
        components: {
            ServerTable
        }
    };
</script>

<style>
</style>
