export interface IUser {
    userName: string,
    role: string,
    token: string,
    userId: number
}

export interface ILoginRequest {
    userName: string,
    password: string
}