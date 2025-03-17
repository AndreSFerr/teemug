import FacebookLogin from '@greatsumini/react-facebook-login';
import { useExternalLoginMutation } from '@/store/api/authApi';
import { useAppDispatch } from '@/hooks/useAppDispatch';
import { setAuthData } from '@/store/slice/authSlice';

const LoginFacebookButton = () => {
  const [externalLogin] = useExternalLoginMutation();
  const dispatch = useAppDispatch();

  const handleFacebookSuccess = async (response: any) => {
    try {
      const result = await externalLogin({
        provider: 'Facebook',
        token: response.accessToken,
      }).unwrap();

      dispatch(setAuthData({
        user: result.user,
        token: result.token,
      }));
    } catch (err) {
      console.error('Erro no login com Facebook:', err);
    }
  };

  return (
    <FacebookLogin className="btn btn-primary"
      appId={import.meta.env.VITE_FACEBOOK_APP_ID}
      onSuccess={handleFacebookSuccess}
      onFail={(err) => console.log('Erro Facebook:', err)}
    />
  );
};

export default LoginFacebookButton;
