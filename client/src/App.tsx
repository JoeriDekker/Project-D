import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import logo from './visuals/logo.svg';

import LoginScreen from './pages/loginscreen';
import AnyPage from './pages/anypage';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" Component={LoginScreen} />
        <Route path="/hoi" Component={AnyPage} />
      </Routes>
      {/* <Route path="/"  component={Index} /> */}
    </Router>
  );
}

export default App;
