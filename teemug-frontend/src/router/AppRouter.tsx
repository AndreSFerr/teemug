import { BrowserRouter, Routes, Route } from "react-router-dom";
import MainLayout from "@/layouts/MainLayout";
import Home from "@/pages/Home";
import TShirts from "@/pages/TShirts";
import Mugs from "@/pages/Mugs";
import Login from "@/pages/Login";
import Register from "@/pages/Register";
import LoginTraditional from "@/pages/ManualLogin";
import PrivacyPolicy from "@/pages/PrivacyPolicy";
import DataDeletion from "@/pages/DataDeletion";
import Logout from '@/pages/Logout';
import OrderConfirmation from "@/pages/OrderConfirmation";

const AppRouter = () => {
  return (
    <BrowserRouter>
      <Routes>        
        <Route path="/" element={<MainLayout />}>
          <Route index element={<Home />} />
          <Route path="tshirts" element={<TShirts />} />
          <Route path="mugs" element={<Mugs />} />
        </Route>        
        <Route path="/login" element={<Login />} />     
        <Route path="/register" element={<Register />} />
        <Route path="/login-traditional" element={<LoginTraditional />} />
        <Route path="/privacy-policy" element={<PrivacyPolicy />} />
        <Route path="/data-deletion" element={<DataDeletion />} />
        <Route path="/logout" element={<Logout />} /> 
        <Route path="/order-confirmation" element={<OrderConfirmation />} />
      </Routes>
    </BrowserRouter>
  );
};

export default AppRouter;
