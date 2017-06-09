<template>
    <div>
        <div class="page-header">
            <h1>Gestion des élèves</h1>
        </div>

        <div class="panel panel-default">
            <div class="panel-body text-right">
                <router-link class="btn btn-primary" :to="`students/create`"><i class="glyphicon glyphicon-plus"></i> Ajouter un élève</router-link>
                
                <div class="form-group">
                    <input type="text" class="form-control" v-model="limitPage" />
                </div>
            </div>
        </div>

        <table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Nom</th>
                    <th>Prénom</th>
                    <th>Date de naissance</th>
                    <th>Login github</th>
                    <th>Options</th>
                </tr>
            </thead>

            <tbody>
                <tr v-if="paginatedList.length == 0">
                    <td colspan="6" class="text-center">Il n'y a actuellement aucun élève.</td>
                </tr>

                <tr v-for="i of paginatedList">
                    <td>{{ i.studentId }}</td>
                    <td>{{ i.lastName }}</td>
                    <td>{{ i.firstName }}</td>
                    <td>{{ i.birthDate }}</td>
                    <td>{{ i.gitHubLogin }}</td>
                    <td>
                        <router-link :to="`students/edit/${i.studentId}`"><i class="glyphicon glyphicon-pencil"></i></router-link>
                        <a href="#" @click="deleteStudent(i.studentId)"><i class="glyphicon glyphicon-remove"></i></a>
                        <a v-if="i.gitHubLogin" href="#" @click="followUser(i)">Follow</a>
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
    import StudentApiService from '../../services/StudentApiService'
    import GitHubApiService from '../../services/GitHubApiService'

    export default {
        data() {
            return {
                studentList: [],
                currentPage: 0,
                limitPage: 5
            }
        },

        async mounted() {
            await this.refreshList();
        },

        computed: {
            numberOfPages() {
                return Math.ceil(this.studentList.length / this.limitPage);
            },

            paginatedList() {
                let begin = this.currentPage == 0 ? 0 : this.currentPage * this.limitPage;
                let end = begin + this.limitPage;

                return this.studentList.slice(begin, end);
            }
        },

        methods: {
            ...mapActions(['executeAsyncRequestOrDefault', 'executeAsyncRequest']),

            goToPage(pageNumber) {
                this.currentPage = pageNumber - 1;
            },

            async refreshList() {
                this.studentList = await this.executeAsyncRequestOrDefault(() => StudentApiService.getStudentListAsync());
            },

            async deleteStudent(studentId) {
                try {
                    await this.executeAsyncRequest(() => StudentApiService.deleteStudentAsync(studentId));
                    await this.refreshList();
                }
                catch(error) {

                }
            },

            async followUser(user) {
                try {
                    await this.executeAsyncRequest(() => GitHubApiService.followUser(user.gitHubLogin));
                    alert("Utilisateur suivi");
                }
                catch(error) {
                    alert("Impossible de suivre l'utilisateur");
                }
            }
        }
    }
</script>

<style lang="less">

</style>