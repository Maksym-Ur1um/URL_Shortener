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
  if (location.pathname === "/register") return null;

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-primary shadow-sm py-3">
      <div className="container-fluid d-flex align-items-center px-4 px-lg-5">
        
        <div className="d-flex align-items-center gap-4">
          <Link className="navbar-brand fw-bold fs-4 m-0" to="/">
            URL Shortener
          </Link>
          <Link 
            className="text-light text-decoration-none fw-semibold opacity-75" 
            to="/about"
            style={{ transition: "opacity 0.2s" }}
            onMouseEnter={(e) => e.currentTarget.style.opacity = "1"}
            onMouseLeave={(e) => e.currentTarget.style.opacity = "0.75"}
          >
            About
          </Link>
        </div>

        <div className="d-flex ms-auto gap-3">
          {isLogged ? (
            <button
              className="btn btn-danger fw-bold px-4 rounded-3 shadow-sm"
              onClick={handleLogout}
            >
              Logout
            </button>
          ) : (
            <>
              <Link
                className="btn btn-outline-light fw-bold px-4 rounded-3 shadow-sm"
                to="/login"
              >
                Sign In
              </Link>
              <Link
                className="btn btn-light text-primary fw-bold px-4 rounded-3 shadow-sm"
                to="/register"
              >
                Sign Up
              </Link>
            </>
          )}
        </div>
        
      </div>
    </nav>
  );
}
