import { BrowserRouter, Routes, Route } from "react-router-dom";
import MainLayout from "@/layouts/MainLayout";
import Home from "@/pages/Home";
import TShirts from "@/pages/TShirts";
import Mugs from "@/pages/Mugs";
import Login from "@/pages/Login";
// import Register from "@/pages/Register";

const AppRouter = () => {
  return (
    <BrowserRouter>
      <Routes>
        {/* Layout principal com Navbar e Sidebar */}
        <Route path="/" element={<MainLayout />}>
          <Route index element={<Home />} />
          <Route path="tshirts" element={<TShirts />} />
          <Route path="mugs" element={<Mugs />} />
        </Route>

        {/* Rotas fora do layout (ex: login/register) */}
        <Route path="/login" element={<Login />} />
        {/* <Route path="/register" element={<Register />} /> */}
      </Routes>
    </BrowserRouter>
  );
};

export default AppRouter;
