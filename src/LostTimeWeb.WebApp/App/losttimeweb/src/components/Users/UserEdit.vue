<template>
   <div id="UserEdit" class="row">
        <div class="col-md-3" id="avatar">
            <img src="../../../dist/img/userSteam.png"/><br/>
            <router-link :to="`password/${this.id}`" class="btn btn-primary">Changer mon mot de passe</router-link>            
            <router-link to="/userdelete/" class="btn btn-danger">Supprimer le compte</router-link>     
        </div>
        <div class="col-md-9">
            <div class="page-header">
                <h1>Editer son profil</h1>
            </div>
            <form @submit="onSubmit($event)">
                <div class="alert alert-danger" v-if="errors.length > 0">
                    <b>Les champs suivants semblent invalides : </b>
                    <ul>
                        <li v-for="e of errors">{{e}}</li>
                    </ul>
                </div>
                <div class="form-group">
                    <label>Pseudo</label>
                    <input type="text" v-model="item.userPseudonym" class="form-control">
                </div>
                <div class="form-group">
                    <label>E-mail</label>
                    <input type="text" v-model="item.userEmail" class="form-control">
                </div>
                <div class="form-group">
                    <label class="required">Veuillez re-saisir votre mot de passe pour confirmer les changements </label>
                    <input type="password" v-model="item.userControlPassword" class="form-control" required>
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
                passwordComfirm: "",
                errors: []  
            }
        },
        async beforeMount() {
            this.id = this.$route.params.id;
            
            try {
                // Here, we use "executeAsyncRequest" action. When an exception is thrown, it is not catched: you have to catch it.
                // It is useful when we have to know if an error occurred, in order to adapt the user experience.
                this.item = await this.executeAsyncRequest(() => UserApiService.getUserAsync(this.id));
            }
            catch(error) {
                //So if an exception occurred, we redirect the user to the students list.
                //this.$router.replace('/user/'+ this.id);
            }
        },

        methods: {

            ...mapActions(['executeAsyncRequest']),

            async onSubmit(e) {
                e.preventDefault();

                var errors = [];

                if(!this.item.userPseudonym) errors.push("Pseudo")
                if(!this.item.userEmail) errors.push("email")
                if(!this.item.userControlPassword) errors.push("mot de passe")

                this.errors = errors;

                if(errors.length == 0) {
                    try {
                        await this.executeAsyncRequest(() => UserApiService.updateUserAsync(this.item));
                        var redirect = '/user/'+ this.id;
                        this.$router.replace(redirect);
                    }
                    catch(error) {
                        console.log(error);
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