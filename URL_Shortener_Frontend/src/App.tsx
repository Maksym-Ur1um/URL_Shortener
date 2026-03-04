import {Routes, Route} from "react-router-dom"
import LoginPage from "./pages/LoginPage"
import UrlInfoPage from "./pages/UrlInfoPage"
import UrlTablePage from "./pages/UrlTablePage"
import ProtectedRoute from "./components/ProtectedRoute"
import Header from "./components/Header"

function App() {
  return (
    <div>
      <Header />
        <Routes>
          <Route path="/login" element = {<LoginPage />}/>
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
