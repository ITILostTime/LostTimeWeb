<template>
   <div id="usersettings" class="row">
        <div class="col-md-3" id="avatar">
            <img src="../../../dist/img/userSteam.png"/>
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
                    <label class="required">Pseudo</label>
                    <input type="text" v-model="item.pseudo" class="form-control" required>
                </div>

                <div class="form-group">
                    <label class="required">E-mail</label>
                    <input type="text" v-model="item.email" class="form-control" required>
                </div>
    <!--
                <div class="form-group">
                    <label class="required">Date de naissance</label>
                    <input type="date" v-model="item.birthDate" class="form-control" required>
                </div>
    -->
                <div class="form-group">
                    <p>Editer son mot de passe</p>
                    <label class="required">Ancien mot de passe</label>
                    <input type="password" v-model="item.oldpassword" class="form-control" required>
                    <label class="required">Nouveau mot de passe</label>
                    <input type="password" v-model="item.password" class="form-control" required>
                    <label class="required">comfirmer nouveau mot de passe</label>
                    <input type="password" v-model="item.passwordComfirm" class="form-control" required>
                </div>
                <button type="submit" class="btn btn-primary" disabled>Sauvegarder</button>
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
                this.id = AuthService.email;//get the ID from the auth data
                console.log(this.item.pseudo)
            try {
                /*this.item.pseudo = ""
                this.item.email = AuthService.email
                this.item.oldpassword = ""
                this.item.password = ""
                this.item.passwordComfirm = ""*/
                // Here, we use "executeAsyncRequest" action. When an exception is thrown, it is not catched: you have to catch it.
                // It is useful when we have to know if an error occurred, in order to adapt the user experience.
                this.item = await this.executeAsyncRequest(() => UserApiService.getUserAsync(this.id));
            }
            catch(error) {
                // So if an exception occurred, we redirect the user to the students list.
                this.$router.replace('/usersettings');
            }
        },

        methods: {
            ...mapActions(['executeAsyncRequest']),

            async onSubmit(e) {
                e.preventDefault();

                var errors = [];

                if(!this.item.pseudo) errors.push("Nom")
                if(!this.item.email) errors.push("Prénom")
                if(!this.item.birthDate) errors.push("Date de naissance")

                this.errors = errors;

                if(errors.length == 0) {
                    try {
                        if(this.mode == 'create') {
                            await this.executeAsyncRequest(() => StudentApiService.createStudentAsync(this.item));
                        }
                        else {
                            await this.executeAsyncRequest(() => StudentApiService.updateStudentAsync(this.item));
                        }

                        this.$router.replace('/students');
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