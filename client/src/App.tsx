import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import LanguageSwitcher from './components/languageswitch/langswitch';

import LoginScreen from './pages/loginscreen';
import AnyPage from './pages/anypage';

function App() {

  return (
    <Router>
      <Routes>
        <LanguageSwitcher />
        <Route path="/" element={<LoginScreen />} />
        <Route path="/home" element={<AnyPage />} />
      </Routes>
    </Router>
  );
}

export default App;
