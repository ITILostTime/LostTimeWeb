<template>
    <div id="NewsEdit" class="row">
        <div class="col-md-5 col-md-offset-2">
            <h1 v-if="mode == 'create'">Rédiger un news</h1>
            <h1 v-else>Editer une news</h1>
        </div>
        <div class="col-md-7 col-md-offset-2">
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
                    <label>Contenu</label>  <!--ADD THE MARKDOWN EDITOR HERE-->
                    <textarea rows="8" cols="50" v-model="item.content" placeholder="Rédiger la news" class="form-control"></textarea>
                </div>
                <button type="submit" class="btn btn-primary">Poster</button>
                <router-link class="btn btn-primary" :to="`/news`"> Annuler </router-link>
                <a class="btn btn-default" href="https://simplemde.com/markdown-guide" target="_blank">Aide Markdown <i class="glyphicon glyphicon-question-sign"></i></a>
            </form>
        </div>
    </div>
</template>
<script>
    import { mapActions } from 'vuex'
    import NewsApiService from '../../services/NewsApiServices'
    import AuthService from '../../services/AuthService'
    
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
                    // So if an exception occurred, we redirect the user to news.
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
                    this.item.authorId = AuthService.id ;
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
                        console.log("error")
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
#NewsEdit textarea
{
    overflow: auto;
    min-height:200px;
}
</style>