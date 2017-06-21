<template>
        <router-link to="/news">Gestion des News</router-link>
</template>
<script>
import AuthService from '../services/AuthService'
import { mapGetters, mapActions } from 'vuex'
import '../directives/requiredProviders'

export default {
    data () {
        return {
            item: {},
            id: null,
            errors: []
        }
    },
    computed: {
        auth: () => AuthService,
    },
    async mounted() {
        this.mode = this.$route.params.mode;
    },
}
</script>