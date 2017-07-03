<template>
    <div id="QuestList" class="row">
        <div class="col-md-5 col-md-offset-2">
            <h1>Quêtes Disponibles</h1>
            <p>Choissisez d'éditer ou d'ajouter une quète à votre jeu.</p>
        </div>
        <div class="panel-body text-right">
            <router-link class="btn btn-primary" :to="`quest/create`"><i class="glyphicon glyphicon-plus"></i> Créer une quète</router-link>
            <button type="button" @click="showSelf" class="btn btn-lg btn-block btn-default">Afficher mes créations</button>
        </div>
        <table class="table table-striped">
            <thead>
                <tr>
                <th>#</th>
                <th>Titre</th>
                <th>Auteur</th>
                <th>Dernière edition</th>
                <th>Actions</th>
                </tr>
            </thead>
            <tbody v-for="(i, index) in questsList" :key="index">
                <tr>
                    <th scope="row">{{i.questID }}</th>
                    <td>{{i.questTitle }}</td>
                    <td>{{i.questAuthorID }}</td>
                    <td>{{i.questLastEdit  | formatDate}}</td>
                    <td>
                        <router-link :to="`quest/edit/${i.questID}`"><i class="glyphicon glyphicon-pencil"></i></router-link>
                         | 
                        <a href="#" @click="deleteQuest(i.questID)"><i class="glyphicon glyphicon-remove"></i></a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>
<script>
    import { mapActions } from 'vuex'
    import QuestApiService from '../../services/QuestApiService'
    import moment from 'moment'
    export default {
        data () {
            return {
                questsList: [],
                currentPage: 0,
                limitPage: 5
            }
        },
        async mounted() {
            await this.refreshList();
        },
        methods: {
            showSelf: function() {
                alert("nope")
            },
            ...mapActions(['executeAsyncRequestOrDefault', 'executeAsyncRequest']),
            async refreshList() {
                this.questsList = await this.executeAsyncRequestOrDefault(() => QuestApiService.getQuestListAsync());
            },
            async deleteQuest(questID) {
                try {
                    await this.executeAsyncRequest(() => QuestApiService.deleteQuestAsync(questID));
                    await this.refreshList();
                }
                catch(error) {
                }
            },
            
        }
    }
</script>