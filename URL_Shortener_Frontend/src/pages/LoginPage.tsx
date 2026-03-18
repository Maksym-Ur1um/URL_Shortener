import { useState } from "react";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { loginApi } from "../api/auth.api";
import type { ILoginRequest } from "../types/auth.types";
import { setCredentials } from "../store/authSlice";
import { z } from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";

const loginSchema = z.object({
  userName: z.string().min(4, "Username should be at least 4 characters"),
  password: z.string().min(6, "Password should be at least 6 characters"),
});

export default function LoginPage() {
  const [globalError, setGlobalError] = useState("");
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<ILoginRequest>({
    resolver: zodResolver(loginSchema),
  });

  const onSubmit = async (data: ILoginRequest) => {
    setGlobalError("");
    try {
      const response = await loginApi(data);
      dispatch(setCredentials(response));
      navigate("/");
    } catch {
      setGlobalError("Invalid Username or Password");
    }
  };

  return (
    <div className="min-vh-100 d-flex align-items-center justify-content-center pb-5 bg-light">
      <div
        className="card shadow-lg rounded-4 border-0 w-100"
        style={{ maxWidth: "400px" }}
      >
        <div className="card-body p-5">
          <h2 className="fw-bold text-center mb-4">Sign In</h2>

          {globalError && (
            <div className="alert alert-danger py-2 small text-center mb-4">
              {globalError}
            </div>
          )}

          <form onSubmit={handleSubmit(onSubmit)}>
            
            <div className="mb-3">
              <label className="form-label small text-secondary fw-semibold">
                Username
              </label>
              <input
                type="text"
                className={`form-control form-control-lg bg-light ${errors.userName ? 'is-invalid' : 'border-0'}`}
                placeholder="Enter your username"
                {...register("userName")}
              />
              {errors.userName && (
                <div className="invalid-feedback fw-semibold">
                  {errors.userName?.message}
                </div>
              )}
            </div>

            <div className="mb-4">
              <label className="form-label small text-secondary fw-semibold">
                Password
              </label>
              <input
                type="password"
                className={`form-control form-control-lg bg-light ${errors.password ? 'is-invalid' : 'border-0'}`}
                placeholder="Enter your password"
                {...register("password")}
              />
              {errors.password && (
                <div className="invalid-feedback fw-semibold">
                  {errors.password?.message}
                </div>
              )}
            </div>

            <button
              type="submit"
              disabled={isSubmitting}
              className="btn btn-primary btn-lg w-100 shadow-sm fw-bold mt-2"
            >
              {isSubmitting ? "Signing in..." : "Sign In"}
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}