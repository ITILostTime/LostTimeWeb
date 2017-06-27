<template>
   <div id="usersettings" class="row">
        <div class="col-md-3" id="avatar">
            <img src="../../../dist/img/userSteam.png"/><br/>
            <button type="button" @click="goDelete" class="btn btn-warning">Supprimer le compte</button>
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
    <!--
                <div class="form-group">
                    <label class="required">Date de naissance</label>
                    <input type="date" v-model="item.birthDate" class="form-control" required>
                </div>
    -->
                <div class="form-group">
                    
                    <h3>Editer son mot de passe</h3>
                    <label>Nouveau mot de passe</label>
                    <input type="password" v-model="item.userNewPassword" class="form-control">
                    <label>Confirmer nouveau mot de passe</label>
                    <input type="password" v-model="this.passwordComfirm" class="form-control">

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
                passwordComfirm: "",
                errors: []  
            }
        },
        async beforeMount() {
            this.id = this.$route.params.id;
            
            try {
                /*this.item.pseudo = ""
                this.item.email = AuthService.email
                this.item.oldpassword = ""
                this.item.password = ""
                this.item.passwordComfirm = ""*/
                // Here, we use "executeAsyncRequest" action. When an exception is thrown, it is not catched: you have to catch it.
                // It is useful when we have to know if an error occurred, in order to adapt the user experience.
                this.item = await this.executeAsyncRequest(() => UserApiService.getUserAsync(this.id));
                console.log(this.item)
            }
            catch(error) {
                //So if an exception occurred, we redirect the user to the students list.
                //this.$router.replace('/user/'+ this.id);
            }
        },

        methods: {
            goDelete: function () {
                this.$router.replace('/deleteaccount');
            },

            ...mapActions(['executeAsyncRequest']),

            async onSubmit(e) {
                e.preventDefault();

                var errors = [];

                if(!this.item.userPseudonym) errors.push("Pseudo")
                if(!this.item.userEmail) errors.push("email")
                if(!this.item.userOldPassword) errors.push("mot de passe")
                
                if(this.item.userNewPassword == this.passwordComfirm)
                {
                    errors.push("les deux mot de passe ne sont pas identiques")
                } 
                console.log(this.item);

                this.errors = errors;

                if(errors.length == 0) {
                    try {
                        if(this.mode == 'create') {
                            await this.executeAsyncRequest(() => UserApiService.createUserAsync(this.item));
                        }
                        else {

                            await this.executeAsyncRequest(() => UserApiService.updateUserAsync(this.item));
                        }
                        this.$router.replace('/students');
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