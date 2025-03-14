// src/router/AppRouter.tsx
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Home from '@/pages/Home';
import Login from '@/pages/Login';

const AppRouter = () => (
  <BrowserRouter>
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/login" element={<Login />} />
      {/* Outros: T-Shirts, Mugs, Register, Cart... */}
    </Routes>
  </BrowserRouter>
);

export default AppRouter;
