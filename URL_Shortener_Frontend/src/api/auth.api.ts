import apiClient from "./apiClient";
import type { ILoginRequest, IUser } from "../types/auth.types";

export const loginApi = async function(loginData:ILoginRequest): Promise<IUser> {
    const response = await apiClient.post<IUser>('/Auth/login', loginData)
    return response.data;    
}
export const logoutApi = async function(): Promise<void> {
    await apiClient.post('/Auth/logout');
}