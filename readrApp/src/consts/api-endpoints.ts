//const API_ROOT = '/api/';
const API_ROOT = 'https://localhost:7283/';

const API_ENDPOINTS = {
    AUTH: {
        GET_SECURITY_CODE: API_ROOT + 'Auth/getcode',
        CHECK_SECURITY_CODE: API_ROOT + 'Auth/checkcode',
        SET_USER_NAME: API_ROOT + 'Auth/setname'
    },
    BOOK: {
        MY_BOOKS: API_ROOT + 'Book/mybooks'
    },
    GENRE: {
        GET: API_ROOT + 'Genre',
        PREFERED_GENRES: {
            ADD: API_ROOT + 'Genre/prefered'
        }
    },
    SUGGESTIONS: API_ROOT + 'Suggestions',
    LIKE: API_ROOT + 'Like',
    NOTIFICATION: API_ROOT + 'Notification'
}

export default API_ENDPOINTS;