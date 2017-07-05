<template>
   <div id="UserEditPassword" class="row backgrey">
        <div class="col-md-3" id="avatar">
            <img src="../../../dist/img/userSteam.png"/><br/>
            <router-link class="btn btn-primary" :to="`/user/edit/${this.id}`">
                <i class="glyphicon glyphicon-chevron-left"></i> Retour
            </router-link>
        </div>
        <div class="col-md-9">
            <div class="page-header">
                <h1>Editer son mot de passe</h1>
            </div>
            <form @submit="onSubmit($event)">
                <div class="alert alert-danger" v-if="errors.length > 0">
                    <b>Les champs suivants semblent invalides : </b>
                    <ul>
                        <li v-for="e of errors">{{e}}</li>
                    </ul>
                </div>
                <div class="form-group">
                    <label class="required">Nouveau mot de passe</label>
                    <input type="password" v-model="item.userNewPassword" class="form-control" required>
                    <label class="required">Confirmer nouveau mot de passe</label>
                    <input type="password" v-model="item.passwordComfirm" class="form-control" required>
                    <h3>Veuillez re-saisir votre mot de passe pour confirmer les changements  </h3>
                    <label class="required">Ancien mot de passe</label>
                    <input type="password" v-model="item.userOldPassword" class="form-control" required>
                </div>
                <button type="submit" class="btn btn-primary">Sauvegarder</button>
            </form>
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
            this.id = this.$route.params.id;
        },

        methods: {
            ...mapActions(['executeAsyncRequest']),

            async onSubmit(e) {
                e.preventDefault();

                var errors = [];
                if(!this.item.userOldPassword) errors.push("mot de passe")
                if(this.item.userNewPassword != this.item.passwordComfirm)
                {
                    errors.push("les deux mot de passe ne sont pas identiques")
                } 

                this.item.userEmail=AuthService.email;
                this.errors = errors;

                if(errors.length == 0) {
                    try {
                        await this.executeAsyncRequest(() => UserApiService.updateUserPasswordAsync(this.item));
                        var redirect = '/user/'+ this.id;
                        this.$router.replace(redirect);
                    }
                    catch(error) {
                        // Custom error management here.
                        // In our application, errors throwed when executing a request are managed globally via the "executeAsyncRequest" action: errors are added to the 'app.errors' state.
                        // A custom component should react to this state when a new error is added, and make an action, like showing an alert message, or something else.
                        // By the way, you can handle errors manually for each component if you need it...
                    }
                }
            }
        }
    }
</script>

<style lang="less">
#avatar
{
    margin-top:100px;
}
</style>