<template>
    <div>
        <div class="text-center">
            <div class="page-header">
                <h1>SUPPRESSION DU COMPTE</h1>
            </div>
         <button type="button" @click="deleteaccount" class="btn btn-lg btn-block btn-default">Supprimer</button>
        </div>
    </div>
</template>

<script>
    export default {
        methods: {
            deleteaccount: function() {
                alert("nope")
            }
        }
    }
</script>

<style lang="less">
</style>