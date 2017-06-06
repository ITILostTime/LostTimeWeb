<template>
    <div class="row" id="home">
        <carrousel></carrousel>
        <news></news>
    </div>
</template>

<script>
import Carrousel from './Carrousel.vue'
import News from './News.vue'

    export default {
        components: {
      'carrousel': Carrousel,
      'news': News,
    },
    }
</script>

<style lang="less">
#home
{
    background-color:brown;
}
</style>