import './index.css';
import './App.css';
import { Fragment } from "react";
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import LoginPage from "./pages/Login/LoginPage.tsx";
import RegisterPage from "./pages/Register/RegisterPage.tsx";

function App() {
    return (
        <Fragment>
            <Router>
                <Routes>
                    <Route path="/login" element={<LoginPage />} />
                    <Route path="/register" element={<RegisterPage />} />
                </Routes>
            </Router>
        </Fragment>
    );
}

export default App;
