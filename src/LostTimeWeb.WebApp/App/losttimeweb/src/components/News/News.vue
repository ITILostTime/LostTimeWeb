<template>
    <section class="news col-md-6">
        <div v-for="(i, index) in newsList" :key="index">
            <article class="newsArticle">
                <h3>{{i.title}}<small> par {{i.authorId}}</small></h3>
                <vue-markdown>{{i.content}}</vue-markdown>
                <footer> 
                    <span> {{i.upVote}} <a href="#" @click="NewsUpVote(i.articleId)"><i class="glyphicon glyphicon-menu-up"></i></a></span>
                    <span> {{i.downVote}} <a href="#" @click="NewsDownVote(i.articleId)"><i class="glyphicon glyphicon-menu-down"></i></a></span>  
                    - Posté le {{i.datePost | formatDate}}
                </footer>
            </article>
        </div>
    </section>
</template>
<script>
    import { mapActions } from 'vuex'
    import NewsApiService from '../../services/NewsApiServices'
    import moment from 'moment'
    import VueMarkdown from 'vue-markdown'
    export default {
        data () {
            return {
                newsList: [],
                currentPage: 0,
                limitPage: 5
            }
        },
        components: {
            VueMarkdown
        },
        async mounted() {
            await this.refreshList();
        },
        
        methods: {
            ...mapActions(['executeAsyncRequestOrDefault', 'executeAsyncRequest']),
            
            async refreshList() {
                this.newsList = await this.executeAsyncRequestOrDefault(() => NewsApiService.getNewsListAsync());
            },
            async NewsUpVote(newsId) {
                try {
                    await this.executeAsyncRequest(() => NewsApiService.updateNewsUpVoteAsync(newsId));
                    await this.refreshList();
                }
                catch(error) {
                }
            },
            async NewsDownVote(newsId) {
                try {
                    await this.executeAsyncRequest(() => NewsApiService.updateNewsDownVoteAsync(newsId));
                    await this.refreshList();
                }
                catch(error) {
                }
            },
        }
    }
</script>
<style lang="less">

.glyphicon-menu-down
{
    color:red;
}
.glyphicon-menu-up
{
    color:green;
}
.newsArticle a:hover
{
    transform: scale(1.1);
}
</style>