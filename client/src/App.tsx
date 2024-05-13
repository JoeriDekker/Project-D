import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

import LoginScreen from './pages/loginscreen';
import AnyPage from './pages/anypage';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<LoginScreen />} />
        <Route path="/hoi" element={<AnyPage />} />
      </Routes>
    </Router>
  );
}

export default App;
