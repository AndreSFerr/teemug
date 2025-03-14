// src/pages/Login.tsx
import { SocialLogin } from '../components/auth/SocialLogin';

const Login = () => {
  return (
    <div className="container mt-5">
      <h2 className="text-center mb-4">Entrar na TeeMug Shop</h2>
      <SocialLogin />
    </div>
  );
};

export default Login;
