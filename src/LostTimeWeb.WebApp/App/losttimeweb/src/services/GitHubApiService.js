import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'

const endpoint = "/api/github";

class GitHubApiService {
    constructor() {

    }

    async getFollowingList() {
        return await getAsync(`${endpoint}/following`);
    }

    async followUser(userName) {
        return await putAsync(`${endpoint}/follow/${userName}`);
    }
}

export default new GitHubApiService()