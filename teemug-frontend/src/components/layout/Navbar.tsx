// src/components/layout/Navbar.tsx
import { useAppSelector } from '@/hooks/useAppSelector';
import { useTranslation } from 'react-i18next';
import i18n from 'i18next';

const Navbar = () => {
  const { isAuthenticated, user } = useAppSelector((state) => state.auth);
  const { t } = useTranslation();

  const handleLanguageChange = (lang: string) => {
    i18n.changeLanguage(lang);
  };

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark border-bottom px-3">
      <div className="container-fluid justify-content-between">
        <a className="navbar-brand" href="/">TeeMug Shop Co.</a>

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
                className="nav-link dropdown-toggle d-flex align-items-center"
                href="#"
                role="button"
                data-bs-toggle="dropdown"
                aria-expanded="false"
              >
                {isAuthenticated && user?.picture && (                 
                  <img
                    src={user.picture}
                    alt="User"
                    width="32"
                    height="32"
                    className="rounded-circle me-2"
                  />
                )}
                {isAuthenticated ? user?.name : t('login')}
              </a>
              
              <ul className="dropdown-menu dropdown-menu-end">
                {!isAuthenticated ? (
                  <>
                    <li><a className="dropdown-item" href="/login">{t('login')}</a></li>
                    <li><a className="dropdown-item" href="/register">{t('register')}</a></li>
                  </>
                ) : (
                  <li><a className="dropdown-item" href="/logout">{t('logout')}</a></li>
                )}
              </ul>
            </li>

           
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
