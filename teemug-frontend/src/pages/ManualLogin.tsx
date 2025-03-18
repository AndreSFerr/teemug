import { useState } from "react";
import { useTranslation } from "react-i18next";
import { useLoginMutation } from "@/store/api/authApi";
import { useAppDispatch } from "@/hooks/useAppDispatch";
import { setAuthData } from "@/store/slice/authSlice";
import { useNavigate } from "react-router-dom";

const ManualLogin = () => {
  const { t } = useTranslation();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [rememberMe, setRememberMe] = useState(false);
  const [login, { isLoading }] = useLoginMutation();
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const result = await login({ email, password }).unwrap();
      dispatch(setAuthData({ user: result.user, token: result.token }));

      if (rememberMe) {
        localStorage.setItem("authToken", result.token);
        localStorage.setItem("authUser", JSON.stringify(result.user));
      }

      navigate("/"); 
    } catch (err) {
      console.error("Erro login:", err);
    }
  };

  return (
    <div className="container mt-5" style={{ maxWidth: "400px" }}>
      <h2 className="text-center mb-4">{t("login") || "Login"}</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="email" className="form-label">{t("email")}</label>
          <input type="email" className="form-control" id="email" required value={email} onChange={(e) => setEmail(e.target.value)} />
        </div>
        <div className="mb-3">
          <label htmlFor="password" className="form-label">{t("password")}</label>
          <input type="password" className="form-control" id="password" required value={password} onChange={(e) => setPassword(e.target.value)} />
        </div>
        <div className="mb-3 form-check">
          <input type="checkbox" className="form-check-input" id="rememberMe" checked={rememberMe} onChange={(e) => setRememberMe(e.target.checked)} />
          <label className="form-check-label" htmlFor="rememberMe">{t("rememberMe")}</label>
        </div>
        <button type="submit" className="btn btn-primary w-100" disabled={isLoading}>
          {isLoading ? t("loading") : t("login")}
        </button>
      </form>
    </div>
  );
};

export default ManualLogin;
