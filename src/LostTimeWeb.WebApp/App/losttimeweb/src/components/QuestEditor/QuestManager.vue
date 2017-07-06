<template>
    <div id="QuestManager" class="row backgrey">
        <div class="col-md-5 col-md-offset-2">
            <h1>Gestion des quètes</h1>
            <router-link class="btn btn-primary" to="/quest/pattern" v-if="auth.role == 'ADMIN'">Gestion des Pattern</router-link>
        </div>
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