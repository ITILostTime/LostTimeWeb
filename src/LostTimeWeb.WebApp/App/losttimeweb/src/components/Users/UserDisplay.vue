<template>
   <div id="UserDisplay" class="row backgrey">
        <div class="col-md-3" id="avatar">
            <img src="../../../dist/img/userSteam.png"/><br/>
            <router-link :to="`edit/${this.id}`" class="btn btn-primary" v-if="authId == id">Editer mon compte</router-link>
        </div>
        <div class="col-md-9">
            <div class="page-header">
                <h1>Affichage du profil</h1>
            </div>
            <dl class="dl-horizontal">
                <dt>pseudo :</dt><dd>{{item.userPseudonym}}</dd>
                <dt>mail :</dt><dd>{{item.userEmail}}</dd>
                <dt>Inscription :</dt><dd>{{item.userAccountCreationDate | formatDate}}</dd>
                <dt>Dernière connexion :</dt><dd>{{item.userLastConnectionDate | formatDate}}</dd>
                <dt>Rang :</dt><dd>{{item.userPermission}}</dd>
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
                errors: [],
                authId: 0
            }
        },
        async mounted() {
            this.authId = AuthService.id;

            if(this.$route.params.id != undefined) 
            {
                this.id = this.$route.params.id;
            }
            else
            {
                this.id = auth.id;//get the ID from the auth data
            }
            try {
                // Here, we use "executeAsyncRequest" action. When an exception is thrown, it is not catched: you have to catch it.
                // It is useful when we have to know if an error occurred, in order to adapt the user experience.
                this.item = await this.executeAsyncRequest(() => UserApiService.getUserAsync(this.id));
            }
            catch(error) {
                console.log(error);
                // So if an exception occurred, we redirect the user to the students list.
                //this.$router.replace('/');
            }
            
        },
        methods: {
            ...mapActions(['executeAsyncRequestOrDefault', 'executeAsyncRequest']),
        }
    }
</script>

<style lang="less">
#avatar
{
    margin-top:100px;
}
</style>