import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'

const endpoint = "/api/questpattern";

class QuestPatternApiService {
    constructor() {
    }
    async getQuestPatternListAsync() {
        return await getAsync(endpoint);
    }
    async getQuestPatternAsync(QuestPatternId) {
        return await getAsync(`${endpoint}/${QuestPatternId}`);
    }
    async createQuestPatternAsync(model) {
        return await postAsync(endpoint, model);
    }
    async updateQuestPatternAsync(model) {
        return await putAsync(`${endpoint}/${model.QuestPatternId}`, model);
    }
    async deleteQuestPatternAsync(model) {
        return await postAsync(`${endpoint}/delete`, model);
    }
}

export default new QuestPatternApiService()