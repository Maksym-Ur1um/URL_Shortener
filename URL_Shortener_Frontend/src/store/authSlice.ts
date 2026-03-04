import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";
import type { IUser } from "../types/auth.types";


interface AuthState {
  user: IUser | null;
  isAuthenticated: boolean;
}

const storedUser = localStorage.getItem('user');
const parsedUser: IUser | null = storedUser ? JSON.parse(storedUser) : null

const initialState: AuthState = {
    user: parsedUser,
    isAuthenticated: !!parsedUser
};

const authSlice = createSlice({
    name: 'Auth',
    initialState,
    reducers: {
        setCredentials(state, action: PayloadAction<IUser>) {
            state.user = action.payload;
            state.isAuthenticated = true;
            localStorage.setItem('user', JSON.stringify(action.payload));
        },
        logout(state) {
            state.user = null;
            state.isAuthenticated = false;
            localStorage.removeItem('user');
        }
    }
})

export const { setCredentials, logout } = authSlice.actions;

export default authSlice.reducer;