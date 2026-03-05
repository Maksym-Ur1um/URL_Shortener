import { useState } from "react";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { loginApi } from "../api/auth.api";
import { setCredentials } from "../store/authSlice";

export default function LoginPage() {
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  const dispatch = useDispatch();
  const navigate = useNavigate();

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    setError("");
    try {
      const response = await loginApi({ userName, password });
      dispatch(setCredentials(response));
      navigate("/");
    } catch {
      setError("Invalid Username or Password");
    }
  }

  return (
    <div className="min-vh-100 d-flex align-items-center justify-content-center pb-5 bg-light">
      <div
        className="card shadow-lg rounded-4 border-0 w-100"
        style={{ maxWidth: "400px" }}
      >
        <div className="card-body p-5">
          <h2 className="fw-bold text-center mb-4">Sign In</h2>

          {error && (
            <div className="alert alert-danger py-2 small text-center mb-4">
              {error}
            </div>
          )}

          <form onSubmit={handleSubmit}>
            <div className="mb-3">
              <label className="form-label small text-secondary fw-semibold">
                Username
              </label>
              <input
                type="text"
                className="form-control form-control-lg bg-light border-0"
                placeholder="Enter your username"
                value={userName}
                onChange={(e) => setUserName(e.target.value)}
                required
              />
            </div>

            <div className="mb-4">
              <label className="form-label small text-secondary fw-semibold">
                Password
              </label>
              <input
                type="password"
                className="form-control form-control-lg bg-light border-0"
                placeholder="Enter your password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required
              />
            </div>
            <button
              type="submit"
              className="btn btn-primary btn-lg w-100 shadow-sm fw-bold mt-2"
            >
              Sign In
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}
