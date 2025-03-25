import { NavLink } from "react-router-dom";
import { useAppSelector } from "@/hooks/useAppSelector";

const Sidebar = () => {
  const { user, isAuthenticated } = useAppSelector((state) => state.auth);
  console.log("🔍 Role do usuário:", user?.role);
  
  const isAdmin = isAuthenticated && user?.role === "Admin";

  return (
    <div className="d-flex flex-column">
      <h5 className="mb-4">Categorias</h5>
      <ul className="nav flex-column">
        <li className="nav-item">
          <NavLink to="/tshirts" className="nav-link">👕 T-Shirts</NavLink>
        </li>
        <li className="nav-item">
          <NavLink to="/mugs" className="nav-link">☕ Mugs</NavLink>
        </li>

        {isAdmin && (
          <>
            <hr />
            <h5 className="mb-2">Administração</h5>
            <li className="nav-item">
              <NavLink to="/admin/dashboard" className="nav-link">📊 Dashboard</NavLink>
            </li>
            <li className="nav-item">
              <NavLink to="/admin/orders" className="nav-link">📦 Pedidos</NavLink>
            </li>
            <li className="nav-item">
              <NavLink to="/admin/users" className="nav-link">👥 Usuários</NavLink>
            </li>
          </>
        )}
      </ul>
    </div>
  );
};

export default Sidebar;
