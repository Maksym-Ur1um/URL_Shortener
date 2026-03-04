import type { ReactNode } from "react";
import { useSelector } from "react-redux";
import type { RootState } from "../store/store";
import { Navigate } from "react-router-dom";

interface ProtectedRouteProps {
    children: ReactNode
};

export default function ProtectedRoute({children} : ProtectedRouteProps) {
    const isLogged = useSelector((state : RootState) => state.auth.isAuthenticated);

    if(!isLogged)
        return <Navigate to="/login" replace />
    return children;
}