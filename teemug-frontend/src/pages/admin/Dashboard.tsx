import { useGetDashboardMetricsQuery } from "@/store/api/adminApi";
import { BarChart, Bar, XAxis, YAxis, Tooltip, CartesianGrid } from 'recharts';

const Dashboard = () => {
  const { data: metrics } = useGetDashboardMetricsQuery();

  return (
    <div className="container py-4">
      <h2>Dashboard de Vendas</h2>
      <div className="row mb-4">
        <div className="col">
          <div className="card text-center">
            <div className="card-body">
              <h5 className="card-title">Total de Vendas</h5>
              <p className="card-text fw-bold">€ {metrics?.totalSales}</p>
            </div>
          </div>
        </div>
        <div className="col">
          <div className="card text-center">
            <div className="card-body">
              <h5 className="card-title">Total de Pedidos</h5>
              <p className="card-text fw-bold">{metrics?.totalOrders}</p>
            </div>
          </div>
        </div>
      </div>

      <h4>Vendas por Categoria</h4>
      <BarChart width={600} height={300} data={metrics?.salesByCategory || []}>
        <CartesianGrid strokeDasharray="3 3" />
        <XAxis dataKey="category" />
        <YAxis />
        <Tooltip />
        <Bar dataKey="sales" fill="#007bff" />
      </BarChart>
    </div>
  );
};
export default Dashboard;