<template>
   <div id="UserDisplay" class="row">
        <div class="col-md-3" id="avatar">
            <img src="../../../dist/img/userSteam.png"/><br/>
            <router-link :to="`user/edit/${this.id}`" class="btn btn-warning">Editer mon compte</router-link>
            <button type="button" @click="goDelete" class="btn btn-warning">Supprimer mon compte</button>
        </div>
        <div class="col-md-9">
            <div class="page-header">
                <h1>Profil personnel</h1>
            </div>
            <dl class="dl-horizontal">
                <dt>pseudo :</dt><dd>{{item.pseudo}}</dd>
                <dt>mail :</dt><dd>{{item.email}}</dd>
                <dt>Inscription :</dt><dd>{{item.AccountCreationDate | formatDate}}</dd>
                <dt>Dernière connexion :</dt><dd>{{item.LastConnectionDate | formatDate}}</dd>
            </dl>
        </div>
    </div>
</template>

<script>
    import { mapActions } from 'vuex'
    import UserApiService from '../../services/UserApiService'
    import AuthService from '../../services/AuthService'

    export default {
        data () {
            return {
                item: {},
                id: null,
                errors: []  
            }
        },
        async beforeMount() {
            this.id = AuthService.Id;//get the ID from the auth data
            try {
                // Here, we use "executeAsyncRequest" action. When an exception is thrown, it is not catched: you have to catch it.
                // It is useful when we have to know if an error occurred, in order to adapt the user experience.
                this.item = await this.executeAsyncRequest(() => UserApiService.getUserAsync(this.id));
            }
            catch(error) {
                // So if an exception occurred, we redirect the user to the students list.
                this.$router.replace('/');
            }
        },
        methods: {
            goDelete: function () {
                this.$router.replace('/deleteaccount');
            },
        }
    }
</script>

<style lang="less">
#avatar
{
    margin-top:100px;
}
</style>