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
  }),
});

export const { useExternalLoginMutation } = authApi;
