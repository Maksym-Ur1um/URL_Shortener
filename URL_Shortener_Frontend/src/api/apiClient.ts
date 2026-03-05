import axios from "axios";
import store from "../store/store";
import { logout } from "../store/authSlice";

const BASE_URL = "https://localhost:7076/api";

const apiClient = axios.create({
  baseURL: BASE_URL,
  withCredentials: true,
  headers: {
    "Content-Type": "application/json;charset=utf-8",
  },
});

apiClient.interceptors.response.use(
  function (res) {
    return res;
  },
  function (error) {
    if (error.response) {
      if (error.response.status === 401) {
        store.dispatch(logout());
        window.location.href = "/login";
      }
    }
    return Promise.reject(error);
  },
);

export default apiClient;
