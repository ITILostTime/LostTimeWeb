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
                newsList: [
                    { id: 1, date: '2017-06-06', title:'New de test' , content:'Ad ad et aliquip dolor. Culpa amet proident exercitation labore. Cupidatat laboris aliqua nisi esse laborum nisi ea veniam commodo ea adipisicing. Commodo labore est excepteur sunt ullamco deserunt. Est nisi occaecat ipsum aute dolore sint cupidatat nulla minim voluptate. Cupidatat reprehenderit excepteur eu culpa officia sint aliquip non ipsum exercitation aliquip mollit mollit ullamco.' , authorName:'Quentin' },
                    { id: 2, date: '2017-06-06', title:'New de test2' , content:'Duis excepteur mollit reprehenderit laboris et ut commodo nisi nulla officia veniam. Sint nisi esse aliquip dolor laboris mollit eu Lorem nisi duis eu exercitation. Nisi sint dolore do id non.' , authorName:'Quentin' },
                    { id: 3, date: '2017-06-06', title:'Another New de test2' , content:'Nisi culpa eiusmod deserunt mollit excepteur nisi amet. Amet mollit adipisicing velit aliquip ullamco cillum proident enim fugiat nisi nulla culpa sint tempor. Et duis commodo fugiat excepteur aliquip eu anim eu veniam consectetur magna sit officia. Incididunt sint elit anim commodo eu voluptate sint. Excepteur aliqua ex occaecat qui mollit anim. Ut cupidatat proident incididunt nisi nisi occaecat eiusmod officia.' , authorName:'Quentin' },
                ],
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