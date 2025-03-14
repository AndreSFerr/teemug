// src/components/auth/SocialLogin.tsx
import { GoogleLogin } from '@react-oauth/google';
import FacebookLogin from '@greatsumini/react-facebook-login';

export const SocialLogin = () => {
  const handleGoogleSuccess = (credentialResponse: any) => {
    console.log('Google token:', credentialResponse.credential);
    
  };

  const handleFacebookSuccess = (response: any) => {
    console.log('Facebook accessToken:', response.accessToken);
   
  };

  return (
    <div className="d-flex flex-column gap-3">
      <GoogleLogin
        onSuccess={handleGoogleSuccess}
        onError={() => console.log('Erro ao logar com Google')}
      />
      <FacebookLogin
        appId="SEU_FACEBOOK_APP_ID"
        onSuccess={handleFacebookSuccess}
        onFail={(err) => console.log('Erro Facebook:', err)}
      />
    </div>
  );
};
