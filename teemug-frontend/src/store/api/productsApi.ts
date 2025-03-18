
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';

export const productsApi = createApi({
  reducerPath: 'productsApi',
  baseQuery: fetchBaseQuery({ baseUrl: '/api/' }),
  endpoints: (builder) => ({
    getProducts: builder.query({
      query: (params) => {
        const queryParams = new URLSearchParams();
        if (params?.name) queryParams.append('name', params.name);
        if (params?.description) queryParams.append('description', params.description);
        if (params?.price) queryParams.append('price', params.price);
        if (params?.category) queryParams.append('category', params.category);

        return `products?${queryParams.toString()}`;
      },
    }),
  }),
});

export const { useGetProductsQuery } = productsApi;
