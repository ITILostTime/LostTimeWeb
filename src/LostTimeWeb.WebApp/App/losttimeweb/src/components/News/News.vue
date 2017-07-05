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
.transform-origin (@x:center, @y:center) {
        -webkit-transform-origin: @x @y;
        -moz-transform-origin:    @x @y;
        -ms-transform-origin:     @x @y;
        -o-transform-origin:      @x @y;
    }
.glyphicon-menu-down
{
    color:red;
}
.glyphicon-menu-up
{
    color:green;
}
.news
{
    //background:pink;
    padding-right:0px;
    padding-left:30px;
    position:relative;
}
.news:before
{
    background:blue;
    position:absolute;
    content:" ";
    bottom:-7px;
    left:-8px;
    width:40px;
    height:105%;
    background:url('../../../dist/img/poutrelle.png') top left repeat-y;
}
@media (max-width: 1200px) 
{
    .news:before
    {
        height:150%;
    }
}
.newsArticle a:hover
{
    transform: scale(1.1);
}
.newsArticle h3
{
    //background:url('../../../dist/img/metal-barre.png') top left no-repeat;
    size:1.1em;
    //padding:3px;
    //text-shadow: 1px 1px 0 rgba(140,140,140,0.6) , -1px -1px 1px rgba(0,0,0,0.67) ;
    padding-top:10px;
    //color:lightgrey;
}
.newsArticle
{
    margin:10px 0px 10px 0px;;
    padding:0px 30px 20px 20px;
    min-height:140px;
    //background:url('../../../dist/img/barre_laterale_mini.png') top left repeat-y;
    text-align:justify;
    position:relative;
    background:#544545;
    box-shadow:black 2px 2px 8px;
    border:black ridge 2px;
}
.newsArticle:before
{
    background:url('../../../dist/img/chevron.png') top right no-repeat;
    position:absolute;
    content:" ";
    top:0px;
    right:0px;
    transform:rotate(180deg) translateY(1px) scale(0.9);
    .transform-origin((175px/2)*1.1,(175px/2)*0.9);
    width:175px;
    height:175px;
}
.newsArticle:after
{
    background:url('../../../dist/img/chevron.png') top right no-repeat;
    position:absolute;
    content:" ";
    bottom:0px;
    left:0px;
    transform: translateY(1px) scale(0.9);
    .transform-origin((175px/2)*-0.9,(175px/2)*2.9);
    width:175px;
    height:175px;
}
.newsArticle div,footer
{
    padding:0px 10px 0px 15px;
}
.newsArticle footer
{
    text-align:right;
}
.newsArticle h3 small
{
    //color:darkgrey;
}
</style>