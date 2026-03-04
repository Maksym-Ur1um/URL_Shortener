import apiClient from "./apiClient";
import type { ILoginRequest, IUser } from "../types/auth.types";

export const login = async function(loginData:ILoginRequest): Promise<IUser> {
    const response = await apiClient.post<IUser>('/Auth/login', loginData)
    return response.data;    
}