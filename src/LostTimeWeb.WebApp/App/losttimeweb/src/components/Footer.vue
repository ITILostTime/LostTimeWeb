<template>
    <footer class="row footer">
        <router-link to="/about">A-propos</router-link>
        <router-link to="/about">Credit</router-link>
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
    background:brown;
    //background-image:url('img/wood-diff.jpg');//bug cause : file loader resolving url
}
</style>