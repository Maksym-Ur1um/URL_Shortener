export interface IUser {
  userName: string;
  role: string;
  userId: number;
}

export interface IRegisterRequest {
  userName: string;
  password: string;
  confirmPassword: string;
}

export interface ILoginRequest {
  userName: string;
  password: string;
}
