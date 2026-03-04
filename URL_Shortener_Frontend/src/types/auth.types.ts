export interface IUser {
    userName: string,
    role: string,
    token: string
}

export interface ILoginRequest {
    userName: string,
    password: string
}