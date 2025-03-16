import { NavLink } from "react-router-dom";

const Sidebar = () => {
  return (
    <div className="d-flex flex-column">
      <h5 className="mb-4">Categorias</h5>
      <ul className="nav flex-column">
        <li className="nav-item">
          <NavLink to="/tshirts" className="nav-link">T-Shirts</NavLink>
        </li>
        <li className="nav-item">
          <NavLink to="/mugs" className="nav-link">Mugs</NavLink>
        </li>
      </ul>
    </div>
  );
};

export default Sidebar;
