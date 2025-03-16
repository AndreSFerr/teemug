import { useState } from "react";
import i18n from "i18next";

const Navbar = () => {
  const [isLoggedIn] = useState(false);

  const handleLanguageChange = (lang: string) => {
    i18n.changeLanguage(lang);
  };

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark border-bottom px-3">
      <div className="container-fluid justify-content-between">
        <a className="navbar-brand" href="/">TeeMug Shop</a>

        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarOptions"
          aria-controls="navbarOptions"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>

        <div className="collapse navbar-collapse" id="navbarOptions">
          <ul className="navbar-nav ms-auto mb-2 mb-lg-0">
         
            <li className="nav-item dropdown">
              <a
                className="nav-link dropdown-toggle"
                href="#"
                role="button"
                data-bs-toggle="dropdown"
                aria-expanded="false"
              >Login
                <i className="bi bi-person-circle"></i>
              </a>
              <ul className="dropdown-menu dropdown-menu-end">
                {!isLoggedIn ? (
                  <>
                    <li><a className="dropdown-item" href="/login">Login</a></li>
                    <li><a className="dropdown-item" href="/register">Register</a></li>
                    <li><hr className="dropdown-divider" /></li>
                    <li><a className="dropdown-item" href="/login-google">Login with Google</a></li>
                    <li><a className="dropdown-item" href="/login-facebook">Login with Facebook</a></li>
                  </>
                ) : (
                  <li><a className="dropdown-item" href="/logout">Logout</a></li>
                )}
              </ul>
            </li>

            {/* Idiomas */}
            <li className="nav-item dropdown ms-2">
              <a
                className="nav-link dropdown-toggle"
                href="#"
                role="button"
                data-bs-toggle="dropdown"
                aria-expanded="false"
              >
                🌐
              </a>
              <ul className="dropdown-menu dropdown-menu-end">
                <li><button className="dropdown-item" onClick={() => handleLanguageChange('pt')}>Português</button></li>
                <li><button className="dropdown-item" onClick={() => handleLanguageChange('en')}>English</button></li>
                <li><button className="dropdown-item" onClick={() => handleLanguageChange('es')}>Español</button></li>
                <li><button className="dropdown-item" onClick={() => handleLanguageChange('fr')}>Français</button></li>
              </ul>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
