import { useState, useEffect } from "react";
import type { IUrlTableItem } from "../types/url.types";
import { useSelector } from "react-redux";
import type { RootState } from "../store/store";
import { createUrl, deleteUrl, getAllUrls } from "../api/url.api";
import { useNavigate } from "react-router-dom";

export default function UrlTablePage() {
  const [urls, setUrls] = useState<IUrlTableItem[]>([]);
  const [newUrl, setNewUrl] = useState("");
  const [error, setError] = useState("");
  const [refreshTrigger, setRefreshTrigger] = useState(0);

  const isAuthenticated = useSelector(
    (state: RootState) => state.auth.isAuthenticated,
  );
  const user = useSelector((state: RootState) => state.auth.user);
  const navigate = useNavigate();

  useEffect(() => {
    async function fetchUrls() {
      try {
        const response = await getAllUrls();
        setUrls(response || []);
      } catch {
        setError("Server connection Error!");
        setUrls([]);
      }
    }
    fetchUrls();
  }, [refreshTrigger]);

  async function handleAddUrl(e: React.FormEvent) {
    e.preventDefault();
    setError("");
    try {
      await createUrl({ originalUrl: newUrl });
      setNewUrl("");
      setRefreshTrigger((prev) => prev + 1);
    } catch {
      setError("Invalid Url or already exists!");
    }
  }

  async function handleDeleteUrl(id: number) {
    setError("");
    try {
      await deleteUrl(id);
      setRefreshTrigger((prev) => prev + 1);
    } catch {
      setError("You have no permissions to delete this URL!");
    }
  }

  return (
    <div className="min-vh-100 bg-light py-5">
      <div className="container" style={{ maxWidth: "800px" }}>
        <div className="text-center mb-5">
          <h1 className="fw-bold text-dark">URL Shortener</h1>
        </div>

        {error && (
          <div className="alert alert-danger py-2 small text-center mb-4">
            {error}
          </div>
        )}

        {isAuthenticated && (
          <div className="card shadow-lg rounded-4 border-0 mb-5">
            <div className="card-body p-4">
              <h5 className="fw-bold mb-3">Create New Link</h5>
              <form onSubmit={handleAddUrl} className="d-flex gap-2">
                <input
                  type="url"
                  className="form-control form-control-lg bg-light border-0"
                  placeholder="Paste your long URL here..."
                  value={newUrl}
                  onChange={(e) => setNewUrl(e.target.value)}
                  required
                />
                <button
                  type="submit"
                  className="btn btn-primary btn-lg px-4 shadow-sm fw-bold"
                >
                  Shorten
                </button>
              </form>
            </div>
          </div>
        )}

        <div className="d-flex flex-column gap-3">
          {urls &&
            urls.map((url) => {
              const isAdmin = user?.role === "Admin";
              const isOwner = user?.userId === url.creatorId;
              const canDelete = isAdmin || isOwner;

              return (
                <div
                  key={url.id}
                  className="card shadow-sm rounded-4 border-0 hover-shadow transition"
                >
                  <div className="card-body p-4 d-flex justify-content-between align-items-center">
                    <div className="overflow-hidden me-3">
                      <div className="small text-secondary text-truncate mb-1">
                        {url.originalUrl}
                      </div>
                      <div className="fw-bold fs-5">
                        <a
                          href={`https://localhost:7076/api/url/${url.shortUrl}`}
                          target="_blank"
                          rel="noopener noreferrer"
                          className="text-primary text-decoration-none"
                        >
                          /{url.shortUrl}
                        </a>
                      </div>
                    </div>
                    <div className="d-flex gap-2">
                      <button
                        className="btn btn-light text-secondary"
                        onClick={() => navigate(`/url/info/${url.id}`)}
                        title="View details"
                      >
                        Details
                      </button>

                      {canDelete && (
                        <button
                          className="btn btn-outline-danger border-0"
                          onClick={() => handleDeleteUrl(url.id)}
                          title="Delete link"
                        >
                          Delete
                        </button>
                      )}
                    </div>
                  </div>
                </div>
              );
            })}
          {(!urls || urls.length === 0) && (
            <div className="text-center py-5 text-secondary">
              <p>No URLs found</p>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
