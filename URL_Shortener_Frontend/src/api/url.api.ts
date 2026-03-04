import apiClient from "./apiClient";
import type { ICreateUrlRequest, IUrlResponse, IUrlDetails, IUrlTableItem } from "../types/url.types";

export const getAllUrls = async function(): Promise<IUrlTableItem[]> {
    const response = await apiClient.get<IUrlTableItem[]>('/url');
    return response.data;
}

export const getUrlInfo = async function(urlId: number): Promise<IUrlDetails> {
    const response = await apiClient.get<IUrlDetails>(`/url/info/${urlId}`);
    return response.data;
}

export const createUrl = async function(data: ICreateUrlRequest): Promise<IUrlResponse> {
    const response = await apiClient.post<IUrlResponse>('url', data);
    return response.data;
}