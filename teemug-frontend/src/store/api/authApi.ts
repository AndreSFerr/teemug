import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

export const authApi = createApi({
  reducerPath: 'authApi',
  baseQuery: fetchBaseQuery({
    baseUrl: '/api/',
  }),
  endpoints: (builder) => ({
    externalLogin: builder.mutation({
      query: ({ provider, token }) => ({
        url: 'account/external-login-token',
        method: 'POST',
        body: { provider, token },
      }),
    }),
    login: builder.mutation({
      query: (data) => ({
        url: 'account/login',
        method: 'POST',
        body: data,
      }),
    }),
    register: builder.mutation({
      query: (data) => ({
        url: 'account/register',
        method: 'POST',
        body: data,
      }),
    }),
    
  }),
});

export const { useExternalLoginMutation, useLoginMutation, useRegisterMutation } = authApi;
