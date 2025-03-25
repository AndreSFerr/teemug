import { configureStore } from '@reduxjs/toolkit';
import { authApi } from '@/store/api/authApi';
import { productsApi } from '@/store/api/productsApi';
import { ordersApi } from "./api/ordersApi";
import authReducer from '@/store/slice/authSlice';
import cartReducer from './slice/cartSlice';

export const store = configureStore({
  reducer: {
    auth: authReducer,
    cart: cartReducer,
    [authApi.reducerPath]: authApi.reducer,
    [productsApi.reducerPath]: productsApi.reducer,
    [ordersApi.reducerPath]: ordersApi.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(authApi.middleware, productsApi.middleware), 
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;


