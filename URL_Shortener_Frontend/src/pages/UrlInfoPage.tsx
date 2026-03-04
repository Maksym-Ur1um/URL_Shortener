import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import type { IUrlDetails } from "../types/url.types";
import { getUrlInfo } from "../api/url.api";

export default function UrlInfoPage() {
  const [urlInfo, setUrlInfo] = useState<IUrlDetails | null>(null);
  const [error, setError] = useState("");

  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  useEffect(() => {
    async function fetchUrlDetails() {
      if (!id) return;
      try {
        const response = await getUrlInfo(Number(id));
        setUrlInfo(response);
      } catch {
        setError("Failed to load URL details or URL not found.");
      }
    }
    fetchUrlDetails();
  }, [id]);

  return (
    <div className="min-vh-100 d-flex align-items-center justify-content-center pb-5 bg-light">
      <div className="card shadow-lg rounded-4 border-0 w-100" style={{ maxWidth: "500px" }}>
        <div className="card-body p-5">
          <h2 className="fw-bold text-center mb-4">URL Details</h2>
          {error && (
            <div className="alert alert-danger py-2 small text-center mb-4">
              {error}
            </div>
          )}
          {urlInfo && (
            <>
              <div className="mb-3">
                <label className="form-label small text-secondary fw-semibold">
                  Original Link
                </label>
                <div className="text-break">
                  <a href={urlInfo.originalUrl} target="_blank" rel="noopener noreferrer">
                    {urlInfo.originalUrl}
                  </a>
                </div>
              </div>

              <div className="mb-3">
                <label className="form-label small text-secondary fw-semibold">
                  Short Link
                </label>
                <div>
                  <a 
                    href={`https://localhost:7076/api/url/${urlInfo.shortUrl}`} 
                    target="_blank" 
                    rel="noopener noreferrer"
                    className="fw-bold"
                  >
                    /{urlInfo.shortUrl}
                  </a>
                </div>
              </div>

              <div className="mb-3">
                <label className="form-label small text-secondary fw-semibold">
                  Created By
                </label>
                <div>{urlInfo.creatorName || "Anonymous"}</div>
              </div>

              <div className="mb-4">
                <label className="form-label small text-secondary fw-semibold">
                  Created At
                </label>
                <div>{new Date(urlInfo.createdAt).toLocaleDateString()}</div>
              </div>

              <button
                onClick={() => navigate("/")}
                className="btn btn-primary btn-lg w-100 shadow-sm fw-bold mt-2"
              >
                Back to List
              </button>
            </>
          )}
        </div>
      </div>
    </div>
  );
}