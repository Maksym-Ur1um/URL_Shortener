import apiClient from "./apiClient";
import type { ILoginRequest, IRegisterRequest, IUser } from "../types/auth.types";

export const registerApi = async function(registerData: IRegisterRequest): Promise<void> {
  await apiClient.post("/Auth/register", registerData)
}

export const loginApi = async function (
  loginData: ILoginRequest,
): Promise<IUser> {
  const response = await apiClient.post<IUser>("/Auth/login", loginData);
  await initializeCsrfToken();
  return response.data;
};
export const logoutApi = async function (): Promise<void> {
  await apiClient.post("/Auth/logout");
  await initializeCsrfToken();
};

export const initializeCsrfToken = async (): Promise<void> => {
  await apiClient.get("/Auth/csrf-token");
};