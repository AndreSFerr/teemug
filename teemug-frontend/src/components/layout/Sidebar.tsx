import { NavLink } from 'react-router-dom';

const Sidebar = () => {
  return (
    <div className="d-flex flex-column p-3 bg-light min-vh-100">
      <h5 className="mb-3">TeeMug Shop</h5>
      <ul className="nav nav-pills flex-column">
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
