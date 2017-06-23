<template>
    <div id="UserList" class="row">
        <div class="col-md-5 col-md-offset-3">
            <h1>Listes des joueurs</h1>
            <p>Rappel : Cette page est réservé aux admin du site</p>
        </div>
        <div class="col-md-4">
            <h2>Afficher :</h2>
            <div class="form-group">
                <input type="text" class="form-control" v-model="limitPage" />
            </div>
            <router-link class="btn btn-primary" :to="`admin`"><i class="glyphicon glyphicon-chevron-left"></i> Retour</router-link>

        </div>
        <table class="table table-striped">
            <thead>
                <tr>
                <th>#</th>
                <th>Pseudo</th>
                <th>Email</th>
                <th>Inscrit le</th>
                <th>Dernière connexion</th>
                <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-if="paginatedList.length == 0">
                    <td colspan="6" class="text-center">Il n'y a actuellement aucun joueurs...</td>
                </tr>
                <tr v-for="i of paginatedList">
                    <th scope="row">{{i.userId}}</th>
                    <td>{{i.pseudo}}</td>
                    <td>{{i.email}}</td>
                    <td>{{i.AccountCreationDate | formatDate}}</td>                    
                    <td>{{i.LastConnectionDate | formatDate}}</td>                    
                    <td>
                        <router-link :to="`user/edit/${i.userId}`"><i class="glyphicon glyphicon-pencil"></i></router-link>
                        <a href="#" @click="deleteUsers(i.userId)"><i class="glyphicon glyphicon-remove"></i></a>
                    </td>
                </tr>
            </tbody>
        </table>
          <nav aria-label="Page navigation">
            <ul class="pagination">
                <li v-for="i in numberOfPages"><a href="#" @click="goToPage(i)">{{ i }}</a></li>
            </ul>
        </nav>
    </div>
</template>

<script>
    import { mapActions } from 'vuex'
    import UserApiService from '../../services/UserApiService'

    export default {
        data() {
            return {
                userList: [],
                currentPage: 0,
                limitPage: 5
            }
        },

        async mounted() {
            await this.refreshList();
        },

        computed: {
            numberOfPages() {
                return Math.ceil(this.userList.length / this.limitPage);
            },

            paginatedList() {
                let begin = this.currentPage == 0 ? 0 : this.currentPage * this.limitPage;
                let end = begin + this.limitPage;

                return this.userList.slice(begin, end);
            }
        },

        methods: {
            ...mapActions(['executeAsyncRequestOrDefault', 'executeAsyncRequest']),

            goToPage(pageNumber) {
                this.currentPage = pageNumber - 1;
            },

            async refreshList() {
                this.userList = await this.executeAsyncRequestOrDefault(() => userApiService.getUserListAsync());
            },

            async deleteUser(userId) {
                try {
                    await this.executeAsyncRequest(() => userApiService.deleteUserAsync(userId));
                    await this.refreshList();
                }
                catch(error) {

                }
            },
        }
    }
</script>

<style lang="less">

</style>