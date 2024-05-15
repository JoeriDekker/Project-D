import React from "react";
import { BrowserRouter as Router, Route, Routes, Navigate } from "react-router-dom";

import AuthOutlet from "@auth-kit/react-router/AuthOutlet";
import useAuthHeader from 'react-auth-kit/hooks/useAuthHeader';

import Dashboard from "./pages/Dashboard/Dashboard";
import LoginScreen from "./pages/LoginScreen/LoginScreen";
import axios, { AxiosError } from "axios";
import useSignOut from "react-auth-kit/hooks/useSignOut";
import useIsAuthenticated from 'react-auth-kit/hooks/useIsAuthenticated'


function App() {
  const authHeader = useAuthHeader();
  const authState = useIsAuthenticated();
  const SignOut = useSignOut();
  async function isTokenValid() {
    if (!authState) return;
    try {
      const response = await axios.get(process.env.REACT_APP_API_URL + "/api/users", {
        headers: {
          Authorization: authHeader,
        }
      });
    } catch (error) {
      const axiosError = error as AxiosError;
      if (axiosError.response?.status === 401) {
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
          <Route path="/" element={<Dashboard />} />
        </Route>
        <Route path="/*" element={<Navigate to="/" />} />
      </Routes>
    </Router>
  );
}

export default App;
