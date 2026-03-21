import {Routes, Route} from "react-router-dom"
import LoginPage from "./pages/LoginPage"
import UrlInfoPage from "./pages/UrlInfoPage"
import UrlTablePage from "./pages/UrlTablePage"
import ProtectedRoute from "./components/ProtectedRoute"
import Header from "./components/Header"
import { useEffect } from "react"
import { initializeCsrfToken } from "./api/auth.api"
import RegisterPage from "./pages/RegisterPage"

function App() {
  useEffect(() => {
    const setupSecurity = async () => {
      if(!document.cookie.includes("XSRF-TOKEN")) {
        try {
          await initializeCsrfToken();
          console.log("CSRF токен успешно получен при старте!");
        }catch (error) {
          console.error("Ошибка при получении CSRF токена", error);
        }

      }
    }
    setupSecurity();
  }, [])
  
  return (
    <div>
      <Header />
        <Routes>
          <Route path="/login" element = {<LoginPage />}/>
          <Route path ="/register" element = {<RegisterPage/>}/>
          <Route path="/" element = {<UrlTablePage />}/>
          <Route path="/url/info/:id" element = {
            <ProtectedRoute>
              <UrlInfoPage />
            </ProtectedRoute>
          }/>
        </Routes>
    </div>
  )
}

export default App
