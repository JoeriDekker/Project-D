import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import logo from "./visuals/logo.svg";

import LoginScreen from "./pages/loginscreen";
import AuthOutlet from "@auth-kit/react-router/AuthOutlet";
import Dashboard from "./Components/Dashboard/Dashboard";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/login" Component={LoginScreen} />
        <Route element={<AuthOutlet fallbackPath="/login" />}>
          <Route path="/" element={<Dashboard />} />
        </Route>
      </Routes>
      {/* <Route path="/"  component={Index} /> */}
    </Router>
  );
}

export default App;
