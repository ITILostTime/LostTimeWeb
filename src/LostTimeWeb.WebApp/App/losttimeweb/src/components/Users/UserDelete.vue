<template>
    <div>
        <div class="text-center row">
            <div class="page-header">
                <h1>SUPPRESSION DU COMPTE</h1>
            </div>
            <form @submit="onSubmit($event)" class="col-md-4 col-md-offset-4">
                <div class="alert alert-danger" v-if="errors.length > 0">
                    <b>Les champs suivants semblent invalides : </b>
                    <ul>
                        <li v-for="e of errors">{{e}}</li>
                    </ul>
                </div>
                <div class="form-group">
                     <p>Veuillez re-saisir votre mot de passe pour confirmer la suppression de votre compte.</p>
                    <label class="required">Mot de passe</label>
                    <input type="password" v-model="item.userConfirmPassword" class="form-control" required>
                </div>
                <button type="submit" class="btn btn-danger">Supprimer</button>
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
                email: null,
                errors: []  
            }
        },
        /*
        async beforeMount() {

        },
*/
        methods: {
            ...mapActions(['executeAsyncRequest']),

            async onSubmit(e) {
                e.preventDefault();

                var errors = [];
                if(!this.item.userConfirmPassword) errors.push("mot de passe")

                this.item.userID = AuthService.id;
                this.item.userEmail = AuthService.email;
                console.log(this.item);

                this.errors = errors;

                if(errors.length == 0) {
                    try {
                        //delete
                        await this.executeAsyncRequest(() => UserApiService.deleteUserAsync(this.item));
                        //logout
                        this.$router.replace('/logout');
                         //redirecttopage
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
</style>