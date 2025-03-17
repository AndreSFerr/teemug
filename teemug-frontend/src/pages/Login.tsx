import { useTranslation } from 'react-i18next';
import { SocialLogin } from '@/components/auth/SocialLogin';

const Login = () => {
  const { t } = useTranslation();

  return (
    <div className="container mt-5">
      <h2 className="text-center mb-4">{t('loginTitle')}</h2>
      <SocialLogin />
    </div>
  );
};

export default Login;
