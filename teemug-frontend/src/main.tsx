import React from 'react';
import ReactDOM from 'react-dom/client';
import AppRouter from './router/AppRouter';
import { Provider } from 'react-redux';
import { store } from './store';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle.min.js'; 
import './i18n'; 
import { GoogleOAuthProvider } from '@react-oauth/google';

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <Provider store={store}>
    <GoogleOAuthProvider clientId="SEU_CLIENT_ID_DO_GOOGLE">
        <AppRouter />
      </GoogleOAuthProvider>
    </Provider>
  </React.StrictMode>
);
