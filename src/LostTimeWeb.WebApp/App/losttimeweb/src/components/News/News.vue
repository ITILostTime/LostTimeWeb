<template>
    <section class="news col-md-6">
        <div v-for="(i, index) in newsList" :key="index">
            <article>
                <h3>{{i.title}} <small>{{i.date}} par {{i.authorName}}</small></h3>
                <p>{{i.content}}</p>
            </article>
        </div>
    </section>
</template>

<script>
    import { mapActions } from 'vuex'
    import NewsApiService from '../../services/NewsApiServices'
    
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
            }
        }
    }
</script>