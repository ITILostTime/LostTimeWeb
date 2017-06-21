<template>
    <div id="News" class="row">
        <div class="col-md-5 col-md-offset-3">
            <h1 v-if="mode == 'create'">Rédiger un news</h1>
            <h1 v-else>Editer une news</h1>
        </div>
        <div class="col-md-9 col-md-offset-3">
            <form @submit="onSubmit($event)">
                <div class="alert alert-danger" v-if="errors.length > 0">
                    <b>Les champs suivants semblent invalides : </b>
                    <ul>
                        <li v-for="e of errors">{{e}}</li>
                    </ul>
                </div>
                <div class="form-group">
                    <label class="required">Titre</label>
                    <input type="text" v-model="item.title" class="form-control" required>
                </div>
                <div class="form-group">
                    <label>Contenu</label> <!--ADD THE MARKDOWN EDITOR HERE-->
                <textarea v-model="item.content" placeholder="Rédiger la news" class="form-control"></textarea>
                </div>
                <button type="submit" class="btn btn-primary">Poster</button>
                <router-link class="btn btn-primary" :to="`/news`"> Annuler</router-link>
            </form>
        </div>
    </div>
</template>
<script>
    import { mapActions } from 'vuex'
    import NewsApiService from '../../services/NewsApiServices'
    
    export default {
        data () {
            return {
                item: {},
                mode: null,
                id: null,
                errors: []
            }
        },
        async mounted() {
            this.mode = this.$route.params.mode;
            this.id = this.$route.params.id;
            
            if(this.mode == 'edit') {
                try {
                    // Here, we use "executeAsyncRequest" action. When an exception is thrown, it is not catched: you have to catch it.
                    // It is useful when we have to know if an error occurred, in order to adapt the user experience.
                    this.item = await this.executeAsyncRequest(() => NewsApiService.getNewsAsync(this.id));
                }
                catch(error) {
                    // So if an exception occurred, we redirect the user to the students list.
                    this.$router.replace('/news');
                }
            }
        },

        methods: {
            ...mapActions(['executeAsyncRequest']),

            async onSubmit(e) {
                e.preventDefault();

                var errors = [];

                if(!this.item.title) errors.push("Titre")
                if(!this.item.content) errors.push("Contenu")

                this.errors = errors;

                if(errors.length == 0) {
                    try {
                        if(this.mode == 'create') {
                            await this.executeAsyncRequest(() => NewsApiService.createNewsAsync(this.item));
                        }
                        else {
                            await this.executeAsyncRequest(() => NewsApiService.updateNewsAsync(this.item));
                        }
                        this.$router.replace('/news');
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