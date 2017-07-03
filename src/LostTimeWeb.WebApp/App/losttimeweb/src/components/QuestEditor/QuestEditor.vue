<template>
    <div id="QuestEditor" class="row">
        
        <questlist></questlist>
    </div>
</template>

<script>
import AuthService from '../../services/AuthService'
import QuestList from './QuestList.vue'

export default {
    data () {
        return {

        }
    },
    components: {
      'questlist': QuestList,
    },
    computed: {
        auth: () => AuthService,
        
    },
}
</script>
<style>
#QuestEditor div
{
    border: black solid 1px;
}
</style>