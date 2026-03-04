import axios from "axios";

const BASE_URL = "https://localhost:7076/api";

const apiClient = axios.create({
    baseURL: BASE_URL,
    headers: {
        "Content-Type": "application/json;charset=utf-8"
    }
});

apiClient.interceptors.request.use(function (config) {
    const token = localStorage.getItem('token')
    if(token)
        config.headers.Authorization = `Bearer ${token}`;
    return config;
});

export default apiClient;