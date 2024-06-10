import React, { useState } from "react";
import {
  BrowserRouter as Router,
  Route,
  Routes,
  Navigate,
} from "react-router-dom";

import AuthOutlet from "@auth-kit/react-router/AuthOutlet";
import useAuthHeader from "react-auth-kit/hooks/useAuthHeader";

import LoginScreen from "./pages/LoginScreen/LoginScreen";
import AccountPage from "./pages/Account/AccountPage";
import axios, { AxiosError } from "axios";
import useSignOut from "react-auth-kit/hooks/useSignOut";
import useIsAuthenticated from 'react-auth-kit/hooks/useIsAuthenticated'
import AnyPage from "./pages/anypage";
import LogboekScreen from "./pages/Logboek/logboekScreen";
import Register from "./pages/Register/RegisterPage";
import VerificationScreen from "./pages/Verification/VerificationScreen"; import Home from './pages/homedashboard';
import WaterLevelSettings from "./pages/WaterlevelSettings/WaterLevelSettings";

function App() {
  const authHeader = useAuthHeader();
  const authState = useIsAuthenticated();
  const SignOut = useSignOut();

  const [welcomeState, setWelcomeState] = useState(false);

  async function isTokenValid() {
    if (!authState) return;
    try {
      await axios.get(
        process.env.REACT_APP_API_URL + "/api/users",
        {
          headers: {
            Authorization: authHeader,
          },
        }
      );
    } catch (error) {
      const axiosError = error as AxiosError;
      if (axiosError.response?.status === 401 || axiosError.response?.status === 404) {
        // If the token is invalid, sign out the user and redirect to the login page
        SignOut();
        // window.location.href = "/login";
        return null;
      }
    }
  }
  isTokenValid();
  return (
    <Router>
      <Routes>
        <Route path="/register" Component={Register} />
        <Route path="/login" Component={LoginScreen} />
        <Route path="/verify/:userId/:token" Component={VerificationScreen} />
        <Route element={<AuthOutlet fallbackPath="/login" />}>
          <Route path="/account" element={<AccountPage />} />
          <Route path="/waterlevelsettings" element={<WaterLevelSettings />} />
          <Route path="/home" element={<Home hasWelcomeBeenShown={welcomeState} setWelcomeState={setWelcomeState} />} />
          <Route path="/*" element={<Navigate to="/home" />} />
          <Route path="/logboek" element={<LogboekScreen />} />
        </Route>
        <Route path="/*" element={<Navigate to="/" />} />
      </Routes>
    </Router>
  );
}

export default App;
