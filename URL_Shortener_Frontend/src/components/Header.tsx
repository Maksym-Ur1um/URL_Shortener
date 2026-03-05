import { useLocation, useNavigate, Link } from "react-router-dom";
import { useSelector, useDispatch } from "react-redux";
import { logout } from "../store/authSlice";
import type { RootState } from "../store/store";
import { logoutApi } from "../api/auth.api";

export default function Header() {
  const isLogged = useSelector(
    (state: RootState) => state.auth.isAuthenticated,
  );

  const dispatch = useDispatch();
  const navigate = useNavigate();

  async function handleLogout() {
    try {
      await logoutApi();
    } catch (error) {
      console.error("Logout failed on server", error);
    } finally {
      dispatch(logout());
      navigate("/login");
    }
  }

  const location = useLocation();

  if (location.pathname === "/login") return null;

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-primary shadow-sm py-3">
      <div className="container" style={{ maxWidth: "800px" }}>
        <Link className="navbar-brand fw-bold fs-4" to="/">
          URL Shortener
        </Link>
        <div className="d-flex ms-auto gap-3">
          <a
            href="https://localhost:7076/about"
            className="btn btn-outline-light fw-bold px-3 rounded-3 shadow-sm d-flex align-items-center"
          >
            About
          </a>

          {isLogged ? (
            <button
              className="btn btn-danger fw-bold px-4 rounded-3 shadow-sm"
              onClick={handleLogout}
            >
              Logout
            </button>
          ) : (
            <Link
              className="btn btn-light text-primary fw-bold px-4 rounded-3 shadow-sm"
              to="/login"
            >
              Sign In
            </Link>
          )}
        </div>
      </div>
    </nav>
  );
}
