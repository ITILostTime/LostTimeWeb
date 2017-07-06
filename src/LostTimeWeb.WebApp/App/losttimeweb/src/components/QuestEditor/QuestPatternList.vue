<template>
    <div id="QuestPatternList" class="row backgrey">
        <div class="col-md-5 col-md-offset-2">
            <h1>Pattern Quêtes</h1>
            <p>Gestion des patterns de quètes utilisables.</p>
        </div>
        <div class="panel-body text-right">
            <router-link class="btn btn-primary" :to="`pattern/create`"><i class="glyphicon glyphicon-plus"></i> Créer un pattern</router-link>
            <router-link class="btn btn-primary" :to="`/quest`"><i class="glyphicon glyphicon-chevron-left"></i> Retour</router-link>
        </div>
        <table class="table table-striped">
            <thead>
                <tr>
                <th>#</th>
                <th>Titre</th>
                </tr>
            </thead>
            <tbody v-for="(i, index) in questPatternsList" :key="index">
                <tr>
                    <th scope="row">{{i.questPatternID  }}</th>
                    <td>{{i.questPatternTitle  }}</td>
                    <td>
                        <router-link :to="`quest/pattern/edit/${i.questPatternID}`"><i class="glyphicon glyphicon-pencil"></i></router-link>
                         | 
                        <a href="#" @click="deleteQuestPattern(i.questPatternID)"><i class="glyphicon glyphicon-remove"></i></a>
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
                questPatternsList: [],
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
                this.questPatternList = await this.executeAsyncRequestOrDefault(() => QuestApiService.getQuestPatternListAsync());
            },
            async deleteQuest(questID) {
                try {
                    await this.executeAsyncRequest(() => QuestApiService.deleteQuestPatternAsync(questID));
                    await this.refreshList();
                }
                catch(error) {
                }
            },
            
        }
    }
</script>