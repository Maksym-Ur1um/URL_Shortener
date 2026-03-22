import { useNavigate } from "react-router-dom";

export default function AboutPage() {
  const navigate = useNavigate();

  return (
    <div className="min-vh-100 d-flex align-items-center justify-content-center pb-5 bg-light">
      <div
        className="card shadow-lg rounded-4 border-0 w-100"
        style={{ maxWidth: "500px" }}
      >
        <div className="card-body p-5">
          <h2 className="fw-bold text-center mb-4">About Project</h2>

          <div className="mb-4 text-center">
            <p className="text-secondary mb-3">
              This URL Shortener is a production-ready Full-Stack application built with React and ASP.NET Core 8.
            </p>
            <a
              href="https://github.com/Maksym-Ur1um/URL_Shortener"
              target="_blank"
              rel="noopener noreferrer"
              className="btn btn-dark fw-bold px-4 rounded-3 shadow-sm"
            >
              View on GitHub
            </a>
          </div>

          <hr className="text-muted my-4" />

          <div className="mb-4">
            <h5 className="fw-bold text-center mb-3">Test Accounts</h5>
            <p className="small text-secondary text-center mb-3">
              Use these pre-configured accounts for quick testing of roles and features:
            </p>
            
            <div className="bg-light border rounded-3 p-3">
              <ul className="list-unstyled mb-0 small fs-6">
                <li className="d-flex justify-content-between border-bottom pb-2 mb-2">
                  <span className="fw-bold text-danger">Admin</span>
                  <span className="font-monospace text-secondary">Admin123</span>
                </li>
                <li className="d-flex justify-content-between border-bottom pb-2 mb-2">
                  <span className="fw-bold text-primary">Ivan</span>
                  <span className="font-monospace text-secondary">Qwerty123</span>
                </li>
                <li className="d-flex justify-content-between">
                  <span className="fw-bold text-primary">Maksym</span>
                  <span className="font-monospace text-secondary">Qwerty123</span>
                </li>
              </ul>
            </div>
          </div>

          <button
            onClick={() => navigate("/")}
            className="btn btn-primary btn-lg w-100 shadow-sm fw-bold mt-2"
          >
            Back to Home
          </button>
        </div>
      </div>
    </div>
  );
}