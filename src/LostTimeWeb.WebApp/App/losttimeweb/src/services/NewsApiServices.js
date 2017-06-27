import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'

const endpoint = "/api/news";

class NewsApiService {
    constructor() {

    }

    async getNewsListAsync() {
        return await getAsync(endpoint);
    }

    async getNewsAsync(articleId) {
        return await getAsync(`${endpoint}/${articleId}`);
    }


    async createNewsAsync(model) {
        console.log("post")
        console.log(model)
        return await postAsync(endpoint, model);
    }

    async updateNewsAsync(model) {
        return await putAsync(`${endpoint}/${model.articleId }`, model);
    }

    async updateNewsVoteAsync(isGoodVote) {
        return await putAsync(`${endpoint}/${model.articleId }`, isGoodVote);
    }


    async deleteNewsAsync(articleId) {
        return await deleteAsync(`${endpoint}/${articleId}`);
    }
}

export default new NewsApiService()