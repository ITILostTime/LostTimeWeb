<template>
    <footer class="row footer">
        <router-link to="/about">A-propos de</router-link>
        <!--<router-link to="/about">Credit</router-link>-->
        - 2017
    </footer>
</template>
<script>
    export default {
        name: 'footer',
    }
</script>
<style lang="less">
.footer
{
    //width:90%;
    margin: 10px 0px 0px 0px;//top right bottom left
    padding :10px;
    text-align:center;
    background:brown;
}
</style>