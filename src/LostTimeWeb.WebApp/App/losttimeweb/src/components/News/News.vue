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
                    - Edité {{i.editions}} fois
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
.newsArticle h3
{
    //background:url('../../../dist/img/metal-barre.png') top left no-repeat;
    size:1.1em;
    padding:3px;
    //text-shadow: 1px 1px 0 rgba(140,140,140,0.6) , -1px -1px 1px rgba(0,0,0,0.67) ;
    margin-top:0px;
    padding-left:10px;
    //color:lightgrey;
}
.newsArticle
{
    padding:0px 0px 20px 16px;
    //background:rgba(165, 42, 42, 0.5) url('../../../dist/img/barre_laterale_mini.png') top left repeat-y;
    text-align:justify;
}
.newsArticle div,footer
{
    padding:0px 10px 0px 15px;
}
.newsArticle h3 small
{
    //color:darkgrey;
}
</style>