import { Outlet } from "react-router-dom";
import Navbar from "@/components/layout/Navbar";
import Sidebar from "@/components/layout/Sidebar";

const MainLayout = () => {
  return (
    <div className="d-flex flex-column min-vh-100">     
      <Navbar />     
      <div className="d-flex flex-grow-1">      
        <div className="bg-light p-3 d-none d-md-block" style={{ width: "240px" }}>
          <Sidebar />
        </div>      
        <div className="flex-grow-1 p-3">
          <Outlet />
        </div>
      </div>
      <footer className="bg-dark text-light text-center py-3 mt-auto">
        &copy; {new Date().getFullYear()} TShirts Mug Shop Co - Todos os direitos reservados.
      </footer>
    </div>
  );
};

export default MainLayout;
