import axios from "axios";
import store from "../store/store";
import { logout } from "../store/authSlice";

const BASE_URL = "https://localhost:7076/api";

const apiClient = axios.create({
    baseURL: BASE_URL,
    headers: {
        "Content-Type": "application/json;charset=utf-8"
    }
});

apiClient.interceptors.request.use(function (req) {
    const storedUser = localStorage.getItem('user');
    if (storedUser) {
        const user = JSON.parse(storedUser);
        if (user.token) {
            req.headers.Authorization = `Bearer ${user.token}`;
        }
    }
    return req;
});

apiClient.interceptors.response.use(function (res) {
    return res;
}, function(error) {
    if(error.response) {
        if(error.response.status === 401 || error.response.status === 403) {
            store.dispatch(logout());
            window.location.href = '/login'
        }
    }
    return Promise.reject(error);
})

export default apiClient;