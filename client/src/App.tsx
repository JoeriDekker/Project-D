import React from "react";
import {
  BrowserRouter as Router,
  Route,
  Routes,
  Navigate,
} from "react-router-dom";

import AuthOutlet from "@auth-kit/react-router/AuthOutlet";
import useAuthHeader from "react-auth-kit/hooks/useAuthHeader";

import Dashboard from "./pages/Dashboard/Dashboard";
import LoginScreen from "./pages/LoginScreen/LoginScreen";
import AccountPage from "./pages/Account/AccountPage";
import axios, { AxiosError } from "axios";
import useSignOut from "react-auth-kit/hooks/useSignOut";
import useIsAuthenticated from "react-auth-kit/hooks/useIsAuthenticated";
import AnyPage from "./pages/anypage";
import LogboekScreen from "./pages/Logboek/logboekScreen";
import Home from './pages/homedashboard';

function App() {
  const authHeader = useAuthHeader();
  const authState = useIsAuthenticated();
  const SignOut = useSignOut();
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
        window.location.href = "/login";
        return null;
      }
    }
  }
  isTokenValid();
  return (
    <Router>
      <Routes>
        <Route path="/login" Component={LoginScreen} />
        <Route element={<AuthOutlet fallbackPath="/login" />}>
          <Route path="/account" element={<AccountPage />} />
          <Route path="/" element={<AnyPage />} />
          <Route path="/home" element={<Home />} />
          <Route path="/logboek" element={<LogboekScreen />} />
        </Route>
        <Route path="/*" element={<Navigate to="/" />} />
      </Routes>
    </Router>
  );
}

export default App;
