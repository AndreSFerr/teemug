// src/pages/Register.tsx
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { useRegisterMutation } from "@/store/api/authApi";
import { useNavigate } from "react-router-dom";

const Register = () => {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const [fullName, setFullName] = useState("");
  const [address, setAddress] = useState("");
  const [email, setEmail] = useState("");
  const [nif, setNif] = useState("");
  const [phone, setPhone] = useState("");
  const [password, setPassword] = useState("");

  const [registerUser, { isLoading }] = useRegisterMutation();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    const formData = {
      fullName,
      address,
      email,
      nif,
      phone,
      password,
    };

    try {
      const result = await registerUser(formData).unwrap();
      console.log("✅ Registro bem-sucedido:", result);

      alert(t("registerSuccess") || "Cadastro realizado com sucesso!");
      navigate("/login"); // redireciona para login após registro

    } catch (err: any) {
      console.error("❌ Erro no registro:", err);
      alert(t("registerError") || "Erro ao registrar. Verifique os dados e tente novamente.");
    }
  };

  return (
    <div className="container mt-5" style={{ maxWidth: "600px" }}>
      <h2 className="text-center mb-4">{t("registerTitle") || "Cadastrar-se"}</h2>

      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label className="form-label">{t("fullName") || "Nome completo"}</label>
          <input
            type="text"
            className="form-control"
            value={fullName}
            onChange={(e) => setFullName(e.target.value)}
            required
          />
        </div>

        <div className="mb-3">
          <label className="form-label">{t("address") || "Endereço (Morada)"}</label>
          <input
            type="text"
            className="form-control"
            value={address}
            onChange={(e) => setAddress(e.target.value)}
            required
          />
        </div>

        <div className="mb-3">
          <label className="form-label">{t("email") || "Email"}</label>
          <input
            type="email"
            className="form-control"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>

        <div className="mb-3">
          <label className="form-label">{t("nif") || "NIF"}</label>
          <input
            type="text"
            className="form-control"
            value={nif}
            onChange={(e) => setNif(e.target.value)}
            required
          />
        </div>

        <div className="mb-3">
          <label className="form-label">{t("phone") || "Telefone"}</label>
          <input
            type="tel"
            className="form-control"
            value={phone}
            onChange={(e) => setPhone(e.target.value)}
            required
          />
        </div>

        <div className="mb-3">
          <label className="form-label">{t("password") || "Senha"}</label>
          <input
            type="password"
            className="form-control"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>

        <div className="text-center">
          <button type="submit" className="btn btn-primary w-100" disabled={isLoading}>
            {isLoading ? t("loading") || "Aguarde..." : t("register") || "Cadastrar"}
          </button>
        </div>
      </form>
    </div>
  );
};

export default Register;
