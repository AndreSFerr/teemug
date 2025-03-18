import { useExternalLoginMutation } from '@/store/api/authApi';
import { useAppDispatch } from '@/hooks/useAppDispatch';
import { setAuthData } from '@/store/slice/authSlice';
import { GoogleLogin } from '@react-oauth/google';
import { useNavigate } from 'react-router-dom'; // 👈 adicionar
import { useEffect } from 'react';

const LoginGoogleButton = () => {
  const [externalLogin] = useExternalLoginMutation();
  const dispatch = useAppDispatch();
  const navigate = useNavigate(); // 👈 hook para redirecionamento

  const handleGoogleSuccess = async (response: any) => {
    try {
      const result = await externalLogin({
        provider: 'Google',
        token: response.credential,
      }).unwrap();

      dispatch(setAuthData({
        user: result.user,
        token: result.token,
      }));

      // Redirecionar após login
      navigate('/'); // 👈 vai para Home
    } catch (err) {
      console.error('Erro no login Google:', err);
    }
  };

  return (
    <GoogleLogin
      onSuccess={handleGoogleSuccess}
      onError={() => console.log('Erro login Google')}
    />
  );
};

export default LoginGoogleButton;
