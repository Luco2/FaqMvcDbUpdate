import axios from 'axios';

const apiClient = axios.create({
    baseURL: 'https://localhost:7296', // Your ASP.NET Core backend URL
    withCredentials: false,
    headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
    },
});

export default {
    getItems() {
        return apiClient.get('/api/items');
    },
    // More API calls...
};
