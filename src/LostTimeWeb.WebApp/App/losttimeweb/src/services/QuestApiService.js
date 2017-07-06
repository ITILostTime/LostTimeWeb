import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'

const endpoint = "/api/quest";

class QuestApiService {
    constructor() {
    }
    async getQuestListAsync() {
        return await getAsync(endpoint);
    }
    async getQuestListByAuthorAsync(AuthorId) {
        return await getAsync(`${endpoint}/${AuthorId}`);
    }
    async getQuestAsync(QuestId) {
        return await getAsync(`${endpoint}/${QuestId}`);
    }
    async createQuestAsync(model) {
        return await postAsync(endpoint, model);
    }
    async updateQuestAsync(model) {
        return await putAsync(`${endpoint}/${model.questId}`, model);
    }
    async deleteQuestAsync(model) {
        return await postAsync(`${endpoint}/delete`, model);
    }
}

export default new QuestApiService()