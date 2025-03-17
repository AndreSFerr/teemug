import { useTranslation } from "react-i18next";
import { Link, useNavigate } from "react-router-dom";
import LoginGoogleButton from "./LoginGoogleButton";
import LoginFacebookButton from "./LoginFacebookButton";

export const SocialLogin = () => {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const handleBack = () => {
    navigate(-1); // Volta para a página anterior
  };

  return (
    <div className="d-flex flex-column gap-4">
      <div className="container mt-5">
        {/* Card Login Social */}
        <div className="card mb-4">
          <div className="card-body">
            <h5 className="card-title text-center mb-3">
              {t("loginWithSocial")}
            </h5>
            <div className="d-flex justify-content-center gap-3">
              <LoginGoogleButton />
              <LoginFacebookButton />
            </div>
          </div>
        </div>

        {/* Card Login Tradicional */}
        <div className="card mb-4">
          <div className="card-body text-center">
            <h5 className="card-title mb-3">{t("orLoginWithEmail")}</h5>
            <Link to="/login-traditional" className="btn btn-success me-2">
              {t("loginWithEmail")}
            </Link>
            <Link to="/register" className="btn btn-secondary">
              {t("register")}
            </Link>
          </div>
        </div>

        {/* Botão Voltar */}
        <div className="text-center">
          <button onClick={handleBack} className="btn btn-primary">
            {t("goBack") || "Voltar"}
          </button>
        </div>
      </div>
    </div>
  );
};
