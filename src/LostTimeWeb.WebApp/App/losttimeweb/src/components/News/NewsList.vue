<template>
    <div id="News" class="row">
        <div class="col-md-5 col-md-offset-3">
            <h1>Articles postés</h1>
            <p>Rappel : tout les articles postés seront publics ! N'hésitez pas à relire les règles de rédaction.</p>
        </div>
        <div class="panel-body text-right">
                <router-link class="btn btn-primary" :to="`news/create`"><i class="glyphicon glyphicon-plus"></i> Rédiger une News</router-link>
            </div>
        <table class="table table-striped">
            <thead>
                <tr>
                <th>#</th>
                <th>Titre</th>
                <th>Auteur</th>
                <th>Posté le</th>
                <th>Popularité</th>
                <th>Actions</th>
                </tr>
            </thead>
            <tbody v-for="(i, index) in newsList" :key="index">
                <tr>
                    <th scope="row">{{i.articleId}}</th>
                    <td>{{i.title}}</td>
                    <td>{{i.authorId}}</td>
                    <td>{{i.datePost | formatDate}}</td>                    
                    <td>{{i.popularity}}</td>
                    <td>
                        <router-link :to="`news/edit/${i.articleId}`"><i class="glyphicon glyphicon-pencil"></i></router-link>
                        <a href="#" @click="deleteNews(i.articleId)"><i class="glyphicon glyphicon-remove"></i></a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>
<script>
    import { mapActions } from 'vuex'
    import NewsApiService from '../../services/NewsApiServices'
    import moment from 'moment'

    export default {
        data () {
            return {
                newsList: [],
                currentPage: 0,
                limitPage: 5
            }
        },
        async mounted() {
            await this.refreshList();
        },
        methods: {
            ...mapActions(['executeAsyncRequestOrDefault', 'executeAsyncRequest']),
            
            async refreshList() {
                this.newsList = await this.executeAsyncRequestOrDefault(() => NewsApiService.getNewsListAsync());
            },
            async deleteNews(newsId) {
                try {
                    await this.executeAsyncRequest(() => NewsApiService.deleteNewsAsync(newsId));
                    await this.refreshList();
                }
                catch(error) {
                }
            },
            
        }
    }
</script>