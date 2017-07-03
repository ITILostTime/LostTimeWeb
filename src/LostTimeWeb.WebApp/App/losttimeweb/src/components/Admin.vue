<template>
    <div id="admin" class="row">
        <div class="col-md-5 col-md-offset-2">
            <h1>Page d'administration</h1>
            <router-link class="btn btn-primary" to="/news">Gestion des News</router-link>
            <router-link class="btn btn-primary" to="/users">Gestion des Utilisateurs</router-link>
        </div>
    </div>
</template>
<script>
import AuthService from '../services/AuthService'
import { mapGetters, mapActions } from 'vuex'
import '../directives/requiredProviders'

export default {
    data () {
        return {
        }
    },
    computed: {
        auth: () => AuthService,
    },
}
</script>