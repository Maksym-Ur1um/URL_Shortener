import apiClient from "./apiClient";
import type {
  ICreateUrlRequest,
  IUrlResponse,
  IUrlDetails,
  IUrlTableItem,
} from "../types/url.types";

export const getAllUrls = async function (): Promise<IUrlTableItem[]> {
  const response = await apiClient.get<IUrlTableItem[]>("/url");
  return response.data;
};

export const getUrlInfo = async function (id: number): Promise<IUrlDetails> {
  const response = await apiClient.get<IUrlDetails>(`/url/info/${id}`);
  return response.data;
};

export const createUrl = async function (
  data: ICreateUrlRequest,
): Promise<IUrlResponse> {
  const response = await apiClient.post<IUrlResponse>("/url", data);
  return response.data;
};

export const deleteUrl = async function (id: number): Promise<void> {
  await apiClient.delete<void>(`/url/${id}`);
};
