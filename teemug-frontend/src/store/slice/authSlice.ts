import { createSlice } from '@reduxjs/toolkit';

interface AuthState {
  isAuthenticated: boolean;
  user: any;
  token: string | null;
}

const savedAuthData = localStorage.getItem('authData');
const parsedAuthData = savedAuthData ? JSON.parse(savedAuthData) : null;

const initialState: AuthState = {
  isAuthenticated: !! parsedAuthData,
  user: parsedAuthData?.user || null,
  token: parsedAuthData?.token || null,
};

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    setAuthData(state, action) {
      state.isAuthenticated = true;
      state.user = action.payload.user;
      state.token = action.payload.token;
          
      localStorage.setItem('authData', JSON.stringify(action.payload));
    },
    logout(state) {
      state.isAuthenticated = false;
      state.user = null;
      state.token = null;        
      localStorage.removeItem('authData');
    }
    
  },
});

export const { setAuthData, logout } = authSlice.actions;
export default authSlice.reducer;
