import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { ThemeProvider } from 'styled-components';
import {theme} from "./utils/theme.ts";
import LoginPage from "./pages/Login/LoginPage.tsx";
import RegisterPage from "./pages/Register/RegisterPage.tsx";
import ManageUsersPage from "./pages/ManageUsersPage.tsx";

function App() {
    return (
        // eslint-disable-next-line @typescript-eslint/ban-ts-comment
        // @ts-ignore
        <ThemeProvider theme={theme}>
            <Router>
                <Routes>
                    <Route path="/login" element={<LoginPage />} />
                    <Route path="/register" element={<RegisterPage />} />
                    <Route path="/manage-users" element={<ManageUsersPage />} />
                </Routes>
            </Router>
        </ThemeProvider>
    );
}

export default App;
