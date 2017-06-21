<template>
        <table class="table">
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
                    <th scope="row">{{i.id}}</th>
                    <td>{{i.title}}</td>
                    <td>{{i.authorId}}</td>
                    <td>{{i.popularity}}</td>
                    <td> {{i.datePost | formatDate}}</td>
                    <td>
                        <router-link :to="`news/edit/${i.id}`"><i class="glyphicon glyphicon-pencil"></i></router-link>
                        <a href="#" @click="deleteNews(i.id)"><i class="glyphicon glyphicon-remove"></i></a>
                    </td>
                </tr>
            </tbody>
        </table>
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