import { useState } from "react";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import type { IRegisterRequest } from "../types/auth.types";
import { zodResolver } from "@hookform/resolvers/zod";
import { registerSchema } from "../validations/auth.schemas";
import { registerApi } from "../api/auth.api";
import axios from "axios";

export default function RegisterPage() {
  const [serverErrors, setServerErrors] = useState<string[]>([]);
  const navigate = useNavigate();

  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
  } = useForm<IRegisterRequest>({
    resolver: zodResolver(registerSchema),
  });

  const onSubmit = async (data: IRegisterRequest) => {
    setServerErrors([]);

    try {
      await registerApi(data);
      navigate("/login", {
        state: { successMessage: "Registration successful! Please log in." },
      });
    } catch (error) {
      if (axios.isAxiosError(error) && error.response?.status === 400) {
        const identityErrors = error.response.data.errors;
        if (Array.isArray(identityErrors)) {
          setServerErrors(identityErrors);
        } else {
          setServerErrors(["An unexpected validation error occured"]);
        }
      } else {
        setServerErrors([
          "Server is currently unavailable. Please try again later.",
        ]);
      }
    }
  };
  return (
    <div className="min-vh-100 d-flex align-items-center justify-content-center pb-5 bg-light">
      <div
        className="card shadow-lg rounded-4 border-0 w-100"
        style={{ maxWidth: "400px" }}
      >
        <div className="card-body p-5">
          <h2 className="fw-bold text-center mb-4">Sign Up</h2>

          {serverErrors.length > 0 && (
            <div className="alert alert-danger py-2 small mb-4">
              <ul className="mb-0 ps-3">
                {serverErrors.map((err, index) => (
                  <li key={index} className="fw-semibold">
                    {err}
                  </li>
                ))}
              </ul>
            </div>
          )}

          <form onSubmit={handleSubmit(onSubmit)}>
            <div className="mb-3">
              <label className="form-label small text-secondary fw-semibold">
                Username
              </label>
              <input
                type="text"
                className={`form-control form-control-lg bg-light ${errors.userName ? "is-invalid" : "border-0"}`}
                placeholder="Choose a username"
                {...register("userName")}
              />
              {errors.userName && (
                <div className="invalid-feedback fw-semibold">
                  {errors.userName?.message}
                </div>
              )}
            </div>

            <div className="mb-3">
              <label className="form-label small text-secondary fw-semibold">
                Password
              </label>
              <input
                type="password"
                className={`form-control form-control-lg bg-light ${errors.password ? "is-invalid" : "border-0"}`}
                placeholder="Create a strong password"
                {...register("password")}
              />
              {errors.password && (
                <div className="invalid-feedback fw-semibold">
                  {errors.password?.message}
                </div>
              )}
            </div>

            <div className="mb-4">
              <label className="form-label small text-secondary fw-semibold">
                Confirm Password
              </label>
              <input
                type="password"
                className={`form-control form-control-lg bg-light ${errors.confirmPassword ? "is-invalid" : "border-0"}`}
                placeholder="Repeat your password"
                {...register("confirmPassword")}
              />
              {errors.confirmPassword && (
                <div className="invalid-feedback fw-semibold">
                  {errors.confirmPassword?.message}
                </div>
              )}
            </div>

            <button
              type="submit"
              disabled={isSubmitting}
              className="btn btn-primary btn-lg w-100 shadow-sm fw-bold mt-2"
            >
              {isSubmitting ? "Signing up..." : "Sign Up"}
            </button>
          </form>

          <div className="text-center mt-4 small">
            <span className="text-secondary fw-semibold">
              Already have an account?{" "}
            </span>
            <button
              type="button"
              className="btn btn-link p-0 align-baseline text-primary fw-bold text-decoration-none"
              onClick={() => navigate("/login")}
            >
              Sign In
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
