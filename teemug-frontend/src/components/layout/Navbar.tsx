import { useState } from 'react';

const Navbar = () => {
  const [isLoggedIn] = useState(false);

  return (
    <nav className="navbar navbar-expand-lg navbar-light bg-light px-3">
      <div className="container-fluid justify-content-between">
        <div>
          <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarOptions">
            <span className="navbar-toggler-icon"></span>
          </button>
        </div>
        <div className="collapse navbar-collapse" id="navbarOptions">
          <ul className="navbar-nav ms-auto mb-2 mb-lg-0">
            <li className="nav-item dropdown">
              <a className="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown">
                <i className="bi bi-person-circle"></i>
              </a>
              <ul className="dropdown-menu dropdown-menu-end">
                {!isLoggedIn ? (
                  <>
                    <li><a className="dropdown-item" href="/login">Login</a></li>
                    <li><a className="dropdown-item" href="/register">Register</a></li>
                  </>
                ) : (
                  <li><a className="dropdown-item" href="/logout">Logout</a></li>
                )}
              </ul>
            </li>
            <li className="nav-item dropdown ms-2">
              <a className="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown">
                🌐
              </a>
              <ul className="dropdown-menu dropdown-menu-end">
                <li><button className="dropdown-item">PT</button></li>
                <li><button className="dropdown-item">EN</button></li>
                <li><button className="dropdown-item">ES</button></li>
                <li><button className="dropdown-item">FR</button></li>
              </ul>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
