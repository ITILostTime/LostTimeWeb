<template>
    <div class="home row">
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
        data () {
            return {
                message: 'I am the home page !'
            }
        }
    }
</script>

<style lang="less">
.home
{
    background-color:pink;
}
</style>