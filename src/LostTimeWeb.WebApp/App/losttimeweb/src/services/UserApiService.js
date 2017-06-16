import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper'

const endpoint = "/api/user";

class UserApiService {
    constructor() {

    }
    async getUserListAsync() {
        return await getAsync(endpoint);
    }

    async getUserAsync(UserId) {
        return await getAsync(`${endpoint}/${UserId}`);
    }

    async createUserAsync(model) {
        return await postAsync(endpoint, model);
    }

    async updateUserAsync(model) {
        return await putAsync(`${endpoint}/${model.UserId}`, model);
    }

    async deleteUserAsync(UserId) {
        return await deleteAsync(`${endpoint}/${UserId}`);
    }
}

export default new UserApiService()