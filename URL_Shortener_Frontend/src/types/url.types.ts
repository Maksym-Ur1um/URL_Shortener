export interface ICreateUrlRequest {
    originalUrl: string
}

export interface IUrlResponse {
    originalUrl: string,
    shortUrl: string
}

export interface IUrlTableItem {
    id: number,
    shortUrl: string,
    originalUrl: string,
    creatorId?: number
}

export interface IUrlDetails {
    shortUrl: string,
    originalUrl: string,
    createdAt: string,
    creatorName: string
}