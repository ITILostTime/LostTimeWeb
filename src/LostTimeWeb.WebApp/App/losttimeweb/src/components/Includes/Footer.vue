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
    margin: 0px 0px 0px 0px;//top right bottom left
    padding: 45px 0px 10px 0px;//top right bottom left
    text-align:center;
    position:relative;
    
}
.footer:before
{
    background:blue;
    position:absolute;
    content:" ";
    top:0px;
    width:100%;
    height:40px;
    background:url('../../../dist/img/poutrelle_h.png') top left repeat-x;
}
</style>