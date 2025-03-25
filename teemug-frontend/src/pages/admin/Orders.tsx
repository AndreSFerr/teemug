import { useGetOrdersQuery } from "@/store/api/adminApi";

const Orders = () => {
  const { data: orders } = useGetOrdersQuery();

  return (
    <div className="container py-4">
      <h2>Lista de Pedidos</h2>
      <table className="table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Cliente</th>
            <th>Produtos</th>
            <th>Status</th>
            <th>Total</th>
          </tr>
        </thead>
        <tbody>
          {orders?.map((order) => (
            <tr key={order.id}>
              <td>{order.id}</td>
              <td>{order.customerName}</td>
              <td>
                <ul>
                  {order.items.map((item) => (
                    <li key={item.id}>{item.productName} x {item.quantity}</li>
                  ))}
                </ul>
              </td>
              <td>{order.status}</td>
              <td>€ {order.total}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};
