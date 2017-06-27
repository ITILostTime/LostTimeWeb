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
        console.log("put")
        console.log(model)
        return await putAsync(`${endpoint}/${model.articleId }`, model);
    }

    async updateNewsUpVoteAsync(articleId) {
        return await putAsync(`${endpoint}/upvote/${articleId }`);
    }
    async updateNewsDownVoteAsync(articleId) {
        return await putAsync(`${endpoint}/downvote/${articleId }`);
    }


    async deleteNewsAsync(articleId) {
        return await deleteAsync(`${endpoint}/${articleId}`);
    }
}

export default new NewsApiService()