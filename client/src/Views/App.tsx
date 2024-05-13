import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

import LoginScreen from './loginScreen';
import DashboardScreen from './DashboardScreen';
import LogboekScreen from './LogboekScreen';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<LoginScreen />} />
        <Route path="/dashboard" element={<DashboardScreen />} />
        <Route path="/logboek" element={<LogboekScreen />} />
      </Routes>
    </Router>
  );
}

export default App;
