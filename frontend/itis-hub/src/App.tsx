import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { ThemeProvider } from 'styled-components';
import {theme} from "./utils/theme.ts";
import LoginPage from "./pages/Login/LoginPage.tsx";
import RegisterPage from "./pages/Register/RegisterPage.tsx";
import ManageUsersPage from "./pages/ManageUsersPage.tsx";
import NotFoundPage from "./pages/ErrorPages/NotFoundPage.tsx";
import ProfilePage from "./pages/UserProfile/ProfilePage.tsx";

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
                    {/* Маршрут для несуществующих страниц */}
                    <Route path="*" element={<NotFoundPage />} />
                    <Route path="/profile/:username" element={<ProfilePage />} />
                </Routes>
            </Router>
        </ThemeProvider>
    );
}

export default App;
